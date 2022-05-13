using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.ViewManagement;
using System.Globalization;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace WeatherAppUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string ApiKey = "fae6086566bfc2ee3f3361ed5f17fe7c";
        public double latitude, longitude;
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;


        }
        private async void PageLoadedMethod(double latitude, double longitude)
        {
            try
            {
                RootobjectOneCall Weather = await OnecallApi.GetCurrentWeather1C(latitude, longitude);
                //DebugLat.Text = Loc.Key.ToString();
                //DebugLon.Text = Convert.ToString(Weather.wind.deg);
                LocationTextBlock.Text = await ReverseGeocodingApi.GetLocationName(latitude, longitude);
                TemperatureTextBlock.Text = ((int)Weather.current.temp).ToString() + "°C";
                //ConditionsTextBlock.Text = Weather.weather[0].description;
                ConditionsTextBlock.Text = Weather.current.weather[0].description;
                MinTempTextBlock.Text = "мин.: " + Convert.ToString(Math.Round(Weather.daily[0].temp.min)) + "°C";
                MaxTempTextBlock.Text = "макс.: " + Convert.ToString(Math.Round(Weather.daily[0].temp.max)) + "°C";
                //BitmapImage Img0 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[0]}.png"));
                BitmapImage Img0 = new BitmapImage(new Uri($"http://openweathermap.org/img/wn/{Weather.current.weather[0].icon}@2x.png"));
                WeatherIcon.Source = Img0;

                //Blocks
                FeelsLikeTextBlock.Text = ((int)Weather.current.feels_like).ToString() + "°C";
                HumidityTextBlock.Text = Weather.current.humidity.ToString() + "%";
                DewPointTextBlock.Text = "Точка росы сейчас: " + Math.Round(Weather.current.dew_point).ToString() + "°C";
                PressureTextBlock.Text = Convert.ToString(Math.Round(Weather.current.pressure / 1.333));
                RotateTransform rotateTransform2 =
                    new RotateTransform()
                    {
                        Angle = Weather.current.wind_deg + 180,
                        CenterX = 0.5,
                        CenterY = 0.5
                    };
                WindBlockArrow.RenderTransform = rotateTransform2;
                WindspeedTextBlock.Text = Convert.ToString(Math.Round(Weather.current.wind_speed)) + " м/c";
                VisibityTextBlock.Text = Convert.ToString(Convert.ToDouble(Weather.current.visibility) / 1000) + " км.";
                UVTextBlock.Text = Math.Round(Weather.current.uvi).ToString();
                if (Math.Round(Weather.current.uvi) < 3)
                {
                    UVCommentTextBlock.Text = "Низкий";
                }
                else if (Math.Round(Weather.current.uvi) >= 3)
                {
                    UVCommentTextBlock.Text = "Средний";
                }
                else if (Math.Round(Weather.current.uvi) > 5)
                {
                    UVCommentTextBlock.Text = "Высокий";
                }
                else if (Math.Round(Weather.current.uvi) > 7)
                {
                    UVCommentTextBlock.Text = "Опасный!";
                }

                //Forecast
                ForecastMinTemp_0.Text = Convert.ToString(Math.Round(Weather.daily[0].temp.min)) + "°C";
                ForecastMaxTemp_0.Text = Convert.ToString(Math.Round(Weather.daily[0].temp.max)) + "°C";
                ForecastIco_0.Source = Img0;

                ForecastDow_1.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(UnixtoDate(Weather.daily[1].dt).DayOfWeek);
                ForecastMinTemp_1.Text = Convert.ToString(Math.Round(Weather.daily[1].temp.min)) + "°C";
                ForecastMaxTemp_1.Text = Convert.ToString(Math.Round(Weather.daily[1].temp.max)) + "°C";
                BitmapImage Img1 = new BitmapImage(new Uri($"http://openweathermap.org/img/wn/{Weather.daily[1].weather[0].icon}@2x.png"));
                ForecastIco_1.Source = Img1;

                ForecastDow_2.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(UnixtoDate(Weather.daily[2].dt).DayOfWeek);
                ForecastMinTemp_2.Text = Convert.ToString(Math.Round(Weather.daily[2].temp.min)) + "°C";
                ForecastMaxTemp_2.Text = Convert.ToString(Math.Round(Weather.daily[2].temp.max)) + "°C";
                BitmapImage Img2 = new BitmapImage(new Uri($"http://openweathermap.org/img/wn/{Weather.daily[2].weather[0].icon}@2x.png"));
                ForecastIco_2.Source = Img2;

                ForecastDow_3.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(UnixtoDate(Weather.daily[3].dt).DayOfWeek);
                ForecastMinTemp_3.Text = Convert.ToString(Math.Round(Weather.daily[3].temp.min)) + "°C";
                ForecastMaxTemp_3.Text = Convert.ToString(Math.Round(Weather.daily[3].temp.max)) + "°C";
                BitmapImage Img3 = new BitmapImage(new Uri($"http://openweathermap.org/img/wn/{Weather.daily[3].weather[0].icon}@2x.png"));
                ForecastIco_3.Source = Img3;

                ForecastDow_4.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(UnixtoDate(Weather.daily[4].dt).DayOfWeek);
                ForecastMinTemp_4.Text = Convert.ToString(Math.Round(Weather.daily[4].temp.min)) + "°C";
                ForecastMaxTemp_4.Text = Convert.ToString(Math.Round(Weather.daily[4].temp.max)) + "°C";
                BitmapImage Img4 = new BitmapImage(new Uri($"http://openweathermap.org/img/wn/{Weather.daily[4].weather[0].icon}@2x.png"));
                ForecastIco_4.Source = Img4;
            }
            catch (NotSupportedException)
            {
                ConditionsTextBlock.Text = "Включите геолокацию или введите город в строке поиска";
            }
            catch
            {
                ConditionsTextBlock.Text = "Проверьте интернет подключение!";
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await UserLocation.GetPosition();
                latitude = position.Coordinate.Point.Position.Latitude;
                longitude = position.Coordinate.Point.Position.Longitude;
            }
            catch (NotSupportedException)
            {
                ConditionsTextBlock.Text = "Включите геолокацию или введите город в строке поиска";
            }
            PageLoadedMethod(latitude, longitude);
        }

        private static DateTime UnixtoDate(int unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (latitude, longitude) = await DirectGeoApi.SearchLocation(SearchField.Text);
            }
            catch
            {
                ConditionsTextBlock.Text = "Проверьте интернет подключение!";
            }
            PageLoadedMethod(latitude, longitude);
        }

        //private void MainScrollView_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        //{
        //    if (!LocationTextBlock.IsHitTestVisible)
        //    {
        //        ConditionsTextBlock.Text = "bebra";
        //    }
        //    else
        //    {
        //        ConditionsTextBlock.Text = "huebra";
        //    }
        //}
    }
}

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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace WeatherAppUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;


        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string ApiKey = "853JUGFNxQ2YKTUN0f39EKGiBe8ZdAT9";
            string[] forecast = new string[5];
            float[] temps_forecast_min = new float[5];
            float[] temps_forecast_max = new float[5];
            int[] icons = new int[5];
            string[] dates = new string[5];
            string city = string.Empty;
            try
            {
                var position = await UserLocation.GetPosition();
                var latitude = position.Coordinate.Point.Position.Latitude;
                var longitude = position.Coordinate.Point.Position.Longitude;
                CurrentRoot Weather = await CurrentApi.GetCurrentWeather(latitude, longitude);
                Rootobject WeatherMisc = await OnecallApi.GetCurrentWeather1C(latitude, longitude);
                LocRoot Loc = await LocationApi.ReverseGeocoding(latitude, longitude, ApiKey);
                (forecast, temps_forecast_min, temps_forecast_max, icons, dates) = await WeekApi.GetWeekForecast(Loc.Key, ApiKey);
                //DebugLon.Text = forecast[0];
                //DebugLat.Text = Loc.Key.ToString();
                DebugLon.Text = Convert.ToString(Weather.wind.deg);
                LocationTextBlock.Text = Loc.LocalizedName;
                TemperatureTextBlock.Text = ((int)Weather.main.temp).ToString() + "°C";
                //ConditionsTextBlock.Text = Weather.weather[0].description;
                ConditionsTextBlock.Text = forecast[0];
                MinTempTextBlock.Text = "Мин.: " + Convert.ToString(Math.Round(temps_forecast_min[0])) + "°C";
                MaxTempTextBlock.Text = "Макс.: " + Convert.ToString(Math.Round(temps_forecast_max[0])) + "°C";
                BitmapImage Img0 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[0]}.png"));
                WeatherIcon.Source = Img0;

                //Blocks
                FeelsLikeTextBlock.Text = ((int)Weather.main.feels_like).ToString() + "°C";
                HumidityTextBlock.Text = Weather.main.humidity.ToString() + "%";
                DewPointTextBlock.Text = "Точка росы сейчас: " + Math.Round(WeatherMisc.current.dew_point).ToString() + "°C";
                PressureTextBlock.Text = Convert.ToString(Math.Round(Weather.main.pressure / 1.333));
                RotateTransform rotateTransform2 = 
                    new RotateTransform()
                {
                        Angle = Weather.wind.deg + 180,
                    CenterX = 0.5,
                    CenterY = 0.5
                };
                WindBlockArrow.RenderTransform = rotateTransform2;
                WindspeedTextBlock.Text = Convert.ToString(Math.Round(Weather.wind.speed)) + " м/c";
                VisibityTextBlock.Text = Convert.ToString(Convert.ToDouble(WeatherMisc.current.visibility) / 1000);
                UVTextBlock.Text = WeatherMisc.current.uvi.ToString();
                if (WeatherMisc.current.uvi <= 3)
                {
                    UVCommentTextBlock.Text = "Низкий";
                } else if (WeatherMisc.current.uvi >= 3)
                {
                    UVCommentTextBlock.Text = "Средний";
                } else { UVCommentTextBlock.Text = "Опасный!"; }

                //Forecast
                ForecastMinTemp_0.Text = Convert.ToString(Math.Round(temps_forecast_min[0])) + "°C";
                ForecastMaxTemp_0.Text = Convert.ToString(Math.Round(temps_forecast_max[0])) + "°C";
                ForecastIco_0.Source = Img0;

                ForecastDow_1.Text = dates[1];
                ForecastMinTemp_1.Text = Convert.ToString(Math.Round(temps_forecast_min[1])) + "°C";
                ForecastMaxTemp_1.Text = Convert.ToString(Math.Round(temps_forecast_max[1])) + "°C";
                BitmapImage Img1 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[1]}.png"));
                ForecastIco_1.Source = Img1;

                ForecastDow_2.Text = dates[2];
                ForecastMinTemp_2.Text = Convert.ToString(Math.Round(temps_forecast_min[2])) + "°C";
                ForecastMaxTemp_2.Text = Convert.ToString(Math.Round(temps_forecast_max[2])) + "°C";
                BitmapImage Img2 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[2]}.png"));
                ForecastIco_2.Source = Img2;

                ForecastDow_3.Text = dates[3];
                ForecastMinTemp_3.Text = Convert.ToString(Math.Round(temps_forecast_min[3])) + "°C";
                ForecastMaxTemp_3.Text = Convert.ToString(Math.Round(temps_forecast_max[3])) + "°C";
                BitmapImage Img3 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[3]}.png"));
                ForecastIco_3.Source = Img3;

                ForecastDow_4.Text = dates[4];
                ForecastMinTemp_4.Text = Convert.ToString(Math.Round(temps_forecast_min[4])) + "°C";
                ForecastMaxTemp_4.Text = Convert.ToString(Math.Round(temps_forecast_max[4])) + "°C";
                BitmapImage Img4 = new BitmapImage(new Uri($"ms-appx:///Assets//{icons[4]}.png"));
                ForecastIco_4.Source = Img4;
            }

            catch
            {
                ConditionsTextBlock.Text = "Произошла ошибка :(";
            }
        }
    }
}

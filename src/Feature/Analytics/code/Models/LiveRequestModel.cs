using Sitecore.Analytics.Tracking;
using System;
using System.Data.SqlClient;
using System.Web;

namespace TeamNoesis.Feature.Analytics.Models
{
    public class LiveRequestModel
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Device DeviceType { get; set; }

        public string DeviceDescription
        {
            get
            {
                switch (DeviceType)
                {
                    case Device.Mobile: return "Mobile";
                    case Device.Tablet: return "Tablet";
                    case Device.Desktop: return "Desktop";
                    default: return "Unkwon";
                }
            }
        }

        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string RawUrl { get; set; }
        public string ReferrerUrl { get; set; }

        public LiveRequestModel(HttpRequestBase request, IInteractionData interaction)
        {
            IpAddress = string.Join(".", interaction.Ip);
            Country = interaction.GeoData.Country;
            City = interaction.GeoData.City;
            Latitude = interaction.GeoData.Latitude;
            Longitude = interaction.GeoData.Longitude;

            var isMobile = request.Browser.IsMobileDevice;
            var isTablet = isMobile && interaction.ScreenInfo.ScreenWidth > 767;
            DeviceType = isMobile ? isTablet ? Device.Tablet : Device.Mobile : Device.Desktop;

            BrowserName = interaction.BrowserInfo.BrowserMajorName;
            BrowserVersion = interaction.BrowserInfo.BrowserVersion;

            RawUrl = request.RawUrl;
            ReferrerUrl = request.UrlReferrer?.AbsoluteUri ?? string.Empty;
        }

        public LiveRequestModel(SqlDataReader reader)
        {
            IpAddress = (string)reader.GetValue(1);
            Country = (string)reader.GetValue(2);
            City = (string)reader.GetValue(3);
            Latitude = (double?)reader.GetValue(4);
            Longitude = (double?)reader.GetValue(5);
            DeviceType = (Device)Enum.ToObject(typeof(Device), (int)reader.GetValue(6));
            BrowserName = (string)reader.GetValue(7);
            BrowserVersion = (string)reader.GetValue(8);
            RawUrl = (string)reader.GetValue(9);
            ReferrerUrl = (string)reader.GetValue(10);
        }
    }

    public enum Device
    {
        Mobile, Tablet, Desktop
    }
}
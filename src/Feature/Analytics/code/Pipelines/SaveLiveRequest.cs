using Sitecore.CES.GeoIp.Core.Lookups;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TeamNoesis.Feature.Analytics.Models;

namespace TeamNoesis.Feature.Analytics.Pipelines
{
    public class SaveLiveRequest : RequestBeginProcessor
    {
        public override void Process(RequestBeginArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var contextItem = Sitecore.Context.Item;

            if (!Sitecore.Analytics.Tracker.IsActive || contextItem == null || !contextItem.Paths.IsContentItem || contextItem.Database.Name != "web")
            {
                return;
            }

            var interaction = Sitecore.Analytics.Tracker.Current.Interaction;
            var request = args.RequestContext.HttpContext.Request;

            //For demo purposes only, we need to hardcode IP Address
            //Because in local instances IP Address is 127.0.0.1
            if (!interaction.HasGeoIpData)
            {
                interaction.SetWhoIsInformation(LookupManager.GetWhoIsInformationByIp("148.63.65.140"));
            }

            var liveRequest = new LiveRequestModel(request, interaction);

            Task.Run(() => SaveInteraction(liveRequest));
        }

        public void SaveInteraction(LiveRequestModel liveRequest)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sqlUpdate = "UPDATE LiveRequests " +
                                         "SET Country = @Country, City = @City, Latitude = @Latitude, Longitude = @Longitude, DeviceType = @DeviceType, " +
                                         "BrowserName = @BrowserName, BrowserVersion = @BrowserVersion, RawUrl = @RawUrl, ReferrerUrl = @ReferrerUrl, LastInteractionOn = @LastInteractionOn " +
                                         "WHERE IpAddress = @IpAddress";

                using (var cmd = new SqlCommand(sqlUpdate, connection))
                {
                    cmd.Parameters.AddWithValue("@IpAddress", liveRequest.IpAddress);
                    cmd.Parameters.AddWithValue("@Country", liveRequest.Country);
                    cmd.Parameters.AddWithValue("@City", liveRequest.City);
                    cmd.Parameters.AddWithValue("@Latitude", liveRequest.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", liveRequest.Longitude);
                    cmd.Parameters.AddWithValue("@DeviceType", (int)liveRequest.DeviceType);
                    cmd.Parameters.AddWithValue("@BrowserName", liveRequest.BrowserName);
                    cmd.Parameters.AddWithValue("@BrowserVersion", liveRequest.BrowserVersion);
                    cmd.Parameters.AddWithValue("@RawUrl", liveRequest.RawUrl);
                    cmd.Parameters.AddWithValue("@ReferrerUrl", liveRequest.ReferrerUrl);
                    cmd.Parameters.AddWithValue("@LastInteractionOn", DateTime.UtcNow);

                    var updated = cmd.ExecuteNonQuery() > 0;

                    if (!updated)
                    {
                        const string sqlInsert = "INSERT INTO " +
                                                 "LiveRequests(IpAddress, Country, City, Latitude, Longitude, DeviceType, BrowserName, BrowserVersion, RawUrl, ReferrerUrl, LastInteractionOn) " +
                                                 "VALUES(@IpAddress, @Country, @City, @Latitude, @Longitude, @DeviceType, @BrowserName, @BrowserVersion, @RawUrl, @ReferrerUrl, @LastInteractionOn)";

                        cmd.CommandText = sqlInsert;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
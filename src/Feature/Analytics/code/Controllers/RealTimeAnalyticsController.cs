using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using TeamNoesis.Feature.Analytics.Models;

namespace TeamNoesis.Feature.Analytics.Controllers
{
    public class RealTimeAnalyticsController : Controller
    {
        public ActionResult Index()
        {
            var model = new RealTimeAnalyticsViewModel
            {
                OnlineUsers = GetActiveUsers()
            };

            return View(model);
        }

        public ActionResult Widget()
        {
            var model = new RealTimeAnalyticsViewModel
            {
                OnlineUsers = GetActiveUsers()
            };

            return View("LaunchPadRealTime", model);
        }

        private List<LiveRequestModel> GetActiveUsers()
        {
            var results = new List<LiveRequestModel>();

            var connectionString = ConfigurationManager.ConnectionStrings["analytics"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = @"SELECT * FROM LiveRequests WHERE LastInteractionOn > @Last3Minutes";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Last3Minutes", DateTime.UtcNow.AddMinutes(-3));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new LiveRequestModel(reader));
                        }
                    }
                }
            }

            return results;
        }
    }
}
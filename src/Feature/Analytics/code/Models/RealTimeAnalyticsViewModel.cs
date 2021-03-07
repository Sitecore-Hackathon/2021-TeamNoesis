using System.Collections.Generic;
using System.Linq;

namespace TeamNoesis.Feature.Analytics.Models
{
    public class RealTimeAnalyticsViewModel
    {
        public List<LiveRequestModel> OnlineUsers { get; set; }

        public List<Counters> OnlineUsersByCountry
        {
            get
            {
            return OnlineUsers.GroupBy(x => x.Country)
                .Select(group => new Counters
                {
                    Metric = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.Metric)
                .ToList();
            }
        }

        public List<Counters> OnlineUsersByBrowser
        {
            get
            {
                return OnlineUsers.GroupBy(x => x.BrowserName)
                    .Select(group => new Counters
                    {
                        Metric = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Metric)
                    .ToList();
            }
        }

        public List<Counters> OnlineUsersByPage
        {
            get
            {
                return OnlineUsers.GroupBy(x => x.RawUrl)
                    .Select(group => new Counters
                    {
                        Metric = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Metric)
                    .ToList();
            }
        }

        public List<Counters> OnlineUsersByReferrer
        {
            get
            {
                return OnlineUsers.GroupBy(x => x.ReferrerUrl)
                    .Select(group => new Counters
                    {
                        Metric = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Metric)
                    .ToList();
            }
        }

        public List<Counters> OnlineUsersByDevice
        {
            get
            {
                return OnlineUsers.GroupBy(x => x.DeviceDescription)
                    .Select(group => new Counters
                    {
                        Metric = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Metric)
                    .ToList();
            }
        }
    }

    public class Counters
    {
        public string Metric { get; set; }
        public int Count { get; set; }
    }
}
using System;

namespace BmaRailwayStatistics
{
    class Train
    {
        public string Id { get; set; }

        public string From { get; set; }

        public string Destination { get; set; }

        public string TimetableDeparture { get; set; }

        public string TimetableArrival { get; set; }

        public string FactualDeparture { get; set; }

        public string FactualArrival { get; set; }

        public int TimetableDuration { get; set; }

        public int TotalDuration { get; set; }

        public int FactualDuration { get; set; }

        public int TotalDelay { get; set; }

        public int FactualDelay { get; set; }
    }
}

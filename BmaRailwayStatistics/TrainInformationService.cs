using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmaRailwayStatistics
{
    class TrainInformationService
    {
        private readonly TimetableRestClient client;

        private readonly TrainLineParser parser;

        public TrainInformationService()
        {
            client = new TimetableRestClient();
            parser = new TrainLineParser();
        }

        public async Task GetTrains(List<Tuple<string, string>> routes)
        {
            //var trains = new List<Train>();
            //routes.AddRange(routes.Select(r => new Tuple<string, string>(r.Item2, r.Item1)));
            //var tasks = from route in routes select client.GetTimetableBetweenEndpoints(route.Item1, route.Item2);
            //var timetables = await Task.WhenAll(tasks);

            //foreach (var timetable in timetables)
            //{
            //    trains.AddRange(parser.ParseTrainsFromHtml(timetable));
            //}

            //return trains;
            var allRoutes = routes.Select(r => new Tuple<string, string>(r.Item2, r.Item1)).ToList();
            allRoutes.AddRange(routes);
            foreach (var route in allRoutes)
            {
                var trains = parser.ParseTrainsFromHtml(await client.GetTimetableBetweenEndpoints(route.Item1, route.Item2));
                trains.ForEach(t => Console.WriteLine($"{t.Id};{t.From};{t.Destination};{t.TimetableDeparture};{t.TimetableArrival};{t.TimetableDuration};{t.FactualDeparture};{t.FactualArrival};{t.FactualDuration};{t.FactualDelay};{t.TotalDelay}"));
            }
        }
    }
}

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BmaRailwayStatistics
{
    class TrainLineParser
    {
        public List<Train> ParseTrainsFromHtml(string content)
        {
            var trains = new List<Train>();
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var nodes = document.DocumentNode.SelectNodes("//div[contains(@class,'more')]//table//tbody");
            foreach (var node in nodes)
            {
                trains.Add(GetTrain(node));
            }

            return trains;
        }

        private Train GetTrain(HtmlNode node)
        {
            var idIndex = 5;
            var nodes = node.ChildNodes.Where(n => n.Name.Equals("tr")).ToList();
            var from = GetTrainEndpoint(nodes.First());
            var destination = GetTrainEndpoint(nodes.Last());
            if (from.Name.Equals("Ferencváros") || destination.Name.Equals("Ferencváros"))
            {
                idIndex = 4;
            }
            var id = nodes.First().ChildNodes.Where(n => n.Name.Equals("td")).Count() > idIndex ? nodes.First().ChildNodes.Where(n => n.Name.Equals("td")).ToList()[idIndex].ChildNodes.Where(n => n.Name.Equals("a")).First().InnerText : string.Empty;
            var factualDeparture = string.IsNullOrEmpty(from.FactualTime) ? from.TimetableTime : from.FactualTime;
            var factualArrival = string.IsNullOrEmpty(destination.FactualTime) ? destination.TimetableTime : destination.FactualTime;
            var factualDepartureParsed = ParseTime(factualDeparture);
            var factualArrivalParsed = ParseTime(factualArrival);
            var timetableDepartureParsed = ParseTime(from.TimetableTime);
            var timetableArrivalParsed = ParseTime(destination.TimetableTime);
            if (timetableArrivalParsed < timetableDepartureParsed)
            {
                timetableArrivalParsed = timetableArrivalParsed.AddDays(1);
            }
            if (factualArrivalParsed < factualDepartureParsed || factualArrivalParsed < timetableDepartureParsed)
            {
                factualArrivalParsed = factualArrivalParsed.AddDays(1);
            }
            var timetableDuration = (timetableArrivalParsed - timetableDepartureParsed).TotalMinutes;
            var totalDuration = (factualArrivalParsed - timetableDepartureParsed).TotalMinutes;
            var factualDuration = (factualArrivalParsed - factualDepartureParsed).TotalMinutes;

            var train = new Train()
            {
                Id = id,
                From = LatinToAscii(from.Name),
                Destination = LatinToAscii(destination.Name),
                TimetableDeparture = from.TimetableTime,
                TimetableArrival = destination.TimetableTime,
                FactualDeparture = factualDeparture,
                FactualArrival = factualArrival,
                TimetableDuration = (int)timetableDuration,
                FactualDuration = (int)factualDuration,
                TotalDuration = (int)totalDuration,
                TotalDelay = (int)(totalDuration - timetableDuration),
                FactualDelay = (int)(factualDuration - timetableDuration)
            };

            return train;
        }

        private TrainEndpoint GetTrainEndpoint(HtmlNode node)
        {
            var nodes = node.ChildNodes.Where(n => n.Name.Equals("td")).ToList();
            if (nodes.Count < 3)
            {
                return null;
            }

            var stationNode = nodes[0].FirstChild;
            var timetableTime = nodes[1];
            var factualTime = nodes[2];

            return new TrainEndpoint()
            {
                Name = stationNode.InnerText.Trim(),
                TimetableTime = timetableTime.InnerText.Trim(),
                FactualTime = factualTime.HasChildNodes ? factualTime.FirstChild.InnerText.Trim() : factualTime.InnerText.Trim()
            };
        }

        private DateTime ParseTime(string time)
        {
            if (time.StartsWith("24:"))
            {
                var parsed = DateTime.ParseExact(time.Replace("24:", "00:"), "HH:mm", CultureInfo.InvariantCulture);
                return parsed.AddDays(1);
            }
            else
            {
                return DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
            }
        }

        private string LatinToAscii(string input)
        {
            var builder = new StringBuilder();
            builder.Append(input.Normalize(NormalizationForm.FormKD).Where(x => x < 128).ToArray());
            return builder.ToString();
        }

        private class TrainEndpoint
        {
            public string Name { get; set; }

            public string TimetableTime { get; set; }

            public string FactualTime { get; set; }
        }
    }
}

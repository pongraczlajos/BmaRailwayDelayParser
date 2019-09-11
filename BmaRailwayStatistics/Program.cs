using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BmaRailwayStatistics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new TrainInformationService();
            Console.WriteLine("Id;From;Destination;Departure;Arrival;Duration;Factual Departure;Factual Arrival;Factual Duration;Factual Delay;Total Delay");
            await service.GetTrains(new List<Tuple<string, string>>()
            {
            });

            //Console.ReadLine();
        }
    }
}

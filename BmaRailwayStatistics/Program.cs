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
                new Tuple<string, string>("Nyugati", "Gyal"),
                new Tuple<string, string>("Nyugati", "Felsopakony"),
                new Tuple<string, string>("Nyugati", "Ocsa"),
                new Tuple<string, string>("Nyugati", "Ullo"),
                new Tuple<string, string>("Nyugati", "Monor"),
                new Tuple<string, string>("Nyugati", "Vac"),
                new Tuple<string, string>("Nyugati", "Szob"),
                new Tuple<string, string>("Nyugati", "God"),
                new Tuple<string, string>("Nyugati", "Erdokertes"),
                new Tuple<string, string>("Nyugati", "Pilisvorosvar"),
                new Tuple<string, string>("Nyugati", "Piliscsaba"),
                new Tuple<string, string>("Nyugati", "Solymar"),
                new Tuple<string, string>("Keleti", "Isaszeg"),
                new Tuple<string, string>("Keleti", "Pecel"),
                new Tuple<string, string>("Keleti", "Godollo"),
                new Tuple<string, string>("Keleti", "Ecser"),
                new Tuple<string, string>("Keleti", "Maglod"),
                new Tuple<string, string>("Keleti", "Gyomro"),
                new Tuple<string, string>("Keleti", "Mende"),
                new Tuple<string, string>("Keleti", "Sulysap"),
                new Tuple<string, string>("Ferencvaros", "Dunaharaszti"),
                new Tuple<string, string>("Ferencvaros", "Taksony"),
                new Tuple<string, string>("Ferencvaros", "Dunavarsany"),
                new Tuple<string, string>("Ferencvaros", "Delegyhaza"),
                new Tuple<string, string>("Ferencvaros", "Martonvasar"),
                new Tuple<string, string>("Ferencvaros", "Kapolnasnyek"),
                new Tuple<string, string>("Kelenfold", "Martonvasar"),
                new Tuple<string, string>("Kelenfold", "Kapolnasnyek")
            });

            //Console.ReadLine();
        }
    }
}

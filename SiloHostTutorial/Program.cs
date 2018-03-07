using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;

namespace SiloHostTutorial
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static private IClusterClient client;

        private static void Main(string[] args)
        {
            // First, configure and start a local silo
            var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            var silo = new SiloHost("TestSilo", siloConfig);
            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            // Then configure and connect a client.
            var clientConfig = ClientConfiguration.LocalhostSilo();
            client = new ClientBuilder().UseConfiguration(clientConfig).Build();

            client.Connect().Wait();

            Console.WriteLine("Client connected.");

            CreateTimer();

            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
            silo.ShutdownOrleansSilo();
        }

        private static void CreateTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += ATimer_Elapsed1; ;
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        private static void ATimer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random rnd = new Random();

            var converterGrain = client.GetGrain<ConverterContracts.IConverter>(new Guid());

            int value1 = rnd.Next(1000);
            int value2 = rnd.Next(1000);

            var result1 = converterGrain.ConvertToKm(value1);
            var result2 = converterGrain.ConvertToMile(value2);

            Console.WriteLine($"Original Value: {value1} Miles Converted Value: {result1.Result} Km");
            Console.WriteLine($"Original Value: {value2} Km Converted Value: {result2.Result} Miles");
        }
    }
}
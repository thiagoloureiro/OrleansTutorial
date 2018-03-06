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
            var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            client.Connect().Wait();

            Console.WriteLine("Client connected.");

            var converterGrain = client.GetGrain<ConverterContracts.IConverter>(new Guid());

            var result1 = converterGrain.ConvertToKm(1000);
            var result2 = converterGrain.ConvertToMile(1000);

            Console.WriteLine(result1.Result);
            Console.WriteLine(result2.Result);

            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
            silo.ShutdownOrleansSilo();
        }
    }
}
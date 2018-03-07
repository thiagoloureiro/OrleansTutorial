using Orleans;
using ConverterContracts;
using System.Threading.Tasks;

namespace ConverterGrain
{
    /// <summary>
    /// Grain implementation class ConverterGrain.
    /// </summary>
    public class ConverterGrain : Grain, IConverter
    {
        public Task<double> ConvertToKm(double value)
        {
            var result = value * 1.6;
            return Task.FromResult(result);
        }

        public Task<double> ConvertToMile(double value)
        {
            var result = value / 1.6;
            return Task.FromResult(result);
        }
    }
}
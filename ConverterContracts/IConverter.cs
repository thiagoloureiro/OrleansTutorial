using System.Threading.Tasks;
using Orleans;

namespace ConverterContracts
{
    /// <summary>
    /// Grain interface IConverter
    /// </summary>
    public interface IConverter : IGrainWithGuidKey
    {
        Task<double> ConvertToMile(double value);

        Task<double> ConvertToKm(double value);
    }
}
using System.Threading.Tasks;

namespace DF.RealEstate.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}
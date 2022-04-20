using System.Threading.Tasks;
using DF.RealEstate.Sessions.Dto;

namespace DF.RealEstate.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}

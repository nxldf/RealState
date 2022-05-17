using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.GeHomesInformations
{
    public interface IGeHomesInformationAppService
    {
        Task<HomeInformation> HomeInformations(string Url);
    }
}

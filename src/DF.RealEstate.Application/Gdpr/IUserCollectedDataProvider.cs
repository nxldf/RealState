using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}

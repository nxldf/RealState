using System.Collections.Generic;
using DF.RealEstate.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace DF.RealEstate.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}

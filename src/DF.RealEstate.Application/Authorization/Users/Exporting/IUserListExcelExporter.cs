using System.Collections.Generic;
using DF.RealEstate.Authorization.Users.Dto;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}
using System.Collections.Generic;
using DF.RealEstate.Authorization.Users.Importing.Dto;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}

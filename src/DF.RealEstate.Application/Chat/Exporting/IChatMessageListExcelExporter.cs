using System.Collections.Generic;
using Abp;
using DF.RealEstate.Chat.Dto;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}

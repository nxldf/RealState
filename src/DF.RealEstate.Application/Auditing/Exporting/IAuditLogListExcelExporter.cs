using System.Collections.Generic;
using DF.RealEstate.Auditing.Dto;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}

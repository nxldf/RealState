using Abp.Application.Services;
using DF.RealEstate.Dto;
using DF.RealEstate.Logging.Dto;

namespace DF.RealEstate.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}

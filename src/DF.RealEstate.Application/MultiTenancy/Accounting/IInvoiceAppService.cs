using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using DF.RealEstate.MultiTenancy.Accounting.Dto;

namespace DF.RealEstate.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}

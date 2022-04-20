using System.Threading.Tasks;
using Abp.Dependency;

namespace DF.RealEstate.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}
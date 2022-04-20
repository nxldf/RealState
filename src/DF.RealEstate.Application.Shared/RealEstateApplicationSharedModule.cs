using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DF.RealEstate
{
    [DependsOn(typeof(RealEstateCoreSharedModule))]
    public class RealEstateApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RealEstateApplicationSharedModule).GetAssembly());
        }
    }
}
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DF.RealEstate.Authorization;

namespace DF.RealEstate
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(RealEstateApplicationSharedModule),
        typeof(RealEstateCoreModule)
        )]
    public class RealEstateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RealEstateApplicationModule).GetAssembly());
        }
    }
}
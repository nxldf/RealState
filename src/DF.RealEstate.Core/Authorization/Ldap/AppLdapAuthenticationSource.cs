using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using DF.RealEstate.Authorization.Users;
using DF.RealEstate.MultiTenancy;

namespace DF.RealEstate.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
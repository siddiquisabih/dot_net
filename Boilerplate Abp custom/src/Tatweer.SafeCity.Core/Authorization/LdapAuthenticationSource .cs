//using Abp.Zero.Ldap.Authentication;
//using Abp.Zero.Ldap.Configuration;

using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Tatweer.SafeCity.Authorization.Users;
using Tatweer.SafeCity.MultiTenancy;

namespace Tatweer.SDTS.Authentication.External
{

	public class SCLdapAuthenticationSource
	: LdapAuthenticationSource<Tenant, User>
	{
		public SCLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
			: base(settings, ldapModuleConfig)
		{
		}

	}
}

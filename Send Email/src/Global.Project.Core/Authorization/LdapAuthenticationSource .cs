using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Global.Project.Authorization.Users;
using Global.Project.MultiTenancy;

namespace Global.Project.Authorization
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

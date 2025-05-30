﻿
using Abp.Zero.Ldap.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace Global.Project.Authorization
{
	public class SCLdapSettings 
        : ILdapSettings
    {
		public async Task<bool> GetIsEnabled(int? tenantId)
		{
			return true;
		}

		public async Task<ContextType> GetContextType(int? tenantId)
		{
			return ContextType.Domain;
		}

		public async Task<string> GetContainer(int? tenantId)
		{
			return null;
		}

		public async Task<string> GetDomain(int? tenantId)
		{
			return "project.local";
		}

		public async Task<string> GetUserName(int? tenantId)
		{
			return "a.mamdouh@project.io";
		}

		public async Task<string> GetPassword(int? tenantId)
		{
			return "Offer@7410";
		}

        public Task<bool> GetUseSsl(int? tenantId)
        {
            throw new System.NotImplementedException();
        }
    }
}

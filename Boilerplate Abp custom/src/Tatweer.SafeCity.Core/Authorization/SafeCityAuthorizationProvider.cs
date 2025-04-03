using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Tatweer.SafeCity.Authorization
{
    public class SafeCityAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
            {
            context.CreatePermission(PermissionNames.ViewAdminMasters, L("ViewAdminMasters"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Department, L("Departments"));
            context.CreatePermission(PermissionNames.Pages_TrafficCounter, L("TrafficCounter"));
            context.CreatePermission(PermissionNames.Pages_RadarManagment, L("RadarManagment"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.ViewAllUsers, L("ViewAllUsers"));
            context.CreatePermission(PermissionNames.ViewDepartmentManagedUsers, L("ViewDepartmentManagedUsers"));
            context.CreatePermission(PermissionNames.ViewDepartmentSupverisedUsers, L("ViewDepartmentSupverisedUsers"));
            context.CreatePermission(PermissionNames.ViewTicketRequestMasters, L("ViewTicketRequestMasters"));
            context.CreatePermission(PermissionNames.ViewServiceDeskIncidentRequests, L("ViewServiceDeskIncidentRequests"));
            context.CreatePermission(PermissionNames.ViewServiceDeskProblemRequests, L("ViewServiceDeskProblemRequests"));
            context.CreatePermission(PermissionNames.ViewServiceDeskChangeRequests, L("ViewServiceDeskChangeRequests"));
            context.CreatePermission(PermissionNames.ViewTicketRequestSelfService, L("ViewTicketRequestSelfService"));
            context.CreatePermission(PermissionNames.ViewAuditLog, L("ViewAuditLog"));
            context.CreatePermission(PermissionNames.ViewHomeMenu, L("ViewHomeMenu"));
            
            context.CreatePermission(PermissionNames.ViewDangerousViolatorDashboard, L("ViewDangerousViolatorDashboard"));
            context.CreatePermission(PermissionNames.ViewWantedVehicleDashboard, L("ViewWantedVehicleDashboard"));
            context.CreatePermission(PermissionNames.ViewWantedTruckDashboard, L("ViewWantedTruckDashboard"));
            context.CreatePermission(PermissionNames.ViewExpiredVehicleDashboard, L("ViewExpiredVehicleDashboard"));



            var itsm = context.CreatePermission(PermissionNames.ITSM, L("ITSM"));
            var pages = itsm.CreateChildPermission(PermissionNames.CHANGEMANAGEMENT, L("ITSM.ChangeManagement"));

            var so = context.CreatePermission(PermissionNames.SO, L("SO"));

            var dashboard = context.CreatePermission(PermissionNames.DASHBOARD, L("DASHBOARD"));
            var mapdashboard = context.CreatePermission(PermissionNames.MAPDASHBOARD, L("MAPDASHBOARD"));

            var ar = context.CreatePermission(PermissionNames.AR, L("AR"));

            var tc = context.CreatePermission(PermissionNames.TC, L("TC"));

            var vems = context.CreatePermission(PermissionNames.VEMS, L("VEMS"));
            var DangerousViolator = context.CreatePermission(PermissionNames.DangerousViolator, L("DangerousViolator"));
            var WantedVehicle = context.CreatePermission(PermissionNames.WantedVehicle, L("WantedVehicle"));
            var WantedTruck = context.CreatePermission(PermissionNames.WantedTruck, L("WantedTruck"));
            var ExpiredVehicle = context.CreatePermission(PermissionNames.ExpiredVehicle, L("ExpiredVehicle"));
           
            var FleetManagementSystem = context.CreatePermission(PermissionNames.FleetManagementSystem, L("FleetManagementSystem"));
            var RITSMS = context.CreatePermission(PermissionNames.RITSMS, L("RITSMS"));
            var IntelligentPatrol = context.CreatePermission(PermissionNames.IntelligentPatrol, L("IntelligentPatrol"));
            var SecurityPatrol = context.CreatePermission(PermissionNames.SecurityPatrol, L("SecurityPatrol"));
           
            var PatrolPolice = context.CreatePermission(PermissionNames.PatrolPolice, L("PatrolPolice"));

            
            var viewServiceDeskAssetGroups = context.CreatePermission(PermissionNames.ViewServiceDeskAssetGroups, L("ViewServiceDeskAssetGroups"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SafeCityConsts.LocalizationSourceName);
        }
    }
}

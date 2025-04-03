using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tatweer.SafeCity.Enums
{
    public enum RoleEnum
    {

        [Description("Admin")]
        Admin = 1,

        [Description("Technician")]
        Technician = 2,

        [Description("Analyst")]
        Analyst = 3,


        [Description("L2 Support")]
        L2Support = 4,

        [Description("Customer")]
        Customer = 5,

        [Description("Department Supervisor")]
        DepartmentSupervisor = 6,

    }


    public enum SafeCityAttachmentsEnum
    {
        [Description("User Profile")]
        UserProfile = 1,
        [Description("Accident Evidence From Mobile")]
        AccidentEvidenceFromMobile = 2,
    }


    public enum TaskStatuses
    {
        [Description("Assigned")]
        Assigned = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Closed")]
        Closed = 4,
        [Description("Re Assigned")]
        ReAssigned = 5,
        [Description("Cancelled")]
        Cancelled = 6
    }


    public enum DangerousVehicleTypeEnum
    {
        [Description("Wanted Vehicle")]
        WantedVehicle = 1,
        [Description("Dangerous Vehicle")]
        DangerousVehicle = 2,
        [Description("Expired Vehicle")]
        ExpiredVehicle = 3,
        [Description("Wanted Truck")]
        WantedTruck = 4,

    }
}

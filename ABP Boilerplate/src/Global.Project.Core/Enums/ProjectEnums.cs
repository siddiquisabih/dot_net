using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Global.Project.Enums
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


   }

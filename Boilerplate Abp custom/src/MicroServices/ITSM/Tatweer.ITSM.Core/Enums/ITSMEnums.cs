using System.ComponentModel;

namespace Tatweer.ITSM.Enums
{
    public enum ReminderTypes
    {
        [Description("Ticket Confirmation Reminder")]
        RTR = 1,
        [Description("Ticket Confirmation Final Reminder")]
        RTFR = 2,
        [Description("Ticket Waiting Customer Action Reminder")]
        WFCTR = 3,
        [Description("Ticket Waiting Customer Action Final Reminder")]
        WFCTFR = 4,
        [Description("Ticket On-Hold Reminder")]
        OHTR = 5,
        [Description("Ticket On-Hold Final Reminder")]
        OHTFR = 6
    }
    public enum TaskTypes
    {
        [Description("Manual Ticket Request Task")]
        MTRT = 1,
        [Description("Waiting For Customer Task")]
        WFCT = 5,
        [Description("Waiting For 3rd Party Task")]
        WFTPT = 6,
        [Description("Waiting For Resolution Task")]
        WFRT = 7,
        [Description("Closed Status Task")]
        CST = 10,
        [Description("On-Hold Status Task")]
        OHST = 11
    }
    public enum ChangeTaskTypes
    {
        [Description("Manual Change Request Task")]
        MCRT = 1,
        [Description("Proposed Change Request Task")]
        PCRT = 5,
        [Description("Rejected Change Request Task")]
        WFTPT = 4
    }
    public enum ReferenceType
    {
        [Description("Ticket Request")]
        TicketRequest = 1,
        [Description("Problem")]
        Problem = 2,
        [Description("Complaint")]
        Complaint = 3,
        [Description("Feedback")]
        Feedback = 4,
        [Description("ChangeRequest")]
        ChangeRequest = 5
    }
    public enum RequestLogType
    {
        [Description("Ticket Request Status Change")]
        TicketRequestStatusChange = 1,
        [Description("Task Status Change")]
        TicketRequestTaskStatusChange = 2,
        [Description("Ticket Request Owner Change")]
        TicketRequestOwnerChange = 3,
        [Description("Task Assinee Change")]
        TicketRequestTaskAssigneeChange = 4,
        [Description("Problem Status Change")]
        TicketProblemStatusChange = 5,
        [Description("Change Request Status Change")]
        ChangeRequestStatusChange = 6,
        [Description("Change Request Task Status Change")]
        ChangeRequestTaskStatusChange = 7,
    }
    public enum TicketRequestTaskStatuses
    {
        [Description("Open")]
        Open = 1,
        [Description("In-Progress")]
        InProgress = 2,
        [Description("Completed")]
        Closed = 3,
        [Description("Closed")]
        Cancelled = 4
    }
    public enum TicketSources
    {
        Other = 1,
        ServiceDeskManagement = 2,
        ProblemManagement = 3,
    }
    public enum RiskLevel
    {
        [Description("Low")]
        Low = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("High")]
        High = 3,
    }
    public enum ChangeType
    {
        [Description("Minor")]
        Minor = 1,
        [Description("Standard")]
        Standard = 2,
        [Description("Major")]
        Major = 3,
        [Description("Emergency")]
        Emergency = 4,
    }
    public enum ChangeRequestStatuses
    {
        [Description("Logged")]// created
        Logged = 1,
        [Description("Requetsed")]// change requester requests for change
        Requetsed = 2,
        [Description("Accepted")]
        Accepted = 3,
        [Description("Rejected")]
        Rejected = 4,
        [Description("Proposed")]//  financial or any other approval //  through task
        Proposed = 5,
        [Description("Pending for Approval")]
        PendingApproval = 6,
        [Description("Approved")]
        Approved = 7,
        [Description("Scheduled")]
        Scheduled = 8,
        [Description("Implemented")]
        Implemented = 9,
        [Description("Review")]
        Review = 10,
        [Description("Completed")]
        Completed = 11,
        [Description("Closed")]
        Closed = 12,
        [Description("On-Hold")]
        OnHold = 13
    }
    public enum TicketProblemStatuses
    {
        [Description("Logged")]
        Logged = 1,
        [Description("Investigation")]
        Investigation = 2,
        [Description("Identified")]
        Identified = 3,
        [Description("Resolved")]
        Resolved = 4,
        [Description("Known Error")]
        KnownError = 5,
        [Description("Completed")]
        Completed = 6,
        [Description("Closed")]
        Closed = 7,
        [Description("On-Hold")]
        OnHold = 8
    }
    public enum TicketRequestStatusesEnum
    {
        [Description("Logged")]
        Logged = 1,
        [Description("Active")]
        Active = 2,
        [Description("Assigned")]
        Assigned = 3,
        [Description("In-Progress")]
        InProgress = 4,
        [Description("Waiting for Customer")]
        WaitingForCustomer = 5,
        [Description("Waiting for 3rd Party")]
        WaitingFor3rdParty = 6,
        [Description("Waiting for Resolution")]
        WaitingForResolution = 7,
        [Description("Resolved")]
        Resolved = 8,
        [Description("Completed")]
        Completed = 9,
        [Description("Closed")]
        Closed = 10,
        [Description("On-Hold")]
        OnHold = 11,
        [Description("Archived")]
        Archived = 12


    }
    public enum TaskSeverity
    {
        Urgent = 1,
        Normal = 2,
        Low = 3,

    }
    public enum Urgency
    {
        Urgent = 1,
        High = 2,
        Normal = 3,
        Low = 4,
    }
}

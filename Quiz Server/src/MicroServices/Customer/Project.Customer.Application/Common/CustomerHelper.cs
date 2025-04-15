using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Project.Customer.Enums;

namespace Project.Customer.Common
{
    public static class CustomerHelper
    {
        public static string GetMimeType(this string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }
        public static string GetTicketRequestStatusMessage(this TicketRequestStatusesEnum status)
        {
            switch (status)
            {
                case TicketRequestStatusesEnum.Active: return "Request status changed to active";
                case TicketRequestStatusesEnum.Assigned: return "Request status changed to assigned";
                case TicketRequestStatusesEnum.InProgress: return "Request status changed to in-progress";
                case TicketRequestStatusesEnum.WaitingForCustomer: return "Request status changed to waiting for customer";
                case TicketRequestStatusesEnum.WaitingFor3rdParty: return "Request status changed to waiting for 3rd party";
                case TicketRequestStatusesEnum.WaitingForResolution: return "Request status changed to waiting for resolution.";
                case TicketRequestStatusesEnum.Completed: return "Request has been completed.";
                case TicketRequestStatusesEnum.Resolved: return "Request has been resolved and waiting for customer confirmation ";
                case TicketRequestStatusesEnum.Closed: return "Request is closed ";
                case TicketRequestStatusesEnum.OnHold: return "Request is put on-hold by service desk team";
                case TicketRequestStatusesEnum.Archived: return "Request status  changed to archive";
                default: return "";
            }
        }
        public static string GetTicketRequestStatusText(this TicketRequestStatusesEnum status)
        {
            switch (status)
            {
                case TicketRequestStatusesEnum.Active: return "active";
                case TicketRequestStatusesEnum.Assigned: return "assigned";
                case TicketRequestStatusesEnum.InProgress: return "in-progress";
                case TicketRequestStatusesEnum.WaitingForCustomer: return " waiting for customer";
                case TicketRequestStatusesEnum.WaitingFor3rdParty: return "waiting for 3rd party";
                case TicketRequestStatusesEnum.WaitingForResolution: return "waiting for resolution.";
                case TicketRequestStatusesEnum.Completed: return "completed.";
                case TicketRequestStatusesEnum.Resolved: return "resolved";
                case TicketRequestStatusesEnum.Closed: return "closed ";
                case TicketRequestStatusesEnum.OnHold: return "on-hold";
                default: return "";
            }
        }
    }
}

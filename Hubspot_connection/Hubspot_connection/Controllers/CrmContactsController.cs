using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using HubSpot.NET.Core;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Interfaces;
using System.IO;
using Microsoft.Extensions.Logging;
namespace Hubspot_connection.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CrmContactsController : Controller
    {



        [HttpGet]
        public async Task<List<ContactHubSpotModel>> GetAllContacts()
        {
            var hubSpotApi = new HubSpotApi("place Access Token Here");
            try
            {
                var contacts = hubSpotApi.Contact.List<ContactHubSpotModel>();
                return contacts.Contacts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tatweer.SafeCity.Model;

namespace Tatweer.SafeCity.Controllers
{
 
    public class ABC : SafeCityControllerBase
    {
        readonly IRepository<UserNotificationGroup> _asd;

        public ABC(IRepository<UserNotificationGroup> context)
        {
            _asd = context;
        }




        public IActionResult Salman()
        {

            //do something

            return Ok(new { message = "sabih " });
        }



    }
}

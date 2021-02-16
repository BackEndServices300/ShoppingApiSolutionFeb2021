using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ShoppingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public class DemoController : ControllerBase
    {
        private readonly ISystemTime _systemTime;
        private readonly IOptions<ConfigurationForPricing> _pricing;



        public DemoController(ISystemTime systemTime, IOptions<ConfigurationForPricing> pricing)
        {
            _systemTime = systemTime;
            _pricing = pricing;
        }

        [HttpGet("demo/time")]
        public ActionResult GetTheTime()
        {
            return Ok(new { ItIsNow = _systemTime.GetCurrent() });
        }

        [HttpGet("demo/markup")]
        public ActionResult GetMarkup()
        {
            return Ok(new { Markup = _pricing.Value.Markup });
        }

        [HttpGet("demo/employees/{id:int}")]
        public ActionResult GetAnEmployee(int id)
        {
            // do the database query, return either an employee or a 404
            if(id % 2 == 0)
            {
                return Ok(new { Id = id, Name = "Putintane" });
            } else
            {
                return NotFound();
            }
        }

        [HttpHead("demo/employees/emails/{emailAddress}")]
        public ActionResult FindEmployeesWithThatEmailAddress(string emailAddress)
        {
            if(emailAddress.EndsWith("@aol.com"))
            {
                return Ok(); // look ma! no entity
            } else
            {
                return NotFound();
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
        [HttpGet("demo/employees/emails/{emailAddress}")]
        public ActionResult FindEmployeesWithThatEmailAddressReally(string emailAddress)
        {
            if (emailAddress.EndsWith("@aol.com"))
            {
                var response = new Models.CollectionBase<string>
                {
                    Data = new List<string> { emailAddress }
                };
                return Ok(response); // look ma! no entity
            }
            else
            {
                return NotFound();
            }
        }
    }
}

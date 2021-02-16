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
    }
}

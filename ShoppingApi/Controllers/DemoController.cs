using Microsoft.AspNetCore.Mvc;
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

        public DemoController(ISystemTime systemTime)
        {
            _systemTime = systemTime;
        }

        [HttpGet("demo/time")]
        public ActionResult GetTheTime()
        {
            return Ok(new { ItIsNow = _systemTime.GetCurrent() });
        }
    }
}

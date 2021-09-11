using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ytuyemekhane_telegram_bot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HometController : ControllerBase
    {
        private readonly ILogger _logger;

        public HometController(ILogger logger)
        {
            _logger = logger;
        }

    }
}

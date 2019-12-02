using IntroToDotNetCoreWebAPI.Models.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IntroToDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly MailConfig _mailConfig;

        public ConfigsController(IConfiguration configuration, MailConfig mailConfig)
        {
            Configuration = configuration;
            _mailConfig = mailConfig;
        }

        [HttpGet]
        public ActionResult GetConfigs()
        {
            return Ok(new { Mail = _mailConfig.mailFrom, ConnectionString = Configuration["ConnectionStrings:DefaultConnection"] });
        }
    }
}
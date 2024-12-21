using DataModels;
using Lotus_Dashboard1.Areas.Identity.Data;
using Lotus_Dashboard1.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog.Web;
using System.Globalization;
using System.Net;

namespace Lotus_Dashboard1.Apis.JWT
{
    [Route("api/[controller]")]
    [ApiController]


    public class CheckLoginController : ControllerBase
    {

        private readonly UserManager<Lotus_Dashboard1User> _userManager;
        private readonly ILogger<CheckLoginController> _logger;
        public CheckLoginController(UserManager<Lotus_Dashboard1User> userManager, ILogger<CheckLoginController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<JsonResult> Check()
        {


            return new JsonResult("");

        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<JsonResult> Check([FromBody] string Model)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            PersianCalendar PC = new PersianCalendar();
            var date1 = PC.GetYear(DateTime.Now)+"/" + PC.GetMonth(DateTime.Now) + "/" + PC.GetDayOfMonth(DateTime.Now);
            var time1 = PC.GetHour(DateTime.Now) + ":" + PC.GetMinute(DateTime.Now) + ":" + PC.GetSecond(DateTime.Now);
            var token = Model;
            var userid = HttpContext.User;
            var UserRole = "";
            var identity = HttpContext.User.Identity;
            var UserName = userid.Claims.Select(x => x.Issuer).FirstOrDefault();
            var fname = _userManager.Users.Where(x => x.UserName == UserName).Select(x => x.Fname1).FirstOrDefault();
            
            int Counter = 1;
            foreach (var item in userid.Claims)
            {

                if (Counter == 5)
                {
                    UserRole = item.Value;
                }
                Counter = Counter + 1;
            }

            logger.Info($"username: {UserName} date: {date1} time: {time1}");

            return new JsonResult(fname);

        }

    }
}

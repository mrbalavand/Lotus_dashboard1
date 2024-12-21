using DataModels;
using Lotus_Dashboard1.Apis;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Globalization;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionAssetsController : ControllerBase
    {
        private readonly IDW_Services _services;

        public PositionAssetsController(IDW_Services services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<string> getdata()
        {
            return "ok";



        }


        [HttpPost]
        public async Task<string> getdata([FromBody] List<inputparameters1> input)
        {

            var nationalcode = "";
            var dscode = "";
            foreach (var item in input)
            {
                if (item.Name=="nationalcode")
                {
                    nationalcode = item.Value;
                }

                if (item.Name == "dscode")
                {
                    dscode = item.Value;
                }


                
            }

            
            var token1=await _services.get_token();

            var data=await _services.get_data(nationalcode,dscode,token1);

            return "ok";



        }


    }


}

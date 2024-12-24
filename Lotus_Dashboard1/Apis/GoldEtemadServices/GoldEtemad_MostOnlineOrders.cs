using DataModels;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace Lotus_Dashboard1.Apis.GoldEtemadServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldEtemad_MostOnlineOrders : ControllerBase
    {
        private readonly LotusibBIContext _lotusibBIContext;
        public GoldEtemad_MostOnlineOrders(LotusibBIContext lotusibBIContext)
        {
            _lotusibBIContext = lotusibBIContext;
        }


        [HttpGet]
        public async Task<IEnumerable<MostOnlineOrdersModel>> getdata(string cdate,string dsname)
        {

            if (dsname=="صندوق طلا") 
          
            {
                dsname = "11509";
            
            }

            else if (dsname == "صندوق اعتماد")

            {
                dsname = "11315";

            }

            PersianCalendar PC = new PersianCalendar();
            List<MostOnlineOrdersModel> mostOnlineOrdersModels = new List<MostOnlineOrdersModel>();
            

            var cdate1 = Convert.ToDateTime(cdate);
            var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
            cdate = sodoordate1.PersianToEnglish();
            cdate = sodoordate1.convertdate();

            //var data = await (from goldetemad in _lotusibBIContext.GoldEtemads
            //                  join tcustomer in _lotusibBIContext.TCustomers
            //                  on goldetemad.NationalCode equals tcustomer.NationalCode into jointable
            //                  from j in jointable.DefaultIfEmpty()
            //                  where goldetemad.ReceiptDate == cdate && goldetemad.DsName == dsname
            //                  select new
            //                  {
            //                      NationalCode = goldetemad.NationalCode,
            //                      CustomerName = j.IsCompany == 0 ? (j.FirstName + " " + j.LastName) : (j.CompanyName),
            //                      FundUnit = goldetemad.Amount,
            //                      CustomerType = j.IsCompany == 0 ? ("حقیقی") : ("حقوقی"),
            //                      OrderType = "صدور",
            //                  }).ToListAsync();


            //foreach (var model in data)
            //{
            //     mostOnlineOrdersModels.Add(new MostOnlineOrdersModel()
            //    {

            //        NationalCode = model.NationalCode,
            //        CustomerName = model.CustomerName,
            //        FundUnit = model.FundUnit/10,
            //        CustomerType = model.CustomerType,
            //        OrderType = model.OrderType,

            //    });

            //}



            return mostOnlineOrdersModels.OrderByDescending(x=>x.FundUnit).Take(50);



        }

    }
}

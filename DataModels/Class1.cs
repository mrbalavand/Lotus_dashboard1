using Newtonsoft.Json;

namespace DataModels
{
    public class sodoor_ebtal_viewmodel
    {

        [JsonProperty("orderId")]
        public long orderId { get; set; }

        [JsonProperty("typeId")]
        public long typeId { get; set; }

        [JsonProperty("unitCount")]
        public long unitCount { get; set; }

        [JsonProperty("unitPrice")]
        public long unitPrice { get; set; }

        [JsonProperty("date")]
        public long date { get; set; }

        [JsonProperty("fundType")]
        public string fundType { get; set; }

        [JsonProperty("fundTypeId")]
        public long fundTypeId { get; set; }

        [JsonProperty("statusId")]
        public long statusId { get; set; }

        [JsonProperty("orderStatusTitle")]
        public string orderStatusTitle { get; set; }

        [JsonProperty("regDate")]
        public long regDate { get; set; }

        [JsonProperty("regTime")]
        public string regTime { get; set; }

        [JsonProperty("branchId")]
        public string branchId { get; set; }

        [JsonProperty("orderAmount")]
        public long orderAmount { get; set; }

        [JsonProperty("receiptId")]
        public long receiptId { get; set; }

        [JsonProperty("OrderTypeTitle")]
        public string OrderTypeTitle { get; set; }

        [JsonProperty("fundId")]
        public long fundId { get; set; }

        [JsonProperty("bourceCode")]
        public long bourceCode { get; set; }


    }




    public class nationalcode_viewmodel
    {


        [JsonProperty("nationalcode")]
        public string nationalcode { get; set; }


        [JsonProperty("dscode")]
        public string dscode { get; set; }

        [JsonProperty("startdate")]
        public string startdate { get; set; }


        [JsonProperty("enddate")]
        public string enddate { get; set; }

        [JsonProperty("dscode1")]
        public string[] dscode1 { get; set; }


    }



    public class SodoorChart_ViewModel
    {


        [JsonProperty("orderdate")]
        public string orderdate { get; set; }


        [JsonProperty("sumsodoor")]
        public long sumsodoor { get; set; }


        [JsonProperty("sumebtal")]
        public long sumebtal { get; set; }

    }



    public class inputparameters1
    {

        public string Name { get; set; }


        public string Value { get; set; }

    }
    public class inputparameters
    {

        //public string title { get; set; }


        public List<inputparameters1> Parameters { get; set; }

    }







    public class FundLicense_ViewModel
    {


        [JsonProperty("fundname")]
        public string fundname { get; set; }


        [JsonProperty("fundlicense")]
        public long fundlicense { get; set; }


     

    }



    public class RegisterChart_ViewModel
    {


        [JsonProperty("registerdate")]
        public string registerdate { get; set; }


        [JsonProperty("registercount")]
        public long registercount { get; set; }




    }



    public class MaxFundunit_ViewModel
    {


        [JsonProperty("CusomerName")]
        public string CustomerName { get; set; }


        [JsonProperty("FundUnit")]
        public long FundUnit { get; set; }


        [JsonProperty("FundName")]
        public string FundName { get; set; }

        [JsonProperty("CusomerName")]
        public string CustomerName1 { get; set; }


        [JsonProperty("NationalCode")]
        public string NationalCode { get; set; }


    }



    public class apisigninviewmodel
    {

        public string username { get; set; }

        public string password { get; set; }
    }



    public class error
    {

        [JsonProperty("timeStamp")]
        public string timeStamp { get; set; }


        [JsonProperty("message")]
        public string message { get; set; }

    }



    public class login
    {

        [JsonProperty("result")]
        public string result { get; set; }

        [JsonProperty("error")]
        public error error { get; set; }

        [JsonProperty("actionId")]
        public string actionId { get; set; }





    };


    public class OnlineData_ViewModel
    {

        [JsonProperty("sodooramount")]
        public long sodooramount { get; set; }

        [JsonProperty("ebtalamount")]
        public long ebtalamount { get; set; }

        [JsonProperty("sodoorunit")]
        public long sodoorunit { get; set; }

        [JsonProperty("ebtalunit")]
        public long ebtalunit { get; set; }

        [JsonProperty("today")]
        public string today { get; set; }


        [JsonProperty("maxebtal")]
        public long maxebtal { get; set; }


        [JsonProperty("maxsodoor")]
        public long maxsodoor { get; set; }

    };




    public class OnlineData_ViewModel1
    {

      

        [JsonProperty("sodoorunit")]
        public long sodoorunit { get; set; }

        [JsonProperty("ebtalunit")]
        public long ebtalunit { get; set; }

   

    };



}
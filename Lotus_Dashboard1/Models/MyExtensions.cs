using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotus_Dashboard1.Models
{
    public static class MyExtensions
    {
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9"
            };
            return LettersDictionary.Aggregate(persianStr, (current, item) =>
                         current.Replace(item.Key, item.Value));
        }

        public static string EnglishToPersian(this string englishStr)
        {
            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["0"] = "۰",
                ["1"] = "۱",
                ["2"] = "۲",
                ["3"] = "۳",
                ["4"] = "۴",
                ["5"] = "۵",
                ["6"] = "۶",
                ["7"] = "۷",
                ["8"] = "۸",
                ["9"] = "۹"
            };
            return LettersDictionary.Aggregate(englishStr, (current, item) =>
                         current.Replace(item.Key, item.Value));
        }


        public static string convertdate(this string calendardate)
        {
            string newday = "";
            string newmonth = "";

            string[] date1 = calendardate.Split("/");

            if (date1[2].Length==1)
            {
                newday = "0" + date1[2];
            }
            else
            {
                newday =  date1[2];
            }


            if (date1[1].Length == 1)
            {
                newmonth = "0" + date1[1];
            }
            else
            {
                newmonth = date1[1];
            }

            var finaldate = date1[0]+"/" + newmonth+"/" + newday;

            return finaldate;


        }

        public static string datetonumber(this string calendardate)
        {
            string newday = "";
            string newmonth = "";

            string[] date1 = calendardate.Split("/");

            if (date1[2].Length == 1)
            {
                newday = "0" + date1[2];
            }
            else
            {
                newday = date1[2];
            }


            if (date1[1].Length == 1)
            {
                newmonth = "0" + date1[1];
            }
            else
            {
                newmonth = date1[1];
            }

            var finaldate = date1[0] +newmonth + newday;

            return finaldate;


        }


        public static string numbertodate(this string date1)
        {

            if (date1.Length<8)
            {
                throw new ArgumentOutOfRangeException("اندازه تاریخ درست نیست");
            }

            string finaldate = date1.Substring(0, 4) + "/" + date1.Substring(4, 2) + "/" + date1.Substring(6, 2);

            return finaldate;

        }


    }
}

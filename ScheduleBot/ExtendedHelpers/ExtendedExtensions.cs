using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.ExtendedHelpers
{
    public static class ExtendedExtensions
    {
        public static IEnumerable<object> Dematrix(this IEnumerable<IEnumerable<object>> matrix)
        {
            List<object> returnValue = new List<object>();
            matrix.ToList().ForEach(f =>
            {
                returnValue.AddRange(f);
            });
            return returnValue;
        }

        public static string Corrective(this string data)
        {
            return data.Replace("-", "\\-").Replace(" ", "");
        }
        public static string Clearing(this string data)
        {
            return data.ToLower().Replace(" ", "").Replace("-", "");
        }
        public static string ToString<TObject>(this List<TObject> list)
        {
            return string.Join("\n", list);
        }
        public static string ToGenitiveCase(this string day)
        {
            if (day.ToLower().Contains("сер".ToLower()))
            {
                return "середу";
            }
            if (day.ToLower().Contains("ниця".ToLower()))
            {
                return "п'ятницю";
            }

            return day;
        }

        #region Inside Viewers (Developer mode viewing)
        public static void ViewDebug(this IEnumerable<IEnumerable<object>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    Console.WriteLine(col);
                }
            }
        }
        public static void ViewDebug(this IEnumerable<object> matrix)
        {
            foreach (var item in matrix)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

    }
}

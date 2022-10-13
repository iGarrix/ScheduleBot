using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.Helpers
{
    public static class Helper
    {
        public static string GetNotDay()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Неділя";
                case DayOfWeek.Monday:
                    return "Понеділок";
                case DayOfWeek.Tuesday:
                    return "Вівторок";
                case DayOfWeek.Wednesday:
                    return "Середа";
                case DayOfWeek.Thursday:
                    return "Чертверг";
                case DayOfWeek.Friday:
                    return "П'ятниця";
                case DayOfWeek.Saturday:
                    return "Субота";
                default:
                    return "";
            }
        }

        public static int GetIndexGroup(string groupname)
        {
            string toLower = groupname.ToLower().Replace("-", "").Replace(" ", "");
            if (toLower.Contains("М11".ToLower()))
            {
                return 0;
            }
            else if (toLower.Contains("ІПЗ11".ToLower()))
            {
                return 1;
            }
            else if (toLower.Contains("ПМ11".ToLower()))
            {
                return 2;
            }
            else if (toLower.Contains("КН11".ToLower()))
            {
                return 3;
            }
            else if (toLower.Contains("І11".ToLower()))
            {
                return 4;
            }
            else if (toLower.Contains("ЦТ11".ToLower()))
            {
                return 5;
            }
            return -1;
        }
    }
}

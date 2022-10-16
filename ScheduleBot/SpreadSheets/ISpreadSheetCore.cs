using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.SpreadSheets
{
    public interface ISpreadSheetCore : IDisposable
    {
        Task<IEnumerable<IEnumerable<string>>> ReadAsync(string spreadId, string range, string errorByte = "Пари немає");
    }
}

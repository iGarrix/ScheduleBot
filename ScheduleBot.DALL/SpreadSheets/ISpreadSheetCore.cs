using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.DALL.SpreadSheets
{
    public interface ISpreadSheetCore : IDisposable
    {
        Task<IEnumerable<IEnumerable<string>>> ReadAsync(string spreadId, string range, string errorByte = "Пари немає");
    }
}

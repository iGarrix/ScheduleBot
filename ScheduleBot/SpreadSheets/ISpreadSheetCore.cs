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
        Task<IEnumerable<IEnumerable<TObject>>> ReadAsync<TObject>(string spreadId, string range);
    }
}

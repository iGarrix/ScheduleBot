using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Sheets.v4.SpreadsheetsResource;

namespace ScheduleBot.SpreadSheets
{
    // release v1.0
    public sealed class SpreadSheetCore : ISpreadSheetCore
    {
        private readonly IEnumerable<string> Scopes = new List<string>();
        private readonly ValuesResource resource = new ValuesResource(default);

        public SpreadSheetCore(string credentials)
        {
            this.Scopes = new List<string>() { SheetsService.Scope.Spreadsheets };
            this.resource = this.GetSheetsService(credentials).Spreadsheets.Values;
        }

        #region Initalizing Spreadsheets Credentials
        protected internal SheetsService GetSheetsService(string credentials)
        {
            var serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromJson(credentials).CreateScoped(Scopes)
            };
            return new SheetsService(serviceInitializer);
        }
        #endregion

        public async Task<IEnumerable<IEnumerable<string>>> ReadAsync(string spreadId, string range, string errorByte = "Пари немає")
        {
            ValueRange response = await resource.Get(spreadId, range).ExecuteAsync();
            if (response.Values is null || !response.Values.Any())
            {
                throw new Exception("Data not found"); 
            }
            return response.Values.Select(s => s.ToList().Select(ss => ss is not null ? ss.ToString() : errorByte));
        }

        // Doesn't detected
        #region Disposable Temporality Memory
        public void Dispose()
        {
            //this.resource.BatchClear(new BatchClearValuesRequest(), "*");
        }

        ~SpreadSheetCore()
        {
            Dispose();
        }
        #endregion

    }
}

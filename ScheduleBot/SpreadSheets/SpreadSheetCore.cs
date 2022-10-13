using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.SpreadSheets
{
    public class SpreadSheetCore
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string SpreadsheetId = "13uEI7ekFk67GJPuTBO0FPxQdZDaiY7_AKN3fNYC9CFI";
        private readonly string GoogleCredentialsFileName = "eminent-prism-365418-7e36fb6b91eb.json";
        private readonly string HeaderRange = "C3:H3";
        private readonly string ContentRange = "C3:H23";


        public string GetCredentialPath()
        {
            var directory = Directory.GetParent(Directory.GetCurrentDirectory().ToString());
            return Path.Combine(directory.Parent.FullName, GoogleCredentialsFileName);
        }

        public async Task<IEnumerable<string>> GetHeaderAsync()
        {
            var serviceValues = GetSheetsService().Spreadsheets.Values;
            var list = await ReadHeaderAsync(serviceValues);
            return list.FirstOrDefault().Select(s => s.ToString());
        }

        public async Task<IEnumerable<IEnumerable<string>>> GetContentAsync()
        {
            var serviceValues = GetSheetsService().Spreadsheets.Values;
            var list = await ReadContentAsync(serviceValues);
            return list.Select(s => s.ToList().Select(ss => ss.ToString()));
        }

        public SheetsService GetSheetsService()
        {
            using (var stream = new FileStream(this.GetCredentialPath(), FileMode.Open, FileAccess.Read))
            {
                var serviceInitializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
                };
                return new SheetsService(serviceInitializer);
            }
        }
        private async Task<IList<IList<object>>> ReadHeaderAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            var response = await valuesResource.Get(SpreadsheetId, HeaderRange).ExecuteAsync();
            var values = response.Values; if (values == null || !values.Any())
            {
                Console.WriteLine("No data found.");
                return null;
            }
            return values;
        }

        private async Task<IList<IList<object>>> ReadContentAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            var response = await valuesResource.Get(SpreadsheetId, ContentRange).ExecuteAsync();
            var values = response.Values; if (values == null || !values.Any())
            {
                Console.WriteLine("No data found.");
                return null;
            }
            return values;
        }


        //private async Task ReadAsync(SpreadsheetsResource.ValuesResource valuesResource)
        //{
        //    var response = await valuesResource.Get(SpreadsheetId, HeaderRange).ExecuteAsync();
        //    var values = response.Values; if (values == null || !values.Any())
        //    {
        //        Console.WriteLine("No data found.");
        //        return;
        //    }
        //    foreach (var item in values)
        //    {
        //        foreach (var item2 in item)
        //        {
        //            Console.WriteLine($"{item2}");
        //        }
        //    }
        //}
    }
}

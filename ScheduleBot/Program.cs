using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ScheduleBot;
using ScheduleBot.ExtendedHelpers;
using ScheduleBot.Source;
using ScheduleBot.SpreadSheets;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

Console.OutputEncoding = Encoding.UTF8;

#region Area for testing spreadsheets fetchs
//ISpreadSheetCore core = new SpreadSheetCore(Env.GoogleCloudDeveloperConsoleCredentials);
//var header = (await core.ReadAsync(Env.SpreadSheets.Test, "C3:H3"));
//List<object> dematrix = header.Dematrix().ToList();
//string selectedGroup = "ІПЗ-11";
//try
//{
//    int index = dematrix.FindIndex(f => f.ToString().Clearing().Contains(selectedGroup.Clearing()));
//    IEnumerable<IEnumerable<object>> content = (await core.ReadAsync(Env.SpreadSheets.Test, Env.ScheduleRange.Bakalavr1_Groups[index]));
//    List<object> contentDematrix = content.Dematrix().ToList();
//    List<string> allSchedule = new List<string>();
//    int days = 1;
//    CultureInfo ua = new CultureInfo("uk-UA");
//    allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName((DayOfWeek)days).ToGenitiveCase() + " 🔖📌" + "\n");
//    for (int i = 0; i < contentDematrix.Count(); ++i)
//    {
//        if (contentDematrix.ToList()[i] is not null && contentDematrix.ToList()[i].ToString() is not null
//            && contentDematrix.ToList()[i].ToString() != "" && contentDematrix.ToList()[i].ToString() != String.Empty && contentDematrix.ToList()[i].ToString() != "None")
//        {
//            allSchedule.Add($" 🔔 " + contentDematrix.ToList()[i]);
//        }
//        else
//        {
//            allSchedule.Add("");
//        }
//        if ((i + 1) % 4 == 0 && i > 0 && i != contentDematrix.Count() - 1)
//        {
//            if (days <= 5)
//            {
//                ++days;
//            }
//            allSchedule.Add("");
//            allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName((DayOfWeek)days).ToGenitiveCase() + " 🔖📌" + "\n");
//            allSchedule.Add("");
//        }
//    }
//    Console.WriteLine(allSchedule.ToString<string>());
//}
//catch (Exception ex)
//{
//    Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
//}
#endregion

try
{
    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = Array.Empty<UpdateType>(),
    };
    SourceBot bot = new SourceBot(Env.BOT_API_TOKEN, Env.GoogleCloudDeveloperConsoleCredentials, receiverOptions, cancellationToken);
    Console.WriteLine("[Started] " + bot.GetCore.GetMeAsync().Result.FirstName);
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("[Stoped] Bot");
}
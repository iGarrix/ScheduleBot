using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ScheduleBot;
using ScheduleBot.Helpers;
using ScheduleBot.Source;
using ScheduleBot.SpreadSheets;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

Console.OutputEncoding = Encoding.UTF8;
//Console.WriteLine("Hello world");
SpreadSheetCore core = new SpreadSheetCore();
//var res = await core.GetHeaderAsync();
//Console.WriteLine(String.Join("\n", res));
//Console.WriteLine("----------------CONTENT----------------");
//var content = await core.GetContentAsync();
//var row = content.Select(s => s.ToList()).Select(ss => $"{ss.ToList()[1]}").Skip(1);
//List<string> scheduleNowdays = new List<string>();
//scheduleNowdays.Add($"\n\n{Helper.GetNotDay()}");
//int skipped = 0;
//switch (DateTime.Now.DayOfWeek)
//{
//    case DayOfWeek.Tuesday:
//        skipped += 4 * 1;
//        break;
//    case DayOfWeek.Wednesday:
//        skipped += 4 * 2;
//        break;
//    case DayOfWeek.Thursday:
//        skipped += 4 * 3;
//        break;
//    case DayOfWeek.Friday:
//        skipped += 4 * 4;
//        break;
//    default:
//        break;
//}
//for (int i = 0; i < row.Take(4).Count(); ++i)
//{
//    scheduleNowdays.Add($"{i + 1}) " + row.Skip(skipped).Take(4).ToList()[i]);
//}


//List<string> schedule = new List<string>();
//schedule.Add("\n\nНа понеділок");
//for (int i = 0; i < row.Take(4).Count(); ++i)
//{
//    schedule.Add($"{i + 1}) " + row.Take(4).ToList()[i]);
//}
//schedule.Add("\n\nНа вівторок");
//for (int i = 0; i < row.Skip(4).Take(4).Count(); ++i)
//{
//    schedule.Add($"{i + 1}) " + row.Skip(4).Take(4).ToList()[i]);
//}
//schedule.Add("\n\nНа середу");
//for (int i = 0; i < row.Skip(8).Take(4).Count(); ++i)
//{
//    schedule.Add($"{i + 1}) " + row.Skip(8).Take(4).ToList()[i]);
//}
//schedule.Add("\n\nНа четверг");
//for (int i = 0; i < row.Skip(12).Take(4).Count(); ++i)
//{
//    schedule.Add($"{i + 1}) " + row.Skip(12).Take(4).ToList()[i]);
//}
//schedule.Add("\n\nНа п'ятницю");
//for (int i = 0; i < row.Skip(16).Take(4).Count(); ++i)
//{
//    schedule.Add($"{i + 1}) " + row.Skip(16).Take(4).ToList()[i]);
//}


//Console.WriteLine(string.Join("\n", scheduleNowdays));



try
{
    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = { },
    };
    SourceBot bot = new SourceBot(Env.BOT_API_TOKEN, receiverOptions, cancellationToken);
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

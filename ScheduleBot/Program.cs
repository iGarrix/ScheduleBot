using System;
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

Console.OutputEncoding = Encoding.UTF8;



//var core = new SpreadSheetCore();

//try
//{
//    var cts = new CancellationTokenSource();
//    var cancellationToken = cts.Token;
//    var receiverOptions = new ReceiverOptions
//    {
//        AllowedUpdates = { },
//    };
//    SourceBot bot = new SourceBot(Env.BOT_API_TOKEN, receiverOptions, cancellationToken);
//    Console.WriteLine("[Started] " + bot.GetCore.GetMeAsync().Result.FirstName);
//    Console.ReadLine();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    Console.WriteLine("[Stoped] Bot");
//}

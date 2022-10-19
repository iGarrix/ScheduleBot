// See https://aka.ms/new-console-template for more information
using ScheduleBot.DALL.Source;
using ScheduleBot.DALL;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

var bot_token = "5532128951:AAHZ72xdH0uVZu2O-0JYQW8gVYfE14tivrA";
var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>(),
};
SourceBot bot = new SourceBot(bot_token, Env.GoogleCloudDeveloperConsoleCredentials, receiverOptions, cancellationToken);
Console.WriteLine("[Started] " + bot.GetCore.GetMeAsync().Result.FirstName);
Console.ReadLine();
Console.WriteLine("[Stoped] Bot");
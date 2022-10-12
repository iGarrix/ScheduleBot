using ScheduleBot.Source;
using ScheduleBot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;

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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleBot.DALL;
using ScheduleBot.DALL.Source;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;

namespace ScheduleBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {

        [HttpGet("TestApi")]
        public IActionResult TestApiRequest()
        {
            return Ok("Api is good");
        }

        [HttpPost("Startbot")]
        public IActionResult StartBot([FromBody] string bot_token = "")
        {
            if (bot_token is null || bot_token == "" || bot_token == String.Empty)
            {
                bot_token = Env.BOT_API_TOKEN;  
            }
            try
            {
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
                return Ok("[Stopped] Bot");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("[Stoped] Bot");
            return BadRequest("[Stoped] Bot");
        }
    }
}

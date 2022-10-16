using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using ScheduleBot.SpreadSheets;
using System.Reflection.Metadata;
using ScheduleBot.ExtendedHelpers;
using System.Globalization;

namespace ScheduleBot.Source
{
    // release v1.0
    public partial class SourceBot : ISourceBot, IBotCommands
    {
        private ITelegramBotClient source { get; set; }
        private ISpreadSheetCore core { get; set; }
        private IEnumerable<IEnumerable<string>> header { get; set; }

        private string selectedGroup { get; set; } = "";

        public SourceBot(string token, string credentials, ReceiverOptions receiverOptions, CancellationToken cancellationToken)
        {
            this.source = new TelegramBotClient(token);
            Console.WriteLine($"{DateTime.Now} Initializing bot core");
            this.core = new SpreadSheetCore(credentials);
            Console.WriteLine($"{DateTime.Now} Initializing spreadsheet core");
            if (receiverOptions is not null)
            {
                source.ReceiveAsync(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
                Console.WriteLine($"{DateTime.Now} Reveiving");
            }
            header = core.ReadAsync(Env.SpreadSheets.Test, "C3:H3").Result;
        }

        public ITelegramBotClient GetCore
        {
            get
            {
                if (source is not null)
                {
                    return source;
                }
                throw new Exception("Source bot doesn't initialized");
            }
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region Equals of null object reference
            if (update is null)
            {
                Console.WriteLine($"{DateTime.Now} Updator reference is null");
                throw new Exception("Updator reference is null");
            }
            Message message = update.Message;
            if (message is null)
            {
                Console.WriteLine($"{DateTime.Now} Sending message reference is null");
                throw new Exception("Sending message reference is null");
            }
            if (message.Text is null)
            {
                Console.WriteLine($"{DateTime.Now} Sended message subinstance reference is null");
                throw new Exception("Sended message subinstance reference is null");
            }
            #endregion

            // Message view log
            Console.WriteLine($"[{DateTime.Now}] {message?.From?.Username} - {message?.Text}");

            List<object> dematrix = header.Dematrix().ToList();
            Console.WriteLine($"{DateTime.Now} Fetching schedule header");

            #region Handler of message types
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        if (dematrix.FirstOrDefault(f => f.ToString().Corrective() == message.Text.Corrective()) is not null)
                        {
                            selectedGroup = message.Text;
                            await GetChoosedGroupMenuAsync(message);
                        }
                        if (selectedGroup is null || selectedGroup == "" || selectedGroup == String.Empty)
                        {
                            await AutoSelectedGroup(message, dematrix);
                        }
                        if (message.Text.ToLower() == "/start" || message.Text.ToLower().Contains("Всі групи".ToLower()) || message.Text.ToLower().Contains("Вибрати групу".ToLower()))
                        {
                            await ChoosingGroupAsync(message, dematrix);
                        }
                        else
                        {
                            if (message.Text.ToLower().Contains("Час пар".ToLower()) || message.Text.ToLower().Contains("Час".ToLower()))
                            {
                                await GetPairEndedTimeAsync(message);
                            }
                            if (message.Text.ToLower().Contains("Розклад на сьогодні".ToLower()))
                            {
                                await GetScheduleTodayAsync(message, dematrix);
                            }
                            if (message.Text.ToLower().Contains("Розклад групи".ToLower()))
                            {
                                await GetScheduleGroupAsync(message, dematrix);
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            #endregion
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            throw exception;
        }
    }
}

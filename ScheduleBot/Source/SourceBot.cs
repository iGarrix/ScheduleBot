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
using ScheduleBot.Helpers;
using System.Reflection.Metadata;

namespace ScheduleBot.Source
{
    public class SourceBot : ISourceBot, IBotCommands
    {
        private ITelegramBotClient source { get; set; }
        private List<Action> actions { get; set; }
        private Update upt { get; set; } = null;
        private SpreadSheetCore core { get; set; }
        private string selectGroup { get; set; }

        public SourceBot(string token, ReceiverOptions receiverOptions, CancellationToken cancellationToken)
        {
            this.source = new TelegramBotClient(token);
            core = new SpreadSheetCore();
            if (receiverOptions is not null)
            {
                source.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
            }
            this.actions = new List<Action>();
            this.actions.Add(new Action(() => { Starting(); }));
            this.actions.Add(new Action(() => { SubscribeOnGroup(); }));
            this.actions.Add(new Action(() => { GetSchedule(); }));
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

            Message message = update.Message;
            this.upt = update;
            if (message is null)
            {
                throw new Exception("Message doesn't not initialized");
            }
            if (upt is null)
            {
                throw new Exception("Updator doesn't not initialized");
            }
            Console.WriteLine($"[{DateTime.Now}] {message.From.Username} - {message.Text}");

            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        foreach (var item in actions)
                        {
                            item.Invoke();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            throw exception;
        }

        public async Task Starting()
        {
            var message = upt.Message;
            if (message.Text.ToLower() == "/start" || message.Text.ToLower() == "Вибрати групу".ToLower())
            {
                List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("М11"), new KeyboardButton("ІПЗ11"), new KeyboardButton("ПМ11") });
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("КН11"), new KeyboardButton("І11"), new KeyboardButton("ЦТ11") });
                ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(rows);
                await source.SendTextMessageAsync(message.Chat, "Виберіть групу", replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
        }
        public async Task SubscribeOnGroup()
        {
            var message = upt.Message;
            if (message.Text.ToLower() == "М11".ToLower() || message.Text.ToLower() == "ІПЗ11".ToLower() || message.Text.ToLower() == "ПМ11".ToLower() ||
                message.Text.ToLower() == "КН11".ToLower() || message.Text.ToLower() == "І11".ToLower() || message.Text.ToLower() == "ЦТ11".ToLower())
            {
                List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("Розклад групи: " + message.Text) });
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("Розклад на сьогодні") });
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("Вибрати групу") });
                selectGroup = message.Text;
                ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(rows);
                await source.SendTextMessageAsync(message.Chat, "Ви вибрали групу " + message.Text, replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
        }

        public async Task GetSchedule()
        {
            var message = upt.Message;
            if (message.Text.ToLower().Contains("Розклад групи".ToLower()))
            {
                var content = await core.GetContentAsync();
                var row = content.Select(s => s.ToList()).Select(ss => $"{ss.ToList()[Helper.GetIndexGroup(selectGroup)]}").Skip(1);
                await source.SendTextMessageAsync(message.Chat, "Розклад на весь тиждень");
                List<string> schedule = new List<string>();
                schedule.Add("\n\nНа понеділок");
                for (int i = 0; i < row.Take(4).Count(); ++i)
                {
                    schedule.Add($"{i + 1}) " + row.Take(4).ToList()[i]);
                }
                schedule.Add("\n\nНа вівторок");
                for (int i = 0; i < row.Skip(4).Take(4).Count(); ++i)
                {
                    schedule.Add($"{i + 1}) " + row.Skip(4).Take(4).ToList()[i]);
                }
                schedule.Add("\n\nНа середу");
                for (int i = 0; i < row.Skip(8).Take(4).Count(); ++i)
                {
                    schedule.Add($"{i + 1}) " + row.Skip(8).Take(4).ToList()[i]);
                }
                schedule.Add("\n\nНа четверг");
                for (int i = 0; i < row.Skip(12).Take(4).Count(); ++i)
                {
                    schedule.Add($"{i + 1}) " + row.Skip(12).Take(4).ToList()[i]);
                }
                schedule.Add("\n\nНа п'ятницю");
                for (int i = 0; i < row.Skip(16).Take(4).Count(); ++i)
                {
                    schedule.Add($"{i + 1}) " + row.Skip(16).Take(4).ToList()[i]);
                }
                await source.SendTextMessageAsync(message.Chat, String.Join("\n", schedule));
            }
            if ((int)DateTime.Now.DayOfWeek >= 1 && (int)DateTime.Now.DayOfWeek <= 5)
            {
                if (message.Text.ToLower().Contains("Розклад на сьогодні".ToLower()))
                {
                    var content = await core.GetContentAsync();
                    var row = content.Select(s => s.ToList()).Select(ss => $"{ss.ToList()[Helper.GetIndexGroup(selectGroup)]}").Skip(1);
                    await source.SendTextMessageAsync(message.Chat, $"Розклад на {Helper.GetNotDay()}");
                    List<string> scheduleNowdays = new List<string>();
                    scheduleNowdays.Add($"\n\n{Helper.GetNotDay()}");
                    int skipped = 0;
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Tuesday:
                            skipped += 4 * 1;
                            break;
                        case DayOfWeek.Wednesday:
                            skipped += 4 * 2;
                            break;
                        case DayOfWeek.Thursday:
                            skipped += 4 * 3;
                            break;
                        case DayOfWeek.Friday:
                            skipped += 4 * 4;
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < row.Take(4).Count(); ++i)
                    {
                        scheduleNowdays.Add($"{i + 1}) " + row.Skip(skipped).Take(4).ToList()[i]);
                    }
                    await source.SendTextMessageAsync(message.Chat, String.Join("\n", scheduleNowdays));
                }
            }
            else if (message.Text.ToLower().Contains("Розклад на сьогодні".ToLower()))
            {     
                await source.SendTextMessageAsync(message.Chat, "Пар на сьогодні немає");
            }
            if (message.Text.ToLower().Contains("Розклад".ToLower()))
            {
                InlineKeyboardButton excel = new InlineKeyboardButton("");
                excel.Text = "Google Excel";
                excel.Url = "https://docs.google.com/spreadsheets/d/1n8aU85K284sf3xBCkCK3cdU1k3y8YuLD/edit#gid=231760637";
                InlineKeyboardMarkup reply = new InlineKeyboardMarkup(excel);
                await source.SendTextMessageAsync(message.Chat, "Оригінальний розклад", replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
        }

    }
}

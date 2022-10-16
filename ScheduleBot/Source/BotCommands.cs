using ScheduleBot.ExtendedHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ScheduleBot.Source
{
    public partial class SourceBot : ISourceBot, IBotCommands
    {
        #region Command Actions

        public async Task ChoosingGroupAsync(Message message, List<object> dematrix)
        {
            try
            {
                List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
                var rows1 = new List<KeyboardButton>();
                dematrix.Take(3).ToList().ForEach(f =>
                {
                    rows1.Add(new KeyboardButton(f is not null ? $"{f}" : " "));
                });
                var rows2 = new List<KeyboardButton>();
                dematrix.Skip(3).Take(3).ToList().ForEach(f =>
                {
                    rows2.Add(new KeyboardButton(f is not null ? $"{f}" : " "));
                });
                rows.Add(rows1);
                rows.Add(rows2);
                ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(rows);
                await source.SendTextMessageAsync(message.Chat, "👨 Виберіть свою групу", replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }

        }

        public async Task GetChoosedGroupMenuAsync(Message message)
        {
            try
            {
                List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("📌 Розклад групи: " + message.Text) });
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("📌 Розклад на сьогодні"), new KeyboardButton("📌 Час пар") });
                rows.Add(new List<KeyboardButton>() { new KeyboardButton("📌 Вибрати групу") });
                ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(rows);
                string sendMessage = "\\\tГрупа " + message.Text.Corrective() + "\\\n" +
                    "Ви можете подивитись:\\\n" +
                    "🔖 Весь розклад цієї групи\\\n" +
                    "🔖 Розклад на сьогодні\\\n" +
                    "🕐 Час пар \\\n" +
                    "👨‍ Вибрати іншу групу\\\n";

                await source.SendTextMessageAsync(message.Chat, sendMessage, replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }
        }

        public async Task GetPairEndedTimeAsync(Message message)
        {
            try
            {
                string sendMessage =
                                   "Час закінчення пар:\n" +
                                   "🕐 8:00 - 9:20\n" +
                                   "🕓 9:35 - 10:55\n" +
                                   "🕝 11:10 - 12:30\n" +
                                   "🕣 12:45 - 14:05\n";
                await source.SendTextMessageAsync(message.Chat, sendMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }
        }

        public async Task GetScheduleGroupAsync(Message message, List<object> dematrix)
        {
            try
            {
                int index = dematrix.FindIndex(f => f.ToString().Clearing().Contains(selectedGroup.Clearing()));
                IEnumerable<IEnumerable<object>> content = (await core.ReadAsync(Env.SpreadSheets.Test, Env.ScheduleRange.Bakalavr1_Groups[index]));
                List<object> contentDematrix = content.Dematrix().ToList();
                List<string> allSchedule = new List<string>();
                int days = 1;
                CultureInfo ua = new CultureInfo("uk-UA");
                allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName((DayOfWeek)days) + " 🔖📌" + "\n");
                for (int i = 0; i < contentDematrix.Count(); ++i)
                {
                    if (contentDematrix.ToList()[i] is not null || contentDematrix.ToList()[i].ToString() is not null || contentDematrix.ToList()[i].ToString() != "" || contentDematrix.ToList()[i].ToString() != String.Empty)
                    {
                        allSchedule.Add($" 🔔 " + contentDematrix.ToList()[i]);
                    }
                    else
                    {
                        allSchedule.Add("");
                    }
                    if (i % 3 == 0 && i > 0 && i != contentDematrix.Count() - 1)
                    {
                        if (days < 5)
                        {
                            ++days;
                        }
                        allSchedule.Add("");
                        allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName((DayOfWeek)days) + " 🔖📌" + "\n");
                        allSchedule.Add("");
                    }
                }
                await source.SendTextMessageAsync(message.Chat, allSchedule.ToString<string>());
                await GetOriginalScheduleDriveAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }
        }

        public async Task GetScheduleTodayAsync(Message message, List<object> dematrix)
        {
            try
            {
                CultureInfo ua = new CultureInfo("uk-UA");
                DayOfWeek days = DateTime.Now.DayOfWeek;
                if ((int)days >= 1 && (int)days <= 5)
                {
                    int index = dematrix.FindIndex(f => f.ToString().Clearing().Contains(selectedGroup.Clearing()));
                    IEnumerable<IEnumerable<object>> content = (await core.ReadAsync(Env.SpreadSheets.Test, Env.ScheduleRange.Bakalavr1_Groups[index]));
                    List<object> contentDematrix = content.Dematrix().ToList();
                    List<string> allSchedule = new List<string>();
                    allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName((DayOfWeek)days).ToGenitiveCase() + " 🔖📌" + "\n");
                    contentDematrix.Skip((int)days == 1 ? 0 : ((int)days - 1) * 4).Take(4).ToList().ForEach(f =>
                    {
                        if (f is not null || f.ToString() is not null || f.ToString() != "" || f.ToString() != String.Empty)
                        {
                            allSchedule.Add($" 🔔 " + f.ToString());
                        }
                        else
                        {
                            allSchedule.Add("");
                        }
                    });
                    await source.SendTextMessageAsync(message.Chat, allSchedule.ToString<string>());
                    await GetOriginalScheduleDriveAsync(message);
                }
                else
                {
                    await source.SendTextMessageAsync(message.Chat, $"📌❌ Сьогодні {ua.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)}, пар немає ❌📌");
                    await GetScheduleForDayAsync(message, dematrix, (DayOfWeek)1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }
        }

        private async Task GetScheduleForDayAsync(Message message, List<object> dematrix, DayOfWeek day)
        {
            if ((int)day >= 1 && (int)day <= 5)
            {
                CultureInfo ua = new CultureInfo("uk-UA");
                int index = dematrix.FindIndex(f => f.ToString().Clearing().Contains(selectedGroup.Clearing()));
                IEnumerable<IEnumerable<object>> content = (await core.ReadAsync(Env.SpreadSheets.Test, Env.ScheduleRange.Bakalavr1_Groups[index]));
                List<object> contentDematrix = content.Dematrix().ToList();
                List<string> allSchedule = new List<string>();
                allSchedule.Add("📌🔖 Розклад на " + ua.DateTimeFormat.GetDayName(day).ToGenitiveCase() + " 🔖📌" + "\n");
                contentDematrix.Skip((int)day == 1 ? 0 : ((int)day - 1) * 4).Take(4).ToList().ForEach(f =>
                {
                    if (f is not null || f.ToString() is not null || f.ToString() != "" || f.ToString() != String.Empty)
                    {
                        allSchedule.Add($" 🔔 " + f.ToString());
                    }
                    else
                    {
                        allSchedule.Add("");
                    }
                });
                await source.SendTextMessageAsync(message.Chat, allSchedule.ToString<string>());
                await GetOriginalScheduleDriveAsync(message);
            }
        }

        public async Task GetOriginalScheduleDriveAsync(Message message)
        {
            try
            {
                InlineKeyboardButton excel = new InlineKeyboardButton("");
                excel.Text = "💾 Google Drive Архів 💾";
                excel.Url = Env.GoogleDriveArchive;
                InlineKeyboardMarkup reply = new InlineKeyboardMarkup(excel);
                await source.SendTextMessageAsync(message.Chat, "🔖 Оригінальні розклади усіх груп 🔖", replyMarkup: reply, parseMode: ParseMode.MarkdownV2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + DateTime.Now + "] " + ex.Message);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ScheduleBot.DALL.Source
{
    public interface IBotCommands
    {
        Task ChoosingGroupAsync(Message message, List<object> dematrix);
        Task GetChoosedGroupMenuAsync(Message message);
        Task GetPairEndedTimeAsync(Message message);
        Task GetScheduleGroupAsync(Message message, List<object> dematrix);
        Task GetScheduleTodayAsync(Message message, List<object> dematrix);
        Task GetOriginalScheduleDriveAsync(Message message);
        Task AutoSelectedGroup(Message message, List<object> dematrix);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleBot.Source
{
    public interface IBotCommands
    {
        Task Starting();
        Task SubscribeOnGroup();
        Task GetSchedule();
    }
}

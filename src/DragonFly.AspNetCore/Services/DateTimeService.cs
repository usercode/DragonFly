using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Current()
        {
            return DateTime.Now;
        }
    }
}

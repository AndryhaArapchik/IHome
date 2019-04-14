using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILogService
    {
        void Write(LogMessage message);
        LogMessage GetById(Guid Id);
        IEnumerable<LogMessage> Search(Func<LogMessage, bool> query);

    }
}

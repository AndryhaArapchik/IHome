using Services.Entities;
using Services.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogService : ILogService
    {
        private readonly LogDataStorage DataStorage;
        private readonly string Connection;
        public LogService(LogDataStorage DataStorage, string Connection)
        {
            this.DataStorage = DataStorage;
            this.Connection = string.Format("{0}\\{1:dd.mm.yyyy}.log", Connection, DateTime.Now.ToShortDateString());
        }
        public LogMessage GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogMessage> Search(Func<LogMessage, bool> query)
        {
            throw new NotImplementedException();
        }

        public void Write(LogMessage message)
        {
            //if (message != null)
            //{
            //    if (DataStorage == LogDataStorage.File)
            //    {
            //        if (!Directory.Exists(Path.GetDirectoryName(Connection)))
            //            Directory.CreateDirectory(Connection);

            //        using (StreamWriter sw = File.AppendText(Connection))
            //        {
            //            sw.WriteLine(message.ToString());
            //        }
            //    }
            //}
        }
    }
}

using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public class LogMessage
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public LogEventType Type { get; set; }
        public string Message { get; set; }
        public Exception LogException { get; set; } 
        public LogMessage(LogEventType Type, string Message, Exception LogException)
        {
            Id = Guid.NewGuid();
            Time = DateTime.Now;
            this.Type = Type;
            this.Message = Message;
            this.LogException = LogException;
        }
        public LogMessage(LogEventType Type, string Message) : this(Type, Message, null) { }
        public LogMessage(string Message) : this(LogEventType.Information, Message, null) { }
        public LogMessage(Exception LogException) : this(LogEventType.Information, "Only exception! ", LogException) { }
        public override string ToString()
        {
            if (LogException != null)
            {
                Type = LogEventType.Error;
                Message += string.Format("; Exception message: {0}; Inner exception message: {1};", 
                    LogException.Message, LogException.InnerException != null ? LogException.InnerException.Message : "none");
            }
            return string.Format("{0}\t{1}\t{2}\tMessage: \"{3}\"", Time.ToLongTimeString(), Id, Type, Message);
        }

    }
}

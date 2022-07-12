using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Phatra.Core.Logging
{
    public class Log4NetLogger : Phatra.Core.Logging.ILog
    {
        static Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public Log4NetLogger(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        private readonly log4net.ILog _log;

        public void Debug(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Info(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void Error(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Error(Exception ex)
        {
            _log.Error(" Exception.ToString():" + ex.ToString());
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            _log.ErrorFormat(format + " Exception.ToString():" + ex.ToString(), args);
        }

        public void Fatal(Exception ex, string format, params object[] args)
        {
            _log.FatalFormat(format + " Exception.ToString():" + ex.ToString(), args);
        }
    }
}

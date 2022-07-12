using log4net.Config;
using log4net;
using log4net.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Phatra.Core.Utilities
{
    public class Logger
    {
        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Debug(string message, params object[] parameters)
        {
            Log(message, Level.Debug, null, parameters);
        }

        public static void Info(string message, params object[] parameters)
        {
            Log(message, Level.Info, null, parameters);
        }

        public static void Warn(string message, params object[] parameters)
        {
            Log(message, Level.Warn, null, parameters);
        }

        public static void Error(string message, params object[] parameters)
        {
            Error(message, null, parameters);
        }

        public static void Error(Exception exception)
        {
            if (exception == null)
                return;
            Error(exception.Message, exception);
        }

        public static void Error(string message, Exception exception, params object[] parameters)
        {
            string exceptionStack = "";

            if (exception != null)
            {
                exceptionStack = exception.GetType().Name + " : " + exception.Message + Environment.NewLine;
                Exception loopException = exception;
                while (loopException.InnerException != null)
                {
                    loopException = loopException.InnerException;
                    exceptionStack += loopException.GetType().Name + " : " + loopException.Message + Environment.NewLine;
                }
            }

            Log(message, Level.Error, exceptionStack, parameters);
        }

        private static void Log(string message, Level logLevel, string exceptionMessage, params object[] parameters)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += LogEvent;
            worker.RunWorkerAsync(new LogMessageSpec
            {
                ExceptionMessage = exceptionMessage,
                LogLevel = logLevel,
                Message = message,
                Parameters = parameters,
                Stack = new StackTrace(),
                LogTime = DateTime.Now
            });
        }

        private static void LogEvent(object sender, DoWorkEventArgs e)
        {
            try
            {
                LogMessageSpec messageSpec = (LogMessageSpec)e.Argument;

                StackFrame frame;

                if (messageSpec.LogLevel == Level.Error)
                {
                    frame = messageSpec.Stack.GetFrame(3);
                }
                else
                {
                    frame = messageSpec.Stack.GetFrame(2);
                }

                MethodBase method = frame.GetMethod();
                Type reflectedType = method.ReflectedType;

                ILogger log = LoggerManager.GetLogger(reflectedType.Assembly, reflectedType);
                Level currenLoggingLevel = ((log4net.Repository.Hierarchy.Logger)log).Parent.Level;

                if (messageSpec.LogLevel < currenLoggingLevel)
                    return;

                messageSpec.Message = string.Format(messageSpec.Message, messageSpec.Parameters);
                string stackTrace = "";
                StackFrame[] frames = messageSpec.Stack.GetFrames();
                if (frames != null)
                {
                    foreach (StackFrame tempFrame in frames)
                    {

                        MethodBase tempMethod = tempFrame.GetMethod();
                        stackTrace += tempMethod.Name + Environment.NewLine;
                    }
                }
                string userName = Thread.CurrentPrincipal.Identity.Name;
                LoggingEventData evdat = new LoggingEventData
                {
                    Domain = stackTrace,
                    Identity = userName,
                    Level = messageSpec.LogLevel,
                    LocationInfo = new LocationInfo(reflectedType.FullName,
                                                    method.Name,
                                                    frame.GetFileName(),
                                                    frame.GetFileLineNumber().ToString()),
                    LoggerName = reflectedType.Name,
                    Message = messageSpec.Message,
                    TimeStamp = messageSpec.LogTime,
                    UserName = userName,
                    ExceptionString = messageSpec.ExceptionMessage
                };
                log.Log(new LoggingEvent(evdat));
            }
            catch (Exception ex)
            //catch
            {
                string ss = ex.ToString();
                //Log(ss, Level.Error, ex.ToString(), ex);
                ////throw; 
            }//don't throw exceptions on background thread especially about logging!
        }

        private class LogMessageSpec
        {
            public StackTrace Stack { get; set; }
            public string Message { get; set; }
            public Level LogLevel { get; set; }
            public string ExceptionMessage { get; set; }
            public object[] Parameters { get; set; }
            public DateTime LogTime { get; set; }
        }
    }
}

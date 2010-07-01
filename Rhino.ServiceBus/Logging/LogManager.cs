using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Logging;

namespace Rhino.ServiceBus.Logging
{
    public static class LogManager
    {

        private class LoggerToILogWrapper : ILog
        {
            private ILogger _Logger;
            public ILogger Logger
            {
                get { return _Logger; }
                set { _Logger = value; }
            }

            /// <summary>
            /// Initializes a new instance of the LoggerToILogWrapper class.
            /// </summary>
            /// <param name="logger"></param>
            public LoggerToILogWrapper(ILogger logger)
            {
                _Logger = logger;
            }

            #region ILogger Members

            public ILogger CreateChildLogger(string loggerName)
            {
                return Logger.CreateChildLogger(loggerName);
            }

            public void Debug(string format, params object[] args)
            {
                Logger.Debug(format, args);
            }

            public void Debug(string message, Exception exception)
            {
                Logger.Debug(message, exception);
            }

            public void Debug(string message)
            {
                Logger.Debug(message);
            }

            public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.DebugFormat(exception, formatProvider, format, args);
            }

            public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.DebugFormat(formatProvider, format, args);
            }

            public void DebugFormat(Exception exception, string format, params object[] args)
            {
                Logger.DebugFormat(exception, format, args);
            }

            public void DebugFormat(string format, params object[] args)
            {
                Logger.DebugFormat(format, args);
            }

            public void Error(string format, params object[] args)
            {
                Logger.Error(format, args);
            }

            public void Error(string message, Exception exception)
            {
                Logger.Error(message, exception);
            }

            public void Error(string message)
            {
                Logger.Error(message);
            }

            public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.ErrorFormat(exception, formatProvider, format, args);
            }

            public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.ErrorFormat(formatProvider, format, args);
            }

            public void ErrorFormat(Exception exception, string format, params object[] args)
            {
                Logger.ErrorFormat(exception, format, args);
            }

            public void ErrorFormat(string format, params object[] args)
            {
                Logger.ErrorFormat(format, args);
            }

            public void Fatal(string format, params object[] args)
            {
                Logger.Fatal(format, args);
            }

            public void Fatal(string message, Exception exception)
            {
                Logger.Fatal(message, exception);
            }

            public void Fatal(string message)
            {
                Logger.Fatal(message);
            }
#pragma warning disable 618
            public void FatalError(string format, params object[] args)
            {
                Logger.FatalError(format, args);
            }

            public void FatalError(string message, Exception exception)
            {
                Logger.FatalError(message, exception);
            }

            public void FatalError(string message)
            {
                Logger.FatalError(message);
            }
#pragma warning restore 618
            public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.FatalFormat(exception, formatProvider, format, args);
            }

            public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.FatalFormat(formatProvider, format, args);
            }

            public void FatalFormat(Exception exception, string format, params object[] args)
            {
                Logger.FatalFormat(exception, format, args);
            }

            public void FatalFormat(string format, params object[] args)
            {
                Logger.FatalFormat(format, args);
            }

            public void Info(string format, params object[] args)
            {
                Logger.Info(format, args);
            }

            public void Info(string message, Exception exception)
            {
                Logger.Info(message, exception);
            }

            public void Info(string message)
            {
                Logger.Info(message);
            }

            public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.InfoFormat(exception, formatProvider, format, args);
            }

            public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.InfoFormat(formatProvider, format, args);
            }

            public void InfoFormat(Exception exception, string format, params object[] args)
            {
                Logger.InfoFormat(exception, format, args);
            }

            public void InfoFormat(string format, params object[] args)
            {
                Logger.InfoFormat(format, args);
            }

            public bool IsDebugEnabled
            {
                get { return Logger.IsDebugEnabled; }
            }

            public bool IsErrorEnabled
            {
                get { return Logger.IsErrorEnabled; }
            }

            public bool IsFatalEnabled
            {
                get { return Logger.IsFatalEnabled; }
            }
#pragma warning disable 618
            public bool IsFatalErrorEnabled
            {
                get { return Logger.IsFatalErrorEnabled; }
            }
#pragma warning restore 618
            public bool IsInfoEnabled
            {
                get { return Logger.IsInfoEnabled; }
            }

            public bool IsWarnEnabled
            {
                get { return Logger.IsWarnEnabled; }
            }

            public void Warn(string format, params object[] args)
            {
                Logger.Warn(format, args);
            }

            public void Warn(string message, Exception exception)
            {
                Logger.Warn(message, exception);
            }

            public void Warn(string message)
            {
                Logger.Warn(message);
            }

            public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.WarnFormat(exception, formatProvider, format, args);
            }

            public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Logger.WarnFormat(formatProvider, format, args);
            }

            public void WarnFormat(Exception exception, string format, params object[] args)
            {
                Logger.WarnFormat(exception, format, args);
            }

            public void WarnFormat(string format, params object[] args)
            {
                Logger.WarnFormat(format, args);
            }

            #endregion
        }
        
        
        private static ILogger _Logger = NullLogger.Instance;

        public static void Init(ILogger logger)
        {
            _Logger = logger;
        }

        public static ILog GetLogger(Type type)
        {            
            
            return new LoggerToILogWrapper(
                _Logger.CreateChildLogger(type.FullName));
        }

    }
}
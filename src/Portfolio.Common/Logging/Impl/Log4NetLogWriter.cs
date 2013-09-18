﻿using System;
using log4net;

namespace Portfolio.Common.Logging.Impl
{
    public class Log4NetLogWriter : ILogWriter
    {
        private readonly ILog log;

        public Log4NetLogWriter(Type type)
        {
            log = LogManager.GetLogger(type);
        }

        public void WriteDebug(string message)
        {
            log.Debug(message);
        }

        public void WriteError(string message)
        {
            log.Error(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLogging
{
    /// <summary>
    /// Customised Logger 
    /// 
    /// 
    /// 
    /// </summary>
    public class Logger : IDisposable
    {
        private StreamWriter _stream;

        private bool disposed = false;

        private LoggingLevel _minimumLevel;

        public bool AppendDateTime { get; set; }

        /// <summary>
        ///     Constructor
        ///     minimumLoggingLevel  -  the minimum level of logging to record.
        ///     targetStream         -  the stream to write to.
        /// </summary>
        public Logger(LoggingLevel minimumLoggingLevel, StreamWriter targetStream)
        {
            _minimumLevel = minimumLoggingLevel;
            _stream = targetStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void Log(LoggingLevel level, string message)
        {
            if (level >= _minimumLevel)
            {
                StringBuilder builder = new StringBuilder();
                if (AppendDateTime)
                {
                    builder.Append(DateTime.Now);
                }

                builder.Append(", ");
                builder.Append(level);
                builder.Append(" :: ");
                builder.Append(message);
                builder.Append(_stream.NewLine);
                builder.Append(_stream.NewLine);
                _stream.Write(builder.ToString());
            }
        }



        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Logger has been disposed.");
            }

            if (disposing)
            {
                _stream.Flush();
                _stream.Close();
                _stream.Dispose();
            }

            disposed = true;
        }
    }

    /// <summary>
    ///     The type of logging being performed.
    /// </summary>
    public enum LoggingLevel
    {
        Debug,
        Info,
        Error
    };
}

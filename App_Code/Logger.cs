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
        // Destination of logger
        private StreamWriter _stream;

        private bool _disposed = false;

        /// <summary>
        ///     Limits what kind of messages are logged.
        /// </summary>
        private LoggingLevel _minimumLevel;

        /// <summary>
        ///     Whether or not to add date time info in logged messages
        /// </summary>
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
        ///     Log a message, if it meets the logging level requirement
        /// </summary>
        /// <param name="level">Log Level of message. Message is only logged if this meets the minimum level requirement.</param>
        /// <param name="message">Message to log.</param>
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


        /// <summary>
        ///     Disposal routines
        /// </summary>
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Logger has been disposed.");
            }

            if (disposing)
            {
                _stream.Flush();
                _stream.Close();
                _stream.Dispose();
            }

            _disposed = true;
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

using System;
using System.Diagnostics;
using EzTextingApiClient.Api.Common.Model;
using RestSharp.Serializers;
using System.Collections;
using System.Linq;

namespace EzTextingApiClient
{
    /// <summary>
    /// Simple logging wrapper around TraceSource
    /// </summary>
    public class Logger
    {
        private readonly TraceSource _traceSource = new TraceSource(ClientConstants.LogTraceSourceName);
        private readonly TraceListener _eztextingLogFile;
        private readonly JsonSerializer _serializer;

        public Logger()
        {
            _eztextingLogFile = _traceSource.Listeners[ClientConstants.LogFileListenerName];
            _serializer = new JsonSerializer();
        }

        ~Logger()
        {
            _traceSource.Flush();
            _traceSource.Close();
        }

        public void Debug(string format, params object[] values)
        {
            WriteToLog("Debug", format, values);
        }

        public void Error(string format, params object[] values)
        {
            WriteToLog("Error", format, values);
        }

        private void WriteToLog(string level, string format, params object[] values)
        {
            if (_traceSource.Switch.Level == SourceLevels.Off)
            {
                return;
            }
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] == null) continue;
                if (values[i].GetType() == typeof(EzTextingModel))
                {
                    values[i] = _serializer.Serialize(values[i]);
                }
                else if (values[i] is ICollection)
                {
                    values[i] = (((ICollection) values[i]).Cast<object>().ToList().ToPrettyString());
                }
            }

            // ReSharper disable once UseStringInterpolation
            _eztextingLogFile.WriteLine(string.Format("{0} - {1} [{2}] {3}",
                DateTime.Now.ToString(ClientConstants.LogDatetimePattern),
                _traceSource.Name, level, string.Format(format, values)));
        }
    }
}
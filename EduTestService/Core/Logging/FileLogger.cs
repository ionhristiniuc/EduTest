using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;
using log4net;

namespace EduTestService.Core.Logging
{
    public sealed class FileLogger : ITraceWriter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FileLogger));
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteLog(rec);
        }

        public void WriteLog(TraceRecord rec)
        {
            var strLog = $"{rec.Category};{rec.Operator};{rec.Operation};{rec.Message}";
            log.Info(strLog);
        }
    }
}
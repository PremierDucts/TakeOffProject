using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class SerilogClass
    {
        public static readonly Serilog.ILogger _log;
        static SerilogClass()
        {
            _log = new LoggerConfiguration().
                    MinimumLevel.Debug().
                    WriteTo.File(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Logs\\Logs.log")).
                    CreateLogger();
        }

    }
}

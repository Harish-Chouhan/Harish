using System;
using Serilog;
using LAPhil.Logging;


namespace HollywoodBowl.Droid.Services
{
    public class PlatfromLogger : IPlatformLogger
    {
        public LoggerConfiguration Configure(LoggerConfiguration config)
        {
            return config.WriteTo.AndroidLog(
                outputTemplate: "[{Level}] [{SourceContext}] {Message:l}{NewLine:l}{Exception:l}"
            );
        }
    }
}

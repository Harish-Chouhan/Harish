using System;
using Serilog;
using LAPhil.Logging;


namespace HollywoodBowl.iOS.Services
{
    public class PlatfromLogger: IPlatformLogger
    {
        public LoggerConfiguration Configure(LoggerConfiguration config)
        {
            return config.WriteTo.NSLog(
                outputTemplate: "[{Level}] [{SourceContext}] {Message:l}{NewLine:l}{Exception:l}"
            );
        }
    }
}

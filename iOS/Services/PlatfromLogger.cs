using System;
using Serilog;
using LAPhil.Logging.Services;


namespace HollywoodBowl.iOS.Services
{
    public class PlatfromLogger: IPlatformLogger
    {
        public LoggerConfiguration Configure(LoggerConfiguration config)
        {
            return config.WriteTo.NSLog();
        }
    }
}

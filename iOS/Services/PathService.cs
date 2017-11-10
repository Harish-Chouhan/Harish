using Foundation;
using System;
using System.IO;


namespace HollywoodBowl.iOS.Services
{
    public static class Path
    {
        
        public static string CachePath
        {
            get
            {
                
                NSError err;

                var fileManager = new NSFileManager();
                var bundle = NSBundle.MainBundle.BundleIdentifier;
                var url = fileManager.GetUrl(NSSearchPathDirectory.ApplicationSupportDirectory, NSSearchPathDomain.All, null, true, out err);
                var path = $"{url.RelativePath}/{bundle}/caches";

                fileManager.createDirectory(path, withIntermediateDirectories: true, attributes: null);
                return path;
            }
        }   
    }
}

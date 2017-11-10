using System.Reactive.Subjects;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using LAPhil.Application;
using LAPhil.Logging;


namespace LAPhil.Connectivity
{
    public class ConnectivityService
    {
        static IConnectivity host = CrossConnectivity.Current;
        static Logger<ConnectivityService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<ConnectivityService>();

        public readonly BehaviorSubject<bool> IsConnectedSubject;
        public bool IsConnected => IsConnectedSubject.Value;


        public ConnectivityService()
        {
            IsConnectedSubject = new BehaviorSubject<bool>(host.IsConnected);
            host.ConnectivityChanged += onConnectivityChanged;
            Log.Info("Listening for connectivity changes");
        }

        void onConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Log.Info("Connectivity Enabled '{IsConnected}'", e.IsConnected);
            IsConnectedSubject.OnNext(e.IsConnected);
        }
    }
}

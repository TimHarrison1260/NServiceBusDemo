using NServiceBus;
using LibraryUI.Injection;

namespace LibraryUI
{
    public class NServiceBusConfig
    {
        public static void RegisterNServiceBus()
        {
            // NServiceBus configuration
            Configure.With()
                .DefaultBuilder()
                .ForMvc()
                .JsonSerializer()
                .Log4Net()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(true)
                .UnicastBus()
                    .ImpersonateSender(false)
                .CreateBus()
                .Start();
        }
    }
}
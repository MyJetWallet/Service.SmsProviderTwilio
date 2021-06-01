using Autofac;
using Service.SmsProviderTwilio.Services;

namespace Service.SmsProviderTwilio.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //for debug
            builder
                .RegisterType<SmsDeliveryService>()
                .AutoActivate();
        }
    }
}
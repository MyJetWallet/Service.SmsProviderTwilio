﻿using Autofac;
using Service.SmsSender.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.SmsProviderTwilio.Client
{
    public static class AutofacHelper
    {
        public static void RegisterSmsProviderTwilioClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new SmsProviderTwilioClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetSmsDeliveryService()).As<ISmsDeliveryService>().SingleInstance();
        }
    }
}

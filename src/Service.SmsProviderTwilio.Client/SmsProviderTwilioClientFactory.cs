using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;
using Service.SmsSender.Grpc;

namespace Service.SmsProviderTwilio.Client
{
    [UsedImplicitly]
    public class SmsProviderTwilioClientFactory
    {
        private readonly CallInvoker _channel;

        public SmsProviderTwilioClientFactory(string assetsDictionaryGrpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(assetsDictionaryGrpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }

        public ISmsDeliveryService GetSmsDeliveryService() => _channel.CreateGrpcService<ISmsDeliveryService>();
    }
}

using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.SmsProviderTwilio.Settings
{
    public class SettingsModel
    {
        [YamlProperty("SmsProviderTwilio.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("SmsProviderTwilio.ZipkinUrl")]
        public string ZipkinUrl { get; set; }
        
        [YamlProperty("SmsProviderTwilio.TwilioAccountSid")]
        public string TwilioAccountSid { get; set; }

        [YamlProperty("SmsProviderTwilio.TwilioAuthToken")]
        public string TwilioAuthToken { get; set; }

        [YamlProperty("SmsProviderNexmo.SenderCompanyPhone")]
        public string SenderCompanyPhone { get; set; }

        [YamlProperty("SmsProviderTwilio.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
    }
}

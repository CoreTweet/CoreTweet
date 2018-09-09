using System;
using CoreTweet.Core;
using System.Text;
namespace CoreTweet.AccountActivity
{
    public static class WebhookUtils
    {
        public static string GenerateCrcJsonResponse(this TokensBase tokens, string crcToken)
        {
            var res = SecurityUtils.HmacSha256(Encoding.UTF8.GetBytes(tokens.ConsumerSecret), Encoding.UTF8.GetBytes(crcToken));
            return $"{{\"response_token\": \"sha256={res}\"}}";
        }
    }
}

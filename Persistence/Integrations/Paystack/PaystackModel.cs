using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.Integrations.Paystack
{
    public class BaseResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
    public class TransactionInitialization: BaseResponse
    {
        [JsonPropertyName("data")]
        public TransactionInitializationData Data { get; set; }
    }

    public class TransactionInitializationData
    {
        [JsonPropertyName("authorization_url")]
        public string AuthorizationUrl { get; set; }
        [JsonPropertyName("access_code")]
        public string AccessCode { get; set; }
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
    }

    public class TransactionVerification: BaseResponse
    {
        [JsonPropertyName("data")]
        public TransactionVerificationData Data { get; set; }
    }

    public class TransactionVerificationData
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("transaction_date")]
        public DateTime TransactionDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        public string TransactionLog { get; set; }

        public bool IsSuccessful => Status == "success";
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.ViewModels;
using Microsoft.Extensions.Options;

namespace ELibrary.Infrastructure.Persistence.Integrations.Paystack
{
    public class PaystackService : IPaystackService
    {
        private readonly HttpClient _httpClient;
        private readonly PaystackSettings _paystackSettings;

        public PaystackService(HttpClient httpClient, IOptions<PaystackSettings> paystackOptions )
        {
            _httpClient = httpClient;
            _paystackSettings = paystackOptions.Value;
            _httpClient.BaseAddress = new Uri(_paystackSettings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_paystackSettings.SecretKey}");
           _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<TransactionInitialization> InitializeTransaction(string reference, string email, decimal amount, string currency = "NGN",
            string callBackUrl = null)
        {
            var koboAmount = amount * 100;
            var requestData = new
            {
                email, amount = koboAmount, reference, currency,
                callback_url = callBackUrl
            };
            var content = JsonSerializer.Serialize(requestData);
            var responseMessage = await _httpClient.PostAsync(_paystackSettings.IntializeTransactionUrl,
                new StringContent(content, Encoding.UTF8, "application/json"));
            responseMessage.EnsureSuccessStatusCode();
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<TransactionInitialization>(responseContent);
            return response;
        }

        public async Task<TransactionVerification> VerifyTransaction(string reference)
        {
            var url = $"{_paystackSettings.TransactionVerificationUrl}{reference}";
            var responseMessage = await _httpClient.GetAsync(url);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<TransactionVerification>(responseContent);
            response.Data.TransactionLog = responseContent;
            return response;
        }
    }




}

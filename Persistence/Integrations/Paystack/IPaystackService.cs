using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.Integrations.Paystack
{
    public interface IPaystackService
    {
        public Task<TransactionInitialization> InitializeTransaction(string reference, string email, decimal amount, string currency = "NGN", string callBackUrl = null);

        public Task<TransactionVerification> VerifyTransaction(string reference);

    }
}

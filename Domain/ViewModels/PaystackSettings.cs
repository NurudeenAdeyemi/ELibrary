using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{

    public class PaystackSettings
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
        public string TransactionVerificationUrl { get; set; }
        public string IntializeTransactionUrl { get; set; }
        public string TransferReceipientUrl { get; set; }
        public string ChargeReturningUrl { get; set; }
        public string TransferUrl { get; set; }
        public string TransferVerificationUrl { get; set; }
    }
}

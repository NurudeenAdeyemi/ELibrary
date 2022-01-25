using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class EmailConfiguration
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ForgotPasswordSubject { get; set; }
        public string WelcomeSubject { get; set; }
        public string CustomerAccountUpgrade { get; set; }
        public string ChangePasswordSubject { get; set; }
        public string TransactionInvoiceSubject { get; set; }
        public string TransactionReceiptSubject { get; set; }
        public string ResetPasswordSubject { get; set; }
        public string VerifyMailSubject { get; set; }
    }

}

using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.Integrations.Email
{
    public interface IMailSender
    {
        public Task SendWelcomeMail(string email, string name, string activationLink);

        public Task SendForgotPasswordMail(string email, string name, string passwordResetLink);

        public Task SendChangePasswordMail(string email, string name);

        public Task SendResetPasswordMail(string email, string name);

        public Task SendVerifyMail(string email, string name);

        public Task SendAccountUpgradeMail(string email, string name, string activationLink);

        public Task SendTransactionInvoiceMail(string email, string name, TransactionInfo transactionInfo);

        public Task SendTransactionReceiptMail(string email, string name, TransactionInfo transactionInfo);
    }
}

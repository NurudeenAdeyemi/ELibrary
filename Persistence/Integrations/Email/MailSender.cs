
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Domain.ViewModels;
using ELibrary.Infrastructure.Persistence.FileConfigurations.TemplateEngine;

namespace ELibrary.Infrastructure.Persistence.Integrations.Email
{
    public class MailSender : IMailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IEmailService _emailService;
        private readonly IRazorEngine _razorEngine;
        public MailSender(IEmailService emailService, IRazorEngine razorEngine, IOptions<EmailConfiguration> options)
        {
            _emailService = emailService;
            _razorEngine = razorEngine;
            _emailConfiguration = options.Value;
        }
        public async Task SendAccountUpgradeMail(string email, string name, string activationLink)
        {
            var model = new AccountUpgradeMail()
            {
                ActivationLink = activationLink,
                Name = name
            };
            var mailBody = await _razorEngine.ParseAsync("AccountUpgradeMail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.CustomerAccountUpgrade, mailBody);
        }

        public async Task SendChangePasswordMail(string email, string name)
        {
            var model = new ChangePasswordMail()
            {
                Name = name
            };
            var mailBody = await _razorEngine.ParseAsync("ChangePasswordMail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.ChangePasswordSubject, mailBody);
        }

        public async Task SendForgotPasswordMail(string email, string name, string passwordResetLink)
        {
            var model = new ForgotPasswordEmail()
            {
                PasswordResetLink = passwordResetLink,
                Name = name
            };
            var mailBody = await _razorEngine.ParseAsync("ForgotPasswordEmail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.ForgotPasswordSubject, mailBody);
        }

        public async Task SendResetPasswordMail(string email, string name)
        {
            var model = new ResetPasswordEmail()
            {
                Name = name
            };
            var mailBody = await _razorEngine.ParseAsync("ResetPasswordEMail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.ResetPasswordSubject, mailBody);
        }

        public async Task SendTransactionInvoiceMail(string email, string name, TransactionInfo transactionInfo)
        {
            var model = new TransactionInvoiceEmail()
            {
                TransactionInfo = transactionInfo,
                Name = name
            };
            var invoiceMessage = await _razorEngine.ParseAsync("TransactionInvoiceEmail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.TransactionInvoiceSubject, invoiceMessage);
        }

        public Task SendTransactionReceiptMail(string email, string name, TransactionInfo transactionInfo)
        {
            throw new NotImplementedException();
        }

        public async Task SendVerifyMail(string email, string name)
        {
            var model = new VerifyMail()
            {
                Name = name,
                Email = email
            };
            var mailBody = await _razorEngine.ParseAsync("VerifyMail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.VerifyMailSubject, mailBody);
        }

        public async  Task SendWelcomeMail(string email, string name, string activationLink)
        {
            var model = new WelcomeEmail()
            {
                ActivationLink = activationLink,
                Name = name
            };
            var mailBody = await _razorEngine.ParseAsync("WelcomeEmail", model);
            await _emailService.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.WelcomeSubject, mailBody);
        }
    }
}

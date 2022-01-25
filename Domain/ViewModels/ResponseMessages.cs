using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ResponseMessages
    {
        public static  string Successful = "Completed Successfully";
        public static string Failed = "Operation Failed";
        public static string MerchantCodeNotAvailable = "Merchant code is not available for use.";
        public static string EmailNotAvailable = "Email is not available for use.";
        public static string InvalidMerchantCode = "Invalid merchant code.";
        public static string EmailAlreadyExistError = "The provided is email is already in use. Kindly login.";
        public static string MerchantNotExist = "Merchant does not exist.";
        public static string MerchantAndBusinessCodeRequired = "Merchant and business code is required";
        public static  string PaymentProviderError = "Payment provider is not available at the moment.";
        public static string MerchantCodeRequired = "Merchant code is required.";
        public static string EmptyCartError = "Please add items to the cart.";
        public static string NotEnoughPinsError = "There are not enough card pins available for {0} to complete your transaction.";

        public static string NotFoundError = "Record not found.";


    }
}

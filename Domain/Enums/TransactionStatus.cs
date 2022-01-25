using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TransactionStatus
    {
        Successful = 1,
        Failed= 2,
        InProgress = 3,
        Cancelled = 4,
        Initialize = 5
    }
}

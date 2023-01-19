using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.Installments.Contract.Response;

public class PaymentPlanResponse
{
    public PaymentPlanResponse()
    {
        this.Installments = new List<InstallmentDetailsResponse>();
    }
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public List<InstallmentDetailsResponse> Installments { get; set; }
}

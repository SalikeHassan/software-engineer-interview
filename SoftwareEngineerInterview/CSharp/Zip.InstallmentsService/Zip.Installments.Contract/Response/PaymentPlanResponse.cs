namespace Zip.Installments.Contract.Response;

/// <summary>
/// Class declares properties that are used as a response of payment installment plan details 
/// </summary>
public class PaymentPlanResponse
{
    public PaymentPlanResponse()
    {
        this.Installments = new List<InstallmentDetailsResponse>();
    }

    /// <summary>
    /// Gets and Sets Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets and Sets Amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets and Sets Installments
    /// </summary>
    public List<InstallmentDetailsResponse> Installments { get; set; }
}

namespace Zip.Installments.Contract.Response;

/// <summary>
/// Class declares properties that are used as a response of payment installment details 
/// </summary>
public class InstallmentDetailsResponse
{
    /// <summary>
    ///Gets and Sets Due date
    /// </summary>
    public string DueDate { get; set; }

    /// <summary>
    ///Gets and Sets Due amount
    /// </summary>
    public decimal DueAmount { get; set; }

    /// <summary>
    ///Gets and Sets Payment id
    /// </summary>
    public Guid PaymentId { get; set; }
}

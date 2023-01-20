namespace Zip.Installments.Contract.Request;

/// <summary>
/// Class declares properties that are used to create installment plan.
/// </summary>
public class PaymentPlanRequest
{
    /// <summary>
    ///Gets and Sets Actual amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///Gets and Set Number of installment
    /// </summary>
    public int NumofInstallment { get; set; }

    /// <summary>
    ///Gets and Sets Frequency of installment
    /// </summary>
    public int Frequency { get; set; }
}
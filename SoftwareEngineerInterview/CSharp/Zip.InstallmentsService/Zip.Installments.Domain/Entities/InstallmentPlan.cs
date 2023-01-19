namespace Zip.Installments.Domain.Entities;

/// <summary>
/// Class declares properties of intallmentplan entity class.
/// </summary>
public class InstallmentPlan : Entity
{
    /// <summary>
    /// Due date.
    /// </summary>
    public DateTimeOffset DueDate { get; set; }

    /// <summary>
    /// Due amount.
    /// </summary>
    public decimal DueAmount { get; set; }

    //Navigation property
    public virtual Payment Payment { get; set; }

    public Guid PaymentId { get; set; }
}

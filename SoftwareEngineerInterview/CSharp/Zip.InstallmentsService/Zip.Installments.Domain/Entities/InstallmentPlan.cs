namespace Zip.Installments.Domain.Entities;

/// <summary>
/// Class declares properties of intallmentplan entity class
/// </summary>
public class InstallmentPlan : Entity
{
    /// <summary>
    ///Gets and Sets Due date
    /// </summary>
    public DateTimeOffset DueDate { get; set; }

    /// <summary>
    ///Gets and Sets Due amount
    /// </summary>
    public decimal DueAmount { get; set; }

    //Navigation property
    /// <summary>
    /// Navigation property used for foreign key relation
    /// </summary>
    public virtual Payment Payment { get; set; }

    /// <summary>
    /// Foreign key column
    /// </summary>
    public Guid PaymentId { get; set; }
}

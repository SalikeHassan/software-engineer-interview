namespace Zip.Installments.Domain.Entities;

/// <summary>
/// Class declares properties of payment entity class
/// </summary>
public class Payment : Entity
{
    public Payment()
    {
        this.InstallmentPlans = new HashSet<InstallmentPlan>();
    }

    /// <summary>
    ///Gets and Sets Actual amount
    /// </summary>
    public decimal Amount { get; set; }

    //Navigation property
    /// <summary>
    /// Navigation property used for foreign key relation
    /// </summary>
    public virtual ICollection<InstallmentPlan> InstallmentPlans { get; set; }
}

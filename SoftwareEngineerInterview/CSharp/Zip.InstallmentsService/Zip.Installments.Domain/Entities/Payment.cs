namespace Zip.Installments.Domain.Entities;

/// <summary>
/// Class declares properties of payment entity class.
/// </summary>
public class Payment : Entity
{
    public Payment()
    {
        this.InstallmentPlans = new HashSet<InstallmentPlan>();
    }

    /// <summary>
    /// Actual amount.
    /// </summary>
    public decimal Amount { get; set; }

    //Navigation property
    public virtual ICollection<InstallmentPlan> InstallmentPlans { get; set; }
}

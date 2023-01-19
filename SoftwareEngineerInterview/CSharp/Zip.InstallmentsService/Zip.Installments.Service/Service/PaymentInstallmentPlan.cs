namespace Zip.InstallmentsService.Service;

/// <summary>
/// Class implements the method to create payment installment plan
/// </summary>
public class PaymentInstallmentPlan : IPaymentInstallementPlan
{
    /// <summary>
    /// Method to create payment installement plan.
    /// </summary>
    /// <param name="paymentPlanRequest">Model contains data to create installement plan.</param>
    /// <returns>Returns payment installement plan.</returns>
    public Payment CreatePaymentPlan(PaymentPlanRequest paymentPlanRequest)
    {
        if (paymentPlanRequest.Amount <= 0)
        {
            return null;
        }

        var payment = new Payment();
        payment.Amount = paymentPlanRequest.Amount;

        var installementAmount = Math.Round(paymentPlanRequest.Amount / paymentPlanRequest.NumofInstallement,
            Constants.RoundOffValue,
            MidpointRounding.ToEven);

        payment.InstallmentPlans = Enumerable.Range(0, paymentPlanRequest.NumofInstallement).Select(iteration => new InstallmentPlan()
        {
            DueAmount = installementAmount,
            DueDate = iteration == 0 ? DateTimeOffset.UtcNow : DateTimeOffset.UtcNow.AddDays(paymentPlanRequest.Frequency * iteration)
        }).ToList();

        return payment;
    }
}

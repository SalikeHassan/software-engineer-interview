namespace Zip.InstallmentsService.Service;

/// <summary>
/// Class implements the method to create payment installment plan
/// </summary>
public class PaymentInstallmentPlan : IPaymentInstallementPlan
{
    /// <summary>
    /// Method to create payment installement plan
    /// </summary>
    /// <param name="paymentPlanRequest">Model contains data to create installement plan</param>
    /// <returns>Returns payment installement plan</returns>
    public Payment CreatePaymentPlan(PaymentPlanRequest paymentPlanRequest)
    {
        if (paymentPlanRequest.Amount <= 0)
        {
            return null;
        }

        var payment = new Payment();
        payment.Amount = paymentPlanRequest.Amount;

        var installmentAmount = Math.Round(paymentPlanRequest.Amount / paymentPlanRequest.NumofInstallment,
            Constants.RoundOffValue,
            MidpointRounding.ToEven);

        payment.InstallmentPlans = Enumerable.Range(0, paymentPlanRequest.NumofInstallment).Select(iteration => new InstallmentPlan()
        {
            DueAmount = iteration == paymentPlanRequest.NumofInstallment - 1 ? AddTheRoundOffDifferenceAmountInLastEmi(installmentAmount, paymentPlanRequest.Amount, paymentPlanRequest.NumofInstallment) : installmentAmount,
            DueDate = iteration == 0 ? DateTimeOffset.UtcNow : DateTimeOffset.UtcNow.AddDays(paymentPlanRequest.Frequency * iteration)
        }).ToList();

        return payment;
    }

    /// <summary>
    /// Method add the round off difference value in the last emi to make sure complete amount recovery is done
    /// </summary>
    /// <param name="installmentAmount"></param>
    /// <param name="amount"></param>
    /// <param name="numofInstallement"></param>
    /// <returns></returns>
    private decimal AddTheRoundOffDifferenceAmountInLastEmi(decimal installmentAmount, decimal amount, int numofInstallement)
    {
        var totalInstallmentAmount = installmentAmount * numofInstallement;
        var roundOfDifference = amount - totalInstallmentAmount;

        return installmentAmount + roundOfDifference;
    }
}

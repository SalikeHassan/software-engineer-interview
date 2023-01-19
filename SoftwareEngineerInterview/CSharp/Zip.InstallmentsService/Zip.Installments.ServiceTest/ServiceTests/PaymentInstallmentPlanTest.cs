namespace Zip.Installments.ServiceTest.ServiceTests;

[TestFixture]
public class PaymentInstallmentPlanTest
{
    private PaymentInstallmentPlan paymentInstallmentPlan;

    [SetUp]
    public void Setup()
    {
        this.paymentInstallmentPlan = new PaymentInstallmentPlan();
    }

    /// <summary>
    /// Test to validate payment installment plan created based on the request model passed
    /// </summary>
    /// <param name="amount">Credit amount</param>
    /// <param name="dueAmount">Emi amount</param>
    /// <param name="numOfInstallement">Number of intallements</param>
    /// <param name="frequency">Frequency of days</param>
    [Test]
    [TestCase(2000, 500, 4, 15)]
    [TestCase(1000, 250, 4, 14)]
    [TestCase(3300, 660, 5, 14)]
    [TestCase(3350, 558.33, 6, 18)]
    public void Should_Create_PaymentPlan(decimal amount,
        decimal dueAmount,
        int numOfInstallement,
        int frequency)
    {
        var paymentPlanRequest = new PaymentPlanRequest()
        {
            Amount = amount,
            NumofInstallement = numOfInstallement,
            Frequency = frequency
        };

        var paymentPlan = this.paymentInstallmentPlan.CreatePaymentPlan(paymentPlanRequest);

        Assert.Multiple(() =>
        {
            Assert.That(paymentPlan, !Is.Null);
            Assert.That(paymentPlan?.InstallmentPlans?.Count, Is.EqualTo(numOfInstallement));
            Assert.That(paymentPlan?.InstallmentPlans?.FirstOrDefault()?.DueAmount, Is.EqualTo(dueAmount));
        });
    }

    /// <summary>
    /// Test to validate payment installment plan not created
    /// </summary>
    [Test]
    public void Should_Not_Create_PaymentPlan()
    {

        var paymentPlanRequest = new PaymentPlanRequest() { };

        var paymentPlan = this.paymentInstallmentPlan.CreatePaymentPlan(paymentPlanRequest);

        Assert.That(paymentPlan, Is.Null);
    }
}

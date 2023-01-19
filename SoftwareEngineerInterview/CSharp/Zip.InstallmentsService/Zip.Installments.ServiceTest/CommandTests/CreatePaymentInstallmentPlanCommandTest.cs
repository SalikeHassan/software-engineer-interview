namespace Zip.Installments.ServiceTest.CommandTests;

[TestFixture]
public class CreatePaymentInstallmentPlanCommandTest
{
    private Mock<ZipPayContext> zipPayContext;

    [SetUp]
    public void Setup()
    {
        var fixture = new Fixture() { OmitAutoProperties = true };
        var param = fixture.Build<DbContextOptions<ZipPayContext>>().Create();
        this.zipPayContext = new Mock<ZipPayContext>(param);
        this.zipPayContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    /// <summary>
    /// Test to validate payment and payment installment plan getting saved
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Should_Create_Payment_InstallementPlan()
    {
        var paymentDbSet = new Mock<DbSet<Payment>>();
        this.zipPayContext.Setup(x => x.Payment).Returns(paymentDbSet.Object);
        var guid = Guid.NewGuid();
        var payment = new Payment()
        {
            Id = guid,
            Amount = 2000,
            CreateDateTime = DateTimeOffset.UtcNow,
            InstallmentPlans = new List<InstallmentPlan>()
                {
                    new InstallmentPlan()
                    {
                        Id  = Guid.NewGuid(),
                        CreateDateTime = DateTimeOffset.UtcNow,
                        DueAmount = 2000,
                        DueDate = DateTimeOffset.UtcNow,
                        PaymentId = guid,
                    }
                }
        };

        var command = new CreatePaymentInstallmentPlanCommand(payment);

        var handler = new CreatePaymentInstallementPlanCommandHandler(this.zipPayContext.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(guid));
            this.zipPayContext.Verify(x => x.Payment.Add(It.IsAny<Payment>()), Times.Once());
            this.zipPayContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        });
    }

    /// <summary>
    /// Test to validate payment and payment installment plan not getting saved
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Should_Not_Create_Payment_InstallementPlan()
    {
        var paymentDbSet = new Mock<DbSet<Payment>>();

        this.zipPayContext.Setup(x => x.Payment).Returns(paymentDbSet.Object);

        var command = new CreatePaymentInstallmentPlanCommand(new Payment());

        var handler = new CreatePaymentInstallementPlanCommandHandler(this.zipPayContext.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.That(result, Is.EqualTo(Guid.Empty));
    }
}

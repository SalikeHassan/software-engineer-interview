namespace Zip.Installments.ServiceTest.QueryTests;

[TestFixture]
public class GetPaymentInstallementPlanByIdQueryTest
{
    private DbContextOptions<ZipPayContext> options;

    [SetUp]
    public void Setup()
    {
        options = new DbContextOptionsBuilder<ZipPayContext>()
       .UseInMemoryDatabase(databaseName: "TestDb", b => b.EnableNullChecks(false)).Options;
    }

    /// <summary>
    /// Method creates payment installment plan based
    /// </summary>
    /// <param name="zipPayContext">Database context object</param>
    /// <param name="amount">Credit amount</param>
    /// <param name="numOfInstallments">Number of intallments</param>
    /// <param name="frequency">Frequency of days</param>
    /// <returns></returns>
    private static async Task<Guid> Init(ZipPayContext zipPayContext,
        decimal amount,
        int numOfInstallments,
        int frequency)
    {
        zipPayContext.Database.EnsureDeleted();

        var dueAmount = Math.Round(amount / numOfInstallments,
           2,
           MidpointRounding.ToEven);

        var payment = new Payment()
        {
            Amount = amount,
            CreateDateTime = DateTimeOffset.UtcNow,
            InstallmentPlans = Enumerable.Range(0, numOfInstallments).Select(iteration => new InstallmentPlan()
            {
                DueAmount = dueAmount,
                CreateDateTime = DateTimeOffset.UtcNow,
                DueDate = iteration == 0 ? DateTimeOffset.UtcNow : DateTimeOffset.UtcNow.AddDays(frequency * iteration)
            }).ToList()
        };

        var command = new CreatePaymentInstallmentPlanCommand(payment);
        var commandHandler = new CreatePaymentInstallementPlanCommandHandler(zipPayContext);
        return await commandHandler.Handle(command, new CancellationToken());
    }

    /// <summary>
    /// Test to validate payment installment plan not retrieved for invalid id
    /// </summary>
    /// <returns></returns>
    [Test]
    [Order(1)]
    public async Task Should_Not_Get_PaymentInstallementPlan()
    {
        using (var context = new ZipPayContext(this.options))
        {
            var query = new GetPaymentInstallmentPlanByIdQuery(Guid.NewGuid());

            var handler = new GetPaymentInstallementPlanByIdQueryHandler(context);

            var result = await handler.Handle(query, new CancellationToken());

            Assert.Multiple(() =>
            {
                Assert.That(result?.Installments?.FirstOrDefault()?.PaymentId, Is.Null);
            });
        }
    }

    /// <summary>
    /// Test to validate payment installment plan retrieved for valid id
    /// </summary>
    /// <param name="amount">Credit amount</param>
    /// <param name="due">Emi amount</param>
    /// <param name="numOfInstallments">Number of intallments</param>
    /// <param name="frequency">Frequency of days</param>
    /// <returns></returns>
    [Test]
    [Order(2)]
    [TestCase(2000, 500, 4, 14)]
    [TestCase(3300, 660, 5, 15)]
    [TestCase(3350, 558.33, 6, 18)]
    public async Task Should_Get_PaymentInstallementPlan(decimal amount,
        decimal due,
        int numOfInstallments,
        int frequency)
    {
        using (var context = new ZipPayContext(this.options))
        {
            var id = await Init(context, amount, numOfInstallments, frequency);

            var query = new GetPaymentInstallmentPlanByIdQuery(id);

            var handler = new GetPaymentInstallementPlanByIdQueryHandler(context);

            var result = await handler.Handle(query, new CancellationToken());

            Assert.Multiple(() =>
            {
                Assert.That(result?.Installments?.FirstOrDefault()?.PaymentId, Is.EqualTo(id));
                Assert.That(result?.Installments?.Count, Is.GreaterThan(0));
                Assert.That(result?.Installments?.FirstOrDefault()?.DueAmount, Is.EqualTo(due));
                Assert.That(result?.Installments?.Count, Is.EqualTo(numOfInstallments));
            });
        }
    }
}

namespace Zip.Installments.ServiceTest.ApiTests;

[TestFixture]
public class PaymentInstallmentControllerTest
{
    private Mock<IMediator> mediator;
    private Mock<IPaymentInstallementPlan> paymentInstallementPlan;
    private PaymentInstallmentController paymentInstallementController;
    private Mock<ILogger<PaymentInstallmentController>> logger;

    [SetUp]
    public void Setup()
    {
        this.mediator = new Mock<IMediator>();
        this.paymentInstallementPlan = new Mock<IPaymentInstallementPlan>();
        this.logger = new Mock<ILogger<PaymentInstallmentController>>();
    }

    /// <summary>
    /// Test to validate payment installment plan created with status code 201
    /// </summary>
    /// <param name="amount">Credit amount</param>
    /// <param name="numOfInstallments">Number of intallments</param>
    /// <param name="frequency">Frequency of days</param>
    /// <param name="due">Emi amount</param>
    /// <returns></returns>
    [Test]
    [TestCase(2000, 4, 14, 500)]
    [TestCase(1000, 4, 14, 250)]
    [TestCase(3300, 5, 14, 660)]
    [TestCase(3350, 6, 18, 558.33)]
    public async Task Should_Create_PaymentInstallementPlan_WithStatus201(decimal amount,
        int numOfInstallements,
        int frequency,
        decimal due)
    {
        var guid = Guid.NewGuid();

        var dueAmount = Math.Round(amount / numOfInstallements,
            2,
            MidpointRounding.ToEven);

        var payment = new Payment()
        {
            Id = guid,
            Amount = amount,
            CreateDateTime = DateTimeOffset.UtcNow,
            InstallmentPlans = Enumerable.Range(0, numOfInstallements).Select(iteration => new InstallmentPlan()
            {
                DueAmount = dueAmount,
                CreateDateTime = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                DueDate = iteration == 0 ? DateTimeOffset.UtcNow : DateTimeOffset.UtcNow.AddDays(frequency * iteration)
            }).ToList()
        };

        var response = new PaymentPlanResponse
        {
            Id = guid,
            Amount = amount,
            Installments = Enumerable.Range(0, numOfInstallements).Select(iteration => new InstallmentDetailsResponse()
            {
                PaymentId = guid,
                DueAmount = dueAmount,
                DueDate = iteration == 0 ? DateTimeOffset.UtcNow.ToString("MM/dd/yyyy") : DateTimeOffset.UtcNow.AddDays(frequency * iteration).ToString("MM/dd/yyyy")
            }).ToList()
        };

        this.paymentInstallementPlan.Setup(x => x.CreatePaymentPlan(It.IsAny<PaymentPlanRequest>()))
            .Returns(payment);

        this.mediator.Setup(x => x.Send(It.IsAny<CreatePaymentInstallmentPlanCommand>(), new CancellationToken()));

        this.mediator.Setup(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()))
            .ReturnsAsync(response);

        this.paymentInstallementController = new PaymentInstallmentController(this.paymentInstallementPlan.Object,
            this.mediator.Object,
            this.logger.Object);

        var result = await this.paymentInstallementController.Post(
            new PaymentPlanRequest()
            {
                Amount = amount,
                Frequency = frequency,
                NumofInstallment = numOfInstallements
            }
          ) as ObjectResult;

        var responseResult = result?.Value as PaymentPlanResponse;
        var paymentId = responseResult?.Installments.FirstOrDefault()?.PaymentId;

        Assert.Multiple(() =>
            {
                Assert.That(responseResult?.Installments?.FirstOrDefault()?.DueAmount, Is.EqualTo(due));
                Assert.That(responseResult?.Installments.Count, Is.EqualTo(numOfInstallements));
                Assert.That(result?.StatusCode, Is.EqualTo(201));
                Assert.That(result?.Value, Is.TypeOf<PaymentPlanResponse>());
                Assert.That(paymentId, Is.EqualTo(guid));
                this.mediator.Verify(x => x.Send(It.IsAny<CreatePaymentInstallmentPlanCommand>(), new CancellationToken()), Times.Exactly(1));
                this.mediator.Verify(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()), Times.Exactly(1));
                this.paymentInstallementPlan.Verify(x => x.CreatePaymentPlan(It.IsAny<PaymentPlanRequest>()), Times.Once);
            });
    }

    /// <summary>
    /// Test to validate the request model is not valid
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Should_PaymentPlanRequestModel_IsInvalid()
    {
        this.paymentInstallementPlan.Setup(x => x.CreatePaymentPlan(It.IsAny<PaymentPlanRequest>()));

        this.mediator.Setup(x => x.Send(It.IsAny<CreatePaymentInstallmentPlanCommand>(), new CancellationToken()));

        this.mediator.Setup(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()));

        this.paymentInstallementController = new PaymentInstallmentController(this.paymentInstallementPlan.Object,
           this.mediator.Object,
           this.logger.Object);

        var requestModel = new PaymentPlanRequest() { Amount = -1, Frequency = -1, NumofInstallment = -1 };
        var validator = new CustomValidator();
        var validationResult = validator.Validate(requestModel);

        await this.paymentInstallementController.Post(requestModel);

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.EqualTo(false));
            Assert.That(validationResult.Errors[0].ErrorMessage, Does.Contain("Amount must be greater than 0.").IgnoreCase);
            Assert.That(validationResult.Errors[1].ErrorMessage, Does.Contain("Number of installment must be greater than 0.").IgnoreCase);
            Assert.That(validationResult.Errors[2].ErrorMessage, Does.Contain("Frequency must be greater than 0.").IgnoreCase);
        });
    }

    /// <summary>
    /// Test to validate payment intallment plan retrieved based on the id passed
    /// </summary>
    /// <param name="amount">Credit amount</param>
    /// <param name="dueAmount">Emi amount</param>
    /// <param name="totalNumOfInstallements">Number of intallments</param>
    /// <param name="frequency">Frequency of days</param>
    /// <returns></returns>
    [Test]
    [TestCase(2000, 500, 4, 15)]
    [TestCase(1000, 200, 5, 14)]
    public async Task Should_Get_PaymentInstallementPlan_WithStatus200Ok(decimal amount,
        decimal dueAmount,
        int totalNumOfInstallements,
        int frequency)
    {
        var guid = Guid.NewGuid();

        var response = new PaymentPlanResponse
        {
            Id = guid,
            Amount = amount,
            Installments = Enumerable.Range(0, totalNumOfInstallements).Select(iteration => new InstallmentDetailsResponse()
            {
                DueAmount = dueAmount,
                DueDate = iteration == 0 ? DateTimeOffset.UtcNow.ToString("MM/dd/yyyy") : DateTimeOffset.UtcNow.AddDays(frequency * iteration).ToString("MM/dd/yyyy"),
                PaymentId = guid
            }).ToList()
        };

        this.mediator.Setup(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()))
         .ReturnsAsync(response);

        this.paymentInstallementController = new PaymentInstallmentController(this.paymentInstallementPlan.Object,
          this.mediator.Object,
          this.logger.Object);

        var result = await this.paymentInstallementController.Get(guid) as OkObjectResult;

        var resultResponse = result?.Value as PaymentPlanResponse;

        var paymentId = resultResponse?.Installments?.FirstOrDefault()?.PaymentId;

        Assert.Multiple(() =>
        {
            Assert.That(resultResponse?.Amount, Is.EqualTo(amount));
            Assert.That(resultResponse?.Installments.Count, Is.EqualTo(totalNumOfInstallements));
            Assert.That(resultResponse?.Installments?.FirstOrDefault()?.DueAmount, Is.EqualTo(dueAmount));
            Assert.That(paymentId, Is.EqualTo(guid));
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            this.mediator.Verify(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()), Times.Exactly(1));
        });
    }

    /// <summary>
    /// Test to validate no payment installment plan available for invalid id
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Should_Not_Get_PaymentInstallementPlan_WithStatus204NoContent()
    {
        this.mediator.Setup(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()));

        this.paymentInstallementController = new PaymentInstallmentController(this.paymentInstallementPlan.Object,
          this.mediator.Object,
          this.logger.Object);

        var result = await this.paymentInstallementController.Get(Guid.NewGuid()) as NoContentResult;

        Assert.Multiple(() =>
        {
            Assert.That(result?.StatusCode, Is.EqualTo(204));
            this.mediator.Verify(x => x.Send(It.IsAny<GetPaymentInstallmentPlanByIdQuery>(), new CancellationToken()), Times.Exactly(1));
        });
    }
}

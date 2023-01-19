namespace Zip.Installments.Api.Controllers;

using static Microsoft.AspNetCore.Http.StatusCodes;

/// <summary>
/// Api to create and get payment installment
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/paymentinstallment")]
[ApiController]
public class PaymentInstallmentController : ControllerBase
{
    private readonly IPaymentInstallementPlan paymentInstallementPlan;
    private readonly IMediator mediator;
    private readonly ILogger<PaymentInstallmentController> logger;

    public PaymentInstallmentController(IPaymentInstallementPlan paymentInstallementPlan,
        IMediator mediator,
        ILogger<PaymentInstallmentController> logger)
    {
        this.paymentInstallementPlan = paymentInstallementPlan;
        this.mediator = mediator;
        this.logger = logger;
    }

    /// <summary>
    /// Action method returns the payment installment details based on the payment id passed
    /// </summary>
    /// <param name="id">payment id</param>
    /// <returns>Returns the list of installement details</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(typeof(PaymentPlanResponse), Status200OK)]
    [ProducesResponseType(Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        logger.LogInformation("Get payment installment plan by id api called.");
        var data = await this.mediator.Send(new GetPaymentInstallmentPlanByIdQuery(id) { });

        if (data == null)
        {
            logger.LogInformation($"No payment installment plan found for id : {id}");
            return this.NoContent();
        }

        else
        {
            return this.Ok(data);
        }
    }

    /// <summary>
    /// Action method to create new payment installment based on the frequency and num of installement passed in request model
    /// </summary>
    /// <param name="paymentPlanRequest">Model contains data to create installment plan</param>
    /// <remarks>
    /// Sample request:
    ///     POST api/v1/paymentinstallment
    ///     {
    ///         "Amount": 1000,
    ///         "NumofInstallement": 4,
    ///         "Frequency": 14
    ///     }
    /// </remarks>
    /// <returns>Returns the payment installment plan</returns>
    [HttpPost]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(typeof(PaymentPlanResponse), Status201Created)]
    [ProducesResponseType(Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] PaymentPlanRequest paymentPlanRequest)
    {
        if (!ModelState.IsValid)
        {
            logger.LogInformation("Payment installment plan can not be created due to bad request model.");

            //Sending bad request status to client when request model validation failed
            return this.BadRequest();
        }

        else
        {
            logger.LogInformation($"Create payment installment plan api called.");

            var payment = this.paymentInstallementPlan.CreatePaymentPlan(paymentPlanRequest);

            var id = await this.mediator.Send(new CreatePaymentInstallmentPlanCommand(payment) { });

            var data = await this.mediator.Send(new GetPaymentInstallmentPlanByIdQuery(id) { });

            if (data == null)
            {
                return this.StatusCode(Constants.InternalServerErrCode, Constants.InstallmentPlanNotCreatedErrMsg);
            }

            else
            {
                return this.CreatedAtAction(nameof(Post), data);
            }
        }
    }
}

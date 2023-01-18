﻿namespace Zip.Installments.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Zip.Installments.Command.Commands;
    using Zip.Installments.Contract.Request;
    using Zip.Installments.Contract.Response;
    using Zip.Installments.Query.Queries;
    using Zip.InstallmentsService.Interface;
    using static Microsoft.AspNetCore.Http.StatusCodes;

    /// <summary>
    /// Api to create and get payment installment.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/paymentinstallment")]
    [ApiController]
    public class PaymentInstallmentController : ControllerBase
    {
        private readonly IPaymentInstallementPlan paymentInstallementPlan;
        private readonly IMediator mediator;

        public PaymentInstallmentController(IPaymentInstallementPlan paymentInstallementPlan,
            IMediator mediator)
        {
            this.paymentInstallementPlan = paymentInstallementPlan;
            this.mediator = mediator;
        }

        /// <summary>
        /// Action method returns the payment installment details based on the payment id passed.
        /// </summary>
        /// <param name="id">payment id.</param>
        /// <returns>Returns the list of installement details.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(typeof(List<InstallmentDetailsResponse>), Status200OK)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await this.mediator.Send(new GetPaymentInstallmentPlanByIdQuery(id) { });

            if (!data.Any())
            {
                return this.NoContent();
            }

            else
            {
                return this.Ok(data);
            }
        }

        /// <summary>
        /// Action method to create new payment installment based on the frequency and num of installement passed in request model.
        /// </summary>
        /// <param name="paymentPlanRequest">Model contains data to create installment plan.</param>
        /// <returns>Returns the list of installement details.</returns>
        [HttpPost]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(typeof(List<InstallmentDetailsResponse>), Status200OK)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PaymentPlanRequest paymentPlanRequest)
        {
            if (!ModelState.IsValid)
            {
                //Sending bad request status to client when request model validation failed
                return this.BadRequest();
            }

            else
            {
                var payment = this.paymentInstallementPlan.CreatePaymentPlan(paymentPlanRequest);

                var id = await this.mediator.Send(new CreatePaymentInstallmentPlanCommand(payment) { });

                var data = await this.mediator.Send(new GetPaymentInstallmentPlanByIdQuery(id) { });

                if (!data.Any())
                {
                    return this.NoContent();
                }

                else
                {
                    return this.Ok(data);
                }
            }
        }
    }
}
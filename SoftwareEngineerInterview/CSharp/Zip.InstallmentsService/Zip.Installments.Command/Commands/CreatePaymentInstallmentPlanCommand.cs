namespace Zip.Installments.Command.Commands;

/// <summary>
/// Class defines method to create payment installment plan
/// </summary>
public class CreatePaymentInstallmentPlanCommand : IRequest<Guid>
{
    private readonly Payment payment;

    public CreatePaymentInstallmentPlanCommand(Payment payment)
    {
        this.payment = payment;
    }

    /// <summary>
    /// Method creates new payment plan and installment plan
    /// </summary>
    public class CreatePaymentInstallementPlanCommandHandler : IRequestHandler<CreatePaymentInstallmentPlanCommand, Guid>
    {
        private readonly ZipPayContext zipPayContext;

        public CreatePaymentInstallementPlanCommandHandler(ZipPayContext zipPayContext)
        {
            this.zipPayContext = zipPayContext;
        }

        public async Task<Guid> Handle(CreatePaymentInstallmentPlanCommand request, CancellationToken cancellationToken)
        {
            this.zipPayContext.Payment.Add(request.payment);

            await this.zipPayContext.SaveChangesAsync();

            return request.payment.Id;
        }
    }
}

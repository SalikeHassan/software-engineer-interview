namespace Zip.Installments.Query.Queries;

/// <summary>
/// Class defines method to get the payment installment plan based on the payment id passed.
/// </summary>
public class GetPaymentInstallmentPlanByIdQuery : IRequest<PaymentPlanResponse>
{
    private readonly Guid id;

    public GetPaymentInstallmentPlanByIdQuery(Guid id)
    {
        this.id = id;
    }

    public class GetPaymentInstallementPlanByIdQueryHandler : IRequestHandler<GetPaymentInstallmentPlanByIdQuery, PaymentPlanResponse>
    {
        private readonly ZipPayContext zipPayContext;

        public GetPaymentInstallementPlanByIdQueryHandler(ZipPayContext zipPayContext)
        {
            this.zipPayContext = zipPayContext;
        }
        public async Task<PaymentPlanResponse> Handle(GetPaymentInstallmentPlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.zipPayContext.Payment
                  .Include(x => x.InstallmentPlans)
                  .AsNoTracking()
                  .Where(x => x.Id == request.id)
                  .Select(paymentPlan => new PaymentPlanResponse()
                  {
                      Id = paymentPlan.Id,
                      Amount = paymentPlan.Amount,
                      Installments = paymentPlan.InstallmentPlans.Select(x => new InstallmentDetailsResponse()
                      {
                          DueAmount = x.DueAmount,
                          DueDate = x.DueDate.ToString("MM/dd/yyyy"),
                          PaymentId = x.PaymentId
                      }).ToList()
                  }).FirstOrDefaultAsync();
        }
    }
}
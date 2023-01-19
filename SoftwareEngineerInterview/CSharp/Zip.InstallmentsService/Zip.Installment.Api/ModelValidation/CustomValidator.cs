namespace Zip.Installments.Api.ModelValidation;
using FluentValidation;

/// <summary>
/// Class defines the custom validation using fluent validation api methods.
/// </summary>
public class CustomValidator : AbstractValidator<PaymentPlanRequest>
{
    public CustomValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(Constants.MinmValue)
            .WithMessage(Constants.AmountMinmValueErrMsg);

        RuleFor(x => x.NumofInstallement).GreaterThan(Constants.MinmValue)
            .WithMessage(Constants.NoOfInstallmentMinmValueErrMsg);

        RuleFor(x => x.Frequency).GreaterThan(Constants.MinmValue)
            .WithMessage(Constants.FrequencyMinmValueErrMsg);
    }
}

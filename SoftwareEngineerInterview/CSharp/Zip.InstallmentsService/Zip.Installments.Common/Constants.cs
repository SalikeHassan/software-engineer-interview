﻿namespace Zip.Installments.Common;

/// <summary>
/// Class defines constants values that are used inside the project solution.
/// </summary>
public static class Constants
{
    public const int MinmValue = 0;
    public const int RoundOffValue = 2;

    public const string CommandClassAssemblyName = "Zip.Installments.Command";
    public const string QueryClassAssemblyName = "Zip.Installments.Query";

    public const string AmountMinmValueErrMsg = "Amount must be greater than 0.";
    public const string FrequencyMinmValueErrMsg = "Frequency must be greater than 0.";
    public const string NoOfInstallmentMinmValueErrMsg = "Number of installment must be greater than 0.";

    public const string InstallmentPlanNotCreatedErrMsg = "Due to an internal server error, the payment installment plan was not created.";
    public const int InternalServerErrCode = 500;
}

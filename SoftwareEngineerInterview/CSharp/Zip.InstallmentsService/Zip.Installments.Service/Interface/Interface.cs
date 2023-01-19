﻿namespace Zip.InstallmentsService.Interface;

/// <summary>
/// Interface declares method to create payment installement plan.
/// </summary>
public interface IPaymentInstallementPlan
{
    /// <summary>
    /// Method to create payment installement plan.
    /// </summary>
    /// <param name="paymentPlanRequest">Model contains data to create installement plan.</param>
    /// <returns>Returns payment installement plan.</returns>
    Payment CreatePaymentPlan(PaymentPlanRequest paymentPlanRequest);
}

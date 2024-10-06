namespace ScaleUp.Core.SharedKernel.Constants;

public static class ErrorConstants
{
    public const string IsExisted = "{0} already exists.";
    public const string InvalidPayment = "Invalid order payment.";
    public const string CantFindCustomer = "Customer's information can't be found.";
    public const string FailHaravanApi = "Haravan Api fail.";
    public const string InvalidCancelReason = "Invalid cancellation reason.";
    public const string CantChangeStatus = "Current status {0} is not allowed to change to {1}.";
    public const string InvalidStatus = "Invalid status.";
    public const string CantCancel = "Cannot cancel order in status {0}.";
    public const string CantDeliver = "Cannot deliver order in status {0}.";
    public const string InvalidDelivery = "Invalid Delivery.";
    public const string CantConfirm = "Cannot confirm order in status {0}.";
    public const string CantPick = "Cannot pick order in status {0}.";
    public const string CantPack = "Cannot pack order in status {0}.";
    public const string InvalidFulfillment = "Invalid fulfillment.";
    public const string InvalidCustomer = "Invalid customer.";
    public const string FailedHaravanApi = "Haravan Api failed.";
    public const string InvalidCodAmout = "Cod amount cannot be greater than order total.";
    public const string FailUpdatedDeliveryHaravanApi = "Failed to update delivery on Haravan.";
    public const string FailUpdatedFulfillmentHaravanApi = "Failed to update fulfillment on Haravan.";
    public const string RoleExisted = "Role name {0} in this tenant is already existed";
    public const string EmailExisted = "{0} with this email {1} already exists";
}
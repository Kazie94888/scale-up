namespace ScaleUp.Core.SharedKernel.Configurations;

public class OrderCancelReasonConfigurations
{
    public List<OrderCancelReason> Values { get; set; }
    public sealed class OrderCancelReason
    {
        public required string Code { get; set; }

        public string? Description { get; set; }
        public int Index { get; set; }
    }
}
namespace ScaleUp.Core.Domain.Base.Interfaces;

public interface IMultiTenant
{
    public Guid TenantId { get; set; }
}
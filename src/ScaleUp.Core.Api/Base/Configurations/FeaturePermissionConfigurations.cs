namespace ScaleUp.Core.Api.Base.Configurations;

public class FeaturePermissionConfigurations
{
    public required List<FeaturePermissionGroup> Values { get; set; }
    
    public sealed class FeaturePermissionGroup
    {
        public required string GroupName { get; set; }
        public required List<FeaturePermission> Features { get; set; }
    }
    
    public sealed class FeaturePermission
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Note { get; set; }
    }
}
namespace ScaleUp.Core.SharedKernel.Configurations;

public sealed record GlobalConfigurations
{
    public GoogleConfiguration GoogleConfig { get; set; }

    public sealed record GoogleConfiguration
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}

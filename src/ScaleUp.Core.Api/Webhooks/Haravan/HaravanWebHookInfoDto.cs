using ScaleUp.Core.Api.Webhooks.Haravan;

namespace ScaleUp.Core.Application.Integrations.Haravan.Base;

internal sealed class HaravanWebHookInfoDto
{
    public required string OrgId { get; set; }
    public required string HmacSha256 { get; set; }
    public string OrderId { get; set; }
    public required HaravanWebHookType Type { get; set; }

    public static ValueTask<HaravanWebHookInfoDto> BindAsync(HttpContext context)
    {
        var headers = context.Request.Headers;
        var orgId = headers["X-Haravan-Org-Id"].First();
        var hmacSha256 = headers["X-Haravan-Hmacsha256"].First();
        var type = headers["X-Haravan-Topic"].First();
        var orderId = headers["X-Haravan-Order-Id"].FirstOrDefault();

        return ValueTask.FromResult(new HaravanWebHookInfoDto
        {
            OrgId = orgId!,
            HmacSha256 = hmacSha256!,
            Type = HaravanWebHookType.FromName(type),
            OrderId = orderId
        });
    }
}

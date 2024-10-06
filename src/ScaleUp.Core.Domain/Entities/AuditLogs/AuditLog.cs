using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.AuditLogs;

public sealed class AuditLog : AggregateRoot
{
    private AuditLog() { }

    [BsonElement("_id")]
    public required Guid Id { get; set; }
    public required string Event { get; set; }
    public required string EventDescription { get; set; }
    public required DateTimeOffset Timestamp { get; set; }

    private readonly List<AuditLogParameter> _parameters = [];
    public IReadOnlyList<AuditLogParameter> Parameters => _parameters;

    private void AddParameters(List<AuditLogParameter> parameters)
    {
        _parameters.AddRange(parameters);
    }

    public static AuditLog Create(string action, string description, List<AuditLogParameter> parameters, DateTimeOffset timestamp)
    {
        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            Event = action,
            EventDescription = description,
            Timestamp = timestamp,
            CreatedBy = UserInfo.System
        };

        auditLog.AddParameters(parameters);

        return auditLog;
    }

    public string? GetParameter(string name)
    {
        return _parameters.Where(param => param.Name == name)
                                                    .Select(param => param.Value)
                                                    .FirstOrDefault();
    }
}

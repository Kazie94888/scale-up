using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Customers;

public sealed class Customer : AggregateRoot
{
    private Customer()
    {
    }

    public static Customer Create(string firstName, string lastName, string phoneNumber, string? email,
        UserInfo createdBy)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            CreatedBy = createdBy,
        };

        // TODO: Add domain event
        // customer.AddDomainEvent(new CustomerCreatedEvent(customer));

        return customer;
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }


    public void UpdateCustomerInfo(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        // TODO: Add domain event
    }
}
using FluentResults;
using ScaleUp.Core.Application.Orders.Dtos;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Application.Orders;

public interface IOrderManager
{
    Task<Result> CancelOrder(Order order, string cancelReasonCode, string cancelNote, UserInfo userInfo);
    Task<Result> ConfirmOrder(Order order, string confirmedNote, UserInfo userInfo);
    Result PickOrder(Order order, string pickedNote, UserInfo userInfo);
    Result PackOrder(Order order, string packedNote, UserInfo userInfo);
    Task<Result> DeliverOrder(Order order, string deliveredNote, UserInfo userInfo);
    Result UpdateOrderStatus(Order order, string newStatus, string note, UserInfo updatedBy);
    Task<Result> UpdateDelivery(Order order, UpdateOrderDeliveryRequestDto request, UserInfo updatedBy);
    Task<Result> ConfirmOrderPayment(Order order, UserInfo userInfo);
}
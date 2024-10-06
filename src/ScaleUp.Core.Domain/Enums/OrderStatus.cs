using ScaleUp.Core.SharedKernel.Base;

namespace ScaleUp.Core.Domain.Enums;


public class OrderStatus : SmartEnumBase<OrderStatus>
{
    private const string _processing = "processing";
    private const string _warning = "warning";
    private const string _success = "success";
    private const string _error = "error";
    private const string _awaitingCall = "awaiting_call";
    private const string _completed = "completed";
    private const string _confirmed = "confirmed";
    private const string _damaged = "damaged";
    private const string _delivered = "delivered";
    private const string _delivering = "delivering";
    private const string _deliveryCancelled = "delivery_cancelled";
    private const string _deliveryFailed = "delivery_failed";
    private const string _deliveryPicking = "delivery_picking";
    private const string _lacked = "lacked";
    private const string _linehaul = "linehaul";
    private const string _held = "held";
    private const string _missed = "missed";
    private const string _new = "new";
    private const string _outOfStock = "out_of_stock";
    private const string _packed = "packed";
    private const string _picked = "picked";
    private const string _picking = "picking";
    private const string _returned = "returned";
    private const string _splitted = "splitted";
    private const string _waitingForReturn = "waiting_for_return";
    private const string _cancelled = "cancelled";
    private const string _shipped = "shipped";
    private const string _actionConfirm = "confirm";
    private const string _actionCancel = "cancel";

    public static readonly OrderStatus AwaitingCall = new AwaitingCallStatus();
    public static readonly OrderStatus Completed = new CompletedStatus();
    public static readonly OrderStatus Confirmed = new ConfirmedStatus();
    public static readonly OrderStatus Damaged = new DamagedStatus();
    public static readonly OrderStatus Delivered = new DeliveredStatus();
    public static readonly OrderStatus Delivering = new DeliveringStatus();
    public static readonly OrderStatus DeliveryCancelled = new DeliveryCancelledStatus();
    public static readonly OrderStatus DeliveryFailed = new DeliveryFailedStatus();
    public static readonly OrderStatus DeliveryPicking = new DeliveryPickingStatus();
    public static readonly OrderStatus Lacked = new LackedStatus();
    public static readonly OrderStatus Linehaul = new LinehaulStatus();
    public static readonly OrderStatus Missed = new MissedStatus();
    public static readonly OrderStatus New = new NewStatus();
    public static readonly OrderStatus OutOfStock = new OutOfStockStatus();
    public static readonly OrderStatus Packed = new PackedStatus();
    public static readonly OrderStatus Picked = new PickedStatus();
    public static readonly OrderStatus Picking = new PickingStatus();
    public static readonly OrderStatus Returned = new ReturnedStatus();
    public static readonly OrderStatus Splitted = new SplittedStatus();
    public static readonly OrderStatus WaitingForReturn = new WaitingForReturnStatus();
    public static readonly OrderStatus Cancelled = new CancelledStatus();
    public static readonly OrderStatus Shipped = new ShippedStatus();
    public static readonly OrderStatus Held = new HeldStatus();

    public static List<OrderStatus> GetAll()
    {
        return [AwaitingCall,
                Completed,
                Confirmed,
                Damaged,
                Delivered,
                Delivering,
                DeliveryCancelled,
                DeliveryFailed,
                DeliveryPicking,
                Lacked,
                Linehaul,
                Missed,
                New,
                OutOfStock,
                Packed,
                Picked,
                Picking,
                Returned,
                Splitted,
                WaitingForReturn,
                Cancelled,
                Shipped,
                Held];
    }

    private OrderStatus(string name, int value, string label, string labelType, List<string> allowedStatuses, List<OrderAction> allowedActions) : base(name, value)
    {
        AllowedStatuses = allowedStatuses;
        Label = label;
        LabelType = labelType;
        AllowedActions = allowedActions;
    }

    public virtual bool CanTransitionTo(OrderStatus next)
    {
        return AllowedStatuses.Contains(next);
    }

    public virtual bool CanDoAction(OrderAction action)
    {
        return AllowedActions.Contains(action);
    }

    public string Label { get; }
    public string LabelType { get; }
    public List<string> AllowedStatuses { get; }
    public List<OrderAction> AllowedActions { get; }

    private sealed class AwaitingCallStatus : OrderStatus
    {
        public AwaitingCallStatus() : base(_awaitingCall, 1, "Chờ liên hệ", _processing, [_outOfStock, _confirmed, _cancelled], [OrderAction.Confirm, OrderAction.Cancel])
        {
        }
    }

    private sealed class CompletedStatus : OrderStatus
    {
        public CompletedStatus() : base(_completed, 2, "Hoàn thành", _success, [], [])
        {
        }
    }
    private sealed class ConfirmedStatus : OrderStatus
    {
        public ConfirmedStatus() : base(_confirmed, 3, "Đã xác nhận", _success, [_picking, _packed, _held, _splitted, _lacked, _awaitingCall, _outOfStock, _cancelled], [OrderAction.Cancel, OrderAction.Pick])
        {
        }
    }

    private sealed class DamagedStatus : OrderStatus
    {
        public DamagedStatus() : base(_damaged, 4, "Hư hỏng hàng hóa", _error, [], [])
        {
        }
    }

    private sealed class DeliveredStatus : OrderStatus
    {
        public DeliveredStatus() : base(_delivered, 5, "Giao hàng thành công", _success, [_completed], [])
        {
        }
    }
    private sealed class DeliveringStatus : OrderStatus
    {
        public DeliveringStatus() : base(_delivering, 6, "Đang vận chuyển", _processing, [_waitingForReturn, _deliveryFailed, _delivered, _damaged, _missed], [])
        {
        }
    }
    private sealed class DeliveryCancelledStatus : OrderStatus
    {
        public DeliveryCancelledStatus() : base(_deliveryCancelled, 7, "Huỷ vận đơn", _warning, [_cancelled], [OrderAction.Cancel])
        {
        }
    }

    private sealed class DeliveryFailedStatus : OrderStatus
    {
        public DeliveryFailedStatus() : base(_deliveryFailed, 8, "Giao hàng thất bại", _error, [_waitingForReturn, _returned, _delivering, _deliveryFailed, _damaged, _missed], [])
        {
        }
    }

    private sealed class DeliveryPickingStatus : OrderStatus
    {
        public DeliveryPickingStatus() : base(_deliveryPicking, 9, "Vận chuyển đang lấy hàng", _processing, [_delivering, _deliveryCancelled, _shipped], [OrderAction.Deliver])
        {
        }
    }
    private sealed class LackedStatus : OrderStatus
    {
        public LackedStatus() : base(_lacked, 10, "Đơn thiếu hàng", _warning, [_linehaul, _cancelled, _awaitingCall, _outOfStock, _splitted], [OrderAction.Cancel])
        {
        }
    }

    private sealed class LinehaulStatus : OrderStatus
    {
        public LinehaulStatus() : base(_linehaul, 11, "Đang luân chuyển", _processing, [_picked, _packed, _cancelled], [OrderAction.Cancel])
        {
        }
    }

    private sealed class MissedStatus : OrderStatus
    {
        public MissedStatus() : base(_missed, 12, "Thất lạc hàng hóa", _error, [], [])
        {
        }
    }

    private sealed class NewStatus : OrderStatus
    {
        public NewStatus() : base(_new, 13, "Đã tiếp nhận", _warning, [_completed, _delivering, _splitted, _outOfStock, _awaitingCall, _cancelled], [OrderAction.Confirm, OrderAction.Cancel])
        {
        }
    }

    private sealed class OutOfStockStatus : OrderStatus
    {
        public OutOfStockStatus() : base(_outOfStock, 14, "Hết hàng", _warning, [_cancelled], [OrderAction.Cancel])
        {
        }
    }

    private sealed class PackedStatus : OrderStatus
    {
        public PackedStatus() : base(_packed, 15, "Đóng gói xong", _success, [_shipped, _packed, _delivering, _deliveryPicking, _cancelled], [OrderAction.Cancel])
        {
        }
    }

    private sealed class PickedStatus : OrderStatus
    {
        public PickedStatus() : base(_picked, 16, "Đã lấy hàng", _success, [_picked, _packed, _picking, _awaitingCall, _outOfStock, _splitted, _cancelled], [OrderAction.Cancel, OrderAction.Pack])
        {
        }
    }

    private sealed class PickingStatus : OrderStatus
    {
        public PickingStatus() : base(_picking, 17, "Đang lấy hàng", _processing, [_picked, _lacked], [])
        {
        }
    }

    private sealed class ReturnedStatus : OrderStatus
    {
        public ReturnedStatus() : base(_returned, 18, "Đã hoàn hàng", _success, [], [])
        {
        }
    }

    private sealed class SplittedStatus : OrderStatus
    {
        public SplittedStatus() : base(_splitted, 19, "Đã tách", _success, [], [])
        {
        }
    }

    private sealed class WaitingForReturnStatus : OrderStatus
    {
        public WaitingForReturnStatus() : base(_waitingForReturn, 20, "Chờ hoàn hàng", _warning, [_returned, _damaged, _missed, _waitingForReturn], [])
        {
        }
    }

    private sealed class CancelledStatus : OrderStatus
    {
        public CancelledStatus() : base(_cancelled, 21, "Đã hủy", _error, [], [])
        {
        }
    }
    private sealed class ShippedStatus : OrderStatus
    {
        public ShippedStatus() : base(_shipped, 22, "Bàn giao vận chuyển", _success, [_delivering, _damaged, _missed], [])
        {
        }
    }

    private sealed class HeldStatus : OrderStatus
    {
        public HeldStatus() : base(_held, 23, "Đang treo", _warning, [], [])
        {
        }
    }

}
// using Microsoft.EntityFrameworkCore;
//
// namespace ScaleUp.Core.Persistence.Context;
//
// public sealed class ReadOnlyRawDataContext
// {
//     private readonly RawDataContext _context;
//
//     public ReadOnlyRawDataContext(RawDataContext context)
//     {
//         _context = context;
//         _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//     }
// }
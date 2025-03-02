using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Booking entity)
        {
            _db.Update(entity);
        }

        public void UpdateStatus(int bookingId, string bookingStatus, int villaNumber = 0)
        {
            var bookingFromDb = _db.Bookings.FirstOrDefault(tmp => tmp.Id == bookingId);
            if (bookingFromDb != null)
            {
                bookingFromDb.Status = bookingStatus;
                if (bookingStatus == SD.StatusCheckedIn)
                {
                    bookingFromDb.VillaNumber = villaNumber;
                    bookingFromDb.ActualCheckInDate = DateTime.Now;
                }
                if (bookingStatus == SD.StatusCompleted)
                {
                    bookingFromDb.ActualCheckOutDate = DateTime.Now;
                }
            }
        }

        public void UpdateStripePaymentId(int bookingId, string sessionId, string paymentIntendId)
        {
            var bookingFromDb = _db.Bookings.FirstOrDefault(tmp => tmp.Id == bookingId);
            if (bookingFromDb != null)
            {
                if (!string.IsNullOrEmpty(sessionId))
                {
                    bookingFromDb.StripeSessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntendId))
                {
                    bookingFromDb.StripePaymentIntentId = paymentIntendId;
                    bookingFromDb.IsPaymentSuccessful = true;
                    bookingFromDb.PaymentDate = DateTime.Now;
                }
            }
        }
    }
}

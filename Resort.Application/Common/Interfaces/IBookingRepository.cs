﻿using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Application.Common.Interfaces
{
    public interface IBookingRepository:IRepository<Booking>
    {
        void Update(Booking entity);
        void UpdateStatus(int bookingId, string bookingStatus);
        void UpdateStripePaymentId(int bookingId    , string sessionId, string paymentIntendId);
    }
}

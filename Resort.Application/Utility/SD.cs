using Microsoft.EntityFrameworkCore.Storage.Internal;
using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Application.Utility;

public static class SD
{
    public const string Role_Admin = "Admin";
    public const string Role_Customer = "Customer";

    public const string StatusPending = "Pending";
    public const string StatusApproved = "Approved";
    public const string StatusRefunded = "Refunded";
    public const string StatusCompleted = "Completed";
    public const string StatusCancelled = "Cancelled";
    public const string StatusCheckedIn = "CheckedIn";

    public static int VillaRoomsAvailable_Count (int VillaId, List<VillaNumber> villaNumbersList, 
        DateOnly checkInDate, int nights, List<Booking> bookings)
    {
        List<int> bookingInDate = new List<int>();
        int finalAvailableRoomsForAllNights = int.MaxValue;
        var roomsInVilla = villaNumbersList.Where(tmp=>tmp.VillaId == VillaId).Count();
        for(int i = 0; i < nights; i++)
        {
            var villasBooked = bookings.Where(tmp => tmp.CheckInDate == checkInDate.AddDays(i)
            &&
            tmp.CheckOutDate > checkInDate.AddDays(i) && tmp.VillaId == VillaId);
            foreach (var item in villasBooked)
            {
                if (!bookingInDate.Contains(item.Id))
                {
                    bookingInDate.Add(item.Id);
                }
            }
            var totalAvailableRooms = roomsInVilla - bookingInDate.Count();
            if(totalAvailableRooms == 0)
            {
                return 0;
            }
            else
            {
                if(finalAvailableRoomsForAllNights > totalAvailableRooms)
                {
                    finalAvailableRoomsForAllNights = totalAvailableRooms;
                }
            }
        }
        return finalAvailableRoomsForAllNights;
    }
}

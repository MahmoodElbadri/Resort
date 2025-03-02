using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities;

public class Booking
{
    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public ApplicationUser User { get; set; }
    [Required]
    [ForeignKey("Villa")]
    public int VillaId { get; set; }
    public Villa? Villa { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    public string? Phone { get; set; }
    [Required]
    public double TotalCost { get; set; }
    public int Nights { get; set; }
    public string? Status { get; set; }
    [Required]
    public DateTime BookingDate { get; set; }
    [Required]
    public DateOnly  CheckInDate { get; set; }
    [Required]
    public DateOnly  CheckOutDate { get; set; }
    public bool IsPaymentSuccessful { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? StripeSessionId { get; set; }
    public string? StripePaymentIntentId { get; set; }
    public DateTime ActualCheckOutDate { get; set; }
    public DateTime ActualCheckInDate { get; set; }
    public int VillaNumber { get; set; }
    [NotMapped]
    public List<VillaNumber> VillaNumbers { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace AksicDavid_LB_M295.Models
{
    public class BitcoinHolding
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Die Menge darf nicht leer sein.")]
        [Range(0.0000001, double.MaxValue, ErrorMessage = "Die Menge muss größer als 0 sein.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Der Preis beim Kauf ist erforderlich.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Der Preis muss positiv sein.")]
        public double PriceAtPurchase { get; set; }

        [Required(ErrorMessage = "Das Kaufdatum ist erforderlich.")]
        public DateTime PurchaseDate { get; set; }
    }
}

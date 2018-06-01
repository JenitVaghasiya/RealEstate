namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemRental")]
    public class ItemRental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Item Rental Id is required")]
        [Display(Name = "Item Rental Id")]
        public int ItemRentalId { get; set; }

        [Required(ErrorMessage = "Item Id is required")]
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Tenant Id is required")]
        [Display(Name = "Tenant Id")]
        public int TenantId { get; set; }

        [Display(Name = "Quantity Rented")]
        public int? QuantityRented { get; set; }

        [Display(Name = "Monthly Rent")]
        public decimal? MonthlyRent { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Rental Duration")]
        public string RentalDuration { get; set; }

        [Display(Name = "Total Charge")]
        public decimal? TotalCharge { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
    }
}

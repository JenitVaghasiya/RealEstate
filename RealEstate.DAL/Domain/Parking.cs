namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Parking")]
    public class Parking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Parking Id is required")]
        [Display(Name = "Parking Id")]
        public int ParkingId { get; set; }
        [Required(ErrorMessage = "Parking Type Id is required")]
        [Display(Name = "Parking Type Id")]
        public int ParkingTypeId { get; set; }
        [Required(ErrorMessage = "Appartment Id is required")]
        [Display(Name = "Appartment Id")]
        public int AppartmentId { get; set; }

        [ForeignKey("ParkingTypeId")]
        public ParkingType ParkingType { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }
    }
}

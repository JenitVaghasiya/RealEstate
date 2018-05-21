namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ParkingType")]
    public class ParkingType
    {
        public ParkingType()
        {
            this.Parkings = new List<Parking>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Parking Type Id is required")]
        [Display(Name = "Parking Type Id")]
        public int ParkingTypeId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Parking Type Name")]
        public string ParkingTypeName { get; set; }

        public List<Parking> Parkings { get; set; }
    }   
}

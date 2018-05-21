namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Apartment")]
    public class Apartment
    {
        public Apartment()
        {
            Parkings = new List<Parking>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Apartment Id is required")]
        [Display(Name = "Apartment Id")]
        public int ApartmentId { get; set; }

        [Display(Name = "Apartment Unit Id")]
        public int? ApartmentUnitId { get; set; }

        [Required(ErrorMessage = "Branch Id is required")]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public List<Parking> Parkings { get; set; }
    }
}

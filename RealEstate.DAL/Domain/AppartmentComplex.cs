namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AppartmentComplex")]
    public class AppartmentComplex
    {
        public AppartmentComplex()
        {
            this.Branches = new List<Branch>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Apartment Complex Id is required")]
        [Display(Name = "Apartment Complex Id")]
        public int ApartmentComplexId { get; set; }
        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Apartment Complex Name")]
        public string ApartmentComplexName { get; set; }

        public List<Branch> Branches { get; set; }
    }
}

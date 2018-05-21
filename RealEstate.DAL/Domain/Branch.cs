namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Branch")]
    public class Branch
    {
        public Branch()
        {
            Apartments = new List<Apartment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Branch Id is required")]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Branch Email")]
        public string BranchEmail { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Branch Contact No")]
        public string BranchContactNo { get; set; }

        [Required(ErrorMessage = "Apartment Complex Id is required")]
        [Display(Name = "Apartment Complex Id")]
        public int ApartmentComplexId { get; set; }

        [ForeignKey("ApartmentComplexId")]
        public AppartmentComplex AppartmentComplex { get; set; }

        public List<Apartment> Apartments { get; set; }
    }
}

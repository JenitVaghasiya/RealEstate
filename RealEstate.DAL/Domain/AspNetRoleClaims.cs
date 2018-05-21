namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetRoleClaims")]
    public class AspNetRoleClaims
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [MaxLength]
        [Display(Name = "Claim Type")]
        public string ClaimType { get; set; }
        [MaxLength]
        [Display(Name = "Claim Value")]
        public string ClaimValue { get; set; }
        
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Role Id is required")]
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }

        [ForeignKey("Id")]
        public AspNetRoles AspNetRole { get; set; }
    }
}

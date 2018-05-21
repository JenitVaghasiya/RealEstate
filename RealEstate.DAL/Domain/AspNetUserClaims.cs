namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUserClaims")]
    public class AspNetUserClaims
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
        [Required(ErrorMessage = "User Id is required")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [ForeignKey("Id")]
        public AspNetUsers AspNetUser { get; set; }
    }
}

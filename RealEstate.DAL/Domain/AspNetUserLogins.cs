namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUserLogins")]
    public class AspNetUserLogins
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Login Provider is required")]
        [Display(Name = "Login Provider")]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Provider Key is required")]
        [Display(Name = "Provider Key")]
        public string ProviderKey { get; set; }

        [MaxLength]
        [Display(Name = "Provider Display Name")]
        public string ProviderDisplayName { get; set; }

        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "User Id is required")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [ForeignKey("Id")]
        public AspNetUsers AspNetUser { get; set; }
    }
}

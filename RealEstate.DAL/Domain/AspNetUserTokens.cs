namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUserTokens")]
    public class AspNetUserTokens
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "User Id is required")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Login Provider is required")]
        [Display(Name = "Login Provider")]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 3)]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength]
        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}

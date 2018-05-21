namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Key")]
    public class Key
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Key Id is required")]
        [Display(Name = "Key Id")]
        public int KeyId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Key Brand")]
        public string KeyBrand { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Key Purpose")]
        public string KeyPurpose { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Key Serial Id")]
        public string KeySerialId { get; set; }
    }
}

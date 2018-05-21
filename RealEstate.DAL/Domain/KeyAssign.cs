namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KeyAssign")]
    public class KeyAssign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Key Assign Id is required")]
        [Display(Name = "Key Assign Id")]
        public int KeyAssignId { get; set; }
        [Required(ErrorMessage = "Key Id is required")]
        [Display(Name = "Key Id")]
        public int KeyId { get; set; }
        [Required(ErrorMessage = "Apartment Id is required")]
        [Display(Name = "Apartment Id")]
        public int ApartmentId { get; set; }
    }
}

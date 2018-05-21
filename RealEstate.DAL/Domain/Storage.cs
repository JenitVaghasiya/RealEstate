namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Storage")]
    public class Storage
    {
        public Storage()
        {
            this.Inventories = new List<Inventory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Storage Id is required")]
        [Display(Name = "Storage Id")]
        public int StorageId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Storage Location")]
        public string StorageLocation { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Storage Capacity")]
        public string StorageCapacity { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Storage Phone No")]
        public string StoragePhoneNo { get; set; }
        [MaxLength(150)]
        [StringLength(150)]
        [Display(Name = "Storage Address")]
        public string StorageAddress { get; set; }

        public List<Inventory> Inventories { get; set; }
    }
}

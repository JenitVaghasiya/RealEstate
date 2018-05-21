namespace RealEstate.DAL.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Inventory Id is required")]
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        [Required(ErrorMessage = "Item Id is required")]
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }
        [Display(Name = "Item Quantity")]
        public long? ItemQuantity { get; set; }
        [Required(ErrorMessage = "Storage Id is required")]
        [Display(Name = "Storage Id")]
        public int StorageId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        [ForeignKey("StorageId")]
        public Storage Storage { get; set; }
    }
}

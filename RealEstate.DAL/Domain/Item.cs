namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Item")]
    public class Item
    {
        public Item()
        {
            Inventories = new List<Inventory>();
            ItemRentals = new List<ItemRental>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Item Brand")]
        public string ItemBrand { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Item Model Id")]
        public string ItemModelId { get; set; }

        [Display(Name = "Item Price")]
        public decimal? ItemPrice { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        public List<Inventory> Inventories { get; set; }

        public List<ItemRental> ItemRentals { get; set; }
    }
}

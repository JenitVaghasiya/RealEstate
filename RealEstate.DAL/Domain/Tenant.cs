namespace RealEstate.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tenant")]
    public class Tenant
    {
        public Tenant()
        {
            this.ItemRentals = new List<ItemRental>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Tenant Id is required")]
        [Display(Name = "Tenant Id")]
        public int TenantId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }
        [Display(Name = "Tenant DOB")]
        public DateTime? TenantDOB { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Tenant Email")]
        public string TenantEmail { get; set; }
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Tenant Phone No")]
        public string TenantPhoneNo { get; set; }

        public List<ItemRental> ItemRentals { get; set; }
    }
}

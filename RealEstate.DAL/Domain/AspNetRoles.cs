namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetRoles")]
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new List<AspNetRoleClaims>();
            AspNetUsers = new List<AspNetUserRoles>();
        }

        [Key]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxLength]
        [Display(Name = "Concurrency Stamp")]
        public string ConcurrencyStamp { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Normalized Name")]
        public string NormalizedName { get; set; }

        public List<AspNetRoleClaims> AspNetRoleClaims { get; set; }

        public List<AspNetUserRoles> AspNetUsers { get; set; }
    }
}

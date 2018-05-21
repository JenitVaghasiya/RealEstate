namespace RealEstate.DAL.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUserRoles")]
    public class AspNetUserRoles
    {
        public AspNetUserRoles()
        {
            AspNetRoles = new List<AspNetRoles>();
            AspNetUsers = new List<AspNetUsers>();
        }

        [Key]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Key]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "RoleId")]
        public string RoleId { get; set; }

        public List<AspNetRoles> AspNetRoles { get; set; }

        public List<AspNetUsers> AspNetUsers { get; set; }
    }
}

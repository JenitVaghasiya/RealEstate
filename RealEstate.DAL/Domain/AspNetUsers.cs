namespace RealEstate.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AspNetUsers")]
    public class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new List<AspNetUserClaims>();
            AspNetUserLogins = new List<AspNetUserLogins>();
            AspNetRoles = new List<AspNetUserRoles>();
        }

        [Key]
        [MaxLength(450)]
        [StringLength(450)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Access Failed Count is required")]
        [Display(Name = "Access Failed Count")]
        public int AccessFailedCount { get; set; }

        [MaxLength]
        [Display(Name = "Concurrency Stamp")]
        public string ConcurrencyStamp { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email Confirmed is required")]
        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "Lockout Enabled is required")]
        [Display(Name = "Lockout Enabled")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Lockout End")]
        public DateTime? LockoutEnd { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Normalized Email")]
        public string NormalizedEmail { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Normalized User Name")]
        public string NormalizedUserName { get; set; }

        [MaxLength]
        [Display(Name = "Password Hash")]
        public string PasswordHash { get; set; }

        [MaxLength]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone Number Confirmed is required")]
        [Display(Name = "Phone Number Confirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [MaxLength]
        [Display(Name = "Security Stamp")]
        public string SecurityStamp { get; set; }

        [Required(ErrorMessage = "Two Factor Enabled is required")]
        [Display(Name = "Two Factor Enabled")]
        public bool TwoFactorEnabled { get; set; }

        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public List<AspNetUserClaims> AspNetUserClaims { get; set; }

        public List<AspNetUserLogins> AspNetUserLogins { get; set; }

        public List<AspNetUserRoles> AspNetRoles { get; set; }
    }
}

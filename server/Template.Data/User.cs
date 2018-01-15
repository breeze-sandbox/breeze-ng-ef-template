namespace Template.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [NotMapped]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "money")]
        public decimal RowVersion { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public long ModifiedByUserId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

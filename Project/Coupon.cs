namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coupon")]
    public partial class Coupon
    {
        [Key]
        public int Coupon_Id { get; set; }

        public string C_Name { get; set; }

        public string C_Store { get; set; }

        public string Expire { get; set; }

        public string Des { get; set; }

        public string Url { get; set; }
    }
}

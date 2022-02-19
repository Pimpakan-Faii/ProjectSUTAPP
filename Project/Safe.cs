namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Safe")]
    public partial class Safe
    {
        [Key]
        public int Safe_id { get; set; }

        public string Safe_name { get; set; }

        public string Safe_img { get; set; }
    }
}

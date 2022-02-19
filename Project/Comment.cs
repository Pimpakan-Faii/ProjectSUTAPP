namespace Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        public int? Store_Id { get; set; }

        public string Text { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public virtual Store Store { get; set; }
    }
}

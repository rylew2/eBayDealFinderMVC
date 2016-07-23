namespace eBayDealFinder.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        public int AlbumId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int MyProperty { get; set; }
    }
}

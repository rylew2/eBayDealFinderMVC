using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eBayDealFinder.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int MyProperty { get; set; }

    

    }


}
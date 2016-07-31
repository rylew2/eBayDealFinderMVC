using eBayDealFinder.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eBayDealFinder
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ss2012", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //EbayData is used as an intermediate object and does not need to be stored
       // public DbSet<EbayData> EbayData { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public System.Data.Entity.DbSet<eBayDealFinder.Models.Album> Albums { get; set; }
    }
}
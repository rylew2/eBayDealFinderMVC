using eBayDealFinder.Abstract;
using eBayDealFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBayDealFinder.Concrete
{
    public class ApplicationRepository: IRepository
    {
        //Concrete

        private ApplicationDbContext context = new ApplicationDbContext();
        
        public IEnumerable<EbayData> ApplicationDbContext
        {
            get { return context.EbayData;  }
        }

        //Create CRUD operations with UoW pattern
        // Save context at end

    }
}
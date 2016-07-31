using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eBayDealFinder.Models
{
    public class Deal
    {

        public int DealID { get; set; }

        [DisplayName("MyTitle")]
        public string title { get; set; }
        public double price { get; set; }
        public DateTimeOffset pub { get; set; }
        public double compEbayPrice { get; set; }
        public string dealURL { get; set; }


        //Constructors
        public Deal(string title, double price)
        {
            this.title = title;
            this.price = price;
        }

        public Deal(string title, double price, DateTimeOffset pub, string dealURL)
        {
            this.title = title;
            this.price = price;
            this.pub = pub;
            this.dealURL = dealURL;
        }

        public Deal(string title, double price, double compEbayPrice)
        {
            this.title = title;
            this.price = price;
            this.compEbayPrice = compEbayPrice;
        }

        public Deal(string title, double price, double compEbayPrice, string dealURL)
        {
            this.title = title;
            this.price = price;
            this.compEbayPrice = compEbayPrice;
            this.dealURL = dealURL;
        }


    }

    public class DealData
    {
    
        public List<Deal> deals { get; set; }

        public DealData() { }
        public DealData(List<Deal> d)
        {
            this.deals = d;
        }

        public int getCount()
        {
            return deals.Count;
        }

        public string getTitle(int i)
        {
            return deals[i].title;
        }

        public double getPrice(int i)
        {
            return deals[i].price;
        }


    }
}

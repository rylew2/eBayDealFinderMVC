using System;
using System.Collections.Generic;

namespace eBayDealFinder.Models
{

    public class Info
    {
        public int start { get; set; }
        public int total_results { get; set; }
    }


    public class BestPage
    {
        public object page_not_found { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string price_confidence { get; set; }
        public double pnp { get; set; }
        public string retailer_name { get; set; }
        public bool in_stock { get; set; }
        public string deeplink { get; set; }
        public object non_product_page { get; set; }
        public string image_url { get; set; }
        public string live_price_url { get; set; }
        public string title { get; set; }
        public string region { get; set; }
        public string currency { get; set; }
        public List<object> category_parents { get; set; }
        public object empty_scrape { get; set; }
        public string original_url { get; set; }
        public double price { get; set; }
        public bool category_is_classified { get; set; }
        public object dead_end { get; set; }
    }


    public class MyIH
    {
        public double totalprice { get; set; }
        public string deeplink { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public List<Object> categories;

        //Fill with constructor
        public MyIH(Result r)
        {
            this.totalprice = r.best_page.price + r.best_page.pnp;
            this.deeplink = r.best_page.deeplink;
            this.title = r.best_page.title;
            this.categories = r.categories;
        }
    }

    public class Result
    {
        public object normalized_url { get; set; }
        public List<object> isbns { get; set; }
        public int number_of_pages { get; set; }
        public BestPage best_page { get; set; }
        public List<object> asins { get; set; }
        public List<object> mpns { get; set; }
        public List<object> eans { get; set; }
        public string image_url { get; set; }
        public string title { get; set; }
        public List<object> upcs { get; set; }
        public List<object> ebay_pages { get; set; }
        public List<object> models { get; set; }
        public List<object> categories { get; set; }
        public string id { get; set; }
        public string resource { get; set; }
        public List<object> brands { get; set; }
    }

    public class IHand
    {
        public List<object> errors { get; set; }
        public Info info { get; set; }
        public List<Result> results { get; set; }
    }



}

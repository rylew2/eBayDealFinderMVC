using System.Collections.Generic;

namespace eBayDealFinder.Models
{

    // https://pipes.yahoo.com/pipes/pipe.info?_id=MNyp_zvL2xGa2vJmmLokhQ
    
    public class YPublished
    {
        public string hour { get; set; }
        public string timezone { get; set; }
        public string second { get; set; }
        public string month { get; set; }
        public string month_name { get; set; }
        public string minute { get; set; }
        public string utime { get; set; }
        public string day { get; set; }
        public string day_ordinal_suffix { get; set; }
        public string day_of_week { get; set; }
        public string day_name { get; set; }
        public string year { get; set; }
    }

    public class YId
    {
        public string permalink { get; set; }
        public string value { get; set; }
    }

    public class Enclosure
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public object author { get; set; }
        public string pubDate { get; set; }
        public object guid { get; set; }
        public string vendorName { get; set; }
        public object category { get; set; }
        public string imageLink { get; set; }
        /*    public YPublished __invalid_name__y:published { get; set; }
            public YId __invalid_name__y:id { get; set; }
            public string __invalid_name__y:title { get; set; }
            public string hot { get; set; }
            public string comments { get; set; }
            public string __invalid_name__dc:creator { get; set; }
            public string __invalid_name__content:encoded { get; set; }
            public string __invalid_name__wfw:commentRss { get; set; }
            public string __invalid_name__feedburner:origLink { get; set; }
            public string __invalid_name__a10:updated { get; set; }
            public Enclosure enclosure { get; set; }
         * */
    }

    public class Value
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string pubDate { get; set; }
        public string generator { get; set; }
        public string callback { get; set; }
        public List<Item> items { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public Value value { get; set; }
    }
}

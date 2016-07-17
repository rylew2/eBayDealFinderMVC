﻿using eBayDealFinder.Models;
using System;
using System.Web.Mvc;
using eBayDealFinder.Utility;
using System.Xml;
using System.ServiceModel.Syndication;
using eBayDealFinder.DealClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace eBayDealFinder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
    
                compareDealsToEbay(getDealRSS());

   
          
            
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }





        //Compare each deal to ebay
        public void compareDealsToEbay(DealData dd)
        {
            DealData anotherdd = new DealData(new List<Deal>());
            for (int i = 0; i < dd.getCount(); i++)
            {
                string search = dd.getTitle(i);
                // EbayData[] ed = getEbayCompletedItems("findCompletedItems", search);

                EbayDatum ed = new EbayDatum();
                ed.getEbayCompletedItems("findCompletedItems", search);

                if (ed.getNumberOfResults() >= 1)
                {

                    ed.useMajorEbayCategory();
                    double avgEbayPrice = double.Parse(ed.getAvgPrice());
                    var dealPrice = dd.getPrice(i);
                    var dealTitle = dd.getTitle(i);
                    if ((avgEbayPrice - dealPrice) > 0)
                    {
                        //     DateTimeOffset o = ed[i].   item.PublishDate;
                        //      DateTimeOffset dt = o.Add(new TimeSpan(-2, 0, 0));

                        Deal d = new Deal(dealTitle, dealPrice, avgEbayPrice, dd.deals[i].dealURL);
                        anotherdd.deals.Add(d);
                    }
                }
            }
           var x = 2;
        }


        public DealData getDealRSS()
        {
            string url = "http://feeds.feedburner.com/Dealsucker?format=xml";

            // string url = "http://rss.steepandcheap.com/docs/steepcheap/rss.xml";

            //RSS feed date issue: http://stackoverflow.com/questions/8891047/exceptions-with-datetime-parsing-in-rss-feed-use-syndicationfeed-in-c-sharp
            XmlReader r = new MyXmlReader(url);
            SyndicationFeed feed = SyndicationFeed.Load(r);
            r.Close();

            DealData dd = new DealData(new List<Deal>());

            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
                string dealURL = item.Id;
                string s = "";
                if (subject.Contains('$'))
                {
                    s = Regex.Match(subject, "[0-9]+.[0-9]+").Value;
                    if (subject.Contains(']'))
                    {
                        int start = subject.IndexOf(']') + 2;
                        int end = subject.IndexOf("by");

                        subject = subject.Substring(start, end - start);
                    }
                }

                DateTimeOffset o = item.PublishDate;
                DateTimeOffset dt = o.Add(new TimeSpan(-2, 0, 0));

                //If we have a deal and its price is more than $40
                if (s != "" && double.Parse(s) > 40.0)
                {
                    Deal d = new Deal(subject, double.Parse(s), dt, dealURL);
                    dd.deals.Add(d);
                }
            }
            return dd;
        }

        public DealData SACDeals()
        {
            string url = "http://rss.steepandcheap.com/docs/steepcheap/rss.xml";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            DealData dd = new DealData(new List<Deal>());
            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
                string dealURL = item.Id;
                string s = "";
                if (summary.Contains('$'))
                {
                    s = Regex.Match(summary, "Price: \\$[0-9]+.[0-9]+").Value;
                    s = s.Substring(8, s.Length - 8);
                }

                DateTimeOffset o = item.PublishDate;
                DateTimeOffset dt = o.Add(new TimeSpan(-2, 0, 0));

                //If we have a deal and its price is more than $40
                if (s != "" && double.Parse(s) > 40.0)
                {
                    Deal d = new Deal(subject, double.Parse(s), dt, dealURL);
                    dd.deals.Add(d);
                }
            }
            return dd;
        }









    }
}
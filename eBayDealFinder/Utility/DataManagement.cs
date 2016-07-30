using eBayDealFinder.DealClasses;
using eBayDealFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace eBayDealFinder.Utility
{
    public  class DataManagement
    {


        //Compare each deal to ebay
        public static void compareDealsToEbay(DealData dd)
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

         /* public void button1_Click(object sender, EventArgs e)
        {
            string search = textBox2.Text;

            EbayDatum ed = new EbayDatum();
            ed.getEbayCompletedItems("findCompletedItems", textBox2.Text);

            ed.useMajorEbayCategory();

            dataGridView1.DataSource = new SortableBindingList<EbayData>(ed.ed);
            ed.setResultStats(textBox3, textBox1);
            ed.setEbayLinks(dataGridView1);
        }

        public void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        public void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

      public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex
            if (e.ColumnIndex == 0)
            {
                string sUrl = dataGridView1.Rows[e.RowIndex].Cells[0].Tag.ToString();
                ProcessStartInfo sInfo = new ProcessStartInfo(sUrl);
                Process.Start(sInfo);
            }
        }

        public void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && dataGridView2.Rows[e.RowIndex].Cells[0].Tag != null)
            {
                string sUrl = dataGridView2.Rows[e.RowIndex].Cells[0].Tag.ToString();
                ProcessStartInfo sInfo = new ProcessStartInfo(sUrl);
                Process.Start(sInfo);
            }
        }

        //Get Deal Aggregator RSS 
        public void button2_Click(object sender, EventArgs e)
        {
            DealData dd = getDealRSS();
            //  DealData dd = SACDeals();
            compareDealsToEbay(dd);
        }*/

        public static DealData getDealRSS()
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

        public static DealData SACDeals()
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
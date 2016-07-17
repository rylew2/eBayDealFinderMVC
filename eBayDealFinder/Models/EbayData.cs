using eBayDealFinder.DealClasses;
using eBayDealFinder.ebaySR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;


namespace eBayDealFinder.Models
{
    //Represents attributes for 1 ebay product result
    public class EbayData
    {
        private FindCompletedItemsResponse response;
        private int i;

        public string title { get; set; }
        public string id { get; set; }
        //   public string globalID { get; set; } //ebay us

        //selling status
        public int bidCount { get; set; }
        public string sellingState { get; set; }
        public double endPrice { get; set; }

        //Condition
        public int condID { get; set; }                  //1000=new, 1500=new other see details, 1750=new with defects, 2000 =refurbished
        public string condDisplayName { get; set; }

        //Listing info
        public string listingType { get; set; }
        public DateTime endTime { get; set; }
        //public double buyItNowPrice { get; set; } //usually not populated for our purposes

        //category
        public string categoryID { get; set; }
        public string categoryName { get; set; }

        public string ebayURL { get; set; }


        public EbayData(FindCompletedItemsResponse response, int i)
        {
            // TODO: Complete member initialization
            this.response = response;
            this.i = i;

            this.id = response.searchResult.item[i].itemId;
            this.title = response.searchResult.item[i].title;

            this.sellingState = response.searchResult.item[i].sellingStatus.sellingState;
            this.bidCount = response.searchResult.item[i].sellingStatus.bidCount;
            this.endPrice = response.searchResult.item[i].sellingStatus.currentPrice.Value;

            this.condDisplayName = response.searchResult.item[i].condition.conditionDisplayName;
            this.condID = response.searchResult.item[i].condition.conditionId;

            this.endTime = response.searchResult.item[i].listingInfo.endTime;
            this.listingType = response.searchResult.item[i].listingInfo.listingType;

            this.categoryID = response.searchResult.item[i].primaryCategory.categoryId;
            this.categoryName = response.searchResult.item[i].primaryCategory.categoryName;

            this.ebayURL = response.searchResult.item[i].viewItemURL;
        }

    }

    public class EbayDatum
    {

        //Production                             
        const string ebayDevID = Credentials.ebayDevID;
        const string ebayAppID = Credentials.ebayAppID;
        const string ebayCertID = Credentials.ebayCertID;
        const string ebayAuthToken = Credentials.ebayAuthToken;

        public EbayData[] ed { get; set; }

        //Initialize with array
        public EbayDatum(EbayData[] edp) { ed = edp; }

        //Blank initializer
        public EbayDatum() { ed = new EbayData[0]; }

        public EbayData[] getED()
        {
            return ed;
        }

        public int getLength()
        {
            return ed.Length;
        }

        public void useMajorEbayCategory()
        {
            //Pick majority category and delete other categories
            var d = new ConcurrentDictionary<string, int>();

            for (int i = 0; i < ed.Length; i++)
            {
                if (ed[i].categoryName != null)
                {
                    string s = ed[i].categoryName;
                    d.AddOrUpdate(s, 1, (t, count) => count + 1);
                }
            }
            List<EbayData> e = new List<EbayData>();
            if (!d.IsEmpty)
            {
                int categoryCount = d.Values.Max();
                var majorCategory = d.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;


                for (int i = 0; i < ed.Length; i++)
                {
                    if (ed[i].categoryName == majorCategory)
                    {
                        e.Add(ed[i]);
                    }
                }
            }
            //dataGridView1.DataSource = new SortableBindingList<EbayData>(e.ToArray());
            this.ed = e.ToArray();
        }

        public int getNumberOfResults()
        {
            //return ed.Length == 0 ? 0 : ed.Length!=null?ed.Length:0    ;

            return ed.Length == 0 ? 0 : ed.Length;
        }

        public string getAvgPrice()
        {
            double avgPrice = 0, i = 0;
            //ed.Count<EbayData>;
            for (int j = 0; j < ed.Length; j++)
            {
                if (ed[j] != null)
                {
                    avgPrice = avgPrice + ed[j].endPrice;
                    i++;
                }
            }
            return (avgPrice / i).ToString();
        }

       /* public void setResultStats(System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox textBox3, System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox textBox1)
        {
            textBox3.Text = "No of Results:" + getNumberOfResults();
            textBox1.Text = "Average:  " + getAvgPrice();
        }*/

        public void setEbayLinks(System.Windows.Forms.DataGridView dgv)
        {
            DataGridViewRow r;
            for (int j = 0; j < ed.Length; j++)
            {

                r = dgv.Rows[j];
                DataGridViewLinkCell lc = new DataGridViewLinkCell();
                lc.Value = "http://www.ebay.com/itm/" + ed[j].id.ToString();
                lc.LinkColor = Color.Blue;
                lc.Tag = lc.Value;
                dgv[0, r.Index] = lc;
            }
            // return dgv;
        }


        public void getEbayCompletedItems(string opName, string keywords)
        {

            using (FindingServicePortTypeClient client = new FindingServicePortTypeClient())
            {
                MessageHeader header = MessageHeader.CreateHeader("CustomHeader", "", "");
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);

                    HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", ebayAppID);
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-OPERATION-NAME", opName);
                    httpRequestProperty.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                    FindCompletedItemsRequest request = new FindCompletedItemsRequest();
                    request.keywords = keywords;

                    request.sortOrder = SortOrderType.EndTimeSoonest;
                    request.sortOrderSpecified = true;

                    request.itemFilter = setItemFilter();

                    FindCompletedItemsResponse response = client.findCompletedItems(request);

                    //Set values to our user defined EbayData object
                    if (response.searchResult != null)
                    {
                        if (response.searchResult.count > 0)
                        {
                            ed = new EbayData[response.searchResult.count];
                            int i = 0;
                            foreach (var item in response.searchResult.item)
                            {
                                ed[i] = new EbayData(response, i);
                                i++;
                            }
                            // return ed.ed;
                        }
                    }
                    // return new EbayData[0];
                }

            }
        }



        public ItemFilter[] setItemFilter()
        {

            ItemFilter fCond = new ItemFilter();
            fCond.name = ItemFilterType.Condition;
            fCond.value = new String[] { "New" };

            ItemFilter fSold = new ItemFilter();
            fSold.name = ItemFilterType.SoldItemsOnly;
            fSold.value = new String[] { "true" };

            ItemFilter[] ift = new ItemFilter[3];

            ift[0] = fCond;
            ift[1] = fSold;

            return ift;
        }







    }





}

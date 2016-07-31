using eBayDealFinder.Abstract;
using eBayDealFinder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eBayDealFinder.Concrete
{
    public class ApplicationRepository: IRepository
    {
        //Concrete

        private ApplicationDbContext context = new ApplicationDbContext();
        
        //public IEnumerable<EbayData> ApplicationDbContext {
        //    get { return context.EbayData;  }
        //}


        //public EbayData Delete(int edID)
        //{
        //    EbayData ed = context.EbayData.Find(edID);
        //    if(ed != null)
        //    {
        //        context.EbayData.Remove(ed);
        //        //context.SaveChanges();

        //    }
        //    return ed;
        //}

        ////UoW pattern  to allow a transaction to be built up and finally saved
        //public void SaveChanges()
        //{
        //    context.SaveChanges();
        //}


        //public IEnumerable<EbayData> GetEbayData()
        //{
        //    return context.EbayData.ToList();
        //}

        //public EbayData GetEbayDataByID(int id)
        //{
        //    return context.EbayData.Find(id);
        //}

        //public void InsertEbayData(EbayData EbayData)
        //{
        //    context.EbayData.Add(EbayData);
        //}

        //public void DeleteEbayData(int EbayDataID)
        //{
        //    EbayData EbayData = context.EbayData.Find(EbayDataID);
        //    context.EbayData.Remove(EbayData);
        //}

        //public void UpdateEbayData(EbayData EbayData)
        //{
        //    context.Entry(EbayData).State = EntityState.Modified;
        //}

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }









    }
}
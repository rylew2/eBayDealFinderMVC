using System.Web.Mvc;
using eBayDealFinder.Utility;
using eBayDealFinder.Abstract;

namespace eBayDealFinder.Controllers
{
    public class HomeController : Controller
    {

        private IRepository repo;

        public HomeController(IRepository repository)
        {
            repo = repository;
        }

        public ActionResult Index()
        {
                //eBayDealFinder.Utility.DataManagement.compareDealsToEbay
                DataManagement.compareDealsToEbay(DataManagement.getDealRSS());
            
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






    }
    
}
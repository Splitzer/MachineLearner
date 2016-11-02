using System.Web.Mvc;

namespace MachineLearner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ExcelImporter importer = new ExcelImporter();
            importer.ImportDataFromExcel("C:\\Users\\Splitzer\\Desktop\\Tei\\PTYXIAKI\\Datasets\\Zoo Data.xlsx");

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
using System.Web.Mvc;

namespace MachineLearner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ExcelImporter importer = new ExcelImporter();
            importer.ImportDataFromExcel(@"C:\_Dev\Zoo Data.xlsx");

            return View();
        }

        public ActionResult DatasetVisualiser()
        {
            ViewBag.Message = "Dataset Visualiser";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
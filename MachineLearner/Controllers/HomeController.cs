using MachineLearner.Algorithms;
using MachineLearner.DB;
using System.Web.Mvc;

namespace MachineLearner.Controllers
{
    public class HomeController : Controller
    {
        DB_Access access;
        LearningManager manager;

        public ActionResult Index()
        {
            ExcelImporter importer = new ExcelImporter();
            importer.ImportDataFromExcel(@"C:\_Dev\Zoo Data.xlsx");

            var zooData = access.GetDataset("Zoo");
            manager = new LearningManager(zooData);

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
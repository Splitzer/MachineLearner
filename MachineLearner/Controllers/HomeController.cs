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
            //importer.ImportDataFromExcel(@"C:\_Dev\Zoo Data.xlsx");
            importer.ImportDataFromExcel(@"C:\Users\ng1e16\Downloads\Zoo Data.xlsx");

            access = new DB_Access();
            var zooData = access.GetDataset("ZooData");
            //manager = new LearningManager(zooData);

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
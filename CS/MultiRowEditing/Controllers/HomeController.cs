using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiRowEditing.Models;
using System.IO;
using System.Web.Script.Serialization;
using MultiRowEditing.Infrastructure;


namespace MultiRowEditing.Controllers {
    public class HomeController : Controller {      
        public ActionResult Index() {
            return View(ProductRepository.GetProducts());
        }
        public ActionResult GridPartial() {
            return PartialView("GridViewPartial", ProductRepository.GetProducts());
        }
        [HttpPost]
        public JsonResult SaveData([ModelBinder(typeof(MyDictionaryModelBinder))] Dictionary<string, object> changedValues) {
            //bool res = ProductRepository.UpdateValues(changedValues); uncomment this line to save data
            bool res = true;  //remove this line to test the project
            var result = new { ResultStatus = (res ? "ok" : "error") };
            return Json(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebToDoList.Service;

namespace WebToDoList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetDbAuth(string id)
        {
            return new JsonResult()
            {
                Data = ToDoService.GetToDo(id),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public ActionResult InsertToDo(string id, string date)
        {
            var searchData = ToDoService.GetToDo(id);

            searchData.TO_DO_ID = id;
            searchData.TO_DO_DATE = date;
            var insertData = ToDoService.InsertToDo(searchData);
            return new JsonResult()
            {
                Data = searchData,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public ActionResult UpdateToDo(string id)
        {
            var updateData = ToDoService.UpdateToDo(id);
            var searchData = ToDoService.GetToDo(id);
            return new JsonResult()
            {
                Data = searchData,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public ActionResult DeleteToDo(string id)
        {
            var deleteData = ToDoService.DeleteToDo(id);
            var searchData = ToDoService.GetToDo(id);
            return new JsonResult()
            {
                Data = searchData,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}

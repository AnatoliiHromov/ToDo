using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Business.DALManipulation;
using ToDoApp.CustomAttribute;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        //
        // GET: /ToDo/
        private void DropDownInit(States state)
        {
            IEnumerable<States> actionTypes = Enum.GetValues(typeof(States))
                                                         .Cast<States>();
            IEnumerable<SelectListItem> ActionsList = from action in actionTypes
                             select new SelectListItem
                             {
                                 Text = action.ToString(),
                                 Value = action.ToString()
                             };
            
            ViewBag.State = new SelectList(actionTypes, state);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Index()
        {
            User user = Helpers.AuthHelper.GetUser(HttpContext);
            return View(user);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Details(int id)
        {
            ToDoesHelper h = new ToDoesHelper(new Repository());
            ToDo t = h.GetToDo(id);
            DropDownInit(States.NotDone);
            DetailsToDoModel DTModel = new DetailsToDoModel
            {
                TID=t.ID,
                SubTodoes=t.SubToDoes,
                ToDo=t,
                Lat = t.GeoLat.ToString(),
                Long = t.GeoLong.ToString()
            };
            return View(DTModel);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Add()
        {
            AddToDoModel vm = new AddToDoModel();
            DropDownInit(States.NotDone);
            vm.Date = DateTime.Now.AddHours(0.5);
            return View(vm);
        }
        [PageAuthorize(UserRoles = "User")]
        [HttpPost]
        public ActionResult Add(ToDo t, AddToDoModel ATModel)
        {
            DropDownInit(ATModel.State);
            if (ATModel.Date < DateTime.Now.AddHours(0.15))
            {
                ModelState.AddModelError("Date", "Please enter date of expire for ToDo");
            }
            if (ModelState.IsValid)
            {
                ToDoesHelper Helper = new ToDoesHelper(new Repository());
                if (ATModel.Lat != null)
                { 
                    CultureInfo culture=new CultureInfo("en-US");
                    t.GeoLat = double.Parse(ATModel.Lat, culture);
                    t.GeoLong = double.Parse(ATModel.Long, culture);
                }
            
                Helper.AddToDo(t,Helpers.AuthHelper.GetUser(HttpContext).ID);
                return RedirectToAction("Index", "ToDo");
            }
            else
            return View(ATModel);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Edit(int id)
        {
            AddToDoModel vm = new AddToDoModel();           
            ToDoesHelper h= new ToDoesHelper(new Repository());
            ToDo t = h.GetToDo(id);
            DropDownInit(t.State);
            AddToDoModel EditModel = new AddToDoModel()
            {
                Name = t.Name,
                Description = t.Description,
                State = t.State,
                Date = t.Date,
                IsPublic = t.IsPublic,
                Lat=t.GeoLat.ToString(),
                Long=t.GeoLong.ToString()
            };
            return View(EditModel);
        }
        [PageAuthorize(UserRoles = "User")]
        [HttpPost]
        public ActionResult Edit(ToDo t, AddToDoModel ATModel)
        {
            t.ID = ATModel.ID;
            DropDownInit(ATModel.State);
            if (ATModel.Date < DateTime.Now.AddHours(0.35))
            {
                ModelState.AddModelError("Date", "Please enter date of expire for ToDo");
            }
            if (ModelState.IsValid)
            {
                ToDoesHelper Helper = new ToDoesHelper(new Repository());
                if (ATModel.Lat != null)
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    t.GeoLat = double.Parse(ATModel.Lat, culture);
                    t.GeoLong = double.Parse(ATModel.Long, culture);
                }
                Helper.EditToDo(t,Helpers.AuthHelper.GetUser(HttpContext).ID,t.ID);
                return RedirectToAction("Index", "Account");
            }
            else
                return View(ATModel);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Delete(int id)
        {
            ToDoesHelper h = new ToDoesHelper(new Repository());
            return View(h.GetToDo(id));
        }
        [PageAuthorize(UserRoles = "User")]
        [HttpPost]
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ToDoesHelper h = new ToDoesHelper(new Repository());
            try
            {
                h.DeleteToDo(id);
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return View();
            }
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult DeleteST(int id, int tid)
        {
            SubToDoHelper h = new SubToDoHelper(new Repository());
            try
            {
                h.DeleteSubToDo(id);
                return RedirectToAction("Details", "ToDo", new { id=tid});
            }
            catch
            {
                return RedirectToAction("Details", "ToDo", new { id = tid });
            }
        }
        [PageAuthorize(UserRoles = "User")]
        [HttpPost]
        public ActionResult AddSub(SubToDo st, DetailsToDoModel DTModel)
        {
            SubToDoHelper h = new SubToDoHelper(new Repository());
            if (ModelState.IsValid)
            {
                h.AddSubToDo(st,DTModel.TID);
                return RedirectToAction("Details", "ToDo", new { id=DTModel.TID});
            }
            else
                return View(DTModel);
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Business.DALManipulation;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class SubToDoController : Controller
    {
        //
        // GET: /SubToDo/
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
        public ActionResult Edit(int id)
        {
            SubToDoHelper h = new SubToDoHelper(new Repository());
            SubToDo t = h.GetSubToDo(id);
            sToDoModel stm = new sToDoModel
            {
                Name = t.Name,
                Description = t.Description,
                State = t.State,
                TID = t.ToDo.ID
            };
            DropDownInit(t.State);
            
            return View(stm);
        }
        [HttpPost]
        public ActionResult Edit(int id, sToDoModel stm)
        {
            SubToDoHelper h = new SubToDoHelper(new Repository());
            var st= h.GetSubToDo(id);
            st.Name = stm.Name;
            st.State = stm.State;
            st.Description = stm.Description;
            DropDownInit(st.State);
            if (ModelState.IsValid)
            {
                SubToDoHelper Helper = new SubToDoHelper(new Repository());
                Helper.EditSubToDo(st);
                return RedirectToAction("Details", "ToDo", new { id=stm.TID});
            }
            else
                return View(stm);
        }

    }
}

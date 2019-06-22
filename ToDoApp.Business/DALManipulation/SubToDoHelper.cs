using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;

namespace ToDoApp.Business.DALManipulation  
{
    public class SubToDoHelper
    {
        public SubToDoHelper(IRepository repository)
        {
            this.context = repository;
            this.todoes = new ToDoesHelper(this.context);
        }

        private IRepository context;
        private ToDoesHelper todoes;

        public SubToDo AddSubToDo(SubToDo st, int todoID)
        {
            var s = st;
            s.ToDo = todoes.GetToDo(todoID);
            context.Add(s);
            context.SaveChanges();
            return s;
        }
        public void DeleteSubToDo(int stodoID)
        {
            var d = GetSubToDo(stodoID);
            context.Delete(d);
            context.SaveChanges();
        }
        public IEnumerable<SubToDo> GetSubToDoes()
        {
            return context.SubToDoes;
        }

        public int SubToDoesCount()
        {
            return GetSubToDoes().Count();
        }

        public IEnumerable<SubToDo> GetSubToDoes(int todoID)
        {
            return context.SubToDoes.Where(x => x.ToDo != null && x.ToDo.ID == todoID).ToList();
        }

        public SubToDo GetSubToDo(int stodoID)
        {
            return GetSubToDoes().First(x => x.ID == stodoID);
        }
        public SubToDo EditSubToDo(SubToDo st)
        {
            var s = GetSubToDo(st.ID);
            s.Name = st.Name;            
            s.Description = st.Description;
            s.State = st.State;
            context.SaveChanges();
            return s;
        }
    }
}

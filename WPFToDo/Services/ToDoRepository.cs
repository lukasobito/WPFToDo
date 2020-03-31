using DalToDo.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using WPFToDo.Model;
using WPFToDo.Utils;

namespace WPFToDo.Services
{
    class ToDoRepository : IRepository<ToDo>
    {
        private static IRepository<ToDo> _instance;

        public static IRepository<ToDo> Instance
        {
            get { return _instance ?? (_instance = new ToDoRepository()); }
        }

        public void Create(ToDo t)
        {
            Consume.Post<ToDo>("ToDo", t);
        }

        public void Delete(int id)
        {
            Consume.Delete("ToDo", id);
        }

        public List<ToDo> GetAll()
        {
            return Consume.GetAll<ToDo>("ToDo");
        }

        public ToDo GetOne(int id)
        {
            return Consume.GetOne<ToDo>("ToDo", id);
        }

        public void Update(ToDo t)
        {
            Consume.Put("ToDo", t);
        }
    }
}

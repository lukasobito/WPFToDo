using DalToDo.Data;
using DalToDo.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToDo.Services
{
    public class ToDoRepository : IRepository<ToDo>
    {
        private static IRepository<ToDo> instance;
        private SqlConnection connection;

        public static IRepository<ToDo> Instance
        {
            get { return instance ?? (new ToDoRepository()); }
        }

        public ToDoRepository()
        {
            connection = new SqlConnection(@"Data Source=desktop-ok74vhk;Initial Catalog=ToDo;Integrated Security=True;Pooling=False");
            connection.Open();
        }
        public void Create(ToDo t)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO ToDo (Titre, Description, IsDone, DateValidation) VALUES (@Titre, @Description, @IsDone, @DateValidation)";
            cmd.Parameters.AddWithValue("Titre", t.Titre);
            cmd.Parameters.AddWithValue("Description", t.Description);
            cmd.Parameters.AddWithValue("IsDone", t.IsDone);
            cmd.Parameters.AddWithValue("DateValidation", t.DateValidation);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM ToDo WHERE Id = @Id";
            cmd.Parameters.AddWithValue("Id", id);

            cmd.ExecuteNonQuery();
        }

        public List<ToDo> GetAll()
        {
            List<ToDo> ToDos = new List<ToDo>();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM ToDo";
            using(SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ToDos.Add(new ToDo
                    {
                        Id = (int)dr["Id"],
                        Titre = dr["Titre"].ToString(),
                        Description = dr["Description"].ToString(),
                        IsDone = (bool)dr["IsDone"],
                        DateValidation = (dr["DateValidation"] is DBNull) ? (DateTime?)null : (DateTime)dr["DateValidation"],
                    });
                }
            }
            cmd.Dispose();
            return ToDos;
        }

        public ToDo GetOne(int id)
        {
            ToDo toDo = new ToDo();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM ToDo WHERE Id = @Id";
            cmd.Parameters.AddWithValue("Id", id);
            using(SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    toDo.Id = (int)dr["Id"];
                    toDo.Titre = dr["Titre"].ToString();
                    toDo.Description = dr["Description"].ToString();
                    toDo.IsDone = (bool)dr["IsDone"];
                    toDo.DateValidation = (dr["DateValidation"] is DBNull) ? (DateTime?)null : (DateTime)dr["DateValidation"];
                }
            }
            return toDo;
        }

        public void Update(ToDo t)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE ToDo SET Titre = @Titre, Description = @Description, IsDone = @IsDone, DateValidation = @DateValidation WHERE Id = @Id";
            cmd.Parameters.AddWithValue("Titre", t.Titre);
            cmd.Parameters.AddWithValue("Description", t.Description);
            cmd.Parameters.AddWithValue("IsDone", t.IsDone);
            cmd.Parameters.AddWithValue("DateValidation", t.DateValidation);
            cmd.Parameters.AddWithValue("Id", t.Id);

            cmd.ExecuteNonQuery();
        }
    }
}

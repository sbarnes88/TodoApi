using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using TodoApi.Models;

namespace TodoApi.Handlers
{
    public class DbHandler : IDbHandler
    {
        private string _dataSource = "Data Source=todos.db";

        public DbHandler()
        {
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS todos (id INT, text VARCHAR(2048), due_date DATETIME, completed BOOLEAN, item_order INT);";
                command.ExecuteNonQuery();
            }
        }

        public int GetNextId()
        {
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(id) FROM todos;";
                var result = command.ExecuteScalar();
                var maxId = int.Parse(result.ToString());
                return maxId + 1;
            }
        }

        public List<TodoModel> GetTodoItems()
        {
            var todos = new List<TodoModel>();
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id, text, due_date, completed, item_order FROM todos;";
                var table = new DataTable();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var todo = new TodoModel
                        {
                            Id = reader.GetInt32("id"),
                            Description = reader.GetString("text"),
                            DueDate = reader.GetDateTime("due_date"),
                            IsCompleted = reader.GetBoolean("completed"),
                            Order = reader.GetInt32("item_order")
                        };
                        todos.Add(todo);
                    }
                }
            }
            return todos;
        }

        public void InsertTodo(TodoModel todo)
        {
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO todos (id, text, due_date, completed, item_order) " +
                    "VALUES(@id, @text, @due_date, @completed, @item_order);";
                command.Parameters.AddWithValue("@id", todo.Id);
                command.Parameters.AddWithValue("@text", todo.Description);
                command.Parameters.AddWithValue("@due_date", todo.DueDate);
                command.Parameters.AddWithValue("@completed", todo.IsCompleted);
                command.Parameters.AddWithValue("@item_order", todo.Order);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTodo(TodoModel todo)
        {
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE todos " +
                    " SET text = @text, " +
                    " due_date = @due_date, " +
                    " completed = @completed, " +
                    " item_order = @item_order " +
                    " WHERE id = @id;";
                command.Parameters.AddWithValue("@id", todo.Id);
                command.Parameters.AddWithValue("@text", todo.Description);
                command.Parameters.AddWithValue("@due_date", todo.DueDate);
                command.Parameters.AddWithValue("@completed", todo.IsCompleted);
                command.Parameters.AddWithValue("@item_order", todo.Order);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTodo(int todoId)
        {
            using (var connection = new SQLiteConnection(_dataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM todos WHERE id = @id;";
                command.Parameters.AddWithValue("@id", todoId);
                command.ExecuteNonQuery();
            }
        }
    }
}

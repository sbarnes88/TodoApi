using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Handlers
{
    public interface IDbHandler
    {
        int GetNextId();
        List<TodoModel> GetTodoItems();
        void InsertTodo(TodoModel todo);
        void UpdateTodo(TodoModel todo);
        void DeleteTodo(int todoId);
    }
}
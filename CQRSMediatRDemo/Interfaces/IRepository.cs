using CQRSMediatRDemo.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Interfaces
{
    public interface IRepository
    {
        Task<Todo> GetTodo(int Id);
        List<Todo> GetAllTodos();
        Task<int> AddTodo(string Name);

        Task<bool> CheckIfNameExists(string Name);
    }


}
using CQRSMediatRDemo.Domain;
using CQRSMediatRDemo.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Database 
{
    public class Repository : IRepository
    {
        public List<Todo> Todos { get; } = new List<Todo>
        {
            new Todo{Id =1 , Name = "Cook Dinner", Completed = false},
            new Todo{Id =2 , Name = "Make Youtube Video", Completed = true},
            new Todo{Id =3 , Name = "Wash car", Completed = false},
            new Todo{Id =4 , Name = "Practice programming", Completed = true},
            new Todo{Id =5 , Name = "Take out garbage", Completed = false}
        };

        public async Task<int> AddTodo(string Name)
        {
            int nextId = -1;
            await Task.Run(() =>
                {
                    nextId = Todos.Max(x => x.Id) + 1;
                    Todos.Add(
                            new Todo
                            {
                                Id = nextId,
                                Name = Name
                            }
                    );
                }
            );
            return nextId;
        }

        public async Task<bool> CheckIfNameExists(string Name)
        {
            return await Task.Run(() => Todos.Any(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)));
        }

        public List<Todo> GetAllTodos()
        {
            return Todos;
        }

        public async Task<Todo> GetTodo(int Id)
        {
            Todo todo = await Task.Run(()=>Todos.FirstOrDefault(x => x.Id == Id));
            return todo == null ? null : todo;
        }
    }
}

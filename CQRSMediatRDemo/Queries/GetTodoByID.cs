using CQRSMediatRDemo.Caching;
using CQRSMediatRDemo.DTOs;
using CQRSMediatRDemo.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Queries
{
    public static class GetTodoByID
    {
        //Query / Command
        // All the data that we need to execute
        public record Query(int Id) : IRequest<Response>, ICacheable
        {
            public string CacheKey => $"GetTodoByID-{Id}";
        }

        // Handler
        // All the business logic to execute. Returns a response
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }


            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                //All the business logic.
                var todo = await repository.GetTodo(request.Id);
                return todo == null ? null : new Response { Id = todo.Id, Name=todo.Name, Completed = todo.Completed };
            }
        }

        // Response
        // The data that we want to return
        public record Response : CQRSResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Completed { get; set; }
        }

    }
}

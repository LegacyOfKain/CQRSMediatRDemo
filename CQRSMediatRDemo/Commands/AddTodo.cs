using CQRSMediatRDemo.Database;
using CQRSMediatRDemo.Domain;
using CQRSMediatRDemo.DTOs;
using CQRSMediatRDemo.Interfaces;
using CQRSMediatRDemo.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Commands
{
    public static class AddTodo
    {
        //Query / Command
        // All the data that we need to execute
        public record Command(string Name) : IRequest<Response>;

        public class Validator : IValidationHandler<Command>
        {
            private readonly IRepository repository;
            public Validator(IRepository repository)
            {
                this.repository = repository;
            }
            public async Task<ValidationResult> Validate(Command Request)
            {
                if (await repository.CheckIfNameExists(Request.Name))
                    return ValidationResult.Fail("Todo Already Exists");

                return ValidationResult.Success();
            }
        }

        // Handler
        // All the business logic to execute. Returns a response
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository repository;


            public Handler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                int nextId = await repository.AddTodo(request.Name);
                return new Response { Id = nextId };
            }
        }

        public record Response : CQRSResponse
        {
            public int Id { get; set; }
        }

    }
}

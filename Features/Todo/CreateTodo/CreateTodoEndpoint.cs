using VerticalSliceFastEndpoints.Entities;

namespace Features.Todo.CreateTodo;

public class CreateTodoEndpoint : EndpointWithMapping<CreateTodoRequest, CreateTodoResponse, TodoItem>
{
    public override void Configure()
    {
        PreProcessor<PreProcessorLogging<CreateTodoRequest>>();
        Post("/todos");
        AllowAnonymous();

        Throttle(hitLimit: 20, durationSeconds: 60);

        Description(x => x
            .Produces<CreateTodoResponse>(201)
            .ProducesProblem(400));
    }

    public override async Task HandleAsync(CreateTodoRequest req, CancellationToken ct)
    {
        var todoItem = MapToEntity(req);
        var response = MapFromEntity(todoItem);

        await Send.CreatedAtAsync("/todos/{id}", new { id = todoItem.Id }, response, cancellation: ct);
    }

    public override TodoItem MapToEntity(CreateTodoRequest req)
    {
        return new TodoItem(req.Title, req.Description);
    }
    
    public override CreateTodoResponse MapFromEntity(TodoItem entity)
    {
        return new CreateTodoResponse { Id = entity.Id };
    }
}
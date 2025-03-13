namespace IREPOSITORYSAMPLE.Dtos.Todo
{

    public class CreateTodoRequestDto
    {

        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

    }
}
namespace IREPOSITORYSAMPLE.Models
{

    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
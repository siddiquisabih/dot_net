

using IREPOSITORYSAMPLE.Models;

namespace IREPOSITORYSAMPLE.Interfaces
{

    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllASync();

    }
}
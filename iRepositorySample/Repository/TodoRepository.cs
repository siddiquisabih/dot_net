using Microsoft.EntityFrameworkCore;
using IREPOSITORYSAMPLE.DATA;
using IREPOSITORYSAMPLE.Interfaces;
using IREPOSITORYSAMPLE.Models;



namespace IREPOSITORYSAMPLE.Repository
{

    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllASync()
        {
            return await _context.Todo.ToListAsync();
        }
    }
}
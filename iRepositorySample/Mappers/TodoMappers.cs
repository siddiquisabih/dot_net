using IREPOSITORYSAMPLE.Dtos.Todo;
using IREPOSITORYSAMPLE.Models;

namespace IREPOSITORYSAMPLE.Mappers
{

    public static class StockMappers
    {


        public static TodoDto ToStockDto(this Todo todoModal)
        {

            return new TodoDto
            {
                Id = todoModal.Id,
                Title = todoModal.Title,
                IsCompleted = todoModal.IsCompleted,
            };

        }


        public static Todo ToStockFromCreateDto(this CreateTodoRequestDto data)
        {
            return new Todo
            {
                Title = data.Title,
                IsCompleted = data.IsCompleted,
            };
        }


    }
}
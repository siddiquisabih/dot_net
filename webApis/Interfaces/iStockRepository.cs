using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WEBAPIS.Dtos.Stock;
using WEBAPIS.Models;

namespace WEBAPIS.Interfaces
{

    public interface IStockRepository
    {
        Task<List<Stock>> GetAllASync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(UpdateStockRequestDto updateBody);
        Task<Stock?> DeleteStockAsync(int id);

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPIS.DATA;
using WEBAPIS.Dtos.Stock;
using WEBAPIS.Interfaces;
using WEBAPIS.Models;

namespace WEBAPIS.Repository
{

    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Stock>> GetAllASync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {


            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }


            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }


        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateStockAsync(UpdateStockRequestDto updateBody)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == updateBody.Id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = updateBody.Symbol;
            stock.CompanyName = updateBody.CompanyName;
            stock.Purchase = updateBody.Purchase;
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
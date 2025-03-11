using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPIS.DATA;
using WEBAPIS.Dtos.Stock;
using WEBAPIS.Interfaces;
using WEBAPIS.Mappers;
using WEBAPIS.Repository;


namespace WEBAPIS.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllASync();

            var stockDto = stocks.Select(e => e.ToStockDto());
            return Ok(stockDto);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockRepo.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {

            var stock = await _stockRepo.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            
            return Ok(new { message = "Stock deleted successfully" });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequestDto updateBody)
        {
            // var updateData = updateBody.ToStockFromUpdateDto();
            var stock = await _stockRepo.UpdateStockAsync(updateBody);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Stock updated successfully" });
        }

    }
}
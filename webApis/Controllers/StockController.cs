using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPIS.DATA;
using WEBAPIS.Dtos.Stock;
using WEBAPIS.Mappers;


namespace WEBAPIS.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(e => e.ToStockDto());
            return Ok(stocks);
        }



        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }


        [HttpPost]
        public IActionResult CreateNewStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();

            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }



        [HttpDelete("{id}")]
        public IActionResult CreateStock([FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return Ok(new { message = "Stock deleted successfully" });
        }


        [HttpPut]
        public IActionResult UpdateStock([FromBody] UpdateStockRequestDto updateBody)
        {

            var updateData = updateBody.ToStockFromUpdateDto();

            if (updateData.Id == 0)
            {
                return BadRequest(new { message = "Stock Id Required" });
            }

            var stock = _context.Stocks.FirstOrDefault(x => x.Id == updateData.Id);
            if (stock == null)
            {
                return NotFound();
            }


            stock.Symbol = updateBody.Symbol;
            stock.CompanyName = updateBody.CompanyName;
            stock.Purchase = updateBody.Purchase;

            _context.SaveChanges();
            return Ok(new { message = "Stock updated successfully" });
        }

    }
}
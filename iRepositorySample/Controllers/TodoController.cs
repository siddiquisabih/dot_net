using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IREPOSITORYSAMPLE.DATA;
using IREPOSITORYSAMPLE.Dtos.Todo;
using IREPOSITORYSAMPLE.Interfaces;
using IREPOSITORYSAMPLE.Mappers;
using IREPOSITORYSAMPLE.Repository;

namespace IREPOSITORYSAMPLE.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITodoRepository _stockRepo;
        public StockController(ApplicationDbContext context, ITodoRepository stockRepo)
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
    }
}
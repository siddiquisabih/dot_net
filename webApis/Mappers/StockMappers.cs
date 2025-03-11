using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WEBAPIS.Dtos.Stock;
using WEBAPIS.Models;

namespace WEBAPIS.Mappers
{

    public static class StockMappers
    {


        public static StockDto ToStockDto(this Stock StockModel)
        {

            return new StockDto
            {
                Id = StockModel.Id,
                Purchase = StockModel.Purchase,
                Symbol = StockModel.Symbol,
                CompanyName = StockModel.CompanyName,
            };

        }


        public static Stock ToStockFromCreateDto(this CreateStockRequestDto data)
        {
            return new Stock
            {
                Symbol = data.Symbol,
                CompanyName = data.CompanyName,
                Purchase = data.Purchase,
            };
        }

        public static Stock ToStockFromUpdateDto(this UpdateStockRequestDto data)
        {
            return new Stock
            {
                Id = data.Id,
                Symbol = data.Symbol,
                CompanyName = data.CompanyName,
                Purchase = data.Purchase,
            };
        }
    }
}
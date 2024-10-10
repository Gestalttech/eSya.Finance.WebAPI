using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface IExchangeRateRepository
    {
        Task<List<DO_CurrencyExchangeRate>> FillExchangeRate(string Countrycode);
        Task<DO_ReturnParameter> InsertIntoExchangeRate(DO_CurrencyExchangeRate obj);
        Task<DO_ReturnParameter> UpdateIntoExchangeRate(DO_CurrencyExchangeRate obj);
    }
}

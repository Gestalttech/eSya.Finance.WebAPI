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
        Task<List<DO_CurrencyExchangeRate>> FillExchangeRate();
        Task<DO_ReturnParameter> InsertUpdateExchangeRate(DO_CurrencyExchangeRate obj);
    }
}

﻿using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface ICommonDataRepository
    {
        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType);

        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType);

        Task<List<DO_BusinessLocation>> GetBusinessKey();
        Task<List<DO_CurrencyMaster>> GetActiveCurrencyCodes(int BusinessKey);
        Task<List<DO_CountryMaster>> GetActiveCountryCodes();
        Task<List<DO_CurrencyMaster>> GetActiveExchangeCurrencyCodes(string Countrycode);
    }
}

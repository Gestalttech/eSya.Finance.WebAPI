using eSya.Finance.DL.Entities;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DL.Repository
{
    public class CommonDataRepository : ICommonDataRepository
    {
        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcds
                        .Where(w => w.CodeType == codeType && w.ActiveStatus)
                        .Select(r => new DO_ApplicationCodes
                        {
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcds
                        .Where(w => w.ActiveStatus
                        && l_codeType.Contains(w.CodeType))
                        .Select(r => new DO_ApplicationCodes
                        {
                            CodeType = r.CodeType,
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.BusinessName + "-" + r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_CurrencyMaster>> GetActiveCurrencyCodes(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var sgltype = db.GtEcbslns.Where(w => w.BusinessKey == BusinessKey).SingleOrDefault();
                    if (sgltype != null)
                    {
                        //var curr_lst = db.GtEccucos.Where(x => x.CurrencyCode != sgltype.CurrencyCode).ToList();

                        var currencies = db.GtEccucos
                        .Where(w => w.ActiveStatus && w.CurrencyCode != sgltype.CurrencyCode)
                        .Select(r => new DO_CurrencyMaster
                        {
                            CurrencyCode = r.CurrencyCode,
                            CurrencyName = r.CurrencyName
                        }).ToListAsync();

                        return await currencies;
                    }
                    else
                    {
                        var currencies = db.GtEccucos
                       .Where(w => w.ActiveStatus)
                       .Select(r => new DO_CurrencyMaster
                       {
                           CurrencyCode = r.CurrencyCode,
                           CurrencyName = r.CurrencyName
                       }).ToListAsync();
                        return await currencies;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

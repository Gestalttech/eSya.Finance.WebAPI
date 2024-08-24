using eSya.Finance.DL.Entities;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DL.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IStringLocalizer<ExchangeRateRepository> _localizer;
        public ExchangeRateRepository(IStringLocalizer<ExchangeRateRepository> localizer)
        {
            _localizer = localizer;
        }

        public async Task<DO_ReturnParameter> InsertUpdateExchangeRate(DO_CurrencyExchangeRate obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var chkD = db.GtIfcrers.Where(c => c.CurrencyCode == obj.CurrencyCode && c.DateOfExchangeRate.Date == obj.DateOfExchangeRate.Date).Count();
                        if (chkD <= 0)
                        {
                            var ex_RT = new GtIfcrer
                            {
                                CurrencyCode = obj.CurrencyCode,
                                DateOfExchangeRate = obj.DateOfExchangeRate,
                                StandardRate = obj.StandardRate,
                                SellingRate = obj.SellingRate,
                                BuyingRate = obj.BuyingRate,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = obj.CreatedON,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtIfcrers.Add(ex_RT);

                            await db.SaveChangesAsync();
                            
                            //CreateExchangeRate(obj);
                        }
                        else
                        {
                            GtIfcrer ex = db.GtIfcrers.Single(c => c.CurrencyCode == obj.CurrencyCode && c.DateOfExchangeRate.Date == obj.DateOfExchangeRate.Date);

                            ex.StandardRate = obj.StandardRate;
                            ex.SellingLastVoucherDate = obj.SellingLastVoucherDate;
                            ex.SellingRate = obj.SellingRate;
                            ex.BuyingLastVoucherDate = obj.BuyingLastVoucherDate;
                            ex.BuyingRate = obj.BuyingRate;
                            ex.ActiveStatus = obj.ActiveStatus;
                            ex.ModifiedBy = obj.UserID;
                            ex.ModifiedOn = obj.CreatedON;
                            ex.ModifiedTerminal = obj.TerminalID;

                            await db.SaveChangesAsync();
                            //UpdateExchangeRate(obj);
                        }
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<DO_CurrencyExchangeRate>> FillExchangeRate(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    //var sgltype = db.GtEcbslns.Where(w => w.BusinessKey == BusinessKey).SingleOrDefault();
                    //if (sgltype != null)
                    //{
                    //    var curr_lst = db.GtEccucos.Where(x => x.CurrencyCode != sgltype.CurrencyCode).ToList();

                        var ds = db.GtIfcrers.Where(x => x.ActiveStatus)
                            .Select(r => new DO_CurrencyExchangeRate
                            {
                                CurrencyCode = r.CurrencyCode,
                                CurrencyDesc = r.CurrencyCode,
                                DateOfExchangeRate = r.DateOfExchangeRate,
                                StandardRate = r.StandardRate,
                                SellingLastVoucherDate = r.SellingLastVoucherDate,
                                SellingRate = r.SellingRate,
                                BuyingLastVoucherDate = r.BuyingLastVoucherDate,
                                BuyingRate = r.BuyingRate,
                                ActiveStatus = r.ActiveStatus,
                            }).ToListAsync();
                        return await ds;
                        //foreach (var cm in curr_lst)
                        //{
                        //    DO_CurrencyExchangeRate erObj;
                        //    var obj = db.GtIfcrers.Where(x => x.CurrencyCode == cm.CurrencyCode).OrderByDescending(x => x.DateOfExchangeRate).FirstOrDefault();
                        //    if (obj != null)
                        //    {
                        //        erObj = new DO_CurrencyExchangeRate()
                        //        {
                        //            CurrencyCode = obj.CurrencyCode,
                        //            DateOfExchangeRate = obj.DateOfExchangeRate,
                        //            StandardRate = obj.StandardRate,
                        //            SellingLastVoucherDate = obj.SellingLastVoucherDate,
                        //            SellingRate = obj.SellingRate,
                        //            BuyingLastVoucherDate = obj.BuyingLastVoucherDate,
                        //            BuyingRate = obj.BuyingRate,
                        //            ActiveStatus = obj.ActiveStatus,
                        //        };
                        //    }
                        //    else
                        //    {
                        //        erObj = new DO_CurrencyExchangeRate()
                        //        {
                        //            CurrencyCode = cm.CurrencyCode,
                        //            DateOfExchangeRate = DateTime.Now,
                        //            Status = true
                        //        };
                        //    }
                        //    list.Add(erObj);
                        //}

                    //}
                    //return await;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<DO_CurrencyExchangeRate> ReadExchangeRate(byte businessId)
        //{
        //    try
        //    {
        //        List<DO_CurrencyExchangeRate> list = new List<DO_CurrencyExchangeRate>();

        //        using (HISFinance_Entities FE = new HISFinance_Entities())
        //        {
        //            Shared_Entities SE = new Shared_Entities();
        //            OrganizationDetail org = SE.OrganizationDetails.Single(c => c.BusinessID == businessId);

        //            var l_GT_IFCRMS = FE.GT_IFCRMS.Where(x => x.CurrencyCode != org.LocalCurrency).ToList();
        //            foreach (var cm in l_GT_IFCRMS)
        //            {
        //                DO_CurrencyExchangeRate erObj;
        //                var obj = FE.GT_IFCRER.Where(x => x.CurrencyCode == cm.CurrencyCode).OrderByDescending(x => x.DateOfExchangeRate).FirstOrDefault();
        //                if (obj != null)
        //                {
        //                    erObj = new DO_CurrencyExchangeRate()
        //                    {
        //                        CurrencyCode = obj.CurrencyCode,
        //                        DateOfExchangeRate = obj.DateOfExchangeRate,
        //                        StandardRate = obj.StandardRate,
        //                        SellingLastVoucherDate = obj.SellingLastVoucherDate,
        //                        SellingRate = obj.SellingRate,
        //                        BuyingLastVoucherDate = obj.BuyingLastVoucherDate,
        //                        BuyingRate = obj.BuyingRate,
        //                        Status = obj.Status,
        //                    };
        //                }
        //                else
        //                {
        //                    erObj = new DO_CurrencyExchangeRate()
        //                    {
        //                        CurrencyCode = cm.CurrencyCode,
        //                        DateOfExchangeRate = DateTime.Now,
        //                        Status = true
        //                    };
        //                }
        //                list.Add(erObj);
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
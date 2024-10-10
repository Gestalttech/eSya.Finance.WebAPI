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
        public async Task<DO_ReturnParameter> InsertIntoExchangeRate(DO_CurrencyExchangeRate obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {


                        var chkDh = db.GtIfcrehs.Where(h => h.CurrencyCode == obj.CurrencyCode && h.CountryCode == obj.CountryCode).FirstOrDefault();
                        if (chkDh != null)
                        {
                            var chkdetails = db.GtIfcrers.Where(c => c.CurrencyKey == chkDh.CurrencyKey && c.DateOfExchangeRate.Date == obj.DateOfExchangeRate.Date).FirstOrDefault();

                            if (chkdetails == null)
                            {
                               
                                var ex_RT = new GtIfcrer
                                {
                                    CurrencyKey = chkDh.CurrencyKey,
                                    DateOfExchangeRate = obj.DateOfExchangeRate,
                                    StandardRate = obj.StandardRate,
                                    SellingRate = obj.SellingRate,
                                    BuyingRate = obj.BuyingRate,
                                    SellingLastVoucherDate=obj.SellingLastVoucherDate,
                                    BuyingLastVoucherDate=obj.BuyingLastVoucherDate,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = obj.CreatedON,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtIfcrers.Add(ex_RT);

                                await db.SaveChangesAsync();

                            }
                        }
                        else
                        {
                            int _ckey = db.GtIfcrehs.Select(x => x.CurrencyKey).DefaultIfEmpty().Max() + 1;
                            var ex_RTheader = new GtIfcreh
                            {
                                CurrencyCode = obj.CurrencyCode,
                                CountryCode = obj.CountryCode,
                                CurrencyKey = _ckey,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = obj.CreatedON,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtIfcrehs.Add(ex_RTheader);
                            await db.SaveChangesAsync();
                            var chkdetails = db.GtIfcrers.Where(c => c.CurrencyKey == _ckey && c.DateOfExchangeRate.Date == obj.DateOfExchangeRate.Date).FirstOrDefault();
                            if (chkdetails == null)
                            {
                                var ex_R = new GtIfcrer
                                {
                                    CurrencyKey = _ckey,
                                    DateOfExchangeRate = obj.DateOfExchangeRate,
                                    StandardRate = obj.StandardRate,
                                    SellingRate = obj.SellingRate,
                                    BuyingRate = obj.BuyingRate,
                                    SellingLastVoucherDate = obj.SellingLastVoucherDate,
                                    BuyingLastVoucherDate = obj.BuyingLastVoucherDate,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = obj.CreatedON,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtIfcrers.Add(ex_R);

                                await db.SaveChangesAsync();

                            }
                            else
                            {
                                chkdetails.StandardRate = obj.StandardRate;
                                chkdetails.SellingRate = obj.SellingRate;
                                chkdetails.BuyingRate = obj.BuyingRate;
                                chkdetails.SellingLastVoucherDate = obj.SellingLastVoucherDate;
                                chkdetails.BuyingLastVoucherDate = obj.BuyingLastVoucherDate;
                                chkdetails.ActiveStatus = obj.ActiveStatus;
                                chkdetails.ModifiedBy = obj.UserID;
                                chkdetails.ModifiedOn = obj.CreatedON;
                                chkdetails.ModifiedTerminal = obj.TerminalID;

                            }
                            await db.SaveChangesAsync();
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
        public async Task<DO_ReturnParameter>UpdateIntoExchangeRate(DO_CurrencyExchangeRate obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {


                        var chkDh = db.GtIfcrehs.Where(h => h.CurrencyCode == obj.CurrencyCode && h.CountryCode == obj.CountryCode && h.CurrencyKey==obj.CurrencyKey).FirstOrDefault();
                        if (chkDh != null)
                        {
                            chkDh.ActiveStatus = obj.ActiveStatus;
                            chkDh.ModifiedBy = obj.UserID;
                            chkDh.ModifiedOn = obj.CreatedON;
                            chkDh.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            var chkdetails = db.GtIfcrers.Where(c => c.CurrencyKey == chkDh.CurrencyKey && c.DateOfExchangeRate.Date == obj.DateOfExchangeRate.Date).FirstOrDefault();

                            if (chkdetails == null)
                            {
                                var ex_RT = new GtIfcrer
                                {
                                    CurrencyKey = chkDh.CurrencyKey,
                                    DateOfExchangeRate = obj.DateOfExchangeRate,
                                    StandardRate = obj.StandardRate,
                                    SellingRate = obj.SellingRate,
                                    BuyingRate = obj.BuyingRate,
                                    SellingLastVoucherDate = obj.SellingLastVoucherDate,
                                    BuyingLastVoucherDate = obj.BuyingLastVoucherDate,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = obj.CreatedON,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtIfcrers.Add(ex_RT);

                                await db.SaveChangesAsync();

                            }
                            else
                            {
                                chkdetails.StandardRate = obj.StandardRate;
                                chkdetails.SellingRate = obj.SellingRate;
                                chkdetails.BuyingRate = obj.BuyingRate;
                                chkdetails.SellingLastVoucherDate = obj.SellingLastVoucherDate;
                                chkdetails.BuyingLastVoucherDate = obj.BuyingLastVoucherDate;
                                chkdetails.ActiveStatus = obj.ActiveStatus;
                                chkdetails.ModifiedBy = obj.UserID;
                                chkdetails.ModifiedOn = obj.CreatedON;
                                chkdetails.ModifiedTerminal = obj.TerminalID;
                                await db.SaveChangesAsync();
                            }


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

        public async Task<List<DO_CurrencyExchangeRate>> FillExchangeRate(string Countrycode)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                        var ds = db.GtIfcrers
                        .Join
                        (db.GtIfcrehs.Where(x=>x.CountryCode==Countrycode),
                        i=>new { i.CurrencyKey },
                        l=>new { l.CurrencyKey},
                        (i,l) => new {i,l})
                        .Join
                        (db.GtEccucos.Where(x=>x.ActiveStatus),
                        lc => new {lc.l.CurrencyCode},
                        c => new {c.CurrencyCode},
                        (lc, c) => new {lc,c})
                        .Join
                        (db.GtEccncds.Where(x=>x.ActiveStatus),
                        lcco => new {lcco.lc.l.CountryCode},
                        co => new {co.CountryCode},
                        (lcco,co) => new { lcco, co })
                         .Select(r => new DO_CurrencyExchangeRate
                           {
                                CurrencyKey=r.lcco.lc.i.CurrencyKey,
                                CurrencyCode = r.lcco.lc.l.CurrencyCode,
                                CurrencyDesc = r.lcco.c.CurrencyName,
                                CountryCode = r.lcco.lc.l.CountryCode,
                                CountryDesc =r.co.CountryName,
                                DateOfExchangeRate = r.lcco.lc.i.DateOfExchangeRate,
                                StandardRate = r.lcco.lc.i.StandardRate,
                                SellingLastVoucherDate = r.lcco.lc.i.SellingLastVoucherDate,
                                SellingRate = r.lcco.lc.i.SellingRate,
                                BuyingLastVoucherDate = r.lcco.lc.i.BuyingLastVoucherDate,
                                BuyingRate = r.lcco.lc.i.BuyingRate,
                                ActiveStatus = r.lcco.lc.i.ActiveStatus,
                            }).ToListAsync();
                        return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
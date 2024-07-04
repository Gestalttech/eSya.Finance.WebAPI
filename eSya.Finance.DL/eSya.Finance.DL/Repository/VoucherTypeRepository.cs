using eSya.Finance.DL.Entities;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DL.Repository
{
    public class VoucherTypeRepository: IVoucherTypeRepository
    {
        private readonly IStringLocalizer<VoucherTypeRepository> _localizer;
        public VoucherTypeRepository(IStringLocalizer<VoucherTypeRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_BookType>> GetActiveBookTypes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIffabts.Where(x=>x.ActiveStatus)
                        .Select(r => new DO_BookType
                        {
                            BookType = r.BookType,
                            BookTypeDesc = r.BookTypeDesc,
                            PaymentMethodLinkReq = r.PaymentMethodLinkReq,
                            ActiveStatus = r.ActiveStatus
                        }).OrderBy(o => o.BookTypeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_VoucherType>> GetBookTypePaymentMethods(string booktype,string? vouchertype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtIffabts
                        .Join(db.GtEccnpms,
                         gc => gc.BookType,
                         ct => ct.PaymentMethod,
                         (gc, ct) => new { gc, ct })
                        .Join(db.GtEcapcds,
                         ctc => ctc.ct.InstrumentType,
                         p => p.ApplicationCode,
                         (ctc, p) => new { ctc, p })
                        .Where(x=>x.ctc.ct.ActiveStatus && x.ctc.gc.BookType == booktype && x.ctc.gc.ActiveStatus 
                        && x.ctc.gc.PaymentMethodLinkReq && x.p.ActiveStatus)

                        .Select(r => new DO_VoucherType
                        {
                            BookType=r.ctc.gc.BookType,
                            InstrumentType=r.ctc.ct.InstrumentType,
                            InstrumentTypeDesc = r.p.CodeDesc,
                            ActiveStatus=false

                        }).ToListAsync();

                    var distpayments = ds.GroupBy(p => p.InstrumentType)
                           .Select(g => g.First())
                           .ToList();

                    foreach (var obj in distpayments)
                    {
                        GtIfbtpm inslink = db.GtIfbtpms.Where(x => x.BookType == booktype && x.VoucherType == vouchertype && x.InstrumentType == obj.InstrumentType
                        && x.InstrumentType == obj.InstrumentType).FirstOrDefault();
                        if (inslink != null)
                        {
                            obj.ActiveStatus = inslink.ActiveStatus;
                        }
                        else
                        {
                            obj.ActiveStatus = false;
                        }
                    }
                    return distpayments;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_VoucherType>> GetVoucherTypesbyBookType(string booktype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds =await db.GtIfbtpms.Where(x=>x.BookType==booktype)
                        .Select(r => new DO_VoucherType
                        {
                            BookType = r.BookType,
                            VoucherType=r.VoucherType,
                            InstrumentType = r.InstrumentType,
                            VoucherTypeDesc = r.VoucherTypeDesc,
                            ActiveStatus = r.ActiveStatus
                        }).ToListAsync();

                    var distvouchers = ds.GroupBy(p => new { p.BookType, p.VoucherType, p.VoucherTypeDesc })
                           .Select(g => g.First())
                           .ToList();

                    return  distvouchers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ChkBookTypePaymentMethodLinkRequried(string booktype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIffabts.Where(x => x.ActiveStatus && x.BookType == booktype && x.PaymentMethodLinkReq).FirstOrDefault();
                    if(ds != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }  

                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateVoucherType(DO_VoucherType obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.lstInstruments.Count > 0)
                        {
                            foreach (var Itype in obj.lstInstruments)
                            {
                                var _linkExist = db.GtIfbtpms.Where(w => w.BookType == obj.BookType && w.VoucherType == obj.VoucherType && w.InstrumentType == Itype.Key).FirstOrDefault();
                                if (_linkExist != null)
                                {
                                    if (_linkExist.ActiveStatus != Itype.Value)
                                    {
                                        _linkExist.VoucherTypeDesc = obj.VoucherTypeDesc;
                                        _linkExist.ActiveStatus = Itype.Value;
                                        _linkExist.ModifiedBy = obj.UserID;
                                        _linkExist.ModifiedOn = System.DateTime.Now;
                                        _linkExist.ModifiedTerminal = obj.TerminalID;
                                    }

                                }
                                else
                                {
                                    if (Itype.Value)
                                    {
                                        var _link = new GtIfbtpm
                                        {
                                            BookType = obj.BookType,
                                            VoucherType = obj.VoucherType,
                                            VoucherTypeDesc= obj.VoucherTypeDesc,
                                            InstrumentType = Itype.Key,
                                            ActiveStatus = Itype.Value,
                                            CreatedBy = obj.UserID,
                                            FormId=obj.FormID,
                                            CreatedOn = System.DateTime.Now,
                                            CreatedTerminal = obj.TerminalID
                                        };
                                        db.GtIfbtpms.Add(_link);
                                    }

                                }
                            }
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {

                            var Instype = db.GtIfbtpms.Where(w => w.BookType == obj.BookType && w.VoucherType == obj.VoucherType && w.InstrumentType == obj.InstrumentType).FirstOrDefault();
                            if(Instype != null )
                            {
                                Instype.VoucherTypeDesc= obj.VoucherTypeDesc;
                                Instype.ActiveStatus = obj.ActiveStatus;
                                Instype.ModifiedBy = obj.UserID;
                                Instype.ModifiedOn = System.DateTime.Now;
                                Instype.ModifiedTerminal = obj.TerminalID;

                            }
                            else
                            {
                                var _instypes = new GtIfbtpm
                                {
                                    BookType = obj.BookType,
                                    VoucherType = obj.VoucherType,
                                    VoucherTypeDesc=obj.VoucherTypeDesc,
                                    InstrumentType = obj.InstrumentType,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId=obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtIfbtpms.Add(_instypes);
                            }
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
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
    }
}

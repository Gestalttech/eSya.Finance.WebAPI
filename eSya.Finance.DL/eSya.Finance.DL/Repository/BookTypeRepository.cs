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
    public class BookTypeRepository: IBookTypeRepository
    {
        private readonly IStringLocalizer<BookTypeRepository> _localizer;
        public BookTypeRepository(IStringLocalizer<BookTypeRepository> localizer)
        {
            _localizer = localizer;
        }

        public async Task<List<DO_BookType>> GetBookTypes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIffabts
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
        public async Task<List<DO_BookType>> GetBooksbyType(string booktype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIffabts.Where(x=>x.BookType.ToUpper().Replace(" ", "") == booktype.ToUpper().Replace(" ", ""))
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
        public async Task<DO_ReturnParameter> InsertIntoBookType(DO_BookType obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var bktype = db.GtIffabts.Where(w => w.BookType.ToUpper().Replace(" ", "") == obj.BookType.ToUpper().Replace(" ", "")).Count();
                        if (bktype > 0)
                        {

                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00143", Message = string.Format(_localizer[name: "W00143"]) };

                        }

                        bool bkdesc = db.GtIffabts.Any(a => a.BookTypeDesc.ToUpper().Replace(" ", "") == obj.BookTypeDesc.ToUpper().Replace(" ", ""));
                        if (bkdesc)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00144", Message = string.Format(_localizer[name: "W00144"]) };
                        }

                       
                        var bk_type = new GtIffabt
                        {
                            BookType = obj.BookType,
                            BookTypeDesc = obj.BookTypeDesc,
                            PaymentMethodLinkReq = obj.PaymentMethodLinkReq,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtIffabts.Add(bk_type);

                        await db.SaveChangesAsync();
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

        public async Task<DO_ReturnParameter> UpdateBookType(DO_BookType obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool bkdesc = db.GtIffabts.Any(a => a.BookTypeDesc.ToUpper().Replace(" ", "") == obj.BookTypeDesc.ToUpper().Replace(" ", "")
                        && a.BookType.ToUpper().Replace(" ", "") != obj.BookType.ToUpper().Replace(" ", ""));
                        if (bkdesc)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00144", Message = string.Format(_localizer[name: "W00144"]) };
                        }
                        var bktype = db.GtIffabts.Where(w => w.BookType.ToUpper().Replace(" ", "") == obj.BookType.ToUpper().Replace(" ", "")).FirstOrDefault();
                        if(bktype != null)
                        {
                            bktype.BookTypeDesc = obj.BookTypeDesc;
                            bktype.PaymentMethodLinkReq = obj.PaymentMethodLinkReq;
                            bktype.ActiveStatus=obj.ActiveStatus;
                            bktype.ModifiedBy = obj.UserID;
                            bktype.ModifiedOn = System.DateTime.Now;
                            bktype.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }

                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00145", Message = string.Format(_localizer[name: "W00145"]) };

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

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
    public class AccountGroupRepository: IAccountGroupRepository
    {
        private readonly IStringLocalizer<AccountGroupRepository> _localizer;
        public AccountGroupRepository(IStringLocalizer<AccountGroupRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_BookType>> GetActiveBookTypes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIffabts.Where(x => x.ActiveStatus)
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
        public async Task<List<DO_AccountGroup>> GetAccountGroupsforTreeview()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfagdfs
                        .Select(r => new DO_AccountGroup
                        {
                            GroupCode = r.GroupCode,
                            GroupDesc = r.GroupDesc,
                            ParentId = r.ParentId,
                            GroupIndex = r.GroupIndex,
                            NatureOfGroup = r.NatureOfGroup
                        }).OrderBy(o => o.GroupIndex).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoAccountGroup(DO_AccountGroup obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                       IEnumerable<GtIfagdf> ghfn = db.GtIfagdfs.ToList();
                        var lll = ghfn.Where(c => c.ParentId == obj.ParentId).Select(a => a.GroupCode).ToList();
                        List<int> list = new List<int>();
                        foreach (var i in lll)
                        {
                            list.Add(int.Parse(i.Replace(obj.ParentId + ".", "")));
                        }
                        Nullable<int> fnL;
                        if (obj.ParentId.Length == 1)
                        {
                            fnL = ghfn.Where(c => c.ParentId.Length == 1).Select(a => a.GroupIndex).DefaultIfEmpty().Max();
                            //fnL = fnL;
                        }
                        else
                        {
                            fnL = ghfn.Where(c => c.ParentId == obj.ParentId).Select(a => a.GroupIndex).DefaultIfEmpty().Max();
                        }

                        if (fnL == null)
                        {
                            fnL = 0;
                        }
                        var fn = new GtIfagdf
                        {
                            GroupCode = (list.Count() == 0) ? (obj.ParentId + "." + 1) : obj.ParentId + "." + (list.Max() + 1),
                            GroupDesc = obj.GroupDesc,
                            ParentId = obj.ParentId,
                            NatureOfGroup = obj.NatureOfGroup,
                            GroupIndex = (int)(fnL + 1),
                            BookType = obj.BookType,
                            PGeneralLedger = false,
                            PrControlAccount = false,
                            PControlAccount = false,
                            PrGeneralLedger = false,
                            SControlAccount = false,
                            SGeneralLedger = false,
                            JGeneralLedger = false,
                            JControlAccount = false,
                            DnGeneralLedger=false,
                            DnControlAccount=false,
                            CnGeneralLedger=false,
                            CnControlAccount=false,
                            IsIntegrateFa=false,
                            ActiveStatus = obj.ActiveStatus,
                            UsageStatus=false,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtIfagdfs.Add(fn);

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

        public async Task<DO_ReturnParameter> UpdateAccountGroup(DO_AccountGroup obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtIfagdf fn = db.GtIfagdfs.Where(x => x.GroupCode == obj.GroupCode).FirstOrDefault();
                        if (fn != null)
                        {
                            fn.GroupDesc = obj.GroupDesc;
                            fn.BookType = obj.BookType;
                            fn.PControlAccount = obj.PControlAccount;
                            fn.PGeneralLedger = obj.PGeneralLedger;
                            fn.PrGeneralLedger = obj.PrGeneralLedger;
                            fn.PrControlAccount = obj.PrControlAccount;
                            fn.SControlAccount = obj.SControlAccount;
                            fn.SGeneralLedger = obj.SGeneralLedger;
                            fn.CnControlAccount = obj.CnControlAccount;
                            fn.CnGeneralLedger = obj.CnGeneralLedger;
                            fn.DnControlAccount = obj.DnControlAccount;
                            fn.DnGeneralLedger = obj.DnGeneralLedger;
                            fn.JGeneralLedger = obj.JGeneralLedger;
                            fn.JControlAccount = obj.JControlAccount;
                            fn.IsIntegrateFa = obj.IsIntegrateFa;
                            fn.ActiveStatus = obj.ActiveStatus;
                            fn.ModifiedBy = obj.UserID;
                            fn.ModifiedOn = System.DateTime.Now;
                            fn.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }

                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00146", Message = string.Format(_localizer[name: "W00146"]) };

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

        public async Task<DO_ReturnParameter> DeleteAccountGroup(string groupcode)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtIfagdf fn = db.GtIfagdfs.Where(x => x.GroupCode == groupcode).FirstOrDefault();
                        GtIfagdf fnL = db.GtIfagdfs.Where(c => c.GroupCode == groupcode).FirstOrDefault();
                        if (fnL != null)
                        {
                            if (fnL.UsageStatus == false)
                            {
                                db.GtIfagdfs.Remove(fnL);
                                //Update Group Indexes
                                IEnumerable<GtIfagdf> list = db.GtIfagdfs.Where(c => c.ParentId == fnL.ParentId && c.GroupIndex > fnL.GroupIndex);
                                foreach (GtIfagdf obj in list)
                                {
                                    obj.GroupIndex--;
                                }
                            }
                            else
                                //return false;
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W00147", Message = string.Format(_localizer[name: "W00147"]) };

                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00146", Message = string.Format(_localizer[name: "W00146"]) };

                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0005", Message = string.Format(_localizer[name: "S0005"]) };

                        //return true;

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

        public async Task<DO_ReturnParameter> AccountGroupMoveUpDown(string GroupCode, string ParentID, short GroupIndex, bool moveUp)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<GtIfagdf> fnL = db.GtIfagdfs.ToList();
                        if (moveUp == false)
                        {
                            GtIfagdf fng = fnL.Single(c => c.GroupCode == GroupCode);
                            GtIfagdf fng1;

                            if (ParentID.Length == 1)
                            {
                                fng1 = fnL.Where(c => c.ParentId.Length == 1).SingleOrDefault(c => c.GroupIndex == (GroupIndex + 1));
                            }
                            else
                            {
                                fng1 = fnL.Where(c => c.ParentId == ParentID).SingleOrDefault(c => c.GroupIndex == (GroupIndex + 1));
                            }
                            fng.GroupIndex++;
                            if (fng1 != null)
                            {
                                fng1.GroupIndex--;
                            }
                        }
                        else
                        {
                            GtIfagdf fng = fnL.Single(c => c.GroupCode == GroupCode);
                            GtIfagdf fng1;

                            if (ParentID.Length == 1)
                            {
                                fng1 = fnL.Where(c => c.ParentId.Length == 1).SingleOrDefault(c => c.GroupIndex == (GroupIndex - 1));
                            }
                            else
                            {
                                fng1 = fnL.Where(c => c.ParentId == ParentID).SingleOrDefault(c => c.GroupIndex == (GroupIndex - 1));
                            }
                            fng.GroupIndex--;
                            if (fng1 != null)
                            {
                                fng1.GroupIndex++;
                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0006", Message = string.Format(_localizer[name: "S0006"]) };

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

        public async Task<DO_AccountGroup> GetAccountGroupsbyGroupCode(string groupcode)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfagdfs.Where(x=>x.GroupCode==groupcode)
                        .Select(r => new DO_AccountGroup
                        {
                            GroupCode = r.GroupCode,
                            GroupDesc = r.GroupDesc,
                            NatureOfGroup = r.NatureOfGroup,
                            BookType=r.BookType,
                            PControlAccount=r.PControlAccount,
                            PGeneralLedger=r.PGeneralLedger,
                            PrGeneralLedger=r.PrGeneralLedger,
                            PrControlAccount=r.PrControlAccount,
                            SControlAccount=r.SControlAccount,
                            SGeneralLedger=r.SGeneralLedger,
                            CnControlAccount=r.CnControlAccount,
                            CnGeneralLedger=r.CnGeneralLedger,
                            DnControlAccount=r.DnControlAccount,
                            DnGeneralLedger=r.DnGeneralLedger,
                            JGeneralLedger=r.JGeneralLedger,
                            JControlAccount=r.JControlAccount,
                            IsIntegrateFa=r.IsIntegrateFa,
                            ActiveStatus = r.ActiveStatus,
                            ParentId = r.ParentId,
                            GroupIndex = r.GroupIndex,
                        }).FirstOrDefaultAsync();

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

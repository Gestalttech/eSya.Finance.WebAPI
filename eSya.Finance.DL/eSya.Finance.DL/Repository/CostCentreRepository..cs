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
    public class CostCentreRepository: ICostCentreRepository
    {
        private readonly IStringLocalizer<CostCentreRepository> _localizer;
        public CostCentreRepository(IStringLocalizer<CostCentreRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Cost Center
        public async Task<List<DO_CostCenter>> GetCostCenterList()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfcoccs
                        .Select(r => new DO_CostCenter
                        {
                            CostCenterCode = r.CostCenterCode,
                            CostCenterDesc = r.CostCenterDesc,
                            CostCenterClass = r.CostCenterClass,
                            ActiveStatus = r.ActiveStatus,
                            UsageStatus = r.UsageStatus,
                        }).OrderBy(o => o.CostCenterDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> CreateCostCenter(DO_CostCenter obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool bkdesc = db.GtIfcoccs.Any(a => a.CostCenterDesc.ToUpper().Replace(" ", "") == obj.CostCenterDesc.ToUpper().Replace(" ", ""));
                        if (bkdesc)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00151", Message = string.Format(_localizer[name: "W00151"]) };
                        }

                        int maxCCCodes = db.GtIfcoccs.Select(c => c.CostCenterCode).DefaultIfEmpty().Max() + 1;

                        var CCCodes_type = new GtIfcocc
                        {
                            CostCenterCode = maxCCCodes,
                            CostCenterDesc = obj.CostCenterDesc,
                            CostCenterClass = obj.CostCenterClass,
                            UsageStatus = obj.UsageStatus,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.CreatedTerminal
                        };
                        db.GtIfcoccs.Add(CCCodes_type);

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
        public async Task<DO_ReturnParameter> UpdateCostCenter(DO_CostCenter obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtIfcocc ccc = db.GtIfcoccs.Where(x => x.CostCenterCode == obj.CostCenterCode).FirstOrDefault();
                        if (ccc != null)
                        {
                            ccc.CostCenterDesc = obj.CostCenterDesc;
                            ccc.CostCenterClass = obj.CostCenterClass;
                            ccc.ActiveStatus = obj.ActiveStatus;
                            ccc.ModifiedBy = obj.CreatedBy;
                            ccc.ModifiedOn = System.DateTime.Now;
                            ccc.ModifiedTerminal = obj.CreatedTerminal;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00152", Message = string.Format(_localizer[name: "W00152"]) };

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
        public async Task<DO_ReturnParameter> DeleteCostCenter(int CostCenterCode)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtIfcocc fn = db.GtIfcoccs.Where(x => x.CostCenterCode == CostCenterCode).FirstOrDefault();
                        if (fn != null)
                        {
                            if (fn.UsageStatus == false)
                            {
                                db.GtIfcoccs.Remove(fn);
                            }
                            else
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W00153", Message = string.Format(_localizer[name: "W00153"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00152", Message = string.Format(_localizer[name: "W00152"]) };
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0005", Message = string.Format(_localizer[name: "S0005"]) };
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
        #endregion Cost Center

        #region Cost Center Class
        public async Task<List<DO_CostCenterClass>> GetCostCenterClassForComobo()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfcocls.Where(x => x.ActiveStatus)
                        .Select(r => new DO_CostCenterClass
                        {
                            CostCenterClass = r.CostCenterClass,
                            CostClassDesc = r.CostClassDesc
                        }).OrderBy(o => o.CostClassDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_CostCenterClass>> GetCostCenterClass()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfcocls.Where(x => x.ActiveStatus)
                        .Select(r => new DO_CostCenterClass
                        {
                            CostCenterClass = r.CostCenterClass,
                            CostClassDesc = r.CostClassDesc,
                            ActiveStatus = r.ActiveStatus,
                            UsageStatus = r.UsageStatus
                        }).OrderBy(o => o.CostClassDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> CreateCostCenterClass(DO_CostCenterClass obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool bkdesc = db.GtIfcocls.Any(a => a.CostClassDesc.ToUpper().Replace(" ", "") == obj.CostClassDesc.ToUpper().Replace(" ", ""));
                        if (bkdesc)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00154", Message = string.Format(_localizer[name: "W00154"]) };
                        }

                        int maxCCCodes = db.GtIfcocls.Select(c => c.CostCenterClass).DefaultIfEmpty().Max() + 1;

                        var CCCodes = new GtIfcocl
                        {
                            CostCenterClass = maxCCCodes,
                            CostClassDesc = obj.CostClassDesc,
                            UsageStatus = obj.UsageStatus,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.CreatedTerminal
                        };
                        db.GtIfcocls.Add(CCCodes);

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

        public async Task<DO_ReturnParameter> UpdateCostCenterClass(DO_CostCenterClass obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtIfcocl ccc = db.GtIfcocls.Where(x => x.CostCenterClass == obj.CostCenterClass).FirstOrDefault();
                        if (ccc != null)
                        {
                            ccc.CostClassDesc = obj.CostClassDesc;
                            ccc.ActiveStatus = obj.ActiveStatus;
                            ccc.ModifiedBy = obj.CreatedBy;
                            ccc.ModifiedOn = System.DateTime.Now;
                            ccc.ModifiedTerminal = obj.CreatedTerminal;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00155", Message = string.Format(_localizer[name: "W00155"]) };

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

        public async Task<DO_ReturnParameter> DeleteCostCenterClass(int CostCenterClass)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtIfcocl fn = db.GtIfcocls.Where(x => x.CostCenterClass == CostCenterClass).FirstOrDefault();
                        if (fn != null)
                        {
                            if (fn.UsageStatus == false)
                            {
                                db.GtIfcocls.Remove(fn);
                            }
                            else
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W00156", Message = string.Format(_localizer[name: "W00156"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00155", Message = string.Format(_localizer[name: "W00155"]) };
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0005", Message = string.Format(_localizer[name: "S0005"]) };
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

        #endregion Cost Center Class
    }
}

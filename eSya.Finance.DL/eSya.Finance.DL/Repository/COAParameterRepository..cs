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
    public class COAParameterRepository: ICOAParameterRepository
    {
        private readonly IStringLocalizer<COAParameterRepository> _localizer;
        public COAParameterRepository(IStringLocalizer<COAParameterRepository> localizer)
        {
            _localizer = localizer;
        }

        public async Task<List<DO_COAParameter>> GetAccountGLType()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfaspgs.Where(x => x.ActiveStatus)
                        .Select(r => new DO_COAParameter
                        {
                            ParameterID = r.AccountSgltype,
                            ParameterDesc = r.AccountSgldesc,
                            UsageStatus = r.UsageStatus,
                            ActiveStatus = r.ActiveStatus
                        }).OrderBy(o => o.ParameterDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_COAParameter>> GetGLTypeDescription()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfaspgs.Where(x => x.ActiveStatus)
                        .Select(r => new DO_COAParameter
                        {
                            ParameterID = r.AccountSgltype,
                            ParameterDesc = r.AccountSgldesc,
                            UsageStatus = r.UsageStatus,
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertAccountGLType(DO_COAParameter obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var sgltype = db.GtIfaspgs.Where(w => w.AccountSgltype == obj.ParameterID).Count();
                        if (sgltype > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00148", Message = string.Format(_localizer[name: "W00148"]) };
                        }

                        bool bkdesc = db.GtIfaspgs.Any(a => a.AccountSgldesc.ToUpper().Replace(" ", "") == obj.ParameterDesc.ToUpper().Replace(" ", ""));
                        if (bkdesc)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00149", Message = string.Format(_localizer[name: "W00149"]) };
                        }

                        //int maxsgltype = db.GtIfaspgs.Select(c => c.AccountSgltype).DefaultIfEmpty().Max() + 1;

                        var sgl_type = new GtIfaspg
                        {
                            AccountSgltype = obj.ParameterID,
                            AccountSgldesc = obj.ParameterDesc,
                            UsageStatus = obj.UsageStatus,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.CreatedBy,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.CreatedTerminal
                        };
                        db.GtIfaspgs.Add(sgl_type);

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

        public async Task<DO_ReturnParameter> UpdateAccountGLType(DO_COAParameter obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtIfaspg sgl = db.GtIfaspgs.Where(x => x.AccountSgltype == obj.ParameterID).FirstOrDefault();
                        if (sgl != null)
                        {
                            sgl.AccountSgltype = obj.ParameterID;
                            sgl.AccountSgldesc = obj.ParameterDesc;
                            sgl.UsageStatus = obj.UsageStatus;
                            sgl.ActiveStatus = obj.ActiveStatus;
                            sgl.ModifiedBy = obj.CreatedBy;
                            sgl.ModifiedOn = System.DateTime.Now;
                            sgl.ModifiedTerminal = obj.CreatedTerminal;
                            await db.SaveChangesAsync();
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }

                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00148", Message = string.Format(_localizer[name: "W00148"]) };

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

        public async Task<DO_ReturnParameter> DeleteAccountGLType(DO_COAParameter obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtIfaspg fn = db.GtIfaspgs.Where(x => x.AccountSgltype == obj.ParameterID).FirstOrDefault();
                        if (fn != null)
                        {
                            if (fn.UsageStatus == false)
                            {
                                db.GtIfaspgs.Remove(fn);
                            }
                            else
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W00150", Message = string.Format(_localizer[name: "W00150"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W00148", Message = string.Format(_localizer[name: "W00148"]) };
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
    }
}

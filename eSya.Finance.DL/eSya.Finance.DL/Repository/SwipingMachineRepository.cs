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
    public class SwipingMachineRepository: ISwipingMachineRepository
    {
        private readonly IStringLocalizer<SwipingMachineRepository> _localizer;
        public SwipingMachineRepository(IStringLocalizer<SwipingMachineRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Manage Swip Machine
        public async Task<List<DO_SwipingMachine>> GetSwipMachinesbyBusinessKey(int businesskey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtIfswms.Where(x=>x.BusinessKey==businesskey)
                        .Select(r => new DO_SwipingMachine
                        {
                            SwipingMachineId = r.SwipingMachineId,
                            ControlAccountCode = r.ControlAccountCode,
                            SwipingMachineName = r.SwipingMachineName,
                            ActiveStatus = r.ActiveStatus
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateSwipMachine(DO_SwipingMachine obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var swmachine = db.GtIfswms.Where(w => w.BusinessKey == obj.BusinessKey && w.ControlAccountCode.ToUpper().Replace(" ", "") == obj.ControlAccountCode.ToUpper().Replace(" ", "")
                        && w.SwipingMachineId.ToUpper().Replace(" ", "") == obj.SwipingMachineId.ToUpper().Replace(" ", "")).FirstOrDefault();
                        if (swmachine == null)
                        {
                            var machine = new GtIfswm
                            {
                                BusinessKey = obj.BusinessKey,
                                SwipingMachineId = obj.SwipingMachineId,
                                ControlAccountCode = obj.ControlAccountCode,
                                SwipingMachineName = obj.SwipingMachineName,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtIfswms.Add(machine);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            swmachine.SwipingMachineName=obj.SwipingMachineName;
                            swmachine.ActiveStatus=obj.ActiveStatus;
                            swmachine.ModifiedBy = obj.UserID;
                            swmachine.ModifiedOn = System.DateTime.Now;
                            swmachine.ModifiedTerminal = obj.TerminalID;
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

        public async Task<DO_ReturnParameter> DeleteSwipMachine(DO_SwipingMachine obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var swmachine = db.GtIfswms.Where(w => w.BusinessKey == obj.BusinessKey && w.ControlAccountCode.ToUpper().Replace(" ", "") == obj.ControlAccountCode.ToUpper().Replace(" ", "")
                                             && w.SwipingMachineId.ToUpper().Replace(" ", "") == obj.SwipingMachineId.ToUpper().Replace(" ", "")).FirstOrDefault(); 
                        
                            if (swmachine != null)
                            {
                                swmachine.ActiveStatus = obj.status;
                                await db.SaveChangesAsync();
                                dbContext.Commit();
                            }
                            else
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W00151", Message = string.Format(_localizer[name: "W00151"]) };

                            }

                        if (obj.status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
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
        #endregion
    }
}

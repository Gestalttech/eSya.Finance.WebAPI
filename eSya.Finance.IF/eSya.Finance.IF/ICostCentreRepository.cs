using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface ICostCentreRepository
    {
        Task<List<DO_CostCenter>> GetCostCenterList();
        Task<List<DO_CostCenter>> GetCostCenterCodes(int CostCentreCode);
        Task<DO_ReturnParameter> CreateCostCenter(DO_CostCenter obj);
        Task<DO_ReturnParameter> UpdateCostCenter(DO_CostCenter obj);
        Task<DO_ReturnParameter> DeleteCostCenter(int CostCenterCode);

        Task<List<DO_CostCenterClass>> GetCostCenterClassForComobo();
        Task<List<DO_CostCenterClass>> GetCostCenterClass();
        Task<List<DO_CostCenterClass>> GetCostCenterClassByCode(int CostCenterClass);
        Task<DO_ReturnParameter> CreateCostCenterClass(DO_CostCenterClass obj);
        Task<DO_ReturnParameter> UpdateCostCenterClass(DO_CostCenterClass obj);
        Task<DO_ReturnParameter> DeleteCostCenterClass(int CostCenterClass);
    }
}

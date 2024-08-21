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
        Task<DO_ReturnParameter> CreateCostCenter(DO_CostCenter obj);
        Task<DO_ReturnParameter> UpdateCostCenter(DO_CostCenter obj);
        Task<DO_ReturnParameter> DeleteCostCenter(int CostCenterCode);

        Task<List<DO_CostCenterClass>> GetCostCenterClassForComobo();
        Task<List<DO_CostCenterClass>> GetCostCenterClass();
        Task<DO_ReturnParameter> CreateCostCenterClass(DO_CostCenterClass obj);
        Task<DO_ReturnParameter> UpdateCostCenterClass(DO_CostCenterClass obj);
        Task<DO_ReturnParameter> DeleteCostCenterClass(int CostCenterClass);
    }
}

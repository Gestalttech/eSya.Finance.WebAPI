using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface ISwipingMachineRepository
    {
        #region Manage Swip Machine
        Task<List<DO_SwipingMachine>> GetSwipMachinesbyBusinessKey(int businesskey);
        Task<DO_ReturnParameter> InsertOrUpdateSwipMachine(DO_SwipingMachine obj);
        Task<DO_ReturnParameter> DeleteSwipMachine(DO_SwipingMachine obj);
        #endregion
    }
}

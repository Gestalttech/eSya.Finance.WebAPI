using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface IAccountGroupRepository
    {
        Task<List<DO_BookType>> GetActiveBookTypes();
        Task<List<DO_AccountGroup>> GetAccountGroupsforTreeview();
        Task<DO_ReturnParameter> InsertIntoAccountGroup(DO_AccountGroup obj);
        Task<DO_ReturnParameter> UpdateAccountGroup(DO_AccountGroup obj);
        Task<DO_ReturnParameter> DeleteAccountGroup(string groupcode);
        Task<DO_ReturnParameter>  AccountGroupMoveUpDown(string GroupCode, string ParentID, short GroupIndex, bool moveUp);
        Task<DO_AccountGroup> GetAccountGroupsbyGroupCode(string groupcode);
    }
}

using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface ICOAParameterRepository
    {
        Task<List<DO_COAParameter>> GetAccountGLType();
        Task<List<DO_COAParameter>> GetGLTypeDescription();
        Task<DO_ReturnParameter> InsertAccountGLType(DO_COAParameter obj);
        Task<DO_ReturnParameter> UpdateAccountGLType(DO_COAParameter obj);
        Task<DO_ReturnParameter> DeleteAccountGLType(DO_COAParameter obj);
    }
}

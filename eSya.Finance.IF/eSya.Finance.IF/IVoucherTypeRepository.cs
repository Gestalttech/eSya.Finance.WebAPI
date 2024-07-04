using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface IVoucherTypeRepository
    {
        Task<List<DO_BookType>> GetActiveBookTypes();
        Task<List<DO_VoucherType>> GetBookTypePaymentMethods(string booktype, string? vouchertype);
        Task<List<DO_VoucherType>> GetVoucherTypesbyBookType(string booktype);
        Task<bool> ChkBookTypePaymentMethodLinkRequried(string booktype);
        Task<DO_ReturnParameter> InsertOrUpdateVoucherType(DO_VoucherType obj);
    }
}

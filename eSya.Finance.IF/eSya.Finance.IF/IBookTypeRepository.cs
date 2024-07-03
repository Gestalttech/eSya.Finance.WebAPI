using eSya.Finance.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.IF
{
    public interface IBookTypeRepository
    {
        Task<List<DO_BookType>> GetBookTypes();
        Task<List<DO_BookType>> GetBooksbyType(string booktype);
        Task<DO_ReturnParameter> InsertIntoBookType(DO_BookType obj);
        Task<DO_ReturnParameter> UpdateBookType(DO_BookType obj);
    }
}

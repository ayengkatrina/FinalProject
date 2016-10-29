using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEFLibrary;

namespace BusinessLogicLibrary
{
   public class RefCountryManger
    {
        DataAccess<REF_COUNTRY> dataAccess = new DataAccess<REF_COUNTRY>();
    

        public IEnumerable<REF_COUNTRY> GetAllCountry() {
            List<REF_COUNTRY> list = new List<REF_COUNTRY>();

            list = dataAccess.GetAll();

            return list;


        }
    }
}

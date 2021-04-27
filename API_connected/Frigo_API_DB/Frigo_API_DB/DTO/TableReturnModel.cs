using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.DTO
{
    public class TableReturnModel
    {
        public List<Amounts> tableData {get; set;}
        public int minAmount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTestContract.Models
{
    public class Items<T>
    {
        public IList<T> Data { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
    }
}

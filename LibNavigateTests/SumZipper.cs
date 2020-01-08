using LibNavigate.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibNavigate.Data;

namespace LibNavigateTests
{
    public class SumZipper : IZipper<int, int, int>
    {
        public int Zip(LibNavigate.Data.Nullable<int> input1,
            LibNavigate.Data.Nullable<int> input2)
        {
            if(!input1.HasValue)
            {
                return input2.Value;
            }

            if(!input2.HasValue)
            {
                return input1.Value;
            }

            return input1.Value + input2.Value;
        }
    }
}

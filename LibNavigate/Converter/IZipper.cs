using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNavigate.Converter
{
    /// <summary>
    /// Function which combine inputs to create new data type
    /// </summary>
    /// <typeparam name="Input1"></typeparam>
    /// <typeparam name="Input2"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public interface IZipper<Input1,Input2,Output>
    {

        Output Zip(Data.Nullable<Input1> input1,
            Data.Nullable<Input2> input2);
    }
}

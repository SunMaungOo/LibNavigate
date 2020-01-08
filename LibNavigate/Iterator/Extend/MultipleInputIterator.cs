using LibNavigate.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Input iterator which support reading from multiple data source
    /// </summary>
    /// <typeparam name="Input1"></typeparam>
    /// <typeparam name="Input2"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public sealed class MultipleInputIterator<Input1, Input2, Output> : IInputIterator<Output>
    {
        private IInputIterator<Input1> inputIterator1;

        private IInputIterator<Input2> inputIterator2;

        private IZipper<Input1, Input2, Output> zipper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <param name="zipper">Function which create new data type</param>
        public MultipleInputIterator(IInputIterator<Input1> inputIterator1,
            IInputIterator<Input2> inputIterator2,
            IZipper<Input1, Input2, Output> zipper)
        {

            this.inputIterator1 = inputIterator1;

            this.inputIterator2 = inputIterator2;

            this.zipper = zipper;
        }

        public void Begin()
        {
            inputIterator1.Begin();

            inputIterator2.Begin();

        }

        public void Dispose()
        {
            inputIterator1.Dispose();

            inputIterator2.Dispose();
        }

        public void End()
        {
            inputIterator1.End();

            inputIterator2.End();
        }

        public bool IsEnd()
        {
            return inputIterator1.IsEnd() &&
                 inputIterator2.IsEnd();
        }

        public void MoveNext()
        {
            if(!inputIterator1.IsEnd())
            {
                inputIterator1.MoveNext();
            }

            if(!inputIterator2.IsEnd())
            {
                inputIterator2.MoveNext();
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>Output which is create from inputs</returns>
        public Output Read()
        {
            Data.Nullable<Input1> input1 = Data.Nullable<Input1>.Empty();

            if(!inputIterator1.IsEnd())
            {
                input1 = Data.Nullable<Input1>.From(inputIterator1.Read());
            }

            Data.Nullable<Input2> input2 = Data.Nullable<Input2>.Empty();

            if (!inputIterator2.IsEnd())
            {
                input2 = Data.Nullable<Input2>.From(inputIterator2.Read());
            }

            return zipper.Zip(input1,input2);
        }
    }
}

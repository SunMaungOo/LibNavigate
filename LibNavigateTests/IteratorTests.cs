using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using LibNavigate.Iterator.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibNavigateTests
{
    [TestClass()]
    public class IteratorTests
    {
        public object Algorthm { get; private set; }

        [TestMethod()]
        public void RangeIteratorTest()
        {
            int[] data = { 1, 2, 3, 4 };

            int[] output = new int[3];

            using (IInputIterator<int> inputIterator = new RangeInputIterator<int>(data, 3))
            {
                int index = 0;

                while (!inputIterator.IsEnd())
                {
                    output[index] = inputIterator.Read();

                    inputIterator.MoveNext();

                    ++index;
                }

                Assert.IsTrue(output[0] == 1 && output[1] == 2 && output[2] == 3);
            }
        }

        [TestMethod()]
        public void RangeIteratorTest2()
        {
            int[] data = { 1, 2, 3, 4 };

            int[] output = new int[2];

            using (IInputIterator<int> inputIterator = new RangeInputIterator<int>(data, 1,2))
            {
                int index = 0;

                while (!inputIterator.IsEnd())
                {
                    output[index] = inputIterator.Read();

                    inputIterator.MoveNext();

                    ++index;
                }

                Assert.IsTrue(output[0] == 2 && output[1] == 3);
            }
        }

        [TestMethod()]
        public void BackwardInputIteratorTest()
        {
            int[] data = { 1, 2, 3 };

            int[] output = new int[data.Length];

            using (IIntRandomAccessIterator<int> inputIterator = new BackwardInputIterator<int>(data))
            {
                int index = 0;

                while (!inputIterator.IsEnd())
                {
                    output[index] = inputIterator.Read();

                    inputIterator.MoveNext();

                    ++index;
                }

                Assert.IsTrue(output[0]==3 && output[1]==2 && output[2]==1);
            }
        }

        [TestMethod()]
        public void BackwardInputIteratorTest2()
        {
            int[] data = { 1, 2, 2, 3 };

            int[] output = new int[2];

            int removeValue = 2;

            using (IIntRandomAccessIterator<int> inputIterator = new BackwardInputIterator<int>(data))
            {
                int index = 0;

                IRemoveable removeable = inputIterator as IRemoveable;

                while (!inputIterator.IsEnd())
                {
                    //testing if the remove work

                    if(inputIterator.Read().Equals(removeValue))
                    {
                        removeable.Remove();
                    }
                    else
                    {
                        output[index] = inputIterator.Read();

                        ++index;
                    }

                    inputIterator.MoveNext();

                }

                Assert.IsTrue(output[0] == 3 && output[1] == 1);
            }
        }

        [TestMethod()]
        public void InputIteratorAdapterTest()
        {
            int[] data = { 1, 2, 3, 4 };

            string[] output = new string[data.Length];

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IInputIterator<string> adapter = new InputIteratorAdapter<int, string>(inputIterator,
                     new IntToStringConverter()))
                {
                    int index = 0;

                    while(!adapter.IsEnd())
                    {
                        output[index] = adapter.Read();

                        adapter.MoveNext();

                        ++index;
                    }

                    bool isCorrectData = (output[0]=="1" && output[1]=="2" && 
                        output[2]=="3" && output[3]=="4");

                    Assert.IsTrue(isCorrectData);
                }
            }
        }

        [TestMethod()]
        public void OutputIteratorAdapterTest()
        {
            string[] data = { "1", "2", "3", "4" };

            IList<int> lst = new List<int>();

            using (IInputIterator<string> inputIterator = new InputIterator<string>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    using (IOutputIterator<string> adapter = new OutputIteratorAdapter<int, string>(outputIterator,
                        new StringToIntConverter()))
                    {
                        while (!inputIterator.IsEnd())
                        {
                            adapter.Write(inputIterator.Read());

                            inputIterator.MoveNext();
                        }
                    }
                }
            }

            bool isCorrectData = (lst[0] == 1 && lst[1] == 2 &&
                lst[2] == 3 && lst[3] == 4);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void MultiOutputIteratorTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst1 = new List<int>();

            IList<int> lst2 = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator1 = new BackInsertIterator<int>(lst1))
                {
                    using (IOutputIterator<int> outputIterator2 = new BackInsertIterator<int>(lst2))
                    {
                        using (MultiOutputIterator<int> adapter = new MultiOutputIterator<int>())
                        {
                            adapter.Add(outputIterator1);
                            adapter.Add(outputIterator2);

                            while (!inputIterator.IsEnd())
                            {
                                adapter.Write(inputIterator.Read());
                                adapter.MoveNext();

                                inputIterator.MoveNext();
                            }

                        }
                    }
                }
            }

            bool isCorrectData = (lst1[0] == 1 && lst1[1] == 2 && lst1[2] == 3 &&
                lst2[0] == 1 && lst2[1] == 2 && lst2[2] == 3);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void LimitInputIterator()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            int expected = 2;

            using (IInputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                using (IInputIterator<int> limitIterator = new LimitInputIterator<int>(inputIterator, 3))
                {
                    while (!limitIterator.IsEnd())
                    {
                        limitIterator.MoveNext();
                    }

                    ICursor cursor = (ICursor)limitIterator;

                    int actual = cursor.GetPosition();

                    Assert.IsTrue(expected==actual);
                    
                }
            }
        }

        [TestMethod()]
        public void LimitInputIterator2()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            int expected = 0;

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IInputIterator<int> limitIterator = new LimitInputIterator<int>(inputIterator, 1))
                {
                    while (!limitIterator.IsEnd())
                    {
                        limitIterator.MoveNext();
                    }

                    ICursor cursor = (ICursor)limitIterator;

                    int actual = cursor.GetPosition();

                    Assert.IsTrue(expected == actual);

                }
            }
        }

        [TestMethod()]
        public void MultipleInputIteratorTest()
        {
            int[] data1 = { 1, 2 };

            int[] data2 = { 3, 4 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IInputIterator<int> multiInputs = new MultipleInputIterator<int, int, int>(inputIterator1,
                        inputIterator2, new SumZipper()))
                    {
                        using (IOutputIterator<int> output = new BackInsertIterator<int>(lst))
                        {
                            Algorithm.Copy(multiInputs, output);
                        }
                    }
                }
            }

            Assert.IsTrue(lst.Count==2 &&
                lst[0]==4 &&
                lst[1]==6);
        }

        [TestMethod()]
        public void MultipleInputIteratorTest2()
        {
            int[] data1 = { 1, 2 };

            int[] data2 = { 3 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IInputIterator<int> multiInputs = new MultipleInputIterator<int, int, int>(inputIterator1,
                        inputIterator2, new SumZipper()))
                    {
                        using (IOutputIterator<int> output = new BackInsertIterator<int>(lst))
                        {
                            Algorithm.Copy(multiInputs, output);
                        }
                    }
                }
            }

            Assert.IsTrue(lst.Count == 2 &&
                lst[0] == 4 &&
                lst[1] == 2);
        }

        [TestMethod()]
        public void MultipleInputIteratorTest3()
        {
            int[] data1 = { 1 };

            int[] data2 = { 3, 4 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IInputIterator<int> multiInputs = new MultipleInputIterator<int, int, int>(inputIterator1,
                        inputIterator2, new SumZipper()))
                    {
                        using (IOutputIterator<int> output = new BackInsertIterator<int>(lst))
                        {
                            Algorithm.Copy(multiInputs, output);
                        }
                    }
                }
            }

            Assert.IsTrue(lst.Count == 2 &&
                lst[0] == 4 &&
                lst[1] == 4);
        }
    }
}

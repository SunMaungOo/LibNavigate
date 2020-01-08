# LibNavigate

[![Build status](https://ci.appveyor.com/api/projects/status/15wuf5u5amxq1b98/branch/master?svg=true)](https://ci.appveyor.com/project/SunMaungOo/libnavigate-iv9ax/branch/master)

C# port of Standard Template Library (STL)

Attempt on porting C++ STL library to C#.

Please notice some of the algorithm from STL are not ported to C#


Most of your time using the library is in implementation InputIterator ( to get the data from) and the OutputIterator to output the data. Therefore you need to be familiar with the InputIterator type and OutputIterator type.

There are only two namespace (**LibNavigate.Iterator** and **LibNavigate.Iterator.Extend**) you need to know in implementation.**LibNavigate.Iterator** contains the interface of the iterator and **LibNavigate.Iterator.Extend** contains the default implementation of iterator.

```
IIteratorBase = base type of all iterator

IInputIterator = base type of input iterator

IOutputIterator = base type of output iterator

IForwardIterator = base type of iterator which both read and write

IBidirectionalIterator = base type of iterator which goes both way (forward and backward)

IRandomAccessIterator = base type of iterator which can have random access

OutputIterator = generic output iterator

ConsoleOutputIterator = output to console

FileOutputIterator = output to file

MultiOutputIterator = output to multiple source

OutputIteratorAdapter = change output to another type

BackInsertIterator = output to collection

FileInputIterator = read from file and convert data

SimpleFileInputIterator = read from file

InputIteratorAdapter = change input to another type

RangeInputIterator = read data only from certain range

LimitInputIterator = subset the input

InputIterator = default implementation of IInputIterator

ForwardIterator = default implementation of IForwardIterator

BidirectionalIterator = default implementation of IBidirectionalIterator

RandomAccessIterator = default implementation of IRandomAccessIterator
```

**Usage**

```c#
// Copying data from input to output

int[] data = { 1, 2, 3 };

IList<int> lst = new List<int>();

using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
{
   using(IOutputIterator<int> outputIterator=new BackInsertIterator<int>(lst))
   {
      Algorithm.Copy(inputIterator, outputIterator);
   }

}


//Display the data into the Console 

int[] data = { 1, 2, 3 };

using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
{
   using(IOutputIterator<int> outputIterator=new ConsoleOutputIterator<int>())
   {
      Algorithm.Copy(inputIterator, outputIterator);
   }

}
```

**Documentation** 

The best place to find documentation about STL is in https://en.cppreference.com/w/cpp/header/algorithm

**Differences** 

There are some thing what is different from C++ STL.I have made the resource cleanup part explicit. In C++ it is your choice whether you put a destructor or not. In my port, I have add IDisposable interface which force you think about resource management (you can still ignore it if you want).

Since there is no concept of moving (std::move) in C# , I have add an interface IRemoveable which implementation need to remove the current element in the iterator.

For cloning method , there is two interface IShallowClone which does not clone the underlying data and IPartialClone which clone the underlying data.




**Alternative project**

https://sourceforge.net/projects/cstl/

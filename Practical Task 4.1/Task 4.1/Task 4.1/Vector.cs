using System;
using System.Collections;
using System.Collections.Generic;

namespace Vector
{
    public class Vector<T> : IEnumerable<T> where T : IComparable<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array.
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity
        {
            get { return data.Length; }
        }

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection.
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[Capacity + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        public ISorter Sorter { set; get; } = new DefaultSorter();

        internal class DefaultSorter : ISorter
        {
            public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
            {
                if (comparer == null) comparer = Comparer<K>.Default;
                Array.Sort(sequence, comparer);
            }
        }

        public void Sort()
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            Sorter.Sort(data, null);
        }

        public void Sort(IComparer<T> comparer)
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            if (comparer == null) Sorter.Sort(data, null);
            else Sorter.Sort(data, comparer);
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public int Searching(T element, int low, int high, IComparer<T> comparer)
        {
            if (low > high)
            {
                return -1;
            }
            int mid = (low + high) / 2; //finding mid point to compare element to it
            if (comparer.Compare(element, data[mid]) == 0)
            {
                //if element equals mid, found.
                return mid;
            }
            else if (comparer.Compare(element, data[mid]) < 0)
            {
                //if element is smaller then data[mid], cut the right half of list
                high = mid - 1;
            }
            else
            {
                //if element is bigger then data[mid], cut the left half of list
                low = mid + 1;
            }
            //perform next search til element is found
            return Searching(element, low, high, comparer);
        }

        public int BinarySearch(T element)
        {
            IComparer<T> comparer = Comparer<T>.Default;
            return BinarySearch(element, comparer);
        }

        public int BinarySearch(T element, IComparer<T> comparer)
        {
            int low = 0;
            int high = Capacity - 1;
            return Searching(element, low, high, comparer);
        }

        // TODO: Add your methods for implementing the appropriate interface here
        public IEnumerator<T> GetEnumerator() //implements GetEnumerator, which returns a new Iterator
        {
            return new Iterator(this);
        }

        private IEnumerator GetEnumerator1() //implement Iemuerable.getenumerator
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        // TODO: Add an Iterator as an inner class here
        //https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-5.0
        //https://programmingwithmosh.com/net/ienumerable-and-ienumerator/#:~:text=IEnumerable%20interface%20has%20only%20a,how%20the%20List%20is%20enumerated. Updated Link
        private class Iterator : IEnumerator<T>
        {
            private Vector<T> myVector;
            private int _currentIndex = -1;
            public Iterator(Vector<T> vector)
            {
                myVector = vector;
            }
            public bool MoveNext()
            {
                _currentIndex++;
                return (_currentIndex < myVector.Count);
            }
            void IDisposable.Dispose()
            {
            }
            public void Reset()
            {
                _currentIndex = -1;
            }
            public T Current
            {
                get
                {
                    try
                    {
                        return myVector[_currentIndex];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        return default(T);
                    }
                }
            }
            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
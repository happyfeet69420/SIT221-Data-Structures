using System;
using System.Text;

namespace Vector
{
    public class Vector<T>
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
        public int Capacity { get; private set; } = 0;

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
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
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

        // TODO:********************************************************************************************
        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public void Insert(int index, T element)
        {
            if (Count == Capacity)
            {
                ExtendData(DEFAULT_CAPACITY);//Calls ExtendData to increase capacity. Then copies all the elements from old array into a new array.
            }

            //Check if the index is equal to count, and if it is, then element will be added to the end
            if (index == Count)
            {
                Add(element);
                return;
            }

            //Check if a element is being insterted outside of the vector. If it is, throws an exception
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index is out of range: " + index);
            }

            
            int count = Count;
            while (count != index)//Loop that shuffle all elements from the end of the vector to the insert location
            {
                data[count] = data[count - 1];//Shuffle
                count--;//Goes Back
            }
            data[count] = element;//Insert element
            Count++;//Increase vector due to insert of element
        }

        public void Clear()
        {
            Array.Clear(data, 0, Count); //Call Clear() to remove all elements from the array.
            Count = 0; //Sets Count back to 0 from property.
        }

        public bool Contains(T element)
        {
            //Checks if element is in the vector which returns true or false
            return IndexOf(element) != -1;
        }

        public bool Remove(T element)
        {
            int index = IndexOf(element);//Variable to find index of a element.

            //Check if its a valid index
            if (index > -1)
            {
                RemoveAt(index);//Calls RemoveAt method to remove that index
                return true;
            }
            return false;//has to return has method has return type
        }

        public void RemoveAt(int index)
        {
            //Check if the index is within the range of the array.
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index is out of range: " + index);
            }

            //Removes the element of an index from the vector
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
                data[Count] = data[Count - 1];
            }
            Count--;//Decrease Count due to remove of element
        }

        public override string ToString()
        {
            StringBuilder stringBuild = new("[", 50);//Instance of Stringbuilder

            //Looping through to add comma to each element
            for (var i = 0; i < Count; i++)
            {
                stringBuild.Append(data[i]);//Calls append to string

                if (i != Count - 1) stringBuild.Append(',');//Check if it the last element, if not, append
            }
            stringBuild.Append(']');
            return stringBuild.ToString();//has to return has method has return type
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    //Constructors
    class BubbleSort : ISorter
    {
        //Constructors
        public BubbleSort()
        {

        }

        //Methods
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //Outer Loop: iterating through the array. Looping through 1 less than the length because we comparing the number after the current number.
            //Thats why we don't go to the last 
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                //Inner Loop: i is the current position that was solved from the right.
                //adds i not to go the last element as the last element was already moved there.
                for (int j = i + 1; j < sequence.Length; j++) 
                {
                    if (comparer.Compare(sequence[i], sequence[j]) > 0)//Calls Compare to compare the elements at index i and j, and check if they need to swapped
                    {
                        //Swapping
                        K temp = sequence[i];
                        sequence[i] = sequence[j];
                        sequence[j] = temp;
                    }
                }
            }
        }
    }
}

//https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-3.php reference code

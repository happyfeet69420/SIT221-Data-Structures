using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class SelectionSort : ISorter
    {
        //Constructors
        public SelectionSort()
        {

        }

        //Methods
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //Going through the array of index
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                //Set variable to min index
                int minIndex = i;

                //Comparing the index to the right of i
                for (int j = i + 1; j < sequence.Length; j++)
                {
                    if (comparer.Compare(sequence[minIndex], sequence[j]) > 0)
                    {
                        minIndex = j;
                    }
                }
                //Swapping. Bring smallest number to the front of the array.
                K temp = sequence[i];
                sequence[i] = sequence[minIndex];
                sequence[minIndex] = temp;
            }
        }
    }
}

//https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-11.php reference code

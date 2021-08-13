using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class InsertionSort : ISorter
    {
        //Constructors
        public InsertionSort()
        {

        }

        //Methods
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            //Going through the array of index
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                //Starts at second index as we're going backwards, index + 1 every loop. Keeping comparing elements until j < 0. Since we're going backwards, we're decreasing the counter.
                for (int j = i + 1; j > 0; j--)
                {
                    //Compareing the element to the left of the current elment
                    if (comparer.Compare(sequence[j - 1], sequence[j]) > 0)
                    {
                        //Swapping
                        K temp = sequence[j - 1];
                        sequence[j - 1] = sequence[j];
                        sequence[j] = temp;
                    }
                }
            }
            
        } 
    }
}

//https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-6.php reference code

// 2, 5, 8, 3, 6, 7
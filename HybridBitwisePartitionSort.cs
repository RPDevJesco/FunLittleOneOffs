namespace HybridBitwisePartitionSortingAlgorithm
{
    /// <summary>
    /// Divide the Array: Divide the array into two halves recursively until each subarray contains only one element.
    /// Bitwise Partitioning: During the partition phase, use a bitwise operation to partition the elements based on their most significant bit (MSB).
    /// Create two temporary arrays, one for elements with the MSB set to 0 and another for elements with the MSB set to 1.
    /// Iterate through the elements of the two halves, and based on the MSB, place them in the corresponding temporary array.
    /// Recursive Sorting: Recursively apply the same process to the two halves of the array, considering the next significant bit in each recursion.
    /// Concatenation: Once all bits have been considered, concatenate the two arrays to form a single sorted array.
    /// Stitching: To ensure the array is fully sorted, stitch the array by iterating through it and swapping elements that are out of order.
    /// This step is necessary because the bitwise partitioning might not fully sort the elements within each partition.
    /// The stitching process involves a single pass through the array, checking adjacent elements and swapping them if they are in the wrong order.
    /// O(n * b) Time complexity (maybe?)
    /// </summary>
    public class HybridBitwisePartitionSort
    {
        // Entry method for the hybrid sort
        public static void Sort(int[] array)
        {
            Sort(array, 0, array.Length - 1, sizeof(int) * 8 - 1);
            Stitch(array); // Stitching step to ensure the array is fully sorted
        }

        // Recursive sort method
        private static void Sort(int[] array, int left, int right, int bit)
        {
            if (left >= right || bit < 0)
            {
                return;
            }

            List<int> zeroBits = new List<int>();
            List<int> oneBits = new List<int>();

            for (int i = left; i <= right; i++)
            {
                if ((array[i] & (1 << bit)) == 0)
                {
                    zeroBits.Add(array[i]);
                }
                else
                {
                    oneBits.Add(array[i]);
                }
            }

            int k = left;
            foreach (int num in zeroBits)
            {
                array[k++] = num;
            }
            int mid = k - 1; // This is the new partition point
            foreach (int num in oneBits)
            {
                array[k++] = num;
            }

            Sort(array, left, mid, bit - 1);
            Sort(array, mid + 1, right, bit - 1);
        }

        // Stitching step to ensure the array is fully sorted
        private static void Stitch(int[] array)
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        Swap(ref array[i - 1], ref array[i]);
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        
        // Utility method to swap two elements
        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
		
		public static void TestHybridBitwiseSort()
        {
            int[] array = { 34, 7, 23, 32, 5, 62 };
            Sort(array);
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
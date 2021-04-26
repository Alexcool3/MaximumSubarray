using System;
using System.Diagnostics;

namespace Maximum_Subarray
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 10000000;
            int[] A = RandomIntArray(size, 0, int.MaxValue);

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //FindMaximumSubarray(A, 0, A.Length-1);
            //Kadane(A);
            //stopwatch.Stop();
            //Console.WriteLine($"elapsed Time:{stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"FindMaximumSubarray:{FindMaximumSubarray(A, 0, A.Length-1)}");
            Console.WriteLine($"MasSubArraySum:{maxSubArraySum(A, 0, A.Length-1)}");;
            Console.WriteLine($"Kadane Other All Negative:{maxSubArraySum(A, A.Length)}");
            Console.WriteLine($"Kadane:{Kadane(A)}");
            Console.ReadKey();
            
        }

        public static int[] RandomIntArray(int size, int minimum = int.MinValue, int maximum = int.MaxValue)
        {
            int[] A = new int[size];

            Random random = new Random();

            for (int i = 0; i < A.Length; i++)
            {
                A[i] = random.Next(minimum, maximum);
            }

            return A;
        }

        public static int Kadane(int[] A)
        {
            int local_max = 0;
            int global_max = int.MinValue;

            for (int i = 0; i < A.Length; i++)
            {
                local_max = Math.Max(A[i], A[i] + local_max);
                if (local_max > global_max)
                {
                    global_max = local_max;
                }
            }

            return global_max;
        }

        // Kadane's other.
        static int maxSubArraySum(int[] a, int size)
        {
            int max_so_far = a[0];
            int curr_max = a[0];

            for (int i = 1; i < size; i++)
            {
                curr_max = Math.Max(a[i], curr_max + a[i]);
                max_so_far = Math.Max(max_so_far, curr_max);
            }

            return max_so_far;
        }

        public static (int, int, int) FindMaxCrossingSubarray(int[] A, int low, int mid, int high)
        {
            int left_sum = int.MinValue;
            int sum = 0;
            int max_left = 0;
            for (int i = mid; i >= low; i--)
            {
                sum += A[i];
                if (sum > left_sum)
                {
                    left_sum = sum;
                    max_left = i;
                }
            }

            int right_sum = int.MinValue;
            sum = 0;
            int max_right = 0;
            for (int j = mid+1; j <= high; j++)
            {
                sum += A[j];
                if (sum > right_sum)
                {
                    right_sum = sum;
                    max_right = j;
                }
            }

            return (max_left, max_right, Math.Max(left_sum + right_sum, Math.Max(left_sum, right_sum)));
        }

        public static (int, int, int) FindMaximumSubarray(int[] A, int low, int high)
        {
            if (high == low) 
            {
                return (low, high, A[low]);
            }
            else
            {
                int mid = (low + high) / 2;

                (int left_low, int left_high, int left_sum) = FindMaximumSubarray(A, low, mid);
                (int right_low, int right_high, int right_sum) = FindMaximumSubarray(A, mid + 1, high);
                (int cross_low, int cross_high, int cross_sum) = FindMaxCrossingSubarray(A, low, mid, high);
                if (left_sum >= right_sum && left_sum >= cross_sum)
                {
                    return (left_low, left_high, left_sum);
                }
                else if (right_sum >= left_sum && right_sum >= cross_sum)
                {
                    return (right_low, right_high, right_sum);
                }
                else
                {
                    return (cross_low, cross_high, cross_sum);
                }
            }
        }
        
        static int maxCrossingSum(int[] arr, int l, int m, int h)
        {
            // Include elements on left of mid.
            int sum = 0;
            int left_sum = int.MinValue;
            for (int i = m; i >= l; i--)
            {
                sum = sum + arr[i];
                if (sum > left_sum)
                    left_sum = sum;
            }

            // Include elements on right of mid
            sum = 0;
            int right_sum = int.MinValue;
            ;
            for (int i = m + 1; i <= h; i++)
            {
                sum = sum + arr[i];
                if (sum > right_sum)
                    right_sum = sum;
            }

            // Return sum of elements on left
            // and right of mid
            // returning only left_sum + right_sum will fail for
            // [-2, 1]
            return Math.Max(left_sum + right_sum,
                            Math.Max(left_sum, right_sum));
        }

        // Returns sum of maxium sum subarray
        // in aa[l..h]
        static int maxSubArraySum(int[] arr, int l, int h)
        {

            // Base Case: Only one element
            if (l == h)
                return arr[l];

            // Find middle point
            int m = (l + h) / 2;

            /* Return maximum of following three
            possible cases:
            a) Maximum subarray sum in left half
            b) Maximum subarray sum in right half
            c) Maximum subarray sum such that the
            subarray crosses the midpoint */
            return Math.Max(
                Math.Max(maxSubArraySum(arr, l, m),
                         maxSubArraySum(arr, m + 1, h)),
                maxCrossingSum(arr, l, m, h));
        }
    }
}

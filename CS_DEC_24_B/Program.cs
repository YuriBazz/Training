public class Program
{
    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }

    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var nums = ReadInts();
        int leftSum = 0, rightSum = 0, l = 0, r = n - 1;
        while (l <= r)
        {
            if (leftSum <= rightSum)
            {
                leftSum += nums[l];
                l++;
            }
            else
            {
                rightSum += nums[r];
                r--;
            }
        }

        if (leftSum != rightSum)
        {
            Console.WriteLine(-1);
        }
        else
        {
            var left = string.Join('+', nums.Take(l));
            var right = string.Join('+', nums.Skip(l));
            Console.WriteLine($"{left}={right}");
        }
    }
}
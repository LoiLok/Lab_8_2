using System;
using System.Globalization;
using System.Reflection.Metadata;

class Program
{
    public static void Main()
    {
        string[] text = File.ReadAllLines("file.txt");
        int startSum = int.Parse(text[0]);
        string[] sumOutStart = SumWithoutStart(text);
        string[][] timeSumOp = TimeSumOperation(sumOutStart);
        long[] time = new long[timeSumOp.Length];
        for (int i = 0; i < timeSumOp.Length; i++)
        {
            time[i] = Time(timeSumOp[i][0]);
        }
        string [][] sorted = Sort(time, timeSumOp);
        bool correct = Correct(sorted, startSum);
        if (correct)
        {
            UserParameters(sorted, startSum);
        }
        else
        {
            Console.WriteLine("Файл некорректен");
        }
     
    }
    public static string[] SumWithoutStart(string[] sum)
    {
        string[] sumOutStart = new string[sum.Length - 1];
        for (int i = 0; i < sumOutStart.Length; i++)
        {
            sumOutStart[i] = sum[i + 1];
        }
        return sumOutStart;
    }
    public static string[][] TimeSumOperation(string[] sumOutStart)
    {

        string[][] timeSumOp = new string[sumOutStart.Length][];
        for (int i = 0; i < sumOutStart.Length; i++)
        {
            timeSumOp[i] = new string[3];
            string[] operation = sumOutStart[i].Replace(" | ", "|").Split('|');
            for (int j = 0; j < operation.Length; j++)
                timeSumOp[i][j] = operation[j];
        }
        return timeSumOp;
    }
    public static long Time(string time)
    {
        time = time.Replace("-", "").Replace(" ", "").Replace(":", "");
        long allTime = long.Parse(time);
        return allTime;
    }
    public static string[][] Sort(long[] time, string[][] sort)
    {
        int[] array = new int[time.Length];
        for (int i = 0; i < time.Length; i++)
        {
            int index = Array.IndexOf(time, time.Min());
            array[i] = index;
            time[index] = 999999999999;
        }
        string[][] sorted = new string[sort.Length][];
        for (int i = 0; i < array.Length; i++)
        {
            int k = array[i];
            sorted[i] = new string [sort[k].Length];
            sorted[i] = sort[k];
        }
        return sorted;
    }
    public static int EndSum(string[][] sum, int startSum)
    {
        for(int i = 0; i < sum.Length; i++)
        {
            if (sum[i][1] == "revert")
            {
                if (sum[i-1][2] == "in")
                {
                    startSum -= int.Parse(sum[i - 1][1]);
                }
                else if (sum[i - 1][2] == "out")
                {
                    startSum += int.Parse(sum[i - 1][1]);
                }
            }
            else
            {
                if (sum[i][2] == "in")
                {
                    startSum += int.Parse(sum[i][1]);
                }
                else if (sum[i][2] == "out")
                {
                    startSum -= int.Parse(sum[i][1]);
                }
            }
        }
        return startSum;
    }
    public static int SumInTime(string[][] sum, long time, int startSum)
    {
        for (int i = 0; i < sum.Length; i++)
        {
            long allTime = Time(sum[i][0]);
            if (allTime <= time)
            {
                if (sum[i][1] == "revert")
                {
                    if (sum[i - 1][2] == "in")
                    {
                        startSum -= int.Parse(sum[i - 1][1]);
                    }
                    else if (sum[i - 1][2] == "out")
                    {
                        startSum += int.Parse(sum[i - 1][1]);
                    }
                }
                else
                {
                    if (sum[i][2] == "in")
                    {
                        startSum += int.Parse(sum[i][1]);
                    }
                    else if (sum[i][2] == "out")
                    {
                        startSum -= int.Parse(sum[i][1]);
                    }
                }
            }
            else
            {
                return startSum;
            }
        }
        return startSum;
    }
    public static bool Correct(string[][] sum, int startSum)
    {
        for (int i = 0; i < sum.Length; i++)
        {
            if (sum[i][1] == "revert")
            {
                if (i == 0)
                {
                    return false;
                }
                else if (sum[i-1][1] == "revert")
                {
                    return false;
                }
                else
                {
                    if (sum[i - 1][2] == "in")
                    {
                        startSum -= Convert.ToInt32(sum[i - 1][1]);
                    }
                    else
                    {
                        startSum += Convert.ToInt32(sum[i - 1][1]);
                    }
                }
            }
            else
            {
                if (sum[i][2] == "in")
                {
                    startSum += Convert.ToInt32(sum[i][1]);
                }
                else
                {
                    startSum -= Convert.ToInt32(sum[i][1]);
                }
                if (startSum < 0)
                {
                    return false;
                }
                
            }
        }
        return true;
    }
    public static void UserParameters(string[][] sum, int startSum)
    {
        string firstTime = sum[0][0];
        long first = Time(firstTime);
        Console.WriteLine("Введите дату и время в формате: YYYY-MM-DD HH:MM");
        Console.WriteLine("Нажмите Enter, чтобы узнать итоговый остаток средств");
        string date = Console.ReadLine();
        if(date == "")
        {
            Console.WriteLine(EndSum(sum, startSum));
        }
        else
        {
            long userDate = Time(date);
            Console.WriteLine(SumInTime(sum, Time(date),startSum));
        }
    }
}
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

//namespace W4Hackerrank
class Result
{

    /*
     * Complete the 'staircase' function below.
     *
     * The function accepts INTEGER n as parameter.
     */

    public static void staircase(int n)
    {
        string result = "";
        for (int i = 0; i<n; i++)
        {
            result += "#";
            Console.WriteLine(result.PadLeft(n,' '));
        }
    }

}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter n length: ");
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Result.staircase(n);
    }
}
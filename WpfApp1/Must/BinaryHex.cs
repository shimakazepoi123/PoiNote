using System;
using System.Collections.Generic;

namespace WpfApp1.Must
{
    internal static class BinaryHex
    {
        /// <summary>
        /// 十进制转换为十六进制
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string TenToHex(int i)
        {
            // 设置堆栈
            Stack<string> stack = new Stack<string>();
            string hex = String.Empty;
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>()
            {
                {10,"A" },
                {11,"B"},
                {12,"C" },
                {13,"D" },
                {14,"E" },
                {15,"F" }
            };

            string CheckTen(int r)
            {
                string value = "";
                try
                {
                    value = keyValuePairs[r];
                }
                catch
                {

                }
                return value;
            }

            while (true)
            {
                if (i / 16 != 0)
                {
                    int mod1 = (i % 16);
                    i = i / 16;
                    string value = CheckTen(mod1) == "" ? mod1.ToString() : CheckTen(mod1);
                    stack.Push(value);
                }
                else
                {
                    string value = CheckTen(i) == "" ? i.ToString() : CheckTen(i);
                    stack.Push(value);
                    break;
                }
            }

            while (stack.Count > 0)
            {
                hex += stack.Pop().ToString();
            }

            return hex;
        }

        /// <summary>
        /// 十进制转为二进制
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string TenToBinary(int i)
        {
            int r;
            string result = string.Empty;
            Stack<int> stack = new Stack<int>();
            if (i == 0 || i == 1)
            {
                return i.ToString();
            }
            else
            {
                while (i != 1)
                {
                    r = (byte)(i % 2);
                    i = i / 2;
                    stack.Push(r);
                }
                stack.Push(1);

                while (stack.Count > 0)
                {
                    result += stack.Pop().ToString();
                }

                return result;
            }
        }
    }
}

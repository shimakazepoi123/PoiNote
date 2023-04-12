using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApp1.Must.StringCheck
{
    /// <summary>
    /// 编码检测模块
    /// </summary>
    internal static class StringCode
    {
        /// <summary>
        /// 检测编码为utf-8或者gbk
        /// </summary>
        public static Encoding CheckCode(byte[] fileBytes)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] fileBytes1 = fileBytes.Take(10).ToArray();
            // 存储提取出来的字节内容
            List<byte> lines = new List<byte>();
            // UTF-8三字节不同字节开头位数，汉字默认为三字节
            string[] utfStart = { "1110", "10", "10" };
            // 将非ASCII字节添加到数组中
            foreach (byte b in fileBytes1)
            {
                if (b < 128) continue;
                else
                {
                    if (lines.Count < 3) lines.Add(b);
                }
            }

            int i = -1;
            // 此处默认，utf-8编码下汉字为3字节，gbk2312汉字编码为2字节。因此，使用3字节的编码方式，将获取的字节尝试与utf-8 3字节的编码开头对应
            // 如“朱”字【假设在utf-8环境下】
            // 其在文件中，读取到字节为E69CB1，转换为2进制为1110 0110 1001 1100 1011 0001，符合UTF-8汉字三字节的开头，因此为UTF-8格式
            while (i < lines.Count - 1)
            {
                i = i + 1;
                if (BinaryHex.TenToBinary(lines[i]).Substring(0, utfStart[i].Length) == utfStart[i]) continue;
                else break;
            }
            if (i == 2) return Encoding.UTF8;
            else return Encoding.GetEncoding("GBK");

        }

    }
}

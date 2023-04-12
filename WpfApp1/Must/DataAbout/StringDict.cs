using System;
using System.Collections.Generic;

namespace WpfApp1.Data.DataAbout
{
    /// <summary>
    /// 用于StringDict存储格式的产生，其中包括StrinmgDict的相关接口，提供显式转化为 List<Dictionary<string, string>>的
    /// </summary>
    public class StringDict
    {
        // 创建列表，该列表为StringDict的存储
        protected List<Dictionary<string, string>> List2 = new List<Dictionary<string, string>>();

        public override string ToString() { 
            return List2.ToString();
        }

        /// <summary>
        /// 显式转换，即(List<Dictionary<string, string>>) StringDict
        /// </summary>
        /// <param name="stringDict"></param>
        public static explicit operator List<Dictionary<string, string>> (StringDict stringDict)
        {
            return stringDict.List2;
        }

        /// <summary>
        /// 传入字典时的用法
        /// </summary>
        /// <param name="keyValuePair"></param>
        public void StringAdd(Dictionary<string, string> keyValuePair)
        {
            List2.Add(keyValuePair);
        }

        /// <summary>
        /// 更新列表中的字典，如果没有，自动添加新的字典到列表中
        /// </summary>
        public void StringUpdate(Tuple<string, string> tuple,string seach)
        {
            foreach (Dictionary<string,string> i in List2)
            {
                try
                {
                    if (i[tuple.Item1] == seach)
                    {
                        i[tuple.Item1] = tuple.Item2;
                        break;
                    }
                }
                catch
                {
                    i.Add(tuple.Item1, tuple.Item2);
                }

            }
        }

        /// <summary>
        /// 指定搜索条件【V1】，默认标题，之后添加【V2和V3】
        /// </summary>
        /// <param name="tuple"></param>
        public void StringUpdate(ValueTuple<string, string, string> tuple)
        {
            foreach (Dictionary<string, string> i in List2)
            {
                if (i["Title"] == tuple.Item1)
                {
                    try
                    {
                        i.Add(tuple.Item2, tuple.Item3);
                    }
                    catch
                    {
                        i[tuple.Item2] = tuple.Item3; 
                    }
                    break;
                }
                else
                {
                    continue;
                }

            }
        }

        /// <summary>
        /// 删除字典，根据所在顺序进行删除
        /// </summary>
        public void StringDelete(int num) => List2.RemoveAt(num);

        /// <summary>
        /// 删除字典，根据键值对删除
        /// </summary>
        /// <param name="tuple"> 需要寻找的数据所在的键值对 </param>
        public void StringDelete(Tuple<string,string> tuple)
        {
            foreach (Dictionary<string, string> i in List2)
            {
                if (i[tuple.Item1] == tuple.Item2)
                {
                    List2.Remove(i);
                }
            }
        }

        /// <summary>
        /// 检测指定的字典中（依据文献标题名），指定的键是否存在，通过遍历方式,尝试将键对应值赋予变量
        /// </summary>
        /// <returns></returns>
        public bool StringExit(string DictTitle, string key = null)
        {
            bool a = false;
            foreach (Dictionary<string, string> i in List2)
            {
                try
                {
                    // 在第一个模式下，默认文献已经存在，仅是找对应的其他键值对
                    if (key != null)
                    {
                        if (i["Title"] == DictTitle){
                            string b = i[key];  // 键值对不存在会跳转到catch部分
                            a = true;
                            break;
                        }
                    }
                    // 第二个模式检测文献是否重复（文献存在未知）
                    else if (key == null) {

                        if (i["Title"] == DictTitle) {
                            a = true;
                            break;

                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
            return a;
        }

        /// <summary>
        /// 根据Tilte寻找Dictionary的索引值
        /// </summary>
        /// <param name="DictTitle"></param>
        /// <returns></returns>
        public int StringIndex(string DictTitle)
        {
            int i = 0;
            foreach (Dictionary<string,string> keyValuePairs in List2)
            {
                if (keyValuePairs["Title"] == DictTitle)
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }
            i = -1;
            return i;
        }
    }

}


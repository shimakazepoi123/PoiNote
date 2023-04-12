using System;
using System.Linq;

namespace WpfApp1.Must.MindingMap
{
    public struct TreeNode
    {
        public int parent; // 节点的父节点所在的序号
        public int[] child; //节点的子节点所在的序号
        public string data;
    };
    public struct Tree
    {
        public int size;
        public TreeNode[] Node;
    };
    public class TreeStruct
    {
        public TreeStruct()
        {
            Tree tree = new Tree();
            tree.size = 0;
            Tree1 = tree;
        }
        private Tree Tree1;

        // 添加数据
        public void Add(string dataInput)
        {
            TreeNode node = new TreeNode()
            {
                data = dataInput,
                parent = 0,
            };
            Tree1.Node.Append(node);
            Tree1.size += 1;
        }

        // 插入数据
        public void Insert(string dataInput, int index)
        {
            if (index > Tree1.size)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                TreeNode node = new TreeNode()
                {
                    data = dataInput,
                    parent = 0,
                };
                int indexCopy = index;
                while (indexCopy < Tree1.size)
                {
                    TreeNode node1 = Tree1.Node[indexCopy + 1];
                    Tree1.Node[indexCopy + 1] = node;
                    indexCopy += 1;
                };
                Tree1.size += 1;
                Tree1.Node = Tree1.Node.Take(Tree1.size).ToArray();
            }
        }

        // 移除数据
        public void Remove(int index) 
        {
            int indexCopy = index;
            while (indexCopy < Tree1.size) 
            {
                TreeNode node = Tree1.Node[indexCopy+1];
                Tree1.Node[indexCopy] = node;
                indexCopy += 1;
            };
            Tree1.size -= 1;
            Tree1.Node = Tree1.Node.Take(Tree1.size).ToArray();
        }

        // 清除整棵树
        public void Clear()
        {
            Tree1.Node = null;
            Tree1.size = 0;
        }

        public TreeNode Pop()
        {
            return Tree1.Node[Tree1.size - 1];
        }

        /// <summary>
        /// 获取树中某一节点的数据，发生错误则返回null。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        // 报错返回null
        public string GetData(int index)
        {
            try{return Tree1.Node[index].data;}
            catch { return null; }
        }

    }
}

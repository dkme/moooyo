using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.CheckHelper
{
    /// 邱志明
    /// 2012/02/20 
    /// 用于装载过滤字词的类
    public class TreeNode
    {
        
        private const int NODE_LEN = 256;
       
        //词组是否结束
        private Boolean end = false;

        private List<TreeNode> subNodes = new List<TreeNode>(NODE_LEN);

        public TreeNode()
        {
            for (int i = 0; i < NODE_LEN; i++)
            {
                subNodes.Add(null);
            }
        }

       /// <summary>
       /// 向指定位置添加节点
       /// </summary>
       /// <param name="index">位置索引</param>
       /// <param name="node">节点实例</param>
        public void setSubNode(int index, TreeNode node)
        {
            subNodes[index] = node;
        }

        /// <summary>
        /// 获取指定位置的树节点
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public TreeNode getSubNode(int index)
        {
            return subNodes[index];
        }


        public Boolean isKeywordEnd()
        {
            return end;
        }

        public void setKeywordEnd(Boolean end)
        {
            this.end = end;
        }
    }

   
}

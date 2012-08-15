using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.CheckHelper
{
    //邱志明
    //2012-02-20
    //关键字过滤类
    public class FilterWord
    {
        //创建字词缓存
        private ByteBuffer _wordBuffer = new ByteBuffer(1024);
        //字词编码默认为GBK
        private string charSet = "utf-8";
        //替换字符，默认用菊花号
        private string shiftChar = "*";
        //匹配关键字树
        private TreeNode rootNode;

        public FilterWord(List<string> wordlist)
        {
            CreateWordTree(wordlist);
        }

        /// <summary>
        /// 替换字符
        /// </summary>
        public string ShiftChar
        {
            get { return shiftChar; }
            set { shiftChar = value; }
        }

        /// <summary>
        /// 字词编码
        /// </summary>
        public string CharSet
        {
            get { return charSet; }
            set { charSet = value; }
        }

        /// <summary>
        /// 创建过滤字词Byte树形
        /// </summary>
        /// <param name="wordlist">过滤字词List集合</param>
        /// <returns></returns>
        private void CreateWordTree(List<string> wordlist)
        {
            //创建根节点
            rootNode = new TreeNode();
            foreach (String word in wordlist)
            {
                if (word == null)
                    continue;
                byte[] word_bytes = Encoding.GetEncoding(CharSet).GetBytes(word.Trim());
                TreeNode tempNode = rootNode;

                for (int i = 0; i < word_bytes.Length; i++)
                {
                    int index = word_bytes[i] & 0xff;
                    TreeNode node = tempNode.getSubNode(index);

                    if (null == node)
                    {
                        node = new TreeNode();
                        tempNode.setSubNode(index, node);
                    }
                    //临时树为最后插入到节点中的树
                    tempNode = node;
                    if (i + 1 == word_bytes.Length)
                    {
                        tempNode.setKeywordEnd(true);
                    }
                }
            }
        }

        /// <summary>
        /// 搜索文本中需要过滤的字符
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="wt">过滤类型</param>
        /// <returns>过滤字词集合</returns>
        public List<string> SearchWord(ref string text, word_type wt)
        {
            if (text.Trim().Length <= 0)
                return null;
            if (wt == word_type.delete)
            {
                return Search(ref text, word_type.delete);
            }
            else if (wt == word_type.shift)
            {
                return Search(ref text, word_type.shift);
            }
            else
            {
                return Search(ref text, word_type.trial);
            }
        }

        private List<String> Search(ref string text, word_type wt)
        {
            List<string> result_list = new List<string>();  //过滤词列表
            byte[] bytes = Encoding.GetEncoding(CharSet).GetBytes(text.Trim()); //转换过滤文本
            if (null == bytes || bytes.Length == 0)
            {
                return null;
            }
            TreeNode tempNode = rootNode;        //临时树等于整个关键字树
            int rollback = 0;                    //记录匹配树节点个数
            int position = 0;                   //过滤文本下标
            int s_postion = 0;                  //遇到关键字时开始的树节点

            while (position < bytes.Length)
            {
                int index = bytes[position] & 0xff;
                _wordBuffer.WriteB(bytes[position]);
                tempNode = tempNode.getSubNode(index);           //查找节点

                if (tempNode == null)
                {
                    position = position - rollback;
                    rollback = 0;
                    tempNode = rootNode;
                    _wordBuffer = new ByteBuffer(1024);
                    s_postion = 0;
                }
                else if (tempNode.isKeywordEnd())             //如果达到节点末尾
                {
                    if (rollback == 0)
                        s_postion = position;
                    _wordBuffer.Flip();
                    string word = Encoding.GetEncoding(CharSet).GetString(_wordBuffer.Array());
                    _wordBuffer.Limit(_wordBuffer.Limit());
                    result_list.Add(word);
                    tempNode = rootNode;
                    if (wt == word_type.shift) //转换操作
                    {
                        byte shift_word = Encoding.GetEncoding(CharSet).GetBytes(ShiftChar)[0];
                        s_postion = (s_postion == 1) ? 0 : s_postion;
                        for (; s_postion <= position; s_postion++)
                        {

                            bytes[s_postion] = shift_word;

                        }
                    }
                    else if (wt == word_type.delete) //删除操作
                    {
                        bytes = getbyte(bytes, s_postion, position);
                        if (position >= rollback)
                            position = position - rollback;
                        rollback = 0;
                        //position = s_postion;
                        continue;
                    }
                    else
                    {
                        //不作修改
                    }

                    rollback = 0;	//遇到结束点  rollback 置为1
                    s_postion = 0;
                }
                else
                {
                    if (rollback == 0)
                        s_postion = position;
                    rollback++;
                }

                position++;
            }
            text = Encoding.GetEncoding(CharSet).GetString(bytes);
            return result_list;
        }

        /// <summary>
        /// 删除数组中的字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private byte[] getbyte(byte[] bytes, int start, int end)
        {
            int length = bytes.Length;
            if (start != end)
                length = length - (end + 1 - start);
            else
                length = length - 1;
            byte[] tempbyte = new byte[length];

            int y = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i < start || i > end)
                {
                    tempbyte[y] = bytes[i];
                    y++;
                }
            }
            string texts = Encoding.GetEncoding(CharSet).GetString(bytes);
            string text = Encoding.GetEncoding(CharSet).GetString(tempbyte);
            return tempbyte;
        }

        /// <summary>
        /// 字符过滤类型枚举
        /// </summary>
        public enum word_type
        {
            /// <summary>
            /// 删除
            /// </summary>
            delete = 1,
            /// <summary>
            /// 替换
            /// </summary>
            shift = 2,
            /// <summary>
            /// 待审
            /// </summary>
            trial = 3
        }
    }
}

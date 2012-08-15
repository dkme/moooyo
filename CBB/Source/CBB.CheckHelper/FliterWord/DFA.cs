using System;
using CBB.CheckHelper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CBB.CheckHelper
{
    /**
     * @author 徐良永
     * @Date   2011-10-13 上午9:23:43
     */
    public class DFA {
	
	    /**
	     * 根节点
	     */
	    private TreeNode rootNode = new TreeNode();
	
	    /**
	     * 关键词缓存
	     */
	    private ByteBuffer keywordBuffer = new ByteBuffer(1024);
	
	    /**
	     * 关键词编码
	     */
	    private String charset = "GBK";

	    /**
	     * 创建DFA
	     * @param keywordList
	     * @throws UnsupportedEncodingException 
	     */
	    public void createKeywordTree(List<String> keywordList)
        {
		    foreach (String keyword in keywordList) 
            {
			    if(keyword == null) continue;
                byte[] bytes = System.Text.Encoding.GetEncoding(charset).GetBytes(keyword.Trim());
			
			    TreeNode tempNode = rootNode;
			    //循环每个字节
			    for (int i = 0; i < bytes.Length; i++) {
				    int index = bytes[i] & 0xff; //字符转换成数字
				    TreeNode node = tempNode.getSubNode(index);
				
				    if(node == null){ //没初始化
					    node = new TreeNode();
					    tempNode.setSubNode(index, node);
				    }
				
				    tempNode = node;
				
				    if(i == bytes.Length - 1){
					    tempNode.setKeywordEnd(true);	 //关键词结束， 设置结束标志
				    }
			    }//end for
		    }//end for
		
	    }
	
	    /**
	     * 搜索关键字
	     */
	    public String searchKeyword(String text)
        {
            return searchKeyword(System.Text.Encoding.GetEncoding(charset).GetBytes(text));
	    }
	
	    /**
	     * 搜索关键字
	     */
	    public String searchKeyword(byte[] bytes){
		    StringBuilder words = new StringBuilder();
		
		    if(bytes == null || bytes.Length == 0)
            {
			    return words.ToString();
		    }
		
		    TreeNode tempNode = rootNode;
		    int rollback = 0;	//回滚数
		    int position = 0; //当前比较的位置
		
		    while (position < bytes.Length) 
            {
			    int index = bytes[position] & 0xFF;
			    keywordBuffer.WriteB(bytes[position]);	//写关键词缓存
			    tempNode = tempNode.getSubNode(index);
			
			    //当前位置的匹配结束
			    if(tempNode == null)
                { 
				    position = position - rollback; //回退 并测试下一个字节
				    rollback = 0;
				    tempNode = rootNode;  	//状态机复位
				    keywordBuffer=new ByteBuffer(1024);	//清空
			    }
			    else if(tempNode.isKeywordEnd())
                {  //是结束点 记录关键词
				    keywordBuffer.Flip();
				    String keyword = System.Text.Encoding.GetEncoding(charset).GetString(keywordBuffer.Array());
				    keywordBuffer.Limit(keywordBuffer.Limit());

                    if (words.Length == 0) words.Append(keyword);
                    else words.Append(":").Append(keyword);
                    tempNode = rootNode;
				    rollback = 1;	//遇到结束点  rollback 置为1
			    }else{	
				    rollback++;	//非结束点 回退数加1
			    }
			
			    position++;
		    }
		
		    return words.ToString();
	    }
	
	    public void setCharset(String charset) {
		    this.charset = charset;
	    }
    }
   
}

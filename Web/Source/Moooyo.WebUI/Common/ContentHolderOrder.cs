using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Common
{
    //用于首页排列内容装载器顺序的类
    //每个时间段的位置为一个4*n的位置空间，横排为4格，纵排按内容多少不同
    public class HolderOrder
    {
        private List<List<string>> holerPlace = new List<List<string>>();
        private const int columNum = 4;

        //获取下一个有足够空格的行，行满了之后，生成一个空行
        private List<string> GetRowHasEmptyPlace(int spaceWidth)
        {
            //第一个空行
            if (holerPlace.Count == 0)
                holerPlace.Add(new List<string>());

            foreach (List<string> row in holerPlace)
            {
                if (holerPlace.Count+spaceWidth > 4) continue;
                return row;
            }
            
            //没有检测到现有的空行，则增加一个空行
            List<string> newrow = new List<string>();
            holerPlace.Add(newrow);
            return newrow;
        }

        //增加内容id和所占的位置
        public void AddContent(string id, int spaceWidth)
        {
            //获取一个有足够位置的行
            List<string> row = GetRowHasEmptyPlace(spaceWidth);

            for (int i = 0; i < spaceWidth; i++)
            {
                row.Add(id);
            }
        }
        //获取已经排序的顺序ID
        public IList<string> GetOrderedContentIDs()
        {
            //检测已返回的结果
            Dictionary<string,string> alreadyAdd = new Dictionary<string,string>();
            //已排序的结果集
            List<string> orderedList = new List<string>();
            
            foreach (List<string> column in holerPlace)
            {
                foreach (string id in column)
                {
                    if (alreadyAdd.ContainsKey(id)) continue;
                    alreadyAdd.Add(id, id);

                    orderedList.Add(id);
                }
            }
            
            return orderedList;
        }
    }
}
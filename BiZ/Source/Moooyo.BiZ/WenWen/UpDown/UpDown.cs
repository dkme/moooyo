using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.UpDown
{
    public class UpDown
    {
        public int UpCount;
        public int DownCount;

        public UpDown(int upcount, int downcount) { UpCount = upcount; DownCount = downcount; }

        private void Up() { UpCount++; }
        private void Down() { DownCount++; }
        public void Update(bool upordown)
        {
            if (upordown) Up();
            else
                Down();
        }
    }
}

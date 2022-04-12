using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect2.Common
{
    class RandData
    {
        static string[] last = { "김", "이", "박", "최" };
        static string[] mid = { "설", "석", "준", "연" };
        static string[] first = { "아", "유", "민", "인" };
        static int[] age = { 10, 20, 30, 40, 50 };
        static string[] addr = { "대구 달서구", "대구 중구", "대구 북구", "대구 남구" };

        static Random r = new Random();
        
        public static string getName()
        {
            string fullName = last[r.Next(0, last.Length)] + 
                mid[r.Next(0, mid.Length)] +
                first[r.Next(0, first.Length)];
            return fullName;
        }
        public static int getAge()
        {
            return age[r.Next(0, age.Length)];
        }
        public static string getAddr()
        {
            return addr[r.Next(0, addr.Length)];
        }

    }
}

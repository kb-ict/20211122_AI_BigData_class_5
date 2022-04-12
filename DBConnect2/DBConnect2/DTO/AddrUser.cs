using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect2.DTO
{
    class AddrUser
    {
        private int id;
        private string name;
        private int age;
        private string addr;

        // 생성자
        public AddrUser(string name, int age, string addr)
        {
            this.name = name;
            this.age = age;
            this.addr = addr;
        }
        public AddrUser(int id, string name, int age, string addr)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.addr = addr;
        }

        // getter, setter
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Addr { get => addr; set => addr = value; }

        

        public override string ToString() // AddrUser 재정의
        {
            string data = "이름: " + name + "\n";
            data += "나이: " + age + "\n";
            data += "주소: " + addr;
            return data;
        }
    }
}

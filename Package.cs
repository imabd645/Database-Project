using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class Package
    {
        public int id;
        public string name;
        public string company;
        public int price;
        public string description;

        public Package(int id,string name="",string company="",int price=0,string description="")
        {
            this.id = id;
            this.name = name;
            this.company = company;
            this.price = price;
            this.description = description;
        }

        public void Add_Package()
        {

        }
    }
}

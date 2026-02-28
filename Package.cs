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
            string query = $"INSERT INTO VALUES('{name}','{company}','{price}','{description}')";
            DatabaseHelper.Instance.Update(query);

        }
        public void Load_Package()
        {
            name = this.Get_Details("name");
            company = this.Get_Details("company");
            price = int.Parse(this.Get_Details("price"));
            description = this.Get_Details("description");
        }
        public string Get_Details(string x)
        {
            string query = $"SELECT * FROM packages WHERE id='{id}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                return reader[x].ToString();
            }
            return "";
        }
        public void Show_Packages()
        {
            string query = $"SELECT * FROM packages";
            var reader=DatabaseHelper.Instance.getData(query);
            Console.WriteLine($"{"ID",-5}{"Name",-15}{"Company",-15}{"Price",-10}{"Description"}");
            bool any = false;
            while(reader.Read())
            {
                Console.WriteLine($"{id,-5}{name,-15}{company,-15}{price,-10}{description}");
                any = true;
            }
            if(!any)
            {
                Console.Clear();
                Console.WriteLine("No Packages available right now");
            }
        }
    }
}

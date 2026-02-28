using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class Bill
    {

        public string id;
        public float amount;
        public string type;
        public string company;


        public Bill(string id,float amount=0.0f,string type="",string company="")
        {
            this.id = id;
            this.amount = amount;
            this.type = type;
            this.company = company;
        }

        public void Add_Bill()
        {
            string query = $"INSERT INTO bills VALUES('{id}','{amount}','{type}','{company}')";
            DatabaseHelper.Instance.Update(query);
        }
        public void Show_Bills()
        {
            string query = "SELECT * FROM bills";
            var reader = DatabaseHelper.Instance.getData(query);

            bool any = false;
            Console.WriteLine($"{"Bill ID",-20}{"Bill Amount",-20}{"Type",-20}{"Company",-20}");
            while(reader.Read())
            {
                any = true;
                Console.WriteLine($"{reader["id"],-20}{reader["amount"],-20}{reader["type"],-20}{reader["company"],-20}");
            }
            if(!any)
            {
                Console.WriteLine("No bills Added Yet");
            }
            
           
        }
        public void Load_Bill()
        {
            amount = float.Parse(this.Get_Details("amount"));
            type = this.Get_Details("type");
            company = this.Get_Details("company");

        }
        public bool Exist()
        {
            string query = $"SELECT id FROM bills WHERE id='{id}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                return true;
            }
            return false;
        }
        public void Show_bill()
        {
            string query = $"SELECT * FROM bills WHERE id='{id}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                Console.WriteLine($"{"Bill ID",-20}{"Bill Amount",-20}{"Type",-20}{"Company",-20}");
                Console.WriteLine($"{reader["id"],-20}{reader["amount"],-20}{reader["type"],-20}{reader["company"],-20}");
            }
            
        }
        public string Get_Details(string x)
        {
            string query = $"SELECT * FROM bills WHERE id='{id}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                return reader[x].ToString();
            }
            return "";
        }
    }
}

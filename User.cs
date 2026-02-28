using Mysqlx.Crud;
using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class User
    {
        public string account;
        public string name;
        public string pin;
        public bool active;
        public float balance;
        private int activeValue;


        public User(string account,string name="",string pin="",bool active=true,float balance=0.0f)
        {
            this.account = account;
            this.name = name;
            this.pin = pin;
            this.active = active;
            this.balance = balance;
            activeValue = active ? 1 : 0;
        }

        public void Load_User()
        {
            this.account = this.Get_Details("account");
            this.name = this.Get_Details("name");
            this.pin = this.Get_Details("pin");
            this.active = bool.Parse(this.Get_Details("active"));
            this.balance = float.Parse(this.Get_Details("balance"));
        }

        public void Activate()
        {
            string query = $"UPADTE users SET active=1 WHERE account='{account}'";
            DatabaseHelper.Instance.Update(query);
        }
        public void Deactivate()
        {
            string query = $"UPADTE users SET active=0 WHERE account='{account}'";
            DatabaseHelper.Instance.Update(query);
        }


        public void Create()
        {
            
            string query = $"INSERT INTO users VALUES('{account}','{name}','{pin}',{activeValue},{balance})";
            DatabaseHelper.Instance.Update(query);
        }
        public void Modify()
        { 
            string query = $"UPDATE users SET name='{name}',pin='{pin}',active='{activeValue}',balance='{balance}' WHERE account='{account}'";
            DatabaseHelper.Instance.Update(query);
        }
        public void Update_Balance(float amount)
        {
           
            string query = $"UPDATE users SET balance='{amount}' WHERE account='{account}'";
            DatabaseHelper.Instance.Update(query);
        }
        public void View_details()
        {
            string query = $"SELECT * FROM users WHERE account='{account}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                Console.WriteLine($"Account Number: {reader["account"]}");
                Console.WriteLine($"Account Holder Name: {reader["name"]}");
                Console.WriteLine($"Balance: {reader["balance"]}");
                Console.WriteLine($"Account Status: ({((bool)reader["active"] ? "Active" : "Block")})");
            }

        }
        public string  Get_Details(string x)
        {
            string query = $"SELECT * FROM users WHERE account='{account}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if(reader.Read())
            {
                return reader[x].ToString();
            }
            return "";
        }

        public bool Exists()
        {
            string query = $"SELECT account FROM users WHERE account='{account}'";
            var reader = DatabaseHelper.Instance.getData(query);
            if (reader.Read())
            {
                return true;
            }
            return false;

        }
        public void Change_Pin()
        {
            string query = $"UPDATE users SET pin='{pin}' WHERE account='{account}'";
            DatabaseHelper.Instance.Update(query);
        }

        public void Show_Transaction()
        {
            string query = $"SELECT * from transactions WHERE fromacc='{account}' OR toacc='{account}'";
            var reader = DatabaseHelper.Instance.getData(query);
          
            Console.WriteLine($"{"Sender",-15}{"Reciever",-15}{"Sender Name",-20}{"Reciever Name",-20}{"Type",-10}{"Amount",-10}{"Time"}");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["fromacc"],-15}{reader["toacc"],-15}{reader["fromname"],-20}{reader["toname"],-20}{reader["type"],-10}{reader["amount"],-10}{reader["time"]}");
            }
            
        }
        public void Search_Transaction(string date)
        {
            
            DateTime dt = DateTime.Parse(date);
            string mysqlDate = dt.ToString("yyyy-MM-dd"); 

            string query = $"SELECT * from transactions WHERE (fromacc='{account}' OR toacc='{account}') AND DATE(time) = '{mysqlDate}'";

            var reader = DatabaseHelper.Instance.getData(query);

            bool any = false;
            Console.WriteLine($"{"Sender",-15}{"Reciever",-15}{"Sender Name",-20}{"Reciever Name",-20}{"Type",-10}{"Amount",-10}{"Time"}");

            while (reader.Read())
            {
                any = true;
                Console.WriteLine($"{reader["fromacc"],-15}{reader["toacc"],-15}{reader["fromname"],-20}{reader["toname"],-20}{reader["type"],-10}{reader["amount"],-10}{reader["time"]}");
            }

            if (!any)
            {
                Console.WriteLine("No Transactions for this date");
            }
        }
        public void Search_By_Name(string rec)
        {
            string query = $"SELECT * FROM transactions WHERE toname LIKE'{rec}'";
            var reader = DatabaseHelper.Instance.getData(query);

            bool any = false;
            Console.WriteLine($"{"Sender",-15}{"Reciever",-15}{"Sender Name",-20}{"Reciever Name",-20}{"Type",-10}{"Amount",-10}{"Time"}");

            while (reader.Read())
            {
                any = true;
                Console.WriteLine($"{reader["fromacc"],-15}{reader["toacc"],-15}{reader["fromname"],-20}{reader["toname"],-20}{reader["type"],-10}{reader["amount"],-10}{reader["time"]}");
            }

            if (!any)
            {
                Console.WriteLine("No Transactions for this date");
            }

        }

    }
}

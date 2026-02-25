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
            string query = $"UPDATE users SET balance='{balance+amount}' WHERE account='{account}'";
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
            string query = $"SELECT * FROM users WHERE account='{account}'";
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

    }
}

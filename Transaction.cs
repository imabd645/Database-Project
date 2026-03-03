using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class Transaction
    {

        public string fromacc;
        public string toacc;
        public string fromname;
        public string toname;
        public string type;
        public float amount;
        public string time;

        public Transaction(string fromacc,string toacc,string fromname="",string toname="",string type="",float amount=0.0f)
        {
            this.fromacc = fromacc;
            this.toacc = toacc;
            this.fromname = fromname;
            this.toname = toname;
            this.type = type;
            this.amount = amount;
            time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
        }
        
        public void Add_Transaction()
        {
            string query = $"INSERT INTO transactions VALUES('{fromacc}','{toacc}','{fromname}','{toname}','{type}','{amount}','{time}')";
            DatabaseHelper.Instance.Update(query);
        }

        public void Show_Transactions()
        {
            string query = $"SELECT * from transactions ";
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

            string query = $"SELECT * from transactions WHERE  DATE(time) = '{mysqlDate}'";

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

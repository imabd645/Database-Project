using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}

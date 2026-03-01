using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Database_Project
{
    internal class Program
    {

        static User current_user = null;
        static void Main(string[] args)
        {

            bool isrunning = true;
            while(isrunning)
            {
                Console.Clear();
                Main_Menu();
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Admin_Code();
                        break;
                    case 2:
                        break;
                    case 3:
                        User_Code();
                        Pause();
                        break;
                    case 4:
                        isrunning = false;
                        break;


                }

            }

           
           
        }

        static bool Check_Account(string account)
        {
            if(account.Length!=11)
            {
                Console.WriteLine("Account Number should be 11 digits");
                return false;
            }
            if (account[0]!='0' || account[1]!='3')
            {
                Console.WriteLine("Account Number Should be in (03xxxx) format");
                return false;
            }
            return true;

        }
        static void Main_Menu()
        {
            Console.WriteLine("======EasyPaisa======");
            Console.WriteLine("1)Admin Login");
            Console.WriteLine("2)Manager Login");
            Console.WriteLine("3)User Login");
            Console.WriteLine("4)Exit");
        }
        static void User_Menu()
        {
            Console.WriteLine("====User Menu=====");
            Console.WriteLine("1)Money Transfer");
            Console.WriteLine("2)Bill Payment");
            Console.WriteLine("3)Mobile Load & Packages");
            Console.WriteLine("4)My Account");
            Console.WriteLine("5)Help Center");
            Console.WriteLine("6)Logout");
        }
        static void User_Code()
        {
            bool is_running = true;
            Console.Clear();
            if (User_Login())
            {
                Pause();
                while (is_running)
                {
                    Console.Clear();
                    User_Menu();
                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Money_Transfer();
                            Pause();
                            break;
                        case 2:
                            Bill_Payment();
                            Pause();
                            break;
                        case 3:
                            Mobile_Load();
                            Pause();
                            break;
                        case 4:
                            MyAccount_Code();
                            break;
                        case 5:
                            Help_Center();
                            break;
                        case 6:
                            is_running = false;
                            break;



                    }


                }
            }

        }

        static void Admin_Code()
        {

        }
        static void Admin_Login()
        static bool User_Login()
        {
            Console.Write("Enter the Account Number: ");
            string acc= Console.ReadLine();
            if (!Check_Account(acc)) return false ;
            Console.Write("Enter the Pin: ");
            string pin = Console.ReadLine();
            User user = new User(acc);
            if (user.Exists())
            {
                user.Load_User();
                if (user.active)
                {
                    if (pin == user.Get_Details("pin"))
                    {
                        Console.WriteLine("Login Successful");
                        current_user = user;
                        current_user.Load_User();
                        return true;
                    }
                    else
                    {
                        Console.Write("Incorrect Pin");
                    }

                }
                else
                {
                    Console.WriteLine("Your Account is Blocked");
                }
            }
            else
            {
                Console.WriteLine("Invalid Account Number");
            }
            return false;


        }

        static void MyAccount_Menu()
        {
            Console.WriteLine("1)change Pin");
            Console.WriteLine("2)View Transaction History");
            Console.WriteLine("3)View Account Details");
            Console.WriteLine("4)Search Transaction by date");
            Console.WriteLine("5)Search Tranasctions by Reciver Name");
            Console.WriteLine("6)Back");
        }
        static void MyAccount_Code()
        {
            bool isrunning = true;
            
                while (isrunning)
                {
                    Console.Clear();



                    MyAccount_Menu();
                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Change_Pin();
                            Pause();
                            break;
                        case 2:
                            current_user.Show_Transaction();
                            Pause();
                            break;
                        case 3:
                        Console.Clear();
                            current_user.View_details();
                            Pause();
                            break;
                        case 4:
                            Search_Tranasctions();
                            Pause();
                            break;
                        case 5:
                            Search_By_Name();
                            Pause();
                            break;
                        case 6:
                            isrunning = false;
                            break;

                    }

                }
            
        }
        static void Pause()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.Clear();
        }
        static void Money_Transfer()
        {
            Console.Clear();
            Console.Write("Enter the Reciever number: ");
            string acc = Console.ReadLine();
            if (!Check_Account(acc)) return;
            User rec = new User(acc);
            if (rec.Exists())
            {
                rec.Load_User();
                if (rec.active)
                {
                    Console.WriteLine($"Reciever Name: {rec.name}");
                    Console.Write("Enter the amount to transfer: ");
                    float amount = float.Parse(Console.ReadLine());
                    if (amount > current_user.balance)
                    {
                        Console.WriteLine("Insuffiecint funds");
                        return;
                    }
                    if (amount <= 0)
                    {
                        Console.WriteLine("Amount should be greater than zero!");
                        return;
                    }
                    Console.Write("Enter your pin to Confirm Transaction");
                    string pin = Console.ReadLine();
                    if (pin != current_user.pin)
                    {
                        Console.WriteLine("Incorrect Pin!");
                        return;
                    }
                    current_user.Update_Balance(current_user.balance - amount);
                    rec.Update_Balance(rec.balance + amount);
                    Console.WriteLine("Transaction Successful");
                    Transaction t = new Transaction(current_user.account, rec.account, current_user.name, rec.name, "Transfer", amount);
                    t.Add_Transaction();
                }
                else
                {
                    Console.WriteLine("The Reciever Account is blocked");
                    return;
                }

                
            }
            else
            {
                Console.WriteLine("Invalid Account Number!");
            }
        }
        static void Bill_Payment()
        {
            current_user.Load_User();
            Console.Clear();
            Console.Write("Enter the Bill ID: ");
            string id = Console.ReadLine();
            Bill bill = new Bill(id);
            if (bill.Exist())
            {
               
                bill.Show_bill();
                bill.Load_Bill();
                Console.Write("Enter pin to confirm transaction: ");
                string pin = Console.ReadLine();
                if (pin == current_user.pin)
                {
                  
                    if (current_user.balance >= bill.amount)
                    { 
                        current_user.Update_Balance(current_user.balance - bill.amount);
                        Transaction t = new Transaction(current_user.account, bill.company, current_user.name, bill.company, bill.type, bill.amount);
                        t.Add_Transaction();
                        Console.WriteLine("Bill Paid Successfully");
                    
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect pin!");
                }

            }
            else
            {
                Console.WriteLine("Invalid Bill ID");
            }

        }

        static void Search_Tranasctions()
        {
            Console.Clear();
            Console.Write("Enter date to search Tranasctions (2026-2-27): ");
            string date = Console.ReadLine();
            current_user.Load_User();
            current_user.Search_Transaction(date);
        }

        static void Search_By_Name()
        {
            Console.Clear();
            Console.Write("Enter the Reciver Name: ");
            string name = Console.ReadLine();
            current_user.Load_User();
            current_user.Search_By_Name(name);

        }
        static void Mobile_Load()
        {
            Console.Clear();
            Package p = new Package(0);
            p.Show_Packages();
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());
            p = new Package(choice);
            p.Load_Package();
            current_user.Load_User();
            Console.Clear();
            p.Show_Package();
            Console.Write("\n\nEnter the Mobile Number: ");
            string acc = Console.ReadLine();
            Console.Write("Enter pin to Confirm: ");
            string pin = Console.ReadLine();
            if(pin==current_user.pin)
            {
                if(p.price<=current_user.balance)
                {
                    current_user.Update_Balance(current_user.balance - p.price);
                    Transaction t = new Transaction(current_user.account, acc, current_user.name, p.company, "Pacakge", p.price);
                    t.Add_Transaction();
                    Console.WriteLine("Package Subscribed sucessfuly");
                }
                else
                {
                    Console.WriteLine("Insufficient funds");
                }
            }
            else
            {
                Console.WriteLine("Incorrect Pin");
            }
        }
        static void Create_User()
        {
            Console.Write("Enter the account number: ");
            string acc = Console.ReadLine();
            Console.Write("Enter the account holder name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter initial balnce: ");
            float balance = float.Parse(Console.ReadLine());
            User user = new User(acc, name, pin, true, balance);
            user.Create();
            Console.WriteLine("Account Created Successfully!");
        }
        static void Modify_User()
        {
            Console.Write("Enter the account number: ");
            string acc = Console.ReadLine();
            Console.Write("Enter the account holder name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter initial balnce: ");
            float balance = float.Parse(Console.ReadLine());
            User user = new User(acc, name, pin, true, balance);
            user.Modify();
            Console.WriteLine("Account Modified Successfully!");
        }
        static void Update_Balance()
        {
            Console.Write("Enter the account number: ");
            string acc = Console.ReadLine();
            Console.Write("Enter the new balance: ");
            float balance = float.Parse(Console.ReadLine());
            User user = new User(acc,  active:true, balance:balance);
            user.Update_Balance(balance);


        }
        static void Help_Center()
        {
            Console.Clear();
            Console.WriteLine("Help Center");
            Console.WriteLine("Call 3737 for more Help");
            Console.WriteLine("Email at help@easypaisa.com");
        }
        static void Change_Pin()
        {
            Console.Write("Enter the the old pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter the new pin: ");
            string newpin = Console.ReadLine();
          
            string confirm = current_user.Get_Details("pin");
            if(confirm==pin)
            {
                current_user.pin = newpin;
                current_user.Change_Pin();
                Console.WriteLine("Pin Changed Successfully");

            }
            else
            {
                Console.WriteLine("Inavlid Current Pin");
            }
           


        }
    }
}

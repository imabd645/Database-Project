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
           
            User_Code();
           
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
            Console.WriteLine("3)My Account");
            Console.WriteLine("4)Help Center");
            Console.WriteLine("5)Logout");
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
                            break;
                        case 2:
                            break;
                        case 3:
                            MyAccount_Code();
                            break;
                        case 4:
                            Help_Center();
                            break;
                        case 5:
                            is_running = false;
                            break;



                    }


                }
            }

        }

        static bool User_Login()
        {
            Console.Write("Enter the Account Number: ");
            string acc= Console.ReadLine();
            Console.Write("Enter the Pin: ");
            string pin = Console.ReadLine();
            User user = new User(acc);
            if(user.Exists())
            {
                if(pin==user.Get_Details("pin"))
                {
                    Console.WriteLine("Login Successful");
                    current_user = user;
                    return true;
                }
                else
                {
                    Console.Write("Incorrect Pin");
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
            Console.WriteLine("4)Back");
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
                            break;
                        case 3:
                            current_user.View_details();
                            Pause();
                            break;
                        case 4:
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

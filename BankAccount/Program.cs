using Newtonsoft.Json;
using System;


namespace BankAccount
{
    
    class Program
    {
        static void SaveAccount(Account account)
        {
            string json = JsonConvert.SerializeObject(account);
            File.AppendAllText("account.json", json + Environment.NewLine);
        }
        static Account LoadAccount(string accountId)
        {
            using (StreamReader file = File.OpenText("account.json"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    Account account = JsonConvert.DeserializeObject<Account>(line);
                    if (account.AccountId == accountId)
                    {
                        return account;
                    }
                }
            }
            return null;
        }

        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            string filePath = "account.json";

            try
            {
                // Create the file
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong :(");
            }

            Console.WriteLine("Welcome To Jhangra Bank");
            Console.WriteLine("Tell us what do you want\nEnter 0 for Creating an Account\nEnter 1 for login to your account");
            Console.Write("Your choice : ");
            int operationChoice = -1;
            try
            {
                operationChoice = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            if(operationChoice == 0)
            {
                Console.Write("Please enter your name : ");
                string name = Console.ReadLine();
                Console.Write("Please enter initial amount you want to deposit : ");
                double initialAmount = double.Parse(Console.ReadLine());
                Account account = new Account(name, initialAmount);
                SaveAccount(account);
                Console.WriteLine("Your account has been successfully created");
                Console.WriteLine("Your account id is : {0} you must save it for later use", account.AccountId);
                Console.WriteLine("Thank you for signing up !!");
            }else if(operationChoice == 1)
            {
                Console.Write("Please enter your account id : ");
                string accountId = Console.ReadLine();
                Account loadedAccount = LoadAccount(accountId);
                if(loadedAccount != null)
                {
                    Console.WriteLine("Tell us what do you want to do\nEnter 0 for Withdrawing from your account\nEnter 1 for Deposit in your account");
                    Console.Write("Your choice : ");
                    int actionChoice = -1;
                    try
                    {
                        actionChoice = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid choice");
                        return;
                    }
                    switch (actionChoice)
                    {
                        case 0:
                            Console.Write("Please enter the amount you want to withdraw : ");
                            double withdrawalAmount = double.Parse(Console.ReadLine());
                            if(withdrawalAmount > loadedAccount.CurrentBalance)
                            {
                                Console.WriteLine("Insufficient funds !!!");
                                break;
                            }
                            double balanceLeft = loadedAccount.Withdraw(withdrawalAmount);
                            Console.WriteLine("Amount has been successfully withdrawn, Remaining balance is : {0}", loadedAccount.CurrentBalance);
                            break;
                        case 1:
                            Console.Write("Please enter the amount you want to deposit : ");
                            double depositAmount = double.Parse(Console.ReadLine());
                            double balanceBecome = loadedAccount.Withdraw(depositAmount);
                            Console.WriteLine("Amount has been successfully deposited, Current balance is : {0}", loadedAccount.CurrentBalance);
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }else
                {
                    Console.WriteLine("Account doesn't exist :(");
                }
            }

        }
    }
}

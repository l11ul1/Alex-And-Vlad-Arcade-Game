using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GroupProjectAlexVlad.MenuLogic
{
    class GameAccountManager
    {

        public List<Account> users = new List<Account>();
        private JSONDataContractHandler handler = new JSONDataContractHandler();

        //Load in the file
        public GameAccountManager()
        {
            //Check if the file exists
            if (!File.Exists(ApplicationData.Current.RoamingFolder.Path + "\\" + "gameUsers.json"))
            {
                Debug.WriteLine("File does not exist");
                
                //Create a random player and save it. Then load the JSON
                if (AddAccount("Alex", "007") == 1)
                {
                    users = handler.Load();
                }
                else
                {
                    Debug.WriteLine("An unkown error occured");
                }
          
            }
            else
            {
                Debug.WriteLine("File does exist");
                users = handler.Load();
            }
          
        }

        //Update the file
        public void UpdateAccount()
        {
            handler.Store(users);           
        } 

        //Add the account
        public int AddAccount(string userID, string userPassword)
        {
            //Check if the tile already exists
            int yesNo = UsersCheckContain(userID);

            if (yesNo == 1)
            {
                Account newAccount = new Account(userID, userPassword);
                users.Add(newAccount);

                handler.Store(users);

                return 1;             
            }
            else
            {
                return 0;
            }
            
        }

        //Check if ID's are the same
        public int UsersCheckContain(string userID)
        {

            foreach (var accounts in users)
            {
                if (accounts.UserID == userID)
                {
                    return 0;
                }
            }
            return 1;

        }

        //Check the password
        public int UsersPasswordCheckContain(string userPassword)
        {
            foreach (Account accounts in users)
            {
                if (accounts.UserPassword == userPassword)
                {
                    return 0;
                }
            }
            return 1;
        }

        //Get the acount 
        public Account GetAccount(string userID, string userPassword)
        {
            foreach (Account accounts in users)
            {
                if (accounts.UserID == userID && accounts.UserPassword == userPassword)
                {
                    return accounts;
                }
            }
            return null;
        }

        public List<Account> List => users;




    }
}

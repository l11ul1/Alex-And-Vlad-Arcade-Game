using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using System.Diagnostics;

namespace GroupProjectAlexVlad.MenuLogic
{
    class JSONDataContractHandler
    {
        readonly string FileName = ApplicationData.Current.RoamingFolder.Path + "\\" + "gameUsers.json";

        public JSONDataContractHandler() { }  
        
        //Save the data
        public void Store(List<Account> users)
        {

            try
            {
                DataContractJsonSerializer dcSerializer = new DataContractJsonSerializer(typeof(List<Account>));
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    dcSerializer.WriteObject(fileStream, users);
                }
                Debug.WriteLine("File did create");
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File didn't create");
            }       
        }


        //Load the data
        public List<Account> Load()
        {
            List<Account> students = null;
            try
            {
                DataContractJsonSerializer xmlSerializer = new DataContractJsonSerializer(typeof(List<Account>));
                using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                {
                    students = (List<Account>)xmlSerializer.ReadObject(fileStream);
                }
                Debug.WriteLine("File did load");
                return students;
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File didn't load");
                return students;
            }          
        }
    }
}

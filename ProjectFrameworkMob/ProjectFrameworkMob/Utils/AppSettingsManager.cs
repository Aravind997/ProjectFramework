using ProjectFrameworkCommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectFrameworkMob.Utils
{
    public class AppSettingsManager
    {
        private const string SettingsFileName = "AppSettings.xml";
        private string AppDataFolder { get; set; }
        private string AppDataPath { get; set; }
        public int UserID { get; set; }
        public string EMail { get; set; }
        public string AuthenticationToken { get; set; }
        public AppSettings Settings { get; set; }
        //Define all necessary data structures in Models folder
        //Declare necessary Objects here
        //serialize or save it to XML or SQLite Table based on the Business decisions

        public AppSettingsManager()
        {
            try
            {
                UserID = -1;
                EMail = "";
                AuthenticationToken = "";
                Settings = new AppSettings();
                AppDataPath = GetApplicationDataPath();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }

        public string GetApplicationDataPath()
        {
            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string title = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;

            AppDataFolder = Path.Combine(folder, title);
            //Create the folder if not there
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }

            AppDataPath = Path.Combine(AppDataFolder, SettingsFileName);
            return AppDataPath;
        }

        public void LoadSettings()
        {
            try
            {
                string strEncryptedData = "";

                if (System.IO.File.Exists(AppDataPath))
                {
                    strEncryptedData = System.IO.File.ReadAllText(AppDataPath);
                }

                if (string.IsNullOrEmpty(strEncryptedData))
                {
                    return;
                }
                string strData = DecryptData(strEncryptedData);
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettingsManager));
                XmlReaderSettings Xmlsettings = new XmlReaderSettings();
                // No settings need modifying here

                using (StringReader textReader = new StringReader(strData))
                {
                    AppSettingsManager Manager = (AppSettingsManager)serializer.Deserialize(textReader);
                   
                    //Assign other Objects as well as when you add other datas 
                    this.UserID = Manager.UserID;
                    this.EMail= Manager.EMail;
                    this.AuthenticationToken = Manager.AuthenticationToken;
                    this.Settings = Manager.Settings;


                }
            }
            catch (Exception Ex)
            {
                //Throw the exception and catch in the called block and display the error mesage
                //No bool return is needed 
                throw Ex;
            }

        }
        public void SaveSettings()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettingsManager));
                string strSerailizedString = "";
                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, this);
                    strSerailizedString = textWriter.ToString();
                }
                //Encrypt the Data
                string strEncryptedData = EncryptData(strSerailizedString);
                //Save it to App Data File
                System.IO.File.WriteAllText(AppDataPath, strEncryptedData);

            }
            catch (Exception Ex)
            {
                //Throw the exception and catch in the called block and display the error mesage
                //No bool return is needed 
                throw Ex;
            }
        }
        private string EncryptData(string strContent)
        {
            return PFCrypt.Encrypt(strContent);
        }
        private string DecryptData(string strContent)
        {
            return PFCrypt.Decrypt(strContent);
        }

    }
}

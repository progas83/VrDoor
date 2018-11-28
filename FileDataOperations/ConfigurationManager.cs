using System;
using System.IO;

namespace FileDataOperations
{
    public class ConfigurationManager<T> where T: class, new()
    {
        private static T currentConfiguration;
        private readonly string configFileName = "fingerConfig.xml";

        private readonly string configFilePath;
        private static ConfigurationManager<T> manager = null;
        private static object padlock = new object();

        //private XmlSerializer serializer;
        private XmlConfigurator<T> xmlConfigurator;
        private ConfigurationManager()
        {
            this.configFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Config\\{configFileName}";
            var res = Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}Config");
            LogManager.Output(res.FullName);
            LogManager.Output(this.configFilePath);
            LogManager.Output(AppDomain.CurrentDomain.BaseDirectory);

            this.xmlConfigurator = new XmlConfigurator<T>(this.configFilePath);
            UpdateConfiguration();
        }
        public static ConfigurationManager<T> Instance
        {
            get
            {
                if (manager == null)
                {
                    lock (padlock)
                    {
                        if (manager == null)
                        {
                            manager = new ConfigurationManager<T>();
                        }
                    }
                }

                return manager;
            }
        }
        public T GetConfiguration()
        {
            if (currentConfiguration == null)
            {
                this.UpdateConfiguration();
            }
            return currentConfiguration; 
        }

        public void UpdateConfiguration()
        {
            try
            {
                currentConfiguration = xmlConfigurator.ReadConfiguration();
            }
            catch (Exception ex)
            {
                LogManager.Output(ex.Message);
            }
        }

        public void SaveConfiguration(T config)
        {
            this.xmlConfigurator.SaveConfiguratio(config);
        }

    }
}

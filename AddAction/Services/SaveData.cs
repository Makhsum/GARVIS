using AddAction.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddAction.Services
{
    public  class SaveData<T>
    {
        private   string _actionFilePath;

        public  SaveData(string aactionFilePath)
        {
            _actionFilePath= aactionFilePath;
        }
        public  void SaveDAta(T model)
        {
            if (File.Exists(_actionFilePath))
            {
                string jsonContent = File.ReadAllText(_actionFilePath);

                var deserializedData = JsonConvert.DeserializeObject<List<T>>(jsonContent);
                deserializedData.Add(model);
                string jsonNewContent = JsonConvert.SerializeObject(deserializedData, Formatting.Indented);
                File.WriteAllText(_actionFilePath, jsonNewContent);
            }
            else
            {
                List<T> newModels = new List<T>();
                newModels.Add(model);
                string jsonNewContent = JsonConvert.SerializeObject(newModels, Formatting.Indented);
                File.WriteAllText(_actionFilePath, jsonNewContent);
            }
        }
    }
  
}

using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Util
{
    public static class LoadProjectFromJSON
    {
        public static SolutionModel LoadProject(string filename)
        {
            BridgeLog.Info($"Preparing to open file '{filename}'");
            string jsonText = File.ReadAllText(filename);
            SolutionModel solutionModel = JsonCommonHelper.DeserializeObject(jsonText, typeof(SolutionModel)) as SolutionModel;
            solutionModel.SolutionFileName = filename;
            solutionModel.SolutionSource = "File System";
            solutionModel.SetProjectReferences();
            BridgeLog.Info($"Project file '{filename}' load is complete");
            return solutionModel;
        }

        public static SolutionModel LoadFromBase64(string base64Data, string name, string dbSource)
        {
            BridgeLog.Info($"Preparing to load solution '{name}' from database source '{dbSource}'");
            var base64EncodedBytes = System.Convert.FromBase64String(base64Data);
            string jsonString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            SolutionModel solutionModel = JsonConvert.DeserializeObject(jsonString, typeof(SolutionModel)) as SolutionModel;

            solutionModel.SetProjectReferences();

            BridgeLog.Info($"Project file '{solutionModel.SolutionName}' load is complete");
            return solutionModel;
        }

        public static bool SaveSolutionToFile(string filename, SolutionModel solutionModel)
        {
            string jsonResult = JsonCommonHelper.SerializeObject(solutionModel);
            File.WriteAllText(filename, jsonResult);
            BridgeLog.Info($"Saving solution file to [{filename}");
            return true;
        }
    }
}

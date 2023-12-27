using Harvest.Bridge.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Util
{
    public static class StringHelpers
    {
        public const string OPEN_VAL = "[~";
        public const string CLOSE_VAL = "~]";

        public static string MergeString(string inputString, List<VariableValueModel> variables)
        {
            string retString = inputString;
            if(string.IsNullOrWhiteSpace(inputString) == false && inputString.Contains("[~") == true)
            {
                NameValueCollection replList = MergeStringReturnCollection(inputString);
                foreach (string r in replList.Keys)
                {
                    VariableValueModel v = variables.FirstOrDefault(p => p.VariableName == r);

                    if(v != null)
                    {
                        retString = retString.Replace(OPEN_VAL + r + CLOSE_VAL, v.ObjectValue.ToString());
                    }
                    else
                    {
                        throw new ApplicationException($"Not able to find declared variable '{r}', unable to continue.");
                    }
                }
            }

            return retString;
        }

        public static NameValueCollection MergeStringReturnCollection(string mergeString)
        {
            if (mergeString == null) { mergeString = string.Empty; }
            // Get Merge Fields            
            NameValueCollection mergeList = new NameValueCollection();

            int start = mergeString.IndexOf(OPEN_VAL, System.StringComparison.OrdinalIgnoreCase);
            int end = 0;
            while (start != -1)
            {
                end = mergeString.IndexOf(CLOSE_VAL, start, System.StringComparison.OrdinalIgnoreCase) - CLOSE_VAL.Length;

                string text = mergeString.Substring((start + OPEN_VAL.Length), (end - start));

                // Store the non modified version
                string originalValue = text;

                mergeList.Add(originalValue, text);

                start = mergeString.IndexOf(OPEN_VAL, end, System.StringComparison.OrdinalIgnoreCase);
            }

            return mergeList;
        }

    }
}

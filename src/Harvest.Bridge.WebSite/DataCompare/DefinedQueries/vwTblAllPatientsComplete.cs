using Harvest.Bridge.WebSite.DataCompare.CompareLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.DataCompare.DefinedQueries
{
    public static class vwTblAllPatientsComplete
    {
        public static string SqlQuery { 
            get
            {
return $@"select * 
from vwTblAllPatientsComplete
where test is NOT NULL
AND [Draw Location] = 'CFKC - DOWNTOWN'
AND [Draw Date] BETWEEN '{DateTime.Now.AddDays(-5).ToShortDateString()}' AND '{DateTime.Now.ToShortDateString()}'";
            }
        }

        public static DataTable BuildCompareTable(Literal ctrlMessage, DataTable dtDB01, DataTable dtDb01New, 
            bool excludeMatchingRecords, bool twoDigitPrecision)
        {
            BaseCompareLogic.ShowMaxRecordCntMessage(ctrlMessage, dtDB01);
            DataTable dtRet = BaseCompareLogic.BuildReturnTable(dtDB01);

            foreach (DataRow dr in dtDB01.Rows)
            {
                bool shouldAdd = true;
                bool didNotFindRecord = false;
                DataRow drProd = dr;
                DataRow drQA = null;

                string sampleId = drProd["Sample Id"].ToString();
                DataRow[] drs = dtDb01New.Select("[Sample Id]='" + sampleId + "'");

                if (drs.Length == 1)
                {
                    drQA = drs[0];
                }
                else
                {
                    // Find the right one if it exists, otherwise put in empty records showing missing data
                    string testkey = drProd["TestKey"].ToString();
                    drs = dtDb01New.Select("[Sample Id]='" + sampleId + "' and testkey='" + testkey + "'");
                    if (drs.Length == 1)
                    {
                        drQA = drs[0];
                    }
                    else
                    {
                        drQA = dtRet.NewRow();
                        didNotFindRecord = true;
                    }
                }

                if (didNotFindRecord == false && excludeMatchingRecords)
                {
                    shouldAdd = BaseCompareLogic.DoRecordsMatch(drProd, drQA, twoDigitPrecision);
                }

                if (shouldAdd)
                {
                    dtRet.ImportRow(drProd);
                    dtRet.Rows[dtRet.Rows.Count - 1][0] = "PROD";

                    if (didNotFindRecord)
                    {
                        dtRet.Rows.Add(drQA);
                        dtRet.Rows[dtRet.Rows.Count - 1][0] = "QA - NO MATCH";
                    }
                    else
                    {
                        dtRet.ImportRow(drQA);
                        dtRet.Rows[dtRet.Rows.Count - 1][0] = "QA";
                    }
                }
            }
            return dtRet;
        }

    }
}
using Harvest.Bridge.WebSite.DataCompare.CompareLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.DataCompare.DefinedQueries
{
    public static class spAllPatientsComplete_JB
    {
        public static string SqlQuery
        {
            get
            {
                return $@"EXEC    [dbo].[spAllPatientsComplete_JB]
        @locationName = N'CFKC - DOWNTOWN',
        @drawDF = N'{DateTime.Now.AddDays(-5).ToShortDateString()}',
        @drawDT = N'{DateTime.Now.ToShortDateString()}'";
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

                string recordKey = drProd["Recordkey"].ToString();
                string chartNumber = drProd["Chart Num"].ToString();
                DataRow[] drs = dtDb01New.Select("[Chart Num]='" + chartNumber + "' and Recordkey='" + recordKey + "'");

                if (drs.Length == 1)
                {
                    drQA = drs[0];
                }
                else
                {
                    string sampleId = drProd["Sample ID"].ToString();
                    if (string.IsNullOrEmpty(sampleId))
                    {
                        sampleId = " is null";
                    }
                    else
                    {
                        sampleId = "='" + sampleId + "'";
                    }
                    string testkey = drProd["testkey"] is DBNull ? null : drProd["testkey"].ToString();
                    if (string.IsNullOrEmpty(testkey))
                    {
                        testkey = " is null";
                    }
                    else
                    {
                        testkey = "=" + testkey;
                    }

                    drs = dtDb01New.Select("[Chart Num]='" + chartNumber + "' and Recordkey='" + recordKey + "' and [Sample Id]" + sampleId + " and testkey" + testkey);

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
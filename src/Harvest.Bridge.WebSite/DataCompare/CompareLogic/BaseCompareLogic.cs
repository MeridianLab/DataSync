using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI.WebControls;

namespace Harvest.Bridge.WebSite.DataCompare.CompareLogic
{
    public static class BaseCompareLogic
    {
        private const int MAX_REC_CNT = 500;

        internal static bool DoRecordsMatch(DataRow drProd, DataRow drQA, bool twoDigitPrecision)
        {
            string[] skipClmns = { "last_result_date" };
            int numResClmPos = -1;
            if(drProd.Table.Columns.Count > 0)
            {
                numResClmPos = drProd.Table.Columns["Num Res"].Ordinal;
            }
            bool shouldAdd = false;
            for (int c = 0; c < drProd.Table.Columns.Count - 1; c++)
            {
                string prodVal = null;
                string qaVal = null;
                if (drProd[c] is DBNull == false)
                {
                    prodVal = drProd[c].ToString();
                }
                if (drQA[c] is DBNull == false)
                {
                    qaVal = drQA[c].ToString();
                }

                if(twoDigitPrecision && (c == numResClmPos && string.IsNullOrEmpty(prodVal) == false && string.IsNullOrEmpty(qaVal) == false))
                {
                    prodVal = double.Parse(prodVal).ToString("#.#0");
                    qaVal = double.Parse(qaVal).ToString("#.#0");
                }

                if (qaVal != prodVal)
                {
                    if (skipClmns.Contains(drProd.Table.Columns[c].ColumnName))
                    {
                        // Skipping do not add for this column
                    }
                    else
                    {
                        shouldAdd = true;
                        break;
                    }
                }
            }
            return shouldAdd;
        }

        internal static DataTable BuildReturnTable(DataTable dtDB01)
        {
            DataTable dtRet = new DataTable();
            dtRet.Columns.Add("Source");
            foreach (DataColumn dc in dtDB01.Columns)
            {
                dtRet.Columns.Add(dc.ColumnName, dc.DataType);
            }
            return dtRet;
        }

        internal static void ShowMaxRecordCntMessage(Literal ctrlMessage, DataTable dtDB01)
        {
            if (dtDB01.Rows.Count > MAX_REC_CNT)
            {
                ShowMessageInfo(ctrlMessage, $"Max record comparison row count is {MAX_REC_CNT}. Actual row count is {dtDB01.Rows.Count}, first {MAX_REC_CNT} records being processed please refine criteria to limit number of records for comparison.");
            }

        }

        #region Show Messages
        internal static void ShowMessageDanger(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "danger", message);
        }
        internal static void ShowMessageSuccess(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "success", message);
        }
        internal static void ShowMessageInfo(Literal ctrlMessage, string message)
        {
            ShowMessage(ctrlMessage, "info", message);
        }
        private static void ShowMessage(Literal ctrlMessage, string msgType, string message)
        {
            ctrlMessage.Text = $"<div class='alert alert-{msgType}' role='alert'>{message}</div>";
        }
        #endregion

    }
}
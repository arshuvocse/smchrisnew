using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.Inverview_DAL
{
   public  class InterviewCandidateInfoListDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public DataTable GetInterviewCandidateInfoList( string param )
       {
           string query = @"SELECT com.ShortName,* FROM dbo.tblInterviewCandidateInfo 
INNER JOIN dbo.tblCompanyInfo com ON tblInterviewCandidateInfo.CompanyId= com.CompanyId " + param + "";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable GetInterviewCandidateInfoListForMailSend(string param)
       {
           string query = @"SELECT com.ShortName,* FROM dbo.tblInterviewCandidateInfo 
INNER JOIN dbo.tblCompanyInfo com ON tblInterviewCandidateInfo.CompanyId= com.CompanyId " + param + "";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetVivaName(string companyId,string jobId)
       {
           string query = @"SELECT DISTINCT VivaName,tblVivaDetailsMark.VivaId FROM dbo.tblVivaDetailsMark
            LEFT JOIN dbo.tblVivaSetupInfo ON tblVivaSetupInfo.VivaId = tblVivaDetailsMark.VivaId
             WHERE JobID='" +jobId+"' AND tblVivaSetupInfo.CompanyId='"+companyId+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable GetVivaNameForSendMAil(string jobId)
       {
           string query = @"SELECT DISTINCT (VivaName+' ( Out Of (' +(CONVERT(NVARCHAR(10),VivaMarks)) +'))') AS VivaName,tblInterviewBoardMarksSetup.VivaId FROM dbo.tblInterviewBoardMarksSetup
            LEFT JOIN dbo.tblVivaSetupInfo ON tblVivaSetupInfo.VivaId = tblInterviewBoardMarksSetup.VivaId
             WHERE  SetupMasterId='" + jobId + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetCandidate(string companyId, string jobId)
       {
           string query = @"SELECT * FROM dbo.tblInterviewCandidateInfo WHERE CompanyId='"+companyId+"' AND JobID='"+jobId+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetOtherWrittenMarks(string candidateId, string jobId)
       {
           string query = @"SELECT cast(AVG(WrittenMarks)as decimal(18,2))WrittenMarks,cast(AVG(OtherMarks)as decimal(18,2))OtherMarks FROM dbo.tblInterviewMarksDetails WHERE JobId='" + jobId + "' AND CandidateID='" + candidateId + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetOtherWrittenMarksOutOf(string candidateId, string jobId)
       {
           string query = @"SELECT * FROM dbo.tblInterviewMarksDetails WHERE JobId='" + jobId + "' ";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetBoardMember(string companyId, string jobId)
       {
           string query = @"SELECT Name,BoardDetailsId FROM dbo.tblInterviewBoardSetupMaster
            LEFT JOIN dbo.tblInterviewBoardSetupDetails ON tblInterviewBoardSetupDetails.MasterId = tblInterviewBoardSetupMaster.SetupMasterId
            WHERE JobTitleId='"+jobId+"' AND CompanyId='"+companyId+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable GetBoardMemberSendMail(string companyId, string jobId)
       {
           string query = @"SELECT '' as Name,BoardDetailsId FROM dbo.tblInterviewBoardSetupMaster
            LEFT JOIN dbo.tblInterviewBoardSetupDetails ON tblInterviewBoardSetupDetails.MasterId = tblInterviewBoardSetupMaster.SetupMasterId
            WHERE  (tblInterviewBoardSetupDetails.Name!=' ') and JobTitleId='" + jobId + "' AND CompanyId='" + companyId + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetVivaName(string companyId, string jobId,string boarddetailId)
       {
           string query = @"SELECT Name,BoardDetailsId FROM dbo.tblInterviewBoardSetupMaster
            LEFT JOIN dbo.tblInterviewBoardSetupDetails ON tblInterviewBoardSetupDetails.MasterId = tblInterviewBoardSetupMaster.SetupMasterId
            WHERE JobTitleId='" + jobId + "' AND CompanyId='" + companyId + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetVivaMarks(string candidateId, string jobId,string vivaId)
       {
           string query = @"SELECT SUM(VivaMarks)Marks FROM dbo.tblVivaDetailsMark WHERE CandidateID='"+candidateId+"' AND JobId='"+jobId+"' AND VivaId='"+vivaId+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetVivaAvg(string candidateId, string jobId)
       {
           string query = @"SELECT cast(AVG(tblt.VivaMarksAvg)as decimal(18,2))VivaAvg FROM (SELECT cast(AVG(VivaMarks)as decimal(18,2))VivaMarksAvg,BoardDetailsId FROM dbo.tblVivaDetailsMark WHERE CandidateID='" + candidateId + "' AND JobId='" + jobId + "' GROUP BY  BoardDetailsId) AS tblt";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetVivaMarks(string candidateId, string jobId, string vivaId,string boarddetailid)
       {
           string query = @"SELECT VivaMarks FROM dbo.tblVivaDetailsMark WHERE CandidateID='" + candidateId + "' AND JobId='" + jobId + "' AND VivaId='" + vivaId + "' AND BoardDetailsId='"+boarddetailid+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetOtherWrittenMarks(string candidateId, string jobId, string boarddetailid)
       {
           string query = @"SELECT * FROM dbo.tblInterviewMarksDetails
LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = tblInterviewMarksDetails.CandidateID
WHERE tblInterviewCandidateInfo.JobID='"+jobId+"' AND tblInterviewMarksDetails.CandidateID='"+candidateId+"' AND BoardDetailsId='"+boarddetailid+"'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable TebulationMarks(string companyId, string jobId)
       {
           DataTable aDataTable=new DataTable();
           
           aDataTable.Columns.Add("CandidateID");
           aDataTable.Columns.Add("JobID");
           aDataTable.Columns.Add("CompanyId");
           aDataTable.Columns.Add("SL");
           aDataTable.Columns.Add("Candidate Name");
           
           //aDataTable.Columns.Add("Written Marks");
           DataTable dtvivacol = GetVivaName(companyId, jobId);
           DataTable dtmembername = GetBoardMember(companyId, jobId);
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               for (int j = 0; j < dtvivacol.Rows.Count; j++)
               {
                   aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString()+"("+dtvivacol.Rows[j][0].ToString()+")");
               }
           }
           aDataTable.Columns.Add("Average Viva");
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Written)");
           }
           aDataTable.Columns.Add("Average Written");
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Other)");
           }
           aDataTable.Columns.Add("Average Other");
           //aDataTable.Columns.Add("Other Marks");
           aDataTable.Columns.Add("Total", typeof(decimal));
           aDataTable.Columns.Add("Position");

           DataTable dtcandidatedata = GetCandidate(companyId, jobId);

           DataRow dataRow = null;

           for (int i = 0; i < dtcandidatedata.Rows.Count; i++)
           {
               dataRow = aDataTable.NewRow();
               dataRow["SL"] = i + 1;
               dataRow["Candidate Name"] = dtcandidatedata.Rows[i]["CandidateName"].ToString();
               dataRow["CandidateID"] = dtcandidatedata.Rows[i]["CandidateID"].ToString();
               dataRow["JobID"] = dtcandidatedata.Rows[i]["JobID"].ToString();
               dataRow["CompanyId"] = dtcandidatedata.Rows[i]["CompanyId"].ToString();

               for (int j = 0; j < dtmembername.Rows.Count; j++)
               {
                   for (int k = 0; k < dtvivacol.Rows.Count; k++)
                   {
                       DataTable dtvivamark = GetVivaMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                           dtcandidatedata.Rows[i]["JobID"].ToString(), dtvivacol.Rows[k][1].ToString(),
                           dtmembername.Rows[j][1].ToString());
                       if (dtvivamark.Rows.Count>0)
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                          dtvivamark.Rows[0][0].ToString();    
                       }
                       else
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                               "0";
                       }
                       
                   }
               }
               for (int j = 0; j < dtmembername.Rows.Count; j++)
               {
                   DataTable dtotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                       dtcandidatedata.Rows[i]["JobID"].ToString(), dtmembername.Rows[j][1].ToString());
                   if (dtotherwritten.Rows.Count>0)
                   {
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = dtotherwritten.Rows[0]["WrittenMarks"].ToString();
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Other)"] = dtotherwritten.Rows[0]["OtherMarks"].ToString();    
                   }
                   else
                   {
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
                   }
                   
               }
               DataTable dtvivaavg = GetVivaAvg(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                   dtcandidatedata.Rows[i]["JobID"].ToString());
               if (dtvivaavg.Rows.Count>0)
               {
                   dataRow["Average Viva"] = dtvivaavg.Rows[0][0].ToString();
               }
               else
               {
                   dataRow["Average Viva"] = "0";
               }
               DataTable dtavgotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                   dtcandidatedata.Rows[i]["JobID"].ToString());
               if (dtavgotherwritten.Rows.Count > 0)
               {
                   dataRow["Average Written"] = dtavgotherwritten.Rows[0]["WrittenMarks"].ToString();
                   dataRow["Average Other"] = dtavgotherwritten.Rows[0]["OtherMarks"].ToString();
               }
               else
               {
                   dataRow["Average Written"] = "0";
                   dataRow["Average Other"] = "0";
               }
               //for (int j = 0; j < dtmembername.Rows.Count; j++)
               //{
               //    aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Other)");
               //}
               //DataTable dtotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(), jobId);
               //if (dtotherwritten.Rows.Count>0)
               //{
               //    dataRow["Written Marks"] = dtotherwritten.Rows[0]["WrittenMarks"].ToString();
               //    dataRow["Other Marks"] = dtotherwritten.Rows[0]["OtherMarks"].ToString();
               //}
               //else
               //{
               //    dataRow["Written Marks"] = "0";
               //    dataRow["Other Marks"] = "0";
               //}
               //decimal vivatotal = 0;
               //for (int j = 0; j < dtvivacol.Rows.Count; j++)
               //{
               //    DataTable dtvivamarks = GetVivaMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(), jobId,
               //        dtvivacol.Rows[j][1].ToString());
               //    if (dtvivamarks.Rows.Count > 0)
               //    {
               //        dataRow[dtvivacol.Rows[i][0].ToString()] = dtvivamarks.Rows[0][0].ToString();
               //        vivatotal +=Convert.ToDecimal(dtvivamarks.Rows[0][0].ToString());
               //    }
               //    else
               //    {
               //        dataRow[dtvivacol.Rows[i][0].ToString()] = "0";
               //        vivatotal += 0;
               //    }

               //}
               if (i==0)
               {
                   dataRow["Position"] = "1st";
               }
               else if (i==1)
               {
                   dataRow["Position"] = "2nd";
               }
               else if (i==2)
               {
                   dataRow["Position"] = "3rd";
               }
               else
               {
                   dataRow["Position"] = (i+1).ToString()+"th";
               }
               try
               {
                   dataRow["Total"] = Convert.ToDecimal((string.IsNullOrEmpty(dataRow["Average Written"].ToString())?0:Convert.ToDecimal(dataRow["Average Written"])) +(string.IsNullOrEmpty(dataRow["Average Other"].ToString())?0: Convert.ToDecimal(dataRow["Average Other"])) +
                       (string.IsNullOrEmpty(dataRow["Average Viva"].ToString())?0: Convert.ToDecimal(dataRow["Average Viva"])));
                   aDataTable.Rows.Add(dataRow);
               }
               catch (Exception)
               {
                   
                   //throw;
               }

           }
           DataView dv = aDataTable.DefaultView;
           dv.Sort = "Total desc";
           DataTable sortedDT = dv.ToTable();
           for (int i = 0; i < sortedDT.Rows.Count; i++)
           {
                if (i==0)
               {
                   sortedDT.Rows[i]["Position"]= "1st";
               }
               else if (i==1)
               {
                   sortedDT.Rows[i]["Position"]= "2nd";
               }
               else if (i==2)
               {
                   sortedDT.Rows[i]["Position"]= "3rd";
               }
               else
               {
                   sortedDT.Rows[i]["Position"]= (i+1).ToString()+"th";
               }
               //sortedDT.Rows[i]["Position"]=
           }
           return sortedDT;
           
       }


       public DataTable TebulationMarksNew(string companyId, string jobId)
       {
           DataTable aDataTable = new DataTable();

           aDataTable.Columns.Add("CandidateID");
           aDataTable.Columns.Add("JobID");
           aDataTable.Columns.Add("CompanyId");
           aDataTable.Columns.Add("SL");
           aDataTable.Columns.Add("Candidate Name");
           aDataTable.Columns.Add("Viva1");
           aDataTable.Columns.Add("Viva2");
           aDataTable.Columns.Add("Viva3");
           aDataTable.Columns.Add("Viva4");
           aDataTable.Columns.Add("Viva5");
           aDataTable.Columns.Add("Viva6");
           aDataTable.Columns.Add("Viva7");
           aDataTable.Columns.Add("Viva8");
           aDataTable.Columns.Add("Viva9");
           aDataTable.Columns.Add("Viva10");
           aDataTable.Columns.Add("Viva11");
           aDataTable.Columns.Add("Viva12");
           aDataTable.Columns.Add("Viva13");
           aDataTable.Columns.Add("Viva14");
           aDataTable.Columns.Add("Viva15");
           aDataTable.Columns.Add("Viva16");
           aDataTable.Columns.Add("Viva17");
           aDataTable.Columns.Add("Viva18");
           aDataTable.Columns.Add("Viva19");
           aDataTable.Columns.Add("Viva20");
           aDataTable.Columns.Add("Viva21");
           aDataTable.Columns.Add("Viva22");
           aDataTable.Columns.Add("Viva23");
           aDataTable.Columns.Add("Viva24");
           aDataTable.Columns.Add("Viva25");
           aDataTable.Columns.Add("Viva26");
           aDataTable.Columns.Add("Viva27");
           aDataTable.Columns.Add("Viva28");
           aDataTable.Columns.Add("Viva29");
           aDataTable.Columns.Add("Viva30");
           

           //aDataTable.Columns.Add("Written Marks");
           DataTable dtvivacol = GetVivaName(companyId, jobId);
           DataTable dtmembername = GetBoardMember(companyId, jobId);
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               for (int j = 0; j < dtvivacol.Rows.Count; j++)
               {
                   aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(" + dtvivacol.Rows[j][0].ToString() + ")");
               }
           }
           aDataTable.Columns.Add("Average Viva");
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Written)");
           }
           aDataTable.Columns.Add("Average Written");
           for (int i = 0; i < dtmembername.Rows.Count; i++)
           {
               aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Other)");
           }
           aDataTable.Columns.Add("Average Other");
           //aDataTable.Columns.Add("Other Marks");
           aDataTable.Columns.Add("Total");

           DataTable dtcandidatedata = GetCandidate(companyId, jobId);

           DataRow dataRow = null;

           for (int i = 0; i < dtcandidatedata.Rows.Count; i++)
           {
               dataRow = aDataTable.NewRow();
               dataRow["SL"] = i + 1;
               dataRow["Candidate Name"] = dtcandidatedata.Rows[i]["CandidateName"].ToString();
               dataRow["CandidateID"] = dtcandidatedata.Rows[i]["CandidateID"].ToString();
               dataRow["JobID"] = dtcandidatedata.Rows[i]["JobID"].ToString();
               dataRow["CompanyId"] = dtcandidatedata.Rows[i]["CompanyId"].ToString();

               for (int j = 0; j < dtmembername.Rows.Count; j++)
               {
                   for (int k = 0; k < dtvivacol.Rows.Count; k++)
                   {
                       DataTable dtvivamark = GetVivaMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                           dtcandidatedata.Rows[i]["JobID"].ToString(), dtvivacol.Rows[k][1].ToString(),
                           dtmembername.Rows[j][1].ToString());
                       if (dtvivamark.Rows.Count > 0)
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                          dtvivamark.Rows[0][0].ToString();
                       }
                       else
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                               "0";
                       }

                   }
               }
               for (int j = 0; j < dtmembername.Rows.Count; j++)
               {
                   DataTable dtotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                       dtcandidatedata.Rows[i]["JobID"].ToString(), dtmembername.Rows[j][1].ToString());
                   if (dtotherwritten.Rows.Count > 0)
                   {
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = dtotherwritten.Rows[0]["WrittenMarks"].ToString();
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Other)"] = dtotherwritten.Rows[0]["OtherMarks"].ToString();
                   }
                   else
                   {
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
                       dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
                   }

               }
               DataTable dtvivaavg = GetVivaAvg(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                   dtcandidatedata.Rows[i]["JobID"].ToString());
               if (dtvivaavg.Rows.Count > 0)
               {
                   dataRow["Average Viva"] = dtvivaavg.Rows[0][0].ToString();
               }
               else
               {
                   dataRow["Average Viva"] = "0";
               }
               DataTable dtavgotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                   dtcandidatedata.Rows[i]["JobID"].ToString());
               if (dtavgotherwritten.Rows.Count > 0)
               {
                   dataRow["Average Written"] = dtavgotherwritten.Rows[0]["WrittenMarks"].ToString();
                   dataRow["Average Other"] = dtavgotherwritten.Rows[0]["OtherMarks"].ToString();
               }
               else
               {
                   dataRow["Average Written"] = "0";
                   dataRow["Average Other"] = "0";
               }
               //for (int j = 0; j < dtmembername.Rows.Count; j++)
               //{
               //    aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Other)");
               //}
               //DataTable dtotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(), jobId);
               //if (dtotherwritten.Rows.Count>0)
               //{
               //    dataRow["Written Marks"] = dtotherwritten.Rows[0]["WrittenMarks"].ToString();
               //    dataRow["Other Marks"] = dtotherwritten.Rows[0]["OtherMarks"].ToString();
               //}
               //else
               //{
               //    dataRow["Written Marks"] = "0";
               //    dataRow["Other Marks"] = "0";
               //}
               //decimal vivatotal = 0;
               //for (int j = 0; j < dtvivacol.Rows.Count; j++)
               //{
               //    DataTable dtvivamarks = GetVivaMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(), jobId,
               //        dtvivacol.Rows[j][1].ToString());
               //    if (dtvivamarks.Rows.Count > 0)
               //    {
               //        dataRow[dtvivacol.Rows[i][0].ToString()] = dtvivamarks.Rows[0][0].ToString();
               //        vivatotal +=Convert.ToDecimal(dtvivamarks.Rows[0][0].ToString());
               //    }
               //    else
               //    {
               //        dataRow[dtvivacol.Rows[i][0].ToString()] = "0";
               //        vivatotal += 0;
               //    }

               //}
               try
               {
                   dataRow["Total"] = (string.IsNullOrEmpty(dataRow["Average Written"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Written"])) + (string.IsNullOrEmpty(dataRow["Average Other"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Other"])) +
                       (string.IsNullOrEmpty(dataRow["Average Viva"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Viva"]));
                   aDataTable.Rows.Add(dataRow);
               }
               catch (Exception)
               {

                   //throw;
               }

           }
           return aDataTable;

       }



       public DataTable TebulationMarksForMailSend(string companyId, string jobId, int mid)
       {
           DataTable aDataTable = new DataTable();

           aDataTable.Columns.Add("CandidateID");
           aDataTable.Columns.Add("JobID");
           aDataTable.Columns.Add("CompanyId");
           aDataTable.Columns.Add("SL");
           aDataTable.Columns.Add("Candidate Name");

           //aDataTable.Columns.Add("Written Marks");
           DataTable dtvivacol = GetVivaNameForSendMAil(mid.ToString());
           DataTable dtmembername = GetBoardMemberSendMail(companyId, jobId);


           try
           {
               for (int i = 0; i < dtmembername.Rows.Count; i++)
               {
                   for (int j = 0; j < dtvivacol.Rows.Count; j++)
                   {
                       aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(" + dtvivacol.Rows[j][0].ToString() + ")");
                   }
               }
           }
           catch (Exception)
           {
               
             
           }
       
           //aDataTable.Columns.Add("Average Viva");
           //for (int i = 0; i < dtmembername.Rows.Count; i++)
           //{
           //    aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Written)");
           //}
           //aDataTable.Columns.Add("Average Written");
           //for (int i = 0; i < dtmembername.Rows.Count; i++)
           //{
           //    aDataTable.Columns.Add(dtmembername.Rows[i][0].ToString() + "(Other)");
           //}
         //  aDataTable.Columns.Add("Average Other");
           //aDataTable.Columns.Add("Other Marks");
        //   aDataTable.Columns.Add("Total");

           DataTable dtcandidatedata = GetCandidate(companyId, jobId);

           DataRow dataRow = null;

           for (int i = 0; i < dtcandidatedata.Rows.Count; i++)
           {
               dataRow = aDataTable.NewRow();
               dataRow["SL"] = i + 1;
               dataRow["Candidate Name"] = dtcandidatedata.Rows[i]["CandidateName"].ToString();
               dataRow["CandidateID"] = dtcandidatedata.Rows[i]["CandidateID"].ToString();
               dataRow["JobID"] = dtcandidatedata.Rows[i]["JobID"].ToString();
               dataRow["CompanyId"] = dtcandidatedata.Rows[i]["CompanyId"].ToString();

               for (int j = 0; j < dtmembername.Rows.Count; j++)
               {
                   for (int k = 0; k < dtvivacol.Rows.Count; k++)
                   {
                       DataTable dtvivamark = GetVivaMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
                           dtcandidatedata.Rows[i]["JobID"].ToString(), dtvivacol.Rows[k][1].ToString(),
                           dtmembername.Rows[j][1].ToString());
                       if (dtvivamark.Rows.Count > 0)
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                         0;
                       }
                       else
                       {
                           dataRow[dtmembername.Rows[j][0].ToString() + "(" + dtvivacol.Rows[k][0].ToString() + ")"] =
                               "0";
                       }

                   }
               }
               //for (int j = 0; j < dtmembername.Rows.Count; j++)
               //{
               //    DataTable dtotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
               //        "00000000000000", dtmembername.Rows[j][1].ToString());
               //    if (dtotherwritten.Rows.Count > 0)
               //    {
               //        dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = dtotherwritten.Rows[0]["WrittenMarks"].ToString();
               //        dataRow[dtmembername.Rows[j][0].ToString() + "(Other)"] = dtotherwritten.Rows[0]["OtherMarks"].ToString();
               //    }
               //    else
               //    {
               //        dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
               //        dataRow[dtmembername.Rows[j][0].ToString() + "(Written)"] = "0";
               //    }

               //}
               //DataTable dtvivaavg = GetVivaAvg(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
               //    dtcandidatedata.Rows[i]["JobID"].ToString());
               //if (dtvivaavg.Rows.Count > 0)
               //{
               //    dataRow["Average Viva"] = dtvivaavg.Rows[0][0].ToString();
               //}
               //else
               //{
               //    dataRow["Average Viva"] = "0";
               //}
               //DataTable dtavgotherwritten = GetOtherWrittenMarks(dtcandidatedata.Rows[i]["CandidateID"].ToString(),
               //    dtcandidatedata.Rows[i]["JobID"].ToString());
               //if (dtavgotherwritten.Rows.Count > 0)
               //{
               //    dataRow["Average Written"] = dtavgotherwritten.Rows[0]["WrittenMarks"].ToString();
               //    dataRow["Average Other"] = dtavgotherwritten.Rows[0]["OtherMarks"].ToString();
               //}
               //else
               //{
               //    dataRow["Average Written"] = "0";
               //    dataRow["Average Other"] = "0";
               //}

               try
               {
                   //dataRow["Total"] = (string.IsNullOrEmpty(dataRow["Average Written"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Written"])) + (string.IsNullOrEmpty(dataRow["Average Other"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Other"])) +
                   //    (string.IsNullOrEmpty(dataRow["Average Viva"].ToString()) ? 0 : Convert.ToDecimal(dataRow["Average Viva"]));
                   aDataTable.Rows.Add(dataRow);
               }
               catch (Exception)
               {


               }

           }
           return aDataTable;

       }

    }
}

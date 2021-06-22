using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Net.Http;
//using Microsoft.Reporting.WebForms;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Xml;
using System.Security.Cryptography;


using System.Web.Script.Services;
using System.Web.Services;

public partial class Test : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        WeeklyRep();
//        BuildPDFBenoniRep();

       // BuildPDFDiscipleshipPastor();
         //UpdateAllUsers();
       // SendLoopMessages();
       // SendMessages();
        //if (Session["LoggedIn"] != null)
        //{

        //    if (!Page.IsPostBack)
        //    {
        //        #region Startups
        //        RunOnLoad();
        //        #endregion 
        //    }

        //}
        //else
        //{
        //    Server.Transfer("logout.aspx");
        //}
    }




    [WebMethod]
    public static List<object> GetChartData()
    {
        List<object> chartData = new List<object>();
        chartData.Add(new object[] { "Delmas Campus", 1, 1 });
        chartData.Add(new object[] { "Benoni Campus", 3,1 });
     
      



  
        return chartData;

        //    string query = "your query";
        //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    List<object> chartData = new List<object>();
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    chartData.Add(new object[]
        //                    {
        //                sdr["Topping"], sdr["Slices"]
        //                    });
        //                }
        //            }
        //            con.Close();
        //            return chartData;
        //        }
        //    }
    }

    [WebMethod]
    public static List<GraphData> KidRepGraph()
    {
        SqlConnMethod connect = new SqlConnMethod();


        string StartDate = "";
        string EndDate = "";


        StartDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        EndDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
                        new DataColumn("a", typeof(string)),
                        new DataColumn("b", typeof(string)),                 
                        new DataColumn("c",typeof(string))});





        dt.Rows.Add("Test", "DATA", "B DATA");//Delmas - Benoni - Eloff
        dt.Rows.Add("Test", "DATA", "B DATA");
        List<GraphData> chartData = new List<GraphData>();
        foreach (DataRow dr in dt.Rows)
        {
            chartData.Add(new GraphData
            {
               a = Convert.ToString(dr["a"]),
               b = (Convert.ToString(dr["b"])),

               c = (Convert.ToString(dr["c"]))
            });
        }


        string s = string.Join("", chartData);
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "MemberApp.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(s);
        }
        #endregion

        return chartData;
    }

    public class GraphData
    {
      
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
      
    }


    void PrintDoc()
    {
        string getDate = "";
        //if (txtExpiry.Text == "")
        //{
           
        //}
        //else
        //{
        //    getDate = Convert.ToDateTime(txtExpiry.Text).ToString("yyyy-MM-dd");
        //}
        getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");


 
        #region Check If File Exist
        //if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"PDF\HazardWork\" + projectid + @"\" + contractorid + @"\" + contractorid + ".pdf"))
        //{
        //    return HttpContext.Current.Session["URL"].ToString() + @"/PDF/HazardWork/" + projectid + "/" + contractorid + "/" + contractorid + ".pdf";
        //}
        #endregion


        string url = "";
        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();
        #region Connect to StoredProc
//        string queryString = @"SELECT peopleid,doctype,
//        CASE WHEN CONVERT(varchar(11),Expirydate,106) = '01 Jan 1900' THEN '0' ELSE CONVERT(varchar(11),Expirydate,106) END,
//        CONVERT(varchar(11),dateofunlink,106) ,B.cDisplayName
//        FROM CPComUnlinkHistory A INNER JOIN _rtblPeople B ON B.ucPPLIDNumber = peopleid WHERE contractorid = '" + contractorid + "' AND projectid = '" + projectid + "' AND doctype = 'Hazardous work'";
//        Demo = connect.DTSQL(queryString, "KBC");

        #endregion

        #region Linked Employee SQL
        //string SQLExistingEmp = "SELECt B.cDisplayName,A.peopleid,CONVERT(varchar(16),A.linkedDate,106),LinkedBy FROM  CPComPeopleProjLink A INNER JOIN  _rtblPeople B On A.peopleid = B.ucPPLIDNumber WHERE A.contractorid = '" + contractorid + "' AND A.projectid = '" + projectid + "'";
        //tableEmp = connect.DTSQL(SQLExistingEmp, "KBC");
        #endregion

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        string projectid = "2";
        string contractorid = "1";
     //   DataTable dt = FrontPage(packid, contractorid, projectid);
        Document document = new Document();
        if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"PDF\WorkPermitDoc\" + projectid + @"\" + contractorid))
        {
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"PDF\WorkPermitDoc\" + projectid + @"\" + contractorid);
        }
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"PDF\WorkPermitDoc\" + projectid + @"\" + contractorid + @"\" + contractorid + ".pdf", FileMode.Create));
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph("WEEKLY REPORT  | BUILDERS KIDZ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(getDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


        PdfPTable NewTable = new PdfPTable(6);

        NewTable.HorizontalAlignment = 1;
        //leave a gap before and after the table
        NewTable.SpacingBefore = 20f;
        NewTable.SpacingAfter = 8f;

        PdfPCell Header = new PdfPCell(new Phrase("Attendence", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header.Colspan = 6;
        NewTable.AddCell(Header);

        string Delmas5 = "0";
        string Delmas8 = "0";
        string Delmas12 = "0";


        string Benoni5 = "0";
        string Benoni8 = "0";
        string Benoni12 = "0";

        string Elof5 = "0";
        string Elof58 = "0";
        string Elof512 = "0";



        if (Session["ShowAll"].ToString() == "Yes")
        {


            #region Delmas
            Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Delmas12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            #endregion

            #region Benoni
            Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Benoni12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            #endregion

            #region Eloff Campus
            Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Eloff Campus Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Eloff Campus Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            Elof512 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '1' and A.campus = 'Eloff Campus Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
            #endregion



        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Delmas12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Benoni12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                Elof512 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) = '" + getDate + "'  ");
                #endregion
            }
        }

      

        DateTime today = DateTime.Today;
        string row1 = "";
        string row2 = "";
        string row3 = "";
        string row4 = "";
        string row5 = "";


        #region First Section
        #region
        PdfPCell statHeader = new PdfPCell(new Phrase("Attendence 3-5 (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader);
        #endregion

        #region 
        PdfPCell statResult = new PdfPCell(new Phrase(Delmas5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult);
        #endregion

        #region 
        PdfPCell statHeader1 = new PdfPCell(new Phrase("Attendence 3-5 (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader1);
        #endregion

        #region row3
        PdfPCell statResult1 = new PdfPCell(new Phrase(Benoni5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult1);
        #endregion

        #region row4

        PdfPCell statHeader2 = new PdfPCell(new Phrase("Attendence 3-5 (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader2);


        PdfPCell statResult2 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult2);
        #endregion

        #endregion

        #region Second Section
        #region
        PdfPCell statHeader3 = new PdfPCell(new Phrase("Attendence 6-8  (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader3);
        #endregion

        #region
        PdfPCell statResult3 = new PdfPCell(new Phrase(Delmas8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult3);
        #endregion

        #region
        PdfPCell statHeader4 = new PdfPCell(new Phrase("Attendence 6-8  (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader4);
        #endregion

        #region row3
        PdfPCell statResult4 = new PdfPCell(new Phrase(Benoni8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult4);
        #endregion

        #region row4

        PdfPCell statHeader5 = new PdfPCell(new Phrase("Attendence 6-8  (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader5);


        PdfPCell statResult5 = new PdfPCell(new Phrase(Elof58, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult5);
        #endregion

        #endregion


        #region Third Section
        #region
        PdfPCell statHeader6 = new PdfPCell(new Phrase("Attendence 9-12 (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader6.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader6);
        #endregion

        #region
        PdfPCell statResult6 = new PdfPCell(new Phrase(Delmas12, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult6.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult6);
        #endregion

        #region
        PdfPCell statHeader7 = new PdfPCell(new Phrase("Attendence 9-12 (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader7.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader7);
        #endregion

        #region row3
        PdfPCell statResult7 = new PdfPCell(new Phrase(Benoni12, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult7.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult7);
        #endregion

        #region row4

        PdfPCell statHeader8 = new PdfPCell(new Phrase("Attendence 9-12 (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader8.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader8);


        PdfPCell statResult8 = new PdfPCell(new Phrase(Elof512, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult8.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult8);
        #endregion

        #endregion
        document.Add(NewTable);



        PdfPTable SecondTable = new PdfPTable(3);

        SecondTable.HorizontalAlignment = 1;
        //leave a gap before and after the table

        SecondTable.SpacingAfter = 8f;

        PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Builders Kidz Volunteers On Duty", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT.Colspan = 3;
        SecondTable.AddCell(SecondHeaderT);



        #region Teachers Headers
        PdfPCell NameHead = new PdfPCell(new Phrase("Name", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        NameHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        SecondTable.AddCell(NameHead);


        PdfPCell CampusHead = new PdfPCell(new Phrase("Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        CampusHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        SecondTable.AddCell(CampusHead);

        PdfPCell TeacherDuty = new PdfPCell(new Phrase("Duty", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        SecondTable.AddCell(TeacherDuty);

        #endregion

        #region Populate Teachers
        DataTable TeacherTable = new DataTable();

        if (Session["ShowAll"].ToString() == "Yes")
        {
            TeacherTable = connect.DTSQL("SELECT A.Name + ' ' + A.Surname, A.Campus,A.iserve FROM Stats_Form  A INNER JOIN IserveCheck B ON A.intid = B.MemberID  WHERE A.iserve = 'Builders Kidz' and A.ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, B.CapturedDate, 23) = '" + getDate + "'");
        }
        else
        {
            TeacherTable = connect.DTSQL("SELECT A.Name + ' ' + A.Surname, A.Campus,A.iserve FROM Stats_Form  A INNER JOIN IserveCheck B ON A.intid = B.MemberID  WHERE A.iserve = 'Builders Kidz' and A.ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, B.CapturedDate, 23) = '" + getDate + "' and A.Campus = '" + Session["Campus"].ToString() + "'");
        }
        if (TeacherTable.Rows.Count > 0)
        {
            foreach (DataRow rows in TeacherTable.Rows)
            {

                PdfPCell Teacher1 = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);


                PdfPCell Teacher2 = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
     
                SecondTable.AddCell(Teacher2);


                PdfPCell Teacher3 = new PdfPCell(new Phrase(rows[2].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                SecondTable.AddCell(Teacher3);



            }
        }
        else
        {
            PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            Teacher1.Colspan = 3;
            SecondTable.AddCell(Teacher1);
        
        }
        #endregion

        document.Add(SecondTable);


        PdfPTable ThirdTable = new PdfPTable(2);

        ThirdTable.HorizontalAlignment = 1;
        //leave a gap before and after the table

        ThirdTable.SpacingAfter = 8f;

        PdfPCell THirdHeaderT = new PdfPCell(new Phrase("General Comments", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        THirdHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        THirdHeaderT.Colspan = 2;
        ThirdTable.AddCell(THirdHeaderT);



        #region Teachers Headers
        PdfPCell CommentHead = new PdfPCell(new Phrase("Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        CommentHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        ThirdTable.AddCell(CommentHead);


        PdfPCell CommentCampusHead = new PdfPCell(new Phrase("Comment", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        CommentCampusHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        ThirdTable.AddCell(CommentCampusHead);

       
        #endregion

        #region Populate Teachers
        DataTable CommentsTable = new DataTable();
        if (Session["ShowAll"].ToString() == "Yes")
        {
            TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'BuildersKidz' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) = '" + getDate + "'");
        }
        else
        {
            TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'JesusKids' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) = '" + getDate + "' and A.Campus = '" + Session["ChurchID"].ToString() + "'");
        }

        
        if (TeacherTable.Rows.Count > 0)
        {
            foreach (DataRow rows in TeacherTable.Rows)
            {

                PdfPCell Teacher1 = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                ThirdTable.AddCell(Teacher1);


                PdfPCell Teacher2 = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                ThirdTable.AddCell(Teacher2);


           



            }
        }
        else
        {
            PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            Teacher1.Colspan = 2;
            ThirdTable.AddCell(Teacher1);

        }
        #endregion


        document.Add(ThirdTable);
        document.Close();

  
    
    }

    void BuildPDFAdmin()
    {


        string StartDate = "";
        string EndDate = "";
        //if (datepicker.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else if (txtEndDate.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else
        //{
        StartDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        EndDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        // }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "EVANGELISM_PASTOR" + StartDate + " - " + EndDate;

     



        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        Document document = new Document();
        MemoryStream memstream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memstream);
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph("BUILDERS CHURCH WEEKLY REPORT", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(StartDate + " - " + EndDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


        PdfPTable NewTable = new PdfPTable(2);

        NewTable.HorizontalAlignment = 1;
        //leave a gap before and after the table
        NewTable.SpacingBefore = 20f;
        NewTable.SpacingAfter = 8f;

   

        string Delmas5 = "0";
        string Delmas8 = "0";



        string Benoni5 = "0";
        string Benoni8 = "0";


        string Elof5 = "0";
        string Elof58 = "0";




        if (Session["ShowAll"].ToString() == "Yes")
        {

            #region Delmas
            Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

            #endregion

            #region Benoni
            Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

            #endregion

            #region Eloff Campus
            Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

            #endregion


        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                #endregion
            }
        }



        int Head1 = 1;
        int Head2 = 2;
        int Head3 = 3;
        int Head4 = 3;


        if (Head1 > 0)
        {
            PdfPCell Header = new PdfPCell(new Phrase("Volunteers On Duty", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            NewTable.AddCell(Header);
            #region First Section

            #region
            PdfPCell statHeaderAA = new PdfPCell(new Phrase("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            statHeaderAA.Colspan = 2;
            NewTable.AddCell(statHeaderAA);
            #endregion



            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("First Time Visitors Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(Delmas5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("8:00  Glory Experience Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(Benoni5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult1);
            #endregion

            #region row4

            PdfPCell statHeader2 = new PdfPCell(new Phrase("First Time Visitors Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult2);
            #endregion

            #endregion
        }


        if (Head2 > 0)
        {

            NewTable.SpacingBefore = 20f;
            NewTable.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Attendance", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            NewTable.AddCell(Header);
            #region First Section

            #region
            PdfPCell statHeaderAA = new PdfPCell(new Phrase("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            statHeaderAA.Colspan = 2;
            NewTable.AddCell(statHeaderAA);
            #endregion



            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("Hosts - Ushers /Protocol", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(Delmas5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("Builders Kidz Volunteers", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(Benoni5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult1);
            #endregion

            #region row4

            PdfPCell statHeader2 = new PdfPCell(new Phrase("Multi Media", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult2);
            #endregion


            #region row4

            PdfPCell statHeader3 = new PdfPCell(new Phrase("Hosts - Hospitality", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader3);


            PdfPCell statResult4 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult4);
            #endregion



            #region row4

            PdfPCell statHeader5 = new PdfPCell(new Phrase("Builders Worship", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader5);


            PdfPCell statResult6 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult6.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult6);
            #endregion




            #region row4

            PdfPCell statHeader7 = new PdfPCell(new Phrase("CO5s / CO10s", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader7.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader7);


            PdfPCell statResult8 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult8.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult8);
            #endregion


           




            #endregion
        }


        if (Head3 > 0)
        {
            NewTable.SpacingBefore = 20f;
            NewTable.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            NewTable.AddCell(Header);
            #region First Section

            #region
            PdfPCell statHeaderAA = new PdfPCell(new Phrase("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            statHeaderAA.Colspan = 2;
            NewTable.AddCell(statHeaderAA);
            #endregion



            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("Tithe", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(Delmas5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("Offering", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(Benoni5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult1);
            #endregion

            #region row4

            PdfPCell statHeader2 = new PdfPCell(new Phrase("Projects", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(Elof5, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult2);
            #endregion


    




            #endregion
        }

       



        document.Add(NewTable);



        PdfPTable SecondTable = new PdfPTable(4);

        SecondTable.HorizontalAlignment = 1;
        //leave a gap before and after the table

        SecondTable.SpacingAfter = 8f;


        PdfPCell SecondHeaderT11s = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT11s.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT11s.Colspan = 1;
        SecondTable.AddCell(SecondHeaderT11s);

        PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Mandate Statistics", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT.Colspan = 1;
        SecondTable.AddCell(SecondHeaderT);

        PdfPCell SecondHeaderT1 = new PdfPCell(new Phrase("No.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT1.Colspan = 1;
        SecondTable.AddCell(SecondHeaderT1);

        PdfPCell SecondHeaderT11 = new PdfPCell(new Phrase("Comments", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT11.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT11.Colspan = 1;
        SecondTable.AddCell(SecondHeaderT11);

        int Inner1 = 1;
        int Inner2 = 1;
        int Inner3 = 1;
        int Inner4 = 1;
        int Inner5 = 1;

        if (Inner1 > 0)
        {
            #region Teachers Headers
            PdfPCell A1 = new PdfPCell(new Phrase("B", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            A1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            A1.Colspan = 1;
            SecondTable.AddCell(A1);


            PdfPCell Head1s = new PdfPCell(new Phrase("no. of souls saved ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            Head1s.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(Head1s);


            PdfPCell T1 = new PdfPCell(new Phrase("0", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T1);



            PdfPCell T2 = new PdfPCell(new Phrase("Commnent Section", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T2);

            #endregion
        }

        if (Inner2 > 0)
        {
            #region Teachers Headers
            PdfPCell A1 = new PdfPCell(new Phrase("U", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            A1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            A1.Colspan = 1;
            SecondTable.AddCell(A1);


            PdfPCell Head1s = new PdfPCell(new Phrase("attendance new believers experience", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            Head1s.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(Head1s);


            PdfPCell T1 = new PdfPCell(new Phrase("0", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T1);



            PdfPCell T2 = new PdfPCell(new Phrase("Commnent Section", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T2);

            #endregion
        }

        if (Inner3 > 0)
        {
            #region Teachers Headers
            PdfPCell A1 = new PdfPCell(new Phrase("I", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            A1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            A1.Colspan = 1;
            SecondTable.AddCell(A1);


            PdfPCell Head1s = new PdfPCell(new Phrase("no. people baptised", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            Head1s.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(Head1s);


            PdfPCell T1 = new PdfPCell(new Phrase("0", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T1);



            PdfPCell T2 = new PdfPCell(new Phrase("Commnent Section", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T2);

            #endregion
        }

        if (Inner4 > 0)
        {
            #region Teachers Headers
            PdfPCell A1 = new PdfPCell(new Phrase("L", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            A1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            A1.Colspan = 1;
            SecondTable.AddCell(A1);


            PdfPCell Head1s = new PdfPCell(new Phrase("no. of iConnect group meetings", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            Head1s.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(Head1s);


            PdfPCell T1 = new PdfPCell(new Phrase("0", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T1);



            PdfPCell T2 = new PdfPCell(new Phrase("Commnent Section", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T2);

            #endregion
        }


        if (Inner5 > 0)
        {
            #region Teachers Headers
            PdfPCell A1 = new PdfPCell(new Phrase("D", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            A1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            A1.Colspan = 1;
            SecondTable.AddCell(A1);


            PdfPCell Head1s = new PdfPCell(new Phrase("no. of members trained", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            Head1s.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(Head1s);


            PdfPCell T1 = new PdfPCell(new Phrase("0", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T1);



            PdfPCell T2 = new PdfPCell(new Phrase("Commnent Section", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            T2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(T2);

            #endregion
        }



 

        document.Add(SecondTable);


       





        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }

    void WeeklyRep()
    {


        string StartDate = "";
        string EndDate = "";
        //if (datepicker.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else if (txtEndDate.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else
        //{
        StartDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        EndDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        // }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "Weekly Report" + StartDate + " - " + EndDate;





        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        Document document = new Document();
        MemoryStream memstream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memstream);
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph("BUILDERS CHURCH WEEKLY REPORT", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(StartDate + " - " + EndDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


    


    

     
        int FirstRow = 1;
        int GetTotalDel = 0;
        int totRes = 0;
        int GetTotalBen = 0;
        if (FirstRow > 0)
        {
            PdfPTable NewTable = new PdfPTable(2);

            NewTable.HorizontalAlignment = 1;
            //leave a gap before and after the table
            NewTable.SpacingBefore = 20f;
            NewTable.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Attendance", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            NewTable.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {
                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("First Time Visitors Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region

                PdfPCell statHeader1 = new PdfPCell(new Phrase("8:00 Glory Experience Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                string Value2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");
                PdfPCell statResult1 = new PdfPCell(new Phrase(Value2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion



                #region
                PdfPCell statHeader2 = new PdfPCell(new Phrase("08:00 Builders Kidz Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);
                #endregion

                #region row4
                string value3 = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult2 = new PdfPCell(new Phrase(value3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion



                GetTotalBen = int.Parse(Value) + int.Parse(Value2) + int.Parse(value3);

            }
            int Del = 1;
            if (Del > 0)
            {
                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("First Time Visitors Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region

                PdfPCell statHeader1 = new PdfPCell(new Phrase("8:00 Glory Experience Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                string Value2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");
                PdfPCell statResult1 = new PdfPCell(new Phrase(Value2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion



                #region
                PdfPCell statHeader2 = new PdfPCell(new Phrase("08:00 Builders Kidz Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);
                #endregion

                #region row4
                string value3 = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult2 = new PdfPCell(new Phrase(value3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion


                GetTotalDel = int.Parse(Value) + int.Parse(Value2) + int.Parse(value3);
            }


            PdfPCell HeaderA = new PdfPCell(new Phrase("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            NewTable.AddCell(HeaderA);

            totRes = GetTotalBen + GetTotalDel;
            PdfPCell HeaderAA = new PdfPCell(new Phrase(totRes.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            NewTable.AddCell(HeaderAA);

            document.Add(NewTable);
     
        }


        int SecondRow = 1;
        if (SecondRow > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(2);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Volunteers On Duty", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            SecondT.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                table.Columns.Add("DocName", typeof(string));
                table.Rows.Add("Trainers");
                table.Rows.Add("Hosts");
                table.Rows.Add("Ushers");
                table.Rows.Add("Protocol");

                table.Rows.Add("Hospitality");
                table.Rows.Add("Builders Worship");
                table.Rows.Add("Multimedia – Sound");
                table.Rows.Add("Multimedia – Video");
                table.Rows.Add("Builders Kidz");

                table.Rows.Add("J316");
                table.Rows.Add("Leadersheep");
                table.Rows.Add("Tribe Leaders");





                foreach (DataRow rows in table.Rows)
                {
                    string GetTotal = connect.SingleRespSQL("SELECT count(intid)  FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, CapturedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

                    totvol += int.Parse(GetTotal);


                    #region
                    PdfPCell statHeader = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondT.AddCell(statHeader);
                    #endregion

                    #region

                    string Value = GetTotal;
                    PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondT.AddCell(statResult);
                    #endregion
                
                }




                PdfPCell HeaderA = new PdfPCell(new Phrase("Total Volunteers", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderA);

          
                PdfPCell HeaderAA = new PdfPCell(new Phrase(totvol.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);

                document.Add(SecondT);
             

              
            }
           
      

       

        }



        int Income = 1;
        if (Income > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(3);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 3;
            SecondT.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                string  Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";
                table = connect.DTSQL(Getqry);


                if (table.Rows.Count > 0)
                {
                    foreach (DataRow rows in table.Rows)
                    {


                        #region
                        PdfPCell statHeader = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statHeader);
                        #endregion

                        #region


                        PdfPCell statResult = new PdfPCell(new Phrase("R " + rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResult);
                        #endregion


                        #region


                        PdfPCell statResultss = new PdfPCell(new Phrase(rows[2].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResultss);
                        #endregion

                    }




                    PdfPCell HeaderA = new PdfPCell(new Phrase("Total Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderA.Colspan = 2;
                    SecondT.AddCell(HeaderA);


                    string GetTotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("R " + GetTotAmount, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                    SecondT.AddCell(HeaderAA);

                    document.Add(SecondT);
                }
                else
                {
                 


                    PdfPCell HeaderAA = new PdfPCell(new Phrase("No Income for that period", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderAA.Colspan = 3;
                    SecondT.AddCell(HeaderAA);
              

                    document.Add(SecondT);
                }



            }





        }



        int Expense = 1;
        if (Expense > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(3);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Expense", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 3;
            SecondT.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                string Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";
                table = connect.DTSQL(Getqry);


                if (table.Rows.Count > 0)
                {
                    foreach (DataRow rows in table.Rows)
                    {


                        #region
                        PdfPCell statHeader = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statHeader);
                        #endregion

                        #region


                        PdfPCell statResult = new PdfPCell(new Phrase("R " + rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResult);
                        #endregion


                        #region


                        PdfPCell statResultss = new PdfPCell(new Phrase(rows[2].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResultss);
                        #endregion

                    }




                    PdfPCell HeaderA = new PdfPCell(new Phrase("Total Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderA.Colspan = 2;
                    SecondT.AddCell(HeaderA);


                    string GetTotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("R " + GetTotAmount, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                    SecondT.AddCell(HeaderAA);

                    document.Add(SecondT);
                }
                else
                {
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("No Expense for that period", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderAA.Colspan = 3;
                    SecondT.AddCell(HeaderAA);


                    document.Add(SecondT);
                }



            }





        }



        int Build = 1;
        if (Build > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(4);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Mandate Statistics", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            SecondT.AddCell(Header);


            PdfPCell Header1 = new PdfPCell(new Phrase("No.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header1.Colspan = 1;
            SecondT.AddCell(Header1);


            PdfPCell Header2 = new PdfPCell(new Phrase("Comments", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header2.Colspan = 1;
            SecondT.AddCell(Header2);



            int B = 1;
            if (B > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("B", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        
                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of souls saved", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                //document.Add(SecondT);
            }

            int U = 1;
            if (U > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("U", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("attendance new believers experience", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'New Believers Experience'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

               // document.Add(SecondT);
            }


            int I = 1;
            if (I > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("I", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. people baptised", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Baptism'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

              //  document.Add(SecondT);
            }


            int L = 1;
            if (L > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("L", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of iConnect group meetings", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'iConnectGroups'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

              //  document.Add(SecondT);
            }

            int D = 1;
            if (D > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("D", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of members trained", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = "0";
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                
            }


            document.Add(SecondT);


        }




        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }

    void BuildPDFDiscipleshipPastor()
    {


        string StartDate = "";
        string EndDate = "";
        //if (datepicker.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else if (txtEndDate.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else
        //{
            StartDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
       // }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
            string reportName = "EVANGELISM_PASTOR" + StartDate + " - " + EndDate;





        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        Document document = new Document();
        MemoryStream memstream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memstream);
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph("REPORT  | DISCIPLESHIP PASTOR", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(StartDate + " - " + EndDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


        PdfPTable NewTable = new PdfPTable(6);

        NewTable.HorizontalAlignment = 1;
        //leave a gap before and after the table
        NewTable.SpacingBefore = 20f;
        NewTable.SpacingAfter = 8f;

        PdfPCell Header = new PdfPCell(new Phrase("New Converts", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header.Colspan = 6;
        NewTable.AddCell(Header);

        string DelmasBornAgain = "0";
        string J316 = "0";



        string BenoniBornAgain = "0";
        string Other = "0";


        string JesusCrusade = "0";
        string Elof58 = "0";
        



        if (Session["ShowAll"].ToString() == "Yes")
        {

            #region Delmas
            DelmasBornAgain = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

            #endregion

            #region Benoni
            BenoniBornAgain = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

            #endregion

          

        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                DelmasBornAgain = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
          
                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                BenoniBornAgain = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
          
                #endregion
            }

        }

        #region Jesus Crusade (Crusades)
        JesusCrusade = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
        #endregion

        #region J316
        J316 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
        #endregion

        #region Other in ('Glory Experience','iConnectGroups','Aflame','iPray','New Believers Experience','iConnect Experience')
        Other = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in ('Glory Experience','iConnectGroups','Aflame','iPray','New Believers Experience','iConnect Experience') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
        #endregion

        int FirstRow = 1;

        if (FirstRow > 0)
        {
            #region First Section
            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("No. of souls saved (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(DelmasBornAgain, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("No. of souls saved (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(BenoniBornAgain, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult1);
            #endregion

            #region Crusade

            PdfPCell statHeader2 = new PdfPCell(new Phrase("No. of souls saved (Jesus Crusade)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(JesusCrusade, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult2);
            #endregion

            #endregion

            #region Second Section
            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("No. of souls saved (J316)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader3);
            #endregion

            #region
            PdfPCell statResult3 = new PdfPCell(new Phrase(J316, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult3);
            #endregion

            #region
            PdfPCell statHeader4 = new PdfPCell(new Phrase("No. of souls saved (Other)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader4);
            #endregion

            #region row3
            PdfPCell statResult4 = new PdfPCell(new Phrase(Other, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult4);
            #endregion

            #region row4

            PdfPCell statHeader5 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader5);


            PdfPCell statResult5 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult5);
            #endregion

            #endregion
        }

  
        document.Add(NewTable);



        PdfPTable SecondTable = new PdfPTable(6);

        SecondTable.HorizontalAlignment = 1;
        //leave a gap before and after the table

        SecondTable.SpacingAfter = 8f;

        PdfPCell SecondHeaderT = new PdfPCell(new Phrase("New Believers Experience - Add", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        SecondHeaderT.Colspan = 6;
        SecondTable.AddCell(SecondHeaderT);



        #region Teachers Headers
        PdfPCell NameHead = new PdfPCell(new Phrase("Delmas Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        NameHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NameHead.Colspan = 2;
        SecondTable.AddCell(NameHead);


        PdfPCell CampusHead = new PdfPCell(new Phrase("Benoni Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        CampusHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        CampusHead.Colspan = 2;
        SecondTable.AddCell(CampusHead);

        PdfPCell TeacherDuty = new PdfPCell(new Phrase("Eloff Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        TeacherDuty.Colspan = 2;
        SecondTable.AddCell(TeacherDuty);

        #endregion

        int SendRow = 1;
        if (SendRow > 0)
        {

            #region Delmas
            string DelmasInvited = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
           string DelmasAttendend = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");


            #endregion

            #region Benoni
           string BenoniInvited = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
           string BenoniAttended = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion


           #region Eloff
           string EloffInvited = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
           string EloffAttended = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
           #endregion





            #region First Section
            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("No. of souls invited", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(DelmasInvited, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("No. of souls invited", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(BenoniInvited, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult1);
            #endregion

            #region Crusade

            PdfPCell statHeader2 = new PdfPCell(new Phrase("No. of souls invited", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(EloffInvited, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult2);
            #endregion

            #endregion

            #region Second Section
            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("No. of souls actually attended", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader3);
            #endregion

            #region
            PdfPCell statResult3 = new PdfPCell(new Phrase(DelmasAttendend, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult3);
            #endregion

            #region
            PdfPCell statHeader4 = new PdfPCell(new Phrase("No. of souls actually attended", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader4);
            #endregion

            #region row3
            PdfPCell statResult4 = new PdfPCell(new Phrase(BenoniAttended, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult4);
            #endregion

            #region row4

            PdfPCell statHeader5 = new PdfPCell(new Phrase("No. of souls actually attended", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader5);


            PdfPCell statResult5 = new PdfPCell(new Phrase(EloffAttended, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult5);
            #endregion

            #endregion
        
        }






        document.Add(SecondTable);


     


      

        //#region Jesus Crusade
        //PdfPTable fourthTable = new PdfPTable(4);

        //fourthTable.HorizontalAlignment = 1;
        ////leave a gap before and after the table

    

        //PdfPCell FourthHeader1 = new PdfPCell(new Phrase("Jesus Crusades", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        //FourthHeader1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        //FourthHeader1.Colspan = 2;
        //fourthTable.AddCell(FourthHeader1);


        //PdfPCell FourthHeader2 = new PdfPCell(new Phrase("Invite Campaigns", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        //FourthHeader2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        //FourthHeader2.Colspan = 2;
        //fourthTable.AddCell(FourthHeader2);
        //#endregion


        //#region Teachers Headers
        //PdfPCell four1 = new PdfPCell(new Phrase("Date", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        //four1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        //fourthTable.AddCell(four1);


        //PdfPCell four2 = new PdfPCell(new Phrase("Venue", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        //four2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        //fourthTable.AddCell(four2);



        //PdfPCell four3 = new PdfPCell(new Phrase("Date", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        //four3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        //fourthTable.AddCell(four3);


        //PdfPCell four4 = new PdfPCell(new Phrase("Venue", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
        //four4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        //fourthTable.AddCell(four4);


        //#endregion

        //#region Rows

        //PdfPCell Norowsfourth = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        //Norowsfourth.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        //Norowsfourth.Colspan = 4;
        //fourthTable.AddCell(Norowsfourth);

        ////DataTable CommentsTable = new DataTable();
        ////if (Session["ShowAll"].ToString() == "Yes")
        ////{
        ////    TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'JesusKids' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "'");  //and convert(varchar, B.CapturedDate, 23) between '" + StartDate + "' and '" + EndDate + "'
        ////}
        ////else
        ////{
        ////    TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'JesusKids' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "' and A.Campus = '" + Session["ChurchID"].ToString() + "'");
        ////}



        ////if (TeacherTable.Rows.Count > 0)
        ////{
        ////    foreach (DataRow rows in TeacherTable.Rows)
        ////    {

        ////        PdfPCell Teacher1 = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        ////        Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        ////        fourthTable.AddCell(Teacher1);


        ////        PdfPCell Teacher2 = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        ////        Teacher2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

        ////        fourthTable.AddCell(Teacher2);






        ////    }
        ////}
        ////else
        ////{
        ////    PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        ////    Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        ////    Teacher1.Colspan = 2;
        ////    fourthTable.AddCell(Teacher1);

        ////}
        //#endregion





        //document.Add(fourthTable);

















        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }

    void BuildPDFBenoniRep()
    {


        string week1 = "";
        string EndDate = "";
        string week3 = "";
        string week2 = "";
        string week4 = "";
        //if (datepicker.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else if (txtEndDate.Value == "")
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        //else
        //{
        week1 = Convert.ToDateTime("2020-04-05 00:00:00.000").ToString("yyyy-MM-dd");
        week2 = Convert.ToDateTime("2020-04-05 00:00:00.000").ToString("yyyy-MM-dd");
        week3 = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        week4 = Convert.ToDateTime("2020-04-05 00:00:00.000").ToString("yyyy-MM-dd");
        // }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "Campus" + week1 + " - " + EndDate;





        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        Document document = new Document();
        MemoryStream memstream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memstream);
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph(Session["Campus"].ToString() + " | Weekly Report" , new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(week1 + " - " + EndDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


        PdfPTable NewTable = new PdfPTable(6);

        NewTable.HorizontalAlignment = 1;
        //leave a gap before and after the table
        NewTable.SpacingBefore = 20f;
        NewTable.SpacingAfter = 8f;


        PdfPCell Header = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header.Colspan = 1;
        NewTable.AddCell(Header);

        PdfPCell Header1 = new PdfPCell(new Phrase("Week 1", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header1.Colspan = 1;
        NewTable.AddCell(Header1);


        PdfPCell Header2 = new PdfPCell(new Phrase("Week 2", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header2.Colspan = 1;
        NewTable.AddCell(Header2);

        PdfPCell Header3 = new PdfPCell(new Phrase("Week 3", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header3.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header3.Colspan = 1;
        NewTable.AddCell(Header3);

        PdfPCell Header4 = new PdfPCell(new Phrase("Week 4", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header4.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header4.Colspan = 1;
        NewTable.AddCell(Header4);


        PdfPCell Header5 = new PdfPCell(new Phrase("Term Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header5.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header5.Colspan = 1;
        NewTable.AddCell(Header5);

        string Delmas5 = "0";
        string Delmas8 = "0";



        string Benoni5 = "0";
        string Benoni8 = "0";


        string Elof5 = "0";
        string Elof58 = "0";




        if (Session["ShowAll"].ToString() == "Yes")
        {

            #region Delmas
            Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
            Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

            #endregion

            #region Benoni
            Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
            Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

            #endregion

            #region Eloff Campus
            Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
            Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

            #endregion


        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
                Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
                Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");
                Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + week1 + "'  and  '" + EndDate + "' ");

                #endregion
            }
        }


        int BEGET = 1;
        int UNVEIL = 1;
        int Initiative = 1;
        int LEAD = 1;
        int PeoplePerZone = 1;
        int SundayConnect = 1;
        int Iserve = 1;
        int ChurchAttendance = 1;
        if (BEGET > 0)
        {
            #region First Section

            string AlterCall1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
            string AlterCall2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
            string AlterCall3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3+ "'");
            string AlterCall4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
            int AlterCallTot = int.Parse(AlterCall1) + int.Parse(AlterCall2) + int.Parse(AlterCall3) + int.Parse(AlterCall4);

            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("BEGET (souls saved)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResultt = new PdfPCell(new Phrase("Alter Calls", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResultt.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResultt);
            #endregion
            #region
            PdfPCell statResult = new PdfPCell(new Phrase(AlterCall1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase(AlterCall2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(AlterCall3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult1);
            #endregion

            #region row4

            PdfPCell statHeader2 = new PdfPCell(new Phrase(AlterCall4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(AlterCallTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult2);
            #endregion

            #endregion

            #region Second Section
            string Followup1 = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23)  = '" + week1 + "'");
            string Followup2 = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23)  = '" + week2 + "'");
            string Followup3 = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23)  = '" + week3 + "'");
            string Followup4 = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23)  = '" + week4 + "'");
            int FollowupTot = int.Parse(Followup1) + int.Parse(Followup2) + int.Parse(Followup3) + int.Parse(Followup4);
       
            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("Follow Ups", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader3);
            #endregion

            #region
            PdfPCell statResult3 = new PdfPCell(new Phrase(Followup1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult3);
            #endregion

            #region
            PdfPCell statHeader4 = new PdfPCell(new Phrase(Followup2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader4);
            #endregion

            #region row3
            PdfPCell statResult4 = new PdfPCell(new Phrase(Followup3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult4);
            #endregion

            #region row4

            PdfPCell statHeader5 = new PdfPCell(new Phrase(Followup4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader5);


            PdfPCell statResult5 = new PdfPCell(new Phrase(FollowupTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult5);
            #endregion

            #endregion



            #region Third Section
            string SchoolConnect1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" +  week1 + "'");
            string SchoolConnect2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
            string SchoolConnect3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
            string SchoolConnect4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
            int SchoolConnectTot = int.Parse(SchoolConnect1) + int.Parse(SchoolConnect2) + int.Parse(SchoolConnect3) + int.Parse(SchoolConnect4);
         
            #region
            PdfPCell statHeaderr4 = new PdfPCell(new Phrase("School Connect", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr4);
            #endregion

            #region
            PdfPCell statResult6 = new PdfPCell(new Phrase(SchoolConnect1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult6.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult6);
            #endregion

            #region
            PdfPCell statHeader7 = new PdfPCell(new Phrase(SchoolConnect2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader7.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader7);
            #endregion

            #region row3
            PdfPCell statResult8 = new PdfPCell(new Phrase(SchoolConnect3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult8.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult8);
            #endregion

            #region row4

            PdfPCell statHeader9 = new PdfPCell(new Phrase(SchoolConnect4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader9.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader9);


            PdfPCell statResult500 = new PdfPCell(new Phrase(SchoolConnectTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult500.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult500);
            #endregion

            #endregion


            #region Four Section

            string J3161 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
            string J3162 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
            string J3163 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
            string J3164 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
            int J316Tot = int.Parse(J3161) + int.Parse(J3162) + int.Parse(J3163) + int.Parse(J3164);



            #region
            PdfPCell statHeaderr10 = new PdfPCell(new Phrase("J316", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr10.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr10);
            #endregion

            #region
            PdfPCell statResult11 = new PdfPCell(new Phrase(J3161, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult11.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult11);
            #endregion

            #region
            PdfPCell statHeader12 = new PdfPCell(new Phrase(J3162, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader12.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader12);
            #endregion

            #region row3
            PdfPCell statResult13 = new PdfPCell(new Phrase(J3163, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult13.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult13);
            #endregion

            #region row4

            PdfPCell statHeader14 = new PdfPCell(new Phrase(J3164, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader14.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader14);


            PdfPCell statResult15 = new PdfPCell(new Phrase(J316Tot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult15.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult15);
            #endregion

            #endregion

            #region Four Section


            string Crusade1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
            string Crusade2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
            string Crusade3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
            string Crusade4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
            int CrusadeTot = int.Parse(Crusade1) + int.Parse(Crusade2) + int.Parse(Crusade3) + int.Parse(Crusade4);

          
            #region
            PdfPCell statHeaderr16 = new PdfPCell(new Phrase("Jesus Crusade", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr16.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr16);
            #endregion

            #region
            PdfPCell statResult17 = new PdfPCell(new Phrase(Crusade1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult17.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult17);
            #endregion

            #region
            PdfPCell statHeader18 = new PdfPCell(new Phrase(Crusade2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader18.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader18);
            #endregion

            #region row3
            PdfPCell statResult19 = new PdfPCell(new Phrase(Crusade3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult19.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult19);
            #endregion

            #region row4

            PdfPCell statHeader20 = new PdfPCell(new Phrase(Crusade4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader20.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader20);


            PdfPCell statResult21 = new PdfPCell(new Phrase(CrusadeTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult21.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResult21);
            #endregion

            #endregion
        }


        if (UNVEIL > 0)
        {
            #region First Section

            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("UNVEIL", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion





            #region
            PdfPCell statResultt = new PdfPCell(new Phrase("New Believers Classes Held", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResultt.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResultt);
            #endregion
            int FirstSection = 1;
            if (FirstSection > 0)
            {
                string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                #region


                PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region
                PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion

                #region row4

                PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);


                PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion
            }

            #endregion
              int Secection = 1;
              if (Secection > 0)
              {
                  #region Second Section

                  #region
                  PdfPCell statHeader3 = new PdfPCell(new Phrase("#People Invited", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader3);
                  #endregion

                  string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                  string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                  string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                  string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                  int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                  #region


                  PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult);
                  #endregion

                  #region
                  PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader1);
                  #endregion

                  #region row3
                  PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult1);
                  #endregion

                  #region row4

                  PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader2);


                  PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult2);
                  #endregion

                  #endregion
              }


             int ThirdSection = 1;
             if (ThirdSection > 0)
             {
                

                 #region Third Section

                 #region
                 PdfPCell statHeaderr4 = new PdfPCell(new Phrase("#People Attended", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statHeaderr4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statHeaderr4);
                 #endregion

                 string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                 string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                 string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                 string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                 int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                 #region


                 PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statResult);
                 #endregion

                 #region
                 PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statHeader1);
                 #endregion

                 #region row3
                 PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statResult1);
                 #endregion

                 #region row4

                 PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statHeader2);


                 PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                 statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 NewTable.AddCell(statResult2);
                 #endregion

                 #endregion
             }
        }


        if (Initiative > 0)
        {
           
       

            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("INITIATE", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion




            #region First Section

              int ThirdSection = 1;
              if (ThirdSection > 0)
              {

                  #region
                  PdfPCell statHeader3 = new PdfPCell(new Phrase("#Baptism Sessions", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader3);
                  #endregion

                  string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                  string Gen2 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                  string Gen3 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                  string Gen4 = connect.SingleRespSQL("SELECT COUNT(intid) FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                  int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                  #region


                  PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult);
                  #endregion

                  #region
                  PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader1);
                  #endregion

                  #region row3
                  PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult1);
                  #endregion

                  #region row4

                  PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader2);


                  PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult2);
                  #endregion


              }
            #endregion
              #region Second Section

               int FourthSection = 1;
               if (FourthSection > 0)
               {
                   #region
                   PdfPCell statHeader3 = new PdfPCell(new Phrase("#Baptism Ready", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statHeader3);
                   #endregion

                   string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(baptismReadySouls AS int)) is null then '0' Else SUM(CAST(baptismReadySouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                   string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(baptismReadySouls AS int)) is null then '0' Else SUM(CAST(baptismReadySouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                   string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(baptismReadySouls AS int)) is null then '0' Else SUM(CAST(baptismReadySouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                   string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(baptismReadySouls AS int)) is null then '0' Else SUM(CAST(baptismReadySouls AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                   int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                   #region


                   PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statResult);
                   #endregion

                   #region
                   PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statHeader1);
                   #endregion

                   #region row3
                   PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statResult1);
                   #endregion

                   #region row4

                   PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statHeader2);


                   PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                   statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                   NewTable.AddCell(statResult2);
                   #endregion
               }
            #endregion



            #region Third Section

          
            int FithSection = 1;
            if (FithSection > 0)
            {
                #region
                PdfPCell statHeader3 = new PdfPCell(new Phrase("#Baptized People", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader3);
                #endregion

                string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                #region


                PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region
                PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion

                #region row4

                PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);


                PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion
            }
            #endregion


            #region Four Section

            #region
            PdfPCell statHeaderr10 = new PdfPCell(new Phrase("iConnect Session", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr10.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr10);
            #endregion
              int SixthSection = 1;
              if (SixthSection > 0)
              {
                  string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                  string Gen2 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                  string Gen3 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                  string Gen4 = connect.SingleRespSQL("SELECT COUNT(intid) FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                  int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                  #region


                  PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult);
                  #endregion

                  #region
                  PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader1);
                  #endregion

                  #region row3
                  PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult1);
                  #endregion

                  #region row4

                  PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader2);


                  PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult2);
                  #endregion
              }

            #endregion

            #region Fith Section

            #region
            PdfPCell statHeaderr16 = new PdfPCell(new Phrase("iConnect Experience Attended", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr16.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr16);
            #endregion
              int SevenSection = 1;
              if (SevenSection > 0)
              {
                  string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                  string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                  string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                  string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                  int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                  #region


                  PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult);
                  #endregion

                  #region
                  PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader1);
                  #endregion

                  #region row3
                  PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult1);
                  #endregion

                  #region row4

                  PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader2);


                  PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult2);
                  #endregion
              }

            #endregion
        }

        if (LEAD > 0)
        {
            #region First Section

            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("LEAD", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion

           int SixthSection = 1;
           if (SixthSection > 0)
           {




               #region
               PdfPCell statResultt = new PdfPCell(new Phrase("No. Of Iconnect Groups", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statResultt.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statResultt);
               #endregion
               string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid)  FROM iConnect WHERE IsActive = '1' and Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
               string Gen2 = Gen1;//connect.SingleRespSQL("SELECT COUNT(intid)  FROM iConnect WHERE IsActive = '1' and Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
               string Gen3 = Gen1;// connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
               string Gen4 = Gen1;// connect.SingleRespSQL("SELECT COUNT(intid) FROM Attendance WHERE TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
               int GenTot = int.Parse(Gen1);//
               #region


               PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statResult);
               #endregion

               #region
               PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statHeader1);
               #endregion

               #region row3
               PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statResult1);
               #endregion

               #region row4

               PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statHeader2);


               PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
               statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
               NewTable.AddCell(statResult2);
               #endregion
           }

            #endregion

            #region Second Section

            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("No. Of Iconnect Groups Active", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader3);
            #endregion
              int SevenSection = 1;
              if (SevenSection > 0)
              {
                  string Gen1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                  string Gen2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                  string Gen3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                  string Gen4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                  int GenTot = int.Parse(Gen1) + int.Parse(Gen2) + int.Parse(Gen3) + int.Parse(Gen4);
                  #region


                  PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult);
                  #endregion

                  #region
                  PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader1);
                  #endregion

                  #region row3
                  PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult1);
                  #endregion

                  #region row4

                  PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statHeader2);


                  PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                  statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                  NewTable.AddCell(statResult2);
                  #endregion
              }

            #endregion



     
        }


        if (PeoplePerZone > 0)
        {
            #region First Section

            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("No. Of People Per Zone", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion

          



       

            int First = 1;
            if (First > 1)
            {
                PdfPCell statResultts = new PdfPCell(new Phrase("Delmas Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultts.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        
                NewTable.AddCell(statResultts);

                string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and MemberType = 'Member'");
                string Gen2 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Gen3 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Gen4 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                int GenTot = int.Parse(Gen1);
                #region


                PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region
                PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion

                #region row4

                PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);


                PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion
            }

            int Sec = 1;
            if (Sec > 1)
            {
                PdfPCell statResultts = new PdfPCell(new Phrase("Benoni Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultts.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                NewTable.AddCell(statResultts);

                string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and MemberType = 'Member'");
                string Gen2 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Gen3 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Gen4 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                int GenTot = int.Parse(Gen1);
                #region


                PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region
                PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion

                #region row4

                PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);


                PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion
            }

            int Thre = 1;
            if (Thre > 1)
            {
                PdfPCell statResultts = new PdfPCell(new Phrase("Eloff Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultts.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                NewTable.AddCell(statResultts);

                string Gen1 = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and Campus = 'Eloff Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and MemberType = 'Member'");
                string Gen2 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Gen3 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Gen4 = Gen1; // connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");
                int GenTot = int.Parse(Gen1);
                #region


                PdfPCell statResult = new PdfPCell(new Phrase(Gen1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region
                PdfPCell statHeader1 = new PdfPCell(new Phrase(Gen2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                PdfPCell statResult1 = new PdfPCell(new Phrase(Gen3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion

                #region row4

                PdfPCell statHeader2 = new PdfPCell(new Phrase(Gen4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);


                PdfPCell statResult2 = new PdfPCell(new Phrase(GenTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion
            }

            #endregion

         
        }

        if (SundayConnect > 0)
        {
            #region First Section
            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("No. Of School Connects", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion


          



         

            DataTable Ntable = new DataTable();

            Ntable = connect.DTSQL("SELECT DISTINCT Zonename  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'");
            if (Ntable.Rows.Count > 0)
            {
                foreach (DataRow rows in Ntable.Rows)
                {
                    #region Heading
                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                    NewTable.AddCell(TeacherDuty);
                    #endregion

                    #region Count

                    string Week1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Zonename = '" + rows[0].ToString() + "' and  isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                    string Week2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Zonename = '" + rows[0].ToString() + "' and  isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                    string Week3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Zonename = '" + rows[0].ToString() + "' and  isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                    string Week4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Zonename = '" + rows[0].ToString() + "' and  isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");

                    int WeekTot = int.Parse(Week1) + int.Parse(Week2) + int.Parse(Week3) + int.Parse(Week4);
                    PdfPCell statResultd = new PdfPCell(new Phrase(Week1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd);
                    #endregion


                    #region Count
                    PdfPCell statResultd2 = new PdfPCell(new Phrase(Week2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd2);
                    #endregion


                    #region Count
                    PdfPCell statResultd3 = new PdfPCell(new Phrase(Week3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd3);
                    #endregion


                    #region Count
                    PdfPCell statResultd4 = new PdfPCell(new Phrase(Week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd4);
                    #endregion

                    #region Count
                    PdfPCell statResultd5 = new PdfPCell(new Phrase(WeekTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd5);
                    #endregion
                }

            }
            else
            {
                #region Count
                PdfPCell statResultd = new PdfPCell(new Phrase("No School Connects", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                statResultd.Colspan = 6;
                NewTable.AddCell(statResultd);
                #endregion
            }



     
            #endregion

         
        }


        if (Iserve > 0)
        {

            #region First Section
            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("iServe Departments", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion








            DataTable Ntable = new DataTable();

            Ntable = connect.DTSQL("SELECT DISTINCT Iserve  FROM IserveCheck WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'");
            if (Ntable.Rows.Count > 0)
            {
                foreach (DataRow rows in Ntable.Rows)
                {
                    #region Heading
                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);

                    NewTable.AddCell(TeacherDuty);
                    #endregion

                    #region Count

                    string Week1 = connect.SingleRespSQL("SELECT COUNT(intid) FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, CapturedBy, 23) = '" + week1 + "'");
                    string Week2 = connect.SingleRespSQL("SELECT COUNT(intid) FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, CapturedBy, 23) = '" + week2 + "'");
                    string Week3 = connect.SingleRespSQL("SELECT COUNT(intid) FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, CapturedBy, 23) = '" + week3 + "'");
                    string Week4 = connect.SingleRespSQL("SELECT COUNT(intid) FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, CapturedBy, 23) = '" + week4 + "'");

                    int WeekTot = int.Parse(Week1) + int.Parse(Week2) + int.Parse(Week3) + int.Parse(Week4);
                    PdfPCell statResultd = new PdfPCell(new Phrase(Week1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd);
                    #endregion


                    #region Count
                    PdfPCell statResultd2 = new PdfPCell(new Phrase(Week2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd2);
                    #endregion


                    #region Count
                    PdfPCell statResultd3 = new PdfPCell(new Phrase(Week3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd3);
                    #endregion


                    #region Count
                    PdfPCell statResultd4 = new PdfPCell(new Phrase(Week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd4);
                    #endregion

                    #region Count
                    PdfPCell statResultd5 = new PdfPCell(new Phrase(WeekTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    NewTable.AddCell(statResultd5);
                    #endregion
                }

            }
            else
            {
                #region Count
                PdfPCell statResultd = new PdfPCell(new Phrase("No Iserve Checked In", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                statResultd.Colspan = 6;
                NewTable.AddCell(statResultd);
                #endregion
            }




            #endregion

        }


        if (ChurchAttendance > 0)
        {
            #region First Section


            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("Church Attendence", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            statHeader.Colspan = 6;
            NewTable.AddCell(statHeader);
            #endregion

          




            #region
            PdfPCell statResultt = new PdfPCell(new Phrase("Glory Experience", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResultt.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statResultt);
            #endregion

            int one = 1;
            if (one > 0)
            {
                #region Count

                string Week1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Glory Experience' and   ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
                string Week2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE  TypeService = 'Glory Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week2 + "'");
                string Week3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Glory Experience' and   ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
                string Week4 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE  TypeService = 'Glory Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week4 + "'");

                int WeekTot = int.Parse(Week1) + int.Parse(Week2) + int.Parse(Week3) + int.Parse(Week4);
                PdfPCell statResultd = new PdfPCell(new Phrase(Week1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd);
                #endregion


                #region Count
                PdfPCell statResultd2 = new PdfPCell(new Phrase(Week2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd2);
                #endregion


                #region Count
                PdfPCell statResultd3 = new PdfPCell(new Phrase(Week3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd3);
                #endregion


                #region Count
                PdfPCell statResultd4 = new PdfPCell(new Phrase(Week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd4);
                #endregion

                #region Count
                PdfPCell statResultd5 = new PdfPCell(new Phrase(WeekTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd5);
                #endregion
            }

            #endregion

            #region Second Section

            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("Builders Kidz", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeader3);
            #endregion
            int Two = 1;
            if (Two > 0)
            {
                #region Count

                string Week1 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM KidsCheckedIn A INNER JOIN Stats_Form B ON A.KidID = B.intid  WHERE  A.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, A.CreatedDate, 23)  = '" + week1 + "'");
                string Week2 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM KidsCheckedIn A INNER JOIN Stats_Form B ON A.KidID = B.intid  WHERE  A.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, A.CreatedDate, 23)  = '" + week2 + "'");
                string Week3 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM KidsCheckedIn A INNER JOIN Stats_Form B ON A.KidID = B.intid  WHERE  A.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, A.CreatedDate, 23)  = '" + week3 + "'");
                string Week4 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM KidsCheckedIn A INNER JOIN Stats_Form B ON A.KidID = B.intid  WHERE  A.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, A.CreatedDate, 23)  = '" + week4 + "'");

                int WeekTot = int.Parse(Week1) + int.Parse(Week2) + int.Parse(Week3) + int.Parse(Week4);
                PdfPCell statResultd = new PdfPCell(new Phrase(Week1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd);
                #endregion


                #region Count
                PdfPCell statResultd2 = new PdfPCell(new Phrase(Week2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd2);
                #endregion


                #region Count
                PdfPCell statResultd3 = new PdfPCell(new Phrase(Week3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd3);
                #endregion


                #region Count
                PdfPCell statResultd4 = new PdfPCell(new Phrase(Week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd4);
                #endregion

                #region Count
                PdfPCell statResultd5 = new PdfPCell(new Phrase(WeekTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd5);
                #endregion
            }

            #endregion

            #region Third Section

            #region
            PdfPCell statHeaderr4 = new PdfPCell(new Phrase("Visitors", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeaderr4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NewTable.AddCell(statHeaderr4);
            #endregion

            int Third = 1;
            if (Third > 0)
            {
                #region Count

                string Week1 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Stats_Form B   WHERE B.MemberType = 'Visitor' and B.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, B.DateCaptured, 23)  = '" + week1 + "'");
                string Week2 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Stats_Form B   WHERE B.MemberType = 'Visitor' and B.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, B.DateCaptured, 23)  = '" + week2 + "'");
                string Week3 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Stats_Form B   WHERE B.MemberType = 'Visitor' and B.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, B.DateCaptured, 23)  = '" + week3 + "'");
                string Week4 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Stats_Form B   WHERE B.MemberType = 'Visitor' and B.ChurchID = '" + Session["ChurchID"].ToString() + "' and B.Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, B.DateCaptured, 23)  = '" + week4 + "'");

                int WeekTot = int.Parse(Week1) + int.Parse(Week2) + int.Parse(Week3) + int.Parse(Week4);
                PdfPCell statResultd = new PdfPCell(new Phrase(Week1, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd);
                #endregion


                #region Count
                PdfPCell statResultd2 = new PdfPCell(new Phrase(Week2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd2);
                #endregion


                #region Count
                PdfPCell statResultd3 = new PdfPCell(new Phrase(Week3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd3);
                #endregion


                #region Count
                PdfPCell statResultd4 = new PdfPCell(new Phrase(Week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd4);
                #endregion

                #region Count
                PdfPCell statResultd5 = new PdfPCell(new Phrase(WeekTot.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResultd5);
                #endregion
            }

            #endregion
        }



        


        
        document.Add(NewTable);



        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }

    void UpdateAllUsers()
    {
        DataTable Ntable = new DataTable();
        Ntable = connect.DTSQL("SELECT name,surname FROM ChurchUsers  WHERE churchid = '1'");
        if (Ntable.Rows.Count > 0)
        {
            foreach (DataRow rows in Ntable.Rows)
            {
                DataTable UserT = new DataTable();
                UserT = connect.DTSQL("SELECT  intid,Campus  FROM Stats_form WHERE  name = '" + rows[0].ToString() + "' and surname = '" + rows[1].ToString() + "'");
                if (UserT.Rows.Count > 0)
                {
                    foreach (DataRow UserRows in UserT.Rows)
                    {
                        connect.SingleIntSQL("UPDATE ChurchUsers SET Campus  = '" + UserRows[1].ToString() + "' ,  MemberID = '" + UserRows[0].ToString() + "' WHERE  name = '" + rows[0].ToString() + "' and surname = '" + rows[1].ToString() + "'");
                    }
                }
            }
        }
    }

    void SendMessages(string CellNo, string Msg)
    {
        // This URL is used for sending messages
        string myURI = "https://api.bulksms.com/v1/messages";

        // change these values to match your own account
        string myUsername = ConfigurationManager.AppSettings["SMSGatewayUsername"].ToString();
        string myPassword = ConfigurationManager.AppSettings["SMSGatewayPassword"].ToString();

      
        string first_xter = CellNo.Substring(0, 1);
        if (first_xter == "0")
        {
            CellNo = "27" + CellNo.Remove(0, 1);

        }
       
        // the details of the message we want to send
        string myData = "{to: \"" + CellNo + "\", body:\"" +  Msg + "\"}";

        // build the request based on the supplied settings
        var request = WebRequest.Create(myURI);

        // supply the credentials
        request.Credentials = new NetworkCredential(myUsername, myPassword);
        request.PreAuthenticate = true;
        // we want to use HTTP POST
        request.Method = "POST";
        // for this API, the type must always be JSON
        request.ContentType = "application/json";

        // Here we use Unicode encoding, but ASCIIEncoding would also work
        var encoding = new UnicodeEncoding();
        var encodedData = encoding.GetBytes(myData);

        // Write the data to the request stream
        var stream = request.GetRequestStream();
        stream.Write(encodedData, 0, encodedData.Length);
        stream.Close();

        // try ... catch to handle errors nicely
        try
        {
            // make the call to the API
            var response = request.GetResponse();

        }
        catch (WebException ex)
        {
            // show the general message
            logthefile("An error occurred:" + ex.Message);


            // print the detail that comes with the error
            //var reader = new StreamReader(ex.Response.GetResponseStream());
            //logthefile("Error details:" + reader.ReadToEnd());
        }
        catch (Exception ex)
        { 
             logthefile("An error occurred:" + ex.Message);
        }



    }

    void SendLoopMessages()
    {
        for (int i = 0; i < 20; i++)
        {
            // This URL is used for sending messages
            string myURI = "https://api.bulksms.com/v1/messages";

            // change these values to match your own account
            string myUsername = "jaybuh";
            string myPassword = "Msomi101!@#";
           // string CellNo = "27731840951";
             string CellNo = "27662431271";
            string Msg = "";

            Msg = "This is loop to tell you that i love you 5 times. I love you Anna. Loop : " + i;
            // the details of the message we want to send
            string myData = "{to: \"" + CellNo + "\", body:\"" + Msg + "\"}";

            // build the request based on the supplied settings
            var request = WebRequest.Create(myURI);

            // supply the credentials
            request.Credentials = new NetworkCredential(myUsername, myPassword);
            request.PreAuthenticate = true;
            // we want to use HTTP POST
            request.Method = "POST";
            // for this API, the type must always be JSON
            request.ContentType = "application/json";

            // Here we use Unicode encoding, but ASCIIEncoding would also work
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);

            // Write the data to the request stream
            var stream = request.GetRequestStream();
            stream.Write(encodedData, 0, encodedData.Length);
            stream.Close();

            // try ... catch to handle errors nicely
            try
            {
                // make the call to the API
                var response = request.GetResponse();
                // read the response and print it to the console
                var reader = new StreamReader(response.GetResponseStream());
                logthefile(reader.ReadToEnd());
            }
            catch (WebException ex)
            {
                // show the general message
                logthefile("An error occurred:" + ex.Message);


                // print the detail that comes with the error
                var reader = new StreamReader(ex.Response.GetResponseStream());
                logthefile("Error details:" + reader.ReadToEnd());
            }


        }

    }

    void RunMenus()
    {
        string MenuName = "";
        string Pageurl = "";

        string html = "";


         MenuDatatble MenuT = new MenuDatatble();
        DataTable tMenu = MenuT.ReturnMenuT();
        if (tMenu.Rows.Count > 0)
        {

            html = @"<div class='kt-aside__head'>
				<h3 class='kt-aside__title'>
				" + Session["ChurchName"].ToString() + "";
            html += @"</h3>
				<a href='#' class='kt-aside__close' id='kt_aside_close'><i class='flaticon2-delete'></i></a>
			</div>
			<div class='kt-aside__body'>

			
				<div class='kt-aside-menu-wrapper' id='kt_aside_menu_wrapper'>
					<div id='kt_aside_menu' class='kt-aside-menu ' data-ktmenu-vertical='1' data-ktmenu-scroll='1'>
						<ul class='kt-menu__nav '>";


            foreach (DataRow rows in tMenu.Rows)
            {

                MenuName = rows[0].ToString();
                Pageurl = rows[1].ToString();


                if (MenuName == "New Membership Applications")
                {
                    PageTitle.InnerText = MenuName;
                
                    html += @"<li class='kt-menu__item ' aria-haspopup='true'><a href='" + Pageurl + "' class='kt-menu__link '><span class='kt-menu__link-text'>" + MenuName + "</span></a></li>";
                }
                else
                {
                    html += @"<li class='kt-menu__item  kt-menu__item--active' aria-haspopup='true'><a href='" + Pageurl + "' class='kt-menu__link '><span class='kt-menu__link-text'>" + MenuName + "</span></a></li>";
                }



            }

            html += @"</ul>
					</div>
				</div>
		</div>";
        }
       MenuStream.Text = html;
    }


    protected void UploadFile_Click_ServerClick(object sender, EventArgs e)
    {
        if (BulkLinkFileUpload.HasFile)
        {
            if (Path.GetExtension(BulkLinkFileUpload.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(BulkLinkFileUpload.PostedFile.InputStream))
                {
                    var tbl = new DataTable();
                    var ws = excel.Workbook.Worksheets[1];
                    var hasHeader = true;  // adjust accordingly
                    // add DataColumns to DataTable
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
                            : String.Format("Column {0}", firstRowCell.Start.Column));

                    // add DataRows to DataTable
                    int startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.NewRow();
                        foreach (var cell in wsRow)
                            row[cell.Start.Column - 1] = cell.Text;
                        tbl.Rows.Add(row);
                    }
                    var msg = String.Format("We have imported your excel with {0} Rows, is that correct?", tbl.Rows.Count);
                    UploadStatusLabel.Text = msg;
                    #region




                    if (tbl.Rows.Count > 0)
                    {
                        ViewState["dtbl"] = tbl;
                        btnImport.Visible = true;
                        BulkLinkFileUpload.Visible = false;
                        UploadFile_Click.Visible = false;
                    }
                    else
                    {
                        //NotieAlert("Your Excel Seems to be empty or is not the correct template, Please confirm", 3);
                    }

                    #endregion
                }
            }
            else
            {
                //NotieAlert("Please Note we can only accept .xlsx Excel files", 3);
            }
        }
        else
        {
           // NotieAlert("No attachment selected. Please try again", 3);
        }
    }

    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
    }

    protected void btnImport_ServerClick(object sender, EventArgs e)
    {

        Random r = new Random();
        string MemberNo = "B_Church";
        int counter = 0;
        DataTable excel = (DataTable)ViewState["dtbl"];

        try
        {
            if (excel.Rows.Count > 0)
            {
                foreach (DataRow dtRows in excel.Rows)
                {
                    string Row1 = dtRows[0].ToString();
                    string Row2 = dtRows[1].ToString().Trim();
                    string Row3 = dtRows[2].ToString();
                    string Row4 = dtRows[3].ToString();
                    string Row5 = dtRows[4].ToString();
                    string Row6 = dtRows[5].ToString();
                    string Row7 = dtRows[6].ToString();
                    string Row8 = dtRows[7].ToString();
                    string Row9 = dtRows[8].ToString();
                    string Row10 = dtRows[9].ToString();
                    string Row11 = dtRows[10].ToString();
                   // string Row12 = dtRows[11].ToString();
                    string DOB = "";
                    if (Row4 == "")
                    {
                        DOB = Convert.ToDateTime("1900-01-01 00:00:00.000").ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        try
                        {
                            DOB = Convert.ToDateTime(Row4).ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {

                            DOB = Convert.ToDateTime("1900-01-01 00:00:00.000").ToString("yyyy-MM-dd");
                        }
                       
                    }


                    string MarriageD = "";
                    if (Row10 == "")
                    {
                        MarriageD = Convert.ToDateTime("1900-01-01 00:00:00.000").ToString("yyyy-MM-dd");
                       
                    }
                    else
                    {
                       

                        try
                        {
                            MarriageD = Convert.ToDateTime(Row10).ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {

                            MarriageD = Convert.ToDateTime("1900-01-01 00:00:00.000").ToString("yyyy-MM-dd");
                        }

                    }
                    string MemberNo1 = MemberNo + "_" + RandomString(5, r);

                    string EmailAdd = "";


                    if (IsValidEmail(Row8))
                    {
                        EmailAdd = Row8;

                    }
                    else
                    {
                        EmailAdd = "";
                    }


                    string Address = "";

                    if (Row5 == "")
                    {
                        Address = "No Address";
                    }
                    else
                    {
                        Address = RemoveSpecialChars(Row5);
                    }


                    if (Row1 != "")
                    {

                        string queryString = "INSERT INTO Stats_Form (Surname, Name, Gender,DOB,ResidentAdd,Churchzone,Celno,EmailAdd,MaritalStat,MarriedDate,ChurchGroup,Campus,sms,MemberType,IsActive,MemberNo,ChurchID,CapturedBy,DateCaptured) VALUES ('" + Row1 + "','" + Row2 + "','" + Row3 + "','" + DOB + "','" + Address + "','" + Row6 + "','" + Row7 + "','" + EmailAdd + "','" + Row9 + "','" + MarriageD + "','" + Row11 + "','Eloff Campus','Yes','Member','1','" + MemberNo1 + "','1','System',GETDATE())";
                        connect.SingleIntSQL(queryString);
                    }



                    counter++;

                }

                SaveNotie();
            }
            else
            {
                Response.Write("no table Details");
                logthefile("no table Details");
            }

            btnImport.Visible = false;

            UploadStatusLabel.Text = counter.ToString() + " Items have been Handled see below for the per line outcome";

            txtactionshistory.Text = "";

        }
        catch (Exception ex)
        {
            logthefile("Something Wrong " + ex.ToString());

        }
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            if (Session["AccessRights"].ToString() == "Admin")
            {
                IsAdmin.Value = "1";
                DivAddMember.Visible = true;
            }

            RunMembers();
        }
        else
        {
            IsAdmin.Value = "1";
            RunMembers();
            DivAddMember.Visible = false;
        }


        RunMenus();
       
    }

   


    void RunMembers()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT intid, Name + ' ' + Surname,gender,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'NewMember'  ORDER BY  Name + ' ' + Surname ASC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                    "    <th> Gender </th> " +

                     "    <th > Cell No</th> " +
                      "    <th >New Member No</th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a> &nbsp;	&nbsp;	&nbsp;<a onclick='BtnConvert(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Convert </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[4].ToString() + "</td> " +
                              "   <td >" + Row[5].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No New Membership Applications";
        }


        htmltext += "    </tbody> " +
                   " </table>";
       // tbTable.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "MemberApp.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }

    public static string RandomString(int length,Random R)
    {
        var chars = "0123456789";
        var stringChars = new char[length];
        var random = R;

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);


        return finalString.ToString();
    }

    #region Message Boxes
    void MemberConverted()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ConvertNotieAlert(); },550);", true);

    }
    void RemoveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RemoveNotieAlert(); },550);", true);
       
    }

    void SaveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SaveNotieAlert(); },550);", true);

    }

    void NotCompleteNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ CompleteNotieAlert(); },550);", true);

    }
    void InvalidIDNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ InvalidIDNotie(); },550);", true);

    }

    #endregion



   

 
}
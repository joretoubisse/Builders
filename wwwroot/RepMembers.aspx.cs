using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Web.Services;

public partial class Reports : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoggedIn"] != null)
        {
           
            if (!Page.IsPostBack)
            {
         
                #region Startups
                RunOnLoad();
                #endregion



              
            
            }

        }
        else
        {
            Server.Transfer("logout.aspx");
        }


    }

    public DataTable GlobalTable()
    {
        DataTable tb = new DataTable();
        string Getqry = "SELECT intid,Name + ' '  +Surname AS [Name],convert(varchar(16),DOB,106) AS [Date of Birth],Gender As [Gender] FROM Kids WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  Name + ' ' + Surname ASC";
        return tb = connect.DTSQL(Getqry);
    }

    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        PopulateGlobalTable();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            if (Session["ReportType"].ToString() == "Kids")
            {
                  HeadLabel.InnerText = "Builders Kidz";
                  ShowAllKids();
            }
            else if (Session["ReportType"].ToString() == "EvanPastor")
            {
                HeadLabel.InnerText = "EVANGELISM PASTOR";
                btnSearchRep.Visible = false;
                AllDiv.Visible = false;
            }
            else if (Session["ReportType"].ToString() == "DesicPastor")
            {
                HeadLabel.InnerText = "DISCIPLESHIP PASTOR";
                AllDiv.Visible = false;
            }
            else if (Session["ReportType"].ToString() == "ConnectPastor")
            {
                AllDiv.Visible = false;
                HeadLabel.InnerText = "CONNECT PASTOR";
           
            }

                
            else if (Session["ReportType"].ToString() == "WeeklyRep")
            {
                DivWeekDate.Visible = true;
                DivNormalDate.Visible = false;
                HeadLabel.InnerText = Session["Campus"].ToString() + " Weekly Report";
                btnSearchRep.Visible = false;
                AllDiv.Visible = false;
            }
            

         

          
        }
       
        RunMenus();
    }

    #region Evangelist Pastor
    void BuildPDFEvangelistPastor()
    {


        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }




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

        Paragraph header = new Paragraph("REPORT  | EVANGELISM PASTOR", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
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

        PdfPCell Header = new PdfPCell(new Phrase("Follow Ups - New Converts", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header.Colspan = 6;
        NewTable.AddCell(Header);

        string DelmasBornAgain = "0";
        string Delmas8 = "0";



        string BenoniBornAgain = "0";
        string Benoni8 = "0";


        string ElofBornAgain = "0";
        string Elof58 = "0";




        if (Session["ShowAll"].ToString() == "Yes")
        {

            #region Delmas
            DelmasBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Delmas Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            Delmas8 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion

            #region Benoni
            BenoniBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Benoni Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            Benoni8 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion

            #region Eloff Campus
            ElofBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Eloff Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            Elof58 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion


        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                DelmasBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Delmas Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                Delmas8 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                BenoniBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Benoni Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                Benoni8 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                ElofBornAgain = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor' and Campus = 'Eloff Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, DateofSalvation, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                Elof58 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE TypeService in('J316','Crusades') and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
        }





        #region First Section
        #region
        PdfPCell statHeader = new PdfPCell(new Phrase("No. of new converts (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader);
        #endregion

        #region
        PdfPCell statResult = new PdfPCell(new Phrase(DelmasBornAgain, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult);
        #endregion

        #region
        PdfPCell statHeader1 = new PdfPCell(new Phrase("No. of new converts (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader1);
        #endregion

        #region row3
        PdfPCell statResult1 = new PdfPCell(new Phrase(BenoniBornAgain, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult1);
        #endregion

        #region row4

        PdfPCell statHeader2 = new PdfPCell(new Phrase("No. of new converts (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader2);


        PdfPCell statResult2 = new PdfPCell(new Phrase(ElofBornAgain, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult2);
        #endregion

        #endregion

        #region Second Section
        #region
        PdfPCell statHeader3 = new PdfPCell(new Phrase("No. of successful home  visits (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader3);
        #endregion

        #region
        PdfPCell statResult3 = new PdfPCell(new Phrase(Delmas8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult3);
        #endregion

        #region
        PdfPCell statHeader4 = new PdfPCell(new Phrase("No. of successful home  visits (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader4);
        #endregion

        #region row3
        PdfPCell statResult4 = new PdfPCell(new Phrase(Benoni8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult4);
        #endregion

        #region row4

        PdfPCell statHeader5 = new PdfPCell(new Phrase("No. of successful home  visits (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader5);


        PdfPCell statResult5 = new PdfPCell(new Phrase(Elof58, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult5);
        #endregion

        #endregion



        document.Add(NewTable);


        int Table2 = 1;
        if (Table2 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("J316 Campaigns", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);



            #region Teachers Headers
            PdfPCell NameHead = new PdfPCell(new Phrase("Campaigns held", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            NameHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(NameHead);



            PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. of souls saved through campaign", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(TeacherDuty);

            #endregion

            #region Populate Teachers


            DataTable TeacherTable = new DataTable();

            TeacherTable = connect.DTSQL("SELECT campaignVenue ,BornAgain FROM Attendance WHERE TypeService = 'J316' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
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


                }
            }
            else
            {
                PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                Teacher1.Colspan = 2;
                SecondTable.AddCell(Teacher1);

            }
            #endregion

            document.Add(SecondTable);
        }



        int Table3 = 1;
        if (Table3 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Jesus Crusades", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);



            #region Teachers Headers
            PdfPCell NameHead = new PdfPCell(new Phrase("Campaigns held", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            NameHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(NameHead);



            PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. of souls saved through campaign", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(TeacherDuty);

            #endregion

            #region Populate Teachers


            DataTable TeacherTable = new DataTable();

            TeacherTable = connect.DTSQL("SELECT campaignVenue ,BornAgain FROM Attendance WHERE TypeService = 'Crusades' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
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


                }
            }
            else
            {
                PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                Teacher1.Colspan = 2;
                SecondTable.AddCell(Teacher1);

            }
            #endregion

            document.Add(SecondTable);
        }

   
   



        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }
    #endregion

    #region Connect Pastor
    void BuildPDFConnectPastor()
    {


        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }




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

        Paragraph header = new Paragraph("REPORT  | CONNECT PASTOR", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
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

        PdfPCell Header = new PdfPCell(new Phrase("Connect Groups", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
        Header.Colspan = 6;
        NewTable.AddCell(Header);

        string AllConnctDel = "0";
        string Delmas8 = "0";



        string AllConnctBen = "0";
        string Benoni8 = "0";


        string AllConnctEloff = "0";
        string Elof58 = "0";




        if (Session["ShowAll"].ToString() == "Yes")
        {

            #region Delmas
            AllConnctDel = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            Delmas8 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion

            #region Benoni
            AllConnctBen = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            Benoni8 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion

            #region Eloff Campus
            AllConnctEloff = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Eloff Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            Elof58 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion

        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                AllConnctDel = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
                Delmas8 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                AllConnctBen = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
                Benoni8 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                AllConnctEloff = connect.SingleRespSQL("SELECT count(intid)   FROM iConnect WHERE IsActive = '1' and Campus = 'Eloff Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
                Elof58 = connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                #endregion
            }
        }





        #region First Section
        #region
        PdfPCell statHeader = new PdfPCell(new Phrase("Existing Connect Group Meeting (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader);
        #endregion

        #region
        PdfPCell statResult = new PdfPCell(new Phrase(AllConnctDel, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult);
        #endregion

        #region
        PdfPCell statHeader1 = new PdfPCell(new Phrase("Existing Connect Group Meeting (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader1);
        #endregion

        #region row3
        PdfPCell statResult1 = new PdfPCell(new Phrase(AllConnctBen, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult1);
        #endregion

        #region row4

        PdfPCell statHeader2 = new PdfPCell(new Phrase("Existing Connect Group Meeting (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader2);


        PdfPCell statResult2 = new PdfPCell(new Phrase(AllConnctEloff, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult2);
        #endregion

        #endregion

        #region Second Section
        #region
        PdfPCell statHeader3 = new PdfPCell(new Phrase("Number of Connect Group Meeting Held (Delmas)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader3);
        #endregion

        #region
        PdfPCell statResult3 = new PdfPCell(new Phrase(Delmas8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult3);
        #endregion

        #region
        PdfPCell statHeader4 = new PdfPCell(new Phrase("Number of Connect Group Meeting Held (Benoni)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader4);
        #endregion

        #region row3
        PdfPCell statResult4 = new PdfPCell(new Phrase(Benoni8, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult4);
        #endregion

        #region row4

        PdfPCell statHeader5 = new PdfPCell(new Phrase("Number of Connect Group Meeting Held (Eloff)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statHeader5);


        PdfPCell statResult5 = new PdfPCell(new Phrase(Elof58, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
        statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
        NewTable.AddCell(statResult5);
        #endregion

        #endregion



        document.Add(NewTable);




        int Table2 = 1;
        if (Table2 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Number Of Active Participants Per Zone - Delmas Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);




            #region Populate Teachers


            DataTable TeacherTable = new DataTable();

            TeacherTable = connect.DTSQL("SELECT DISTINCT  Zone FROM iConnect WHERE IsActive = '1' and Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            if (TeacherTable.Rows.Count > 0)
            {
                foreach (DataRow rows in TeacherTable.Rows)
                {
                    string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups' and zonename = '" + rows[0].ToString() + "'   and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(TeacherDuty);



                    PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(Teacher1);


                }
            }
            else
            {
                PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                Teacher1.Colspan = 2;
                SecondTable.AddCell(Teacher1);

            }
            #endregion

            document.Add(SecondTable);
        }

        int Table3 = 1;
        if (Table3 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Number Of Active Participants Per Zone - Benoni Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);




            #region Populate Teachers


            DataTable TeacherTable = new DataTable();

            TeacherTable = connect.DTSQL("SELECT DISTINCT  Zone FROM iConnect WHERE IsActive = '1' and Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            if (TeacherTable.Rows.Count > 0)
            {
                foreach (DataRow rows in TeacherTable.Rows)
                {
                    string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups' and zonename = '" + rows[0].ToString() + "'   and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(TeacherDuty);



                    PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(Teacher1);


                }
            }
            else
            {
                PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                Teacher1.Colspan = 2;
                SecondTable.AddCell(Teacher1);

            }
            #endregion

            document.Add(SecondTable);
        }




        int Table4 = 1;
        if (Table4 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Number Of Active Participants Per Zone - Eloff Campus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);




            #region Populate Teachers


            DataTable TeacherTable = new DataTable();

            TeacherTable = connect.DTSQL("SELECT DISTINCT  Zone FROM iConnect WHERE IsActive = '1' and Campus = 'Eloff Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            if (TeacherTable.Rows.Count > 0)
            {
                foreach (DataRow rows in TeacherTable.Rows)
                {
                    string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups' and zonename = '" + rows[0].ToString() + "'   and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(TeacherDuty);



                    PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(Teacher1);


                }
            }
            else
            {
                PdfPCell Teacher1 = new PdfPCell(new Phrase("No Results", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                Teacher1.Colspan = 2;
                SecondTable.AddCell(Teacher1);

            }
            #endregion

            document.Add(SecondTable);
        }




        int Table5 = 1;
        if (Table5 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("iConnect Experience", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);




       
            int Row1 = 1;
            if (Row1 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE  TypeService = 'iConnectGroups' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("iConnect Experiences Held", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);
        
            }

            int Row2 = 1;
            if (Row2 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. Of Possible Attendees", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);
       
            }


            int Row3 = 1;
            if (Row3 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. Of Actual Attendees", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);

            }

            document.Add(SecondTable);
        }



        int Table6 = 1;
        if (Table6 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Baptism", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);





            int Row1 = 1;
            if (Row1 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT COUNT(intid)  FROM Attendance WHERE  TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("Baptisim Session  Held", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);

            }

            int Row2 = 1;
            if (Row2 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(baptismReadySouls AS int)) is null then '0' Else SUM(CAST(baptismReadySouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'Baptism'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. of Baptism Ready Souls", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);

            }


            int Row4 = 1;
            if (Row4 > 0)
            {
                string GetConnect = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'Baptism'  and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");

                PdfPCell TeacherDuty = new PdfPCell(new Phrase("No. of Actiual Souls Baptised", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(TeacherDuty);



                PdfPCell Teacher1 = new PdfPCell(new Phrase(GetConnect, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                Teacher1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondTable.AddCell(Teacher1);

            }

            document.Add(SecondTable);
        }       






        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }
    #endregion

    #region Discipleship
    void BuildPDFDiscipleshipPastor()
    {


        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "DISCIPLESHIP_PASTOR" + StartDate + " - " + EndDate;





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



    

        int SendRow = 1;
        if (SendRow > 0)
        {
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
            document.Add(SecondTable);
        }



        int SendRow1 = 1;
        if (SendRow1 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(6);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("Tribes (Aflame Experience)", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
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

            #region Delmas
            string DelmasVisitors = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE  Typeofservice = 'Aflame' and IsActive = '1' and  membertype = 'Visitor' and Campus = 'Delmas Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            string DelmasAttendend = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Aflame' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");


            #endregion

            #region Benoni
            string BenoniVisitors = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE  Typeofservice = 'Aflame' and IsActive = '1' and  membertype = 'Visitor' and Campus = 'Benoni Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            string BenoniAttended = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE TypeService = 'Aflame' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion


            #region Eloff
            string EloffVisitor = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE  Typeofservice = 'Aflame' and IsActive = '1' and  membertype = 'Visitor' and Campus = 'Eloff Campus' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            string EloffAttended = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE  TypeService = 'Aflame' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            #endregion





            #region First Section
            #region
            PdfPCell statHeader = new PdfPCell(new Phrase("Attendence", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader);
            #endregion

            #region
            PdfPCell statResult = new PdfPCell(new Phrase(DelmasAttendend, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult);
            #endregion

            #region
            PdfPCell statHeader1 = new PdfPCell(new Phrase("Attendence", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader1);
            #endregion

            #region row3
            PdfPCell statResult1 = new PdfPCell(new Phrase(BenoniAttended, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult1);
            #endregion

            #region Crusade

            PdfPCell statHeader2 = new PdfPCell(new Phrase("Attendence", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader2);


            PdfPCell statResult2 = new PdfPCell(new Phrase(EloffAttended, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult2);
            #endregion

            #endregion

            #region Second Section
            #region
            PdfPCell statHeader3 = new PdfPCell(new Phrase("Visitors", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader3);
            #endregion

            #region
            PdfPCell statResult3 = new PdfPCell(new Phrase(DelmasVisitors, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult3.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult3);
            #endregion

            #region
            PdfPCell statHeader4 = new PdfPCell(new Phrase("Visitors", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader4);
            #endregion

            #region row3
            PdfPCell statResult4 = new PdfPCell(new Phrase(BenoniVisitors, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult4.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult4);
            #endregion

            #region row4

            PdfPCell statHeader5 = new PdfPCell(new Phrase("Visitors", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statHeader5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statHeader5);


            PdfPCell statResult5 = new PdfPCell(new Phrase(EloffVisitor, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult5.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult5);
            #endregion

            #endregion
            document.Add(SecondTable);
        }


        int SendRow3 = 3;
        if (SendRow3 > 0)
        {
            PdfPTable SecondTable = new PdfPTable(2);

            SecondTable.HorizontalAlignment = 1;
            //leave a gap before and after the table

            SecondTable.SpacingAfter = 8f;

            PdfPCell SecondHeaderT = new PdfPCell(new Phrase("School Connect", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            SecondHeaderT.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            SecondHeaderT.Colspan = 2;
            SecondTable.AddCell(SecondHeaderT);



            #region Teachers Headers
            PdfPCell NameHead = new PdfPCell(new Phrase("No. of active school connects", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            NameHead.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            NameHead.Colspan = 1;
            SecondTable.AddCell(NameHead);


            string TotGroups = connect.SingleRespSQL("SELECT Count(intid)  FROM iConnect  WHERE Campus = '" + Session["Campus"].ToString() + "' and IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "'");
            #region
            PdfPCell statResult = new PdfPCell(new Phrase(TotGroups, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
            SecondTable.AddCell(statResult);
            #endregion





            PdfPCell CampusHead = new PdfPCell(new Phrase("No. of  active participants per school", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            CampusHead.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            CampusHead.Colspan = 2;
            SecondTable.AddCell(CampusHead);


            DataTable Ntable = new DataTable();

            Ntable = connect.DTSQL("SELECT Zonename ,CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  Group by zonename");
            if (Ntable.Rows.Count > 0)
            {
                foreach (DataRow rows in Ntable.Rows)
                {
                    #region Heading
                    PdfPCell TeacherDuty = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                    TeacherDuty.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                 
                    SecondTable.AddCell(TeacherDuty);
                    #endregion

                    #region Count
                    PdfPCell statResultd = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondTable.AddCell(statResultd);
                    #endregion
                }

            }
            else
            {
                #region Count
                PdfPCell statResultd = new PdfPCell(new Phrase("No School Connects", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultd.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                statResultd.Colspan = 2;
                SecondTable.AddCell(statResultd);
                #endregion
            }

            #endregion


            document.Add(SecondTable);

        }






   







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
        ////    TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'BuilderKidz' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "'");  //and convert(varchar, B.CapturedDate, 23) between '" + StartDate + "' and '" + EndDate + "'
        ////}
        ////else
        ////{
        ////    TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'BuildersKidz' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "' and A.Campus = '" + Session["ChurchID"].ToString() + "'");
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
    #endregion

    #region FullRep
    void BuildPDFBenoniRep()
    {


        string week1 = "";
        string EndDate = "";
        string week3 = "";
        string week2 = "";
        string week4 = "";
        if (txtWeek1.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtWeek2.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtWeek3.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtWeek4.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            week1 = Convert.ToDateTime(txtWeek1.Value).ToString("yyyy-MM-dd");
            week2 = Convert.ToDateTime(txtWeek2.Value).ToString("yyyy-MM-dd");
            week3 = Convert.ToDateTime(txtWeek3.Value).ToString("yyyy-MM-dd");
            week4 = Convert.ToDateTime(txtWeek4.Value).ToString("yyyy-MM-dd");
        }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "Campus" + week1 + " - " + week4;





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

        Paragraph header = new Paragraph(Session["Campus"].ToString() + " | Weekly Report", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(week1 + " - " + week4, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
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
            string AlterCall3 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week3 + "'");
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
            string SchoolConnect1 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE isSchoolConnectgrp = 'Yes' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) = '" + week1 + "'");
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

    #endregion

    void PopulateGlobalTable()
    {
        GlobalTable();
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


                if (MenuName == "Reports")
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

    #region Kids PDF
    void BuildPDFKids()
    {

       
        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }
      


     
      //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "BuildersKidz" + StartDate + " - " + EndDate;





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

        Paragraph header = new Paragraph("WEEKLY ATTENDANCE REPORT  | BUILDERS KIDZ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
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
            Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Delmas12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            #endregion

            #region Benoni
            Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Benoni12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            #endregion

            #region Eloff Campus
            Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            Elof512 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
            #endregion


        }
        else
        {
            if (Session["Campus"].ToString() == "Delmas Campus")
            {
                #region Delmas
                Delmas5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Delmas8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Delmas12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Benoni Campus")
            {
                #region Benoni
                Benoni5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Benoni8 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Benoni12 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID =  '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar,  GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                #endregion
            }
            else if (Session["Campus"].ToString() == "Eloff Campus")
            {
                #region Eloff Campus
                Elof5 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Elof58 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                Elof512 = connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12'  and convert(varchar, B.CreatedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' ");
                #endregion
            }
        }





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
            TeacherTable = connect.DTSQL("SELECT A.Name + ' ' + A.Surname, A.Campus,A.iserve FROM Stats_Form  A INNER JOIN IserveCheck B ON A.intid = B.MemberID  WHERE A.iserve = 'Builders Kidz' and A.ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, B.CapturedDate, 23) between '" + StartDate + "' and '" + EndDate + "'");
        }
        else
        {
            TeacherTable = connect.DTSQL("SELECT A.Name + ' ' + A.Surname, A.Campus,A.iserve FROM Stats_Form  A INNER JOIN IserveCheck B ON A.intid = B.MemberID  WHERE A.iserve = 'Builders Kidz' and A.ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, B.CapturedDate, 23) between '" + StartDate + "' and '" + EndDate + "' and A.Campus = '" + Session["Campus"].ToString() + "'");
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
            TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'BuildersKidz' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "'");  //and convert(varchar, B.CapturedDate, 23) between '" + StartDate + "' and '" + EndDate + "'
        }
        else
        {
            TeacherTable = connect.DTSQL("SELECT TOP(5) A.Campus,  B.A9 FROM ChurchChecklistholder A INNER JOIN ChurchChecklist B ON A.intid =  B.QuestionID WHERE A.QuestionType = 'BuildersKidz' and A.churchid= '" + Session["ChurchID"].ToString() + "' and convert(varchar, A.UploadDate, 23) between '" + StartDate + "' and '" + EndDate + "' and A.Campus = '" + Session["ChurchID"].ToString() + "'");
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

        document.Dispose();
 
        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



    }

    void ShowAllKids()
    {

        string htmltext = "";
        DataTable table = GlobalTable();
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                
                    "    <th> Name </th> " +

                     "    <th > Date of Birth</th> " +
                     "    <th > Gender</th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {

                htmltext += " <tr> ";
                htmltext += "   <td >" + Row[1].ToString() + "</td> " +
                "   <td >" + Row[2].ToString() + "</td> " +

                 "   <td >" + Row[3].ToString() + "</td> " +


           " </tr>";


            }
        }
        else
        {
            htmltext = "No Builders Kidz";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }
    #endregion

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Members.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }

    public static string RandomString(int length)
    {
        var chars = "0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);


        return finalString.ToString();
    }

    #region Message Boxes
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

    void NotieInvalidEmail()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ NotieInvalidEmail(); },550);", true);

    }

    void NotieRunInvalidPass()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ NotieInvalidPass();},750);", true);

    }

    void NotieUserExist()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ExistsNotieAlert();},750);", true);

    }

    
    
    void InvalidIDNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ InvalidIDNotie(); },550);", true);

    }

    #endregion

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

  
    protected void btnExcel_ServerClick(object sender, EventArgs e)
    {
        var reports = GlobalTable();
        ExcelPackage excel = new ExcelPackage();
        var workSheet = excel.Workbook.Worksheets.Add("Reports");
        var totalCols = reports.Columns.Count;
        var totalRows = reports.Rows.Count;

        for (var col = 1; col <= totalCols; col++)
        {
            workSheet.Cells[1, col].Value = reports.Columns[col - 1].ColumnName;
        }
        for (var row = 1; row <= totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                workSheet.Cells[row + 1, col + 1].Value = reports.Rows[row - 1][col];
            }
        }
        using (var memoryStream = new MemoryStream())
        {
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=reports.xlsx");
            excel.SaveAs(memoryStream);
            memoryStream.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();

        }
    }

    protected void btndownloadPDF_ServerClick(object sender, EventArgs e)
    {
        if (Session["ReportType"].ToString() == "Kids")
        {
            BuildPDFKids();
        }
        else if (Session["ReportType"].ToString() == "EvanPastor")
        {
            BuildPDFEvangelistPastor();
        }
        else if (Session["ReportType"].ToString() == "DesicPastor")
        {
            BuildPDFDiscipleshipPastor();
        }
        else if (Session["ReportType"].ToString() == "ConnectPastor")
        {
            BuildPDFConnectPastor();
        }
        else if (Session["ReportType"].ToString() == "WeeklyRep")
        {
            BuildPDFBenoniRep();
        }


      
       
        
    }


    [WebMethod]
    public static List<GraphData> KidRepGraph()
    {
         SqlConnMethod connect = new SqlConnMethod();


         string StartDate = HttpContext.Current.Session["StartDate"].ToString();
         string EndDate = HttpContext.Current.Session["EndDate"].ToString();
   
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
                        new DataColumn("w", typeof(string)),
                        new DataColumn("a", typeof(int)),
                        new DataColumn("b", typeof(int)),
                        new DataColumn("c", typeof(int)),
                        new DataColumn("x", typeof(int)),
                        new DataColumn("y",typeof(int)),
                          new DataColumn("z", typeof(int)),
                            new DataColumn("x1",typeof(int)),
                        new DataColumn("y1",typeof(int)),
                        new DataColumn("z1",typeof(int))});

    
                #region Delmas
                int GetTotalKidDelmas = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM Kids A   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1'"));
                int GetCheckINDelmas8 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
             
                double totalDelmas8 = 0;
                if (GetCheckINDelmas8 == 0)
                {
                    totalDelmas8 = 0;
                }
                else if (GetTotalKidDelmas == 0)
                {
                    totalDelmas8 = 0;
                }
                else
                {
                    totalDelmas8 = (double)GetCheckINDelmas8 / GetTotalKidDelmas * 100;
                }

                int GetCheckINDelmas5 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1'  and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalDelmas5 = 0;
                if (GetCheckINDelmas5 == 0)
                {
                    totalDelmas5 = 0;
                }
                else if (GetTotalKidDelmas == 0)
                {
                    totalDelmas5 = 0;
                }
                else
                {
                    totalDelmas5 = (double)GetCheckINDelmas5 / GetTotalKidDelmas * 100;
                }



                int GetCheckINDelmas12 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12' and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalDelmas12 = 0;
                if (GetCheckINDelmas12 == 0)
                {
                    totalDelmas12 = 0;
                }
                else if (GetTotalKidDelmas == 0)
                {
                    totalDelmas12 = 0;
                }
                else
                {
                    totalDelmas12 = (double)GetCheckINDelmas12 / GetTotalKidDelmas * 100;
                }


                #endregion

                #region Benoni
                int GetTotalKidBenoni = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM Kids A   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1'"));
                int GetCheckINBenoni = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalBenoni8 = 0;
                if (GetCheckINBenoni == 0)
                {
                    totalBenoni8 = 0;
                }
                else if (GetTotalKidBenoni == 0)
                {
                    totalBenoni8 = 0;
                }
                else
                {
                    totalBenoni8 = (double)GetCheckINBenoni / GetTotalKidBenoni * 100;
                }


                int GetCheckINBenoni5 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1'  and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalBenoni5 = 0;
                if (GetCheckINBenoni5 == 0)
                {
                    totalBenoni5 = 0;
                }
                else if (GetTotalKidBenoni == 0)
                {
                    totalBenoni5 = 0;
                }
                else
                {
                    totalBenoni5 = (double)GetCheckINBenoni5 / GetTotalKidBenoni * 100;
                }


                int GetCheckINBenoni12 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12' and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalBenoni12 = 0;
                if (GetCheckINBenoni12 == 0)
                {
                    totalBenoni12 = 0;
                }
                else if (GetTotalKidBenoni == 0)
                {
                    totalBenoni12 = 0;
                }
                else
                {
                    totalBenoni12 = (double)GetCheckINBenoni12 / GetTotalKidBenoni * 100;
                }

                #endregion

                #region Eloff
                int GetTotalKidEloff = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT A.intid) FROM Kids A   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1'"));
                int GetCheckINEloff = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '6' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '8'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalEloff8 = 0;
                if (GetCheckINEloff == 0)
                {
                    totalEloff8 = 0;
                }
                else if (GetTotalKidEloff == 0)
                {
                    totalEloff8 = 0;
                }
                else
                {
                    totalEloff8 = (double)GetCheckINEloff / GetTotalKidEloff * 100;
                }


                int GetCheckINEloff5 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1'  and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '5'  and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalEloff5 = 0;
                if (GetCheckINEloff5 == 0)
                {
                    totalEloff5 = 0;
                }
                else if (GetTotalKidEloff == 0)
                {
                    totalEloff5 = 0;
                }
                else
                {
                    totalEloff5 = (double)GetCheckINEloff5 / GetTotalKidEloff * 100;
                }


                int GetCheckINEloff12 = int.Parse(connect.SingleRespSQL("SELECT  COUNT(DISTINCT B.intid) FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid = B.KidID   WHERE A.ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and A.campus = 'Eloff Campus' and A.IsActive = '1' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) > '9' and DATEDIFF(year,convert(varchar, A.DOB, 23) ,convert(varchar, GETDATE(), 23)) <= '12' and convert(varchar, B.CreatedDate, 23)  between '" + StartDate + "'  and  '" + EndDate + "'"));
                double totalEloff12 = 0;
                if (GetCheckINEloff12 == 0)
                {
                    totalEloff12 = 0;
                }
                else if (GetTotalKidEloff == 0)
                {
                    totalEloff12 = 0;
                }
                else
                {
                    totalEloff12 = (double)GetCheckINEloff12 / GetTotalKidEloff * 100;
                }


                #endregion

                dt.Rows.Add(StartDate + " - " + EndDate, totalDelmas5, totalBenoni5, totalEloff5, totalDelmas8, totalBenoni8, totalEloff8, totalDelmas12, totalBenoni12, totalEloff12);//Delmas - Benoni - Eloff
  
        List<GraphData> chartData = new List<GraphData>();
        foreach (DataRow dr in dt.Rows)
        {
            chartData.Add(new GraphData
            {
                w = Convert.ToString(dr["w"]),
                a = (Convert.ToInt32(dr["a"])),
                b = (Convert.ToInt32(dr["b"])),
                c = (Convert.ToInt32(dr["c"])),
                x = (Convert.ToInt32(dr["x"])),
                y = (Convert.ToInt32(dr["y"])),
                z = (Convert.ToInt32(dr["z"])),
                x1 = (Convert.ToInt32(dr["x1"])),
                y1 = (Convert.ToInt32(dr["y1"])),
                z1 = (Convert.ToInt32(dr["z1"]))
            });
        }

        return chartData;
    }


    [WebMethod]
    public static List<GraphDisc> DiscRepGraph()
    {
        SqlConnMethod connect = new SqlConnMethod();


        string StartDate = HttpContext.Current.Session["StartDate"].ToString();
        string EndDate = HttpContext.Current.Session["EndDate"].ToString();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
                        new DataColumn("w", typeof(string)),
                        new DataColumn("a", typeof(int)),
                        new DataColumn("b", typeof(int)),
                        new DataColumn("c", typeof(int)),
                        new DataColumn("x", typeof(int)),
                        new DataColumn("y",typeof(int)),
                          new DataColumn("z", typeof(int))});


        #region Delmas
        #region FinalAttendActual
        int GetTotMemberDel = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = 'Delmas Campus' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "'"));
        int GetAttendendDelActual = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));

        double FinalAttendActualDel = 0;
        if (GetTotMemberDel == 0)
        {
            FinalAttendActualDel = 0;
        }
        else if (GetAttendendDelActual == 0)
        {
            FinalAttendActualDel = 0;
        }
        else
        {
            FinalAttendActualDel = (double)GetAttendendDelActual / GetTotMemberDel * 100;
        }
        #endregion

        #region FinalInvites
        int GetAttendendPossibleDel = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = '" + HttpContext.Current.Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));
        double FinalAttendDelPossible = 0;
        if (GetTotMemberDel == 0)
        {
            FinalAttendDelPossible = 0;
        }
        else if (GetAttendendPossibleDel == 0)
        {
            FinalAttendDelPossible = 0;
        }
        else
        {
            FinalAttendDelPossible = (double)GetAttendendPossibleDel / GetTotMemberDel * 100;
        }
        #endregion

        #endregion

        #region Benoni
        #region FinalAttendActual
        int GetTotMemberBenoni = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = 'Benoni Campus' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "'"));
        int GetAttendendBenActual = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));

        double FinalAttendActualBen = 0;
        if (GetTotMemberBenoni == 0)
        {
            FinalAttendActualBen = 0;
        }
        else if (GetAttendendBenActual == 0)
        {
            FinalAttendActualBen = 0;
        }
        else
        {
            FinalAttendActualBen = (double)GetAttendendBenActual / GetTotMemberBenoni * 100;
        }
        #endregion

        #region FinalInvites
        int GetAttendendPossibleBen = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));
        double FinalAttendBenPossible = 0;
        if (GetTotMemberBenoni == 0)
        {
            FinalAttendBenPossible = 0;
        }
        else if (GetAttendendPossibleBen == 0)
        {
            FinalAttendBenPossible = 0;
        }
        else
        {
            FinalAttendBenPossible = (double)GetAttendendPossibleBen / GetTotMemberBenoni * 100;
        }
        #endregion

        #endregion



        #region Eloff
        #region FinalAttendActual
        int GetTotMemberEloff = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = 'Eloff Campus' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "'"));
        int GetAttendendEloffActual = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));

        double FinalAttendActualEloof = 0;
        if (GetTotMemberEloff == 0)
        {
            FinalAttendActualEloof = 0;
        }
        else if (GetAttendendEloffActual == 0)
        {
            FinalAttendActualEloof = 0;
        }
        else
        {
            FinalAttendActualEloof = (double)GetAttendendEloffActual / GetTotMemberEloff * 100;
        }
        #endregion

        #region FinalInvites
        int GetAttendendPossibleEloff = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'New Believers Experience' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = 'Eloff Campus' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));
        double FinalAttendEloffPossible = 0;
        if (GetTotMemberEloff == 0)
        {
            FinalAttendEloffPossible = 0;
        }
        else if (GetAttendendPossibleEloff == 0)
        {
            FinalAttendEloffPossible = 0;
        }
        else
        {
            FinalAttendEloffPossible = (double)GetAttendendPossibleEloff / GetTotMemberEloff * 100;
        }
        #endregion

        #endregion

        dt.Rows.Add(StartDate + " - " + EndDate, FinalAttendActualDel, FinalAttendActualBen, FinalAttendActualEloof, FinalAttendDelPossible, FinalAttendBenPossible, FinalAttendEloffPossible);//Delmas - Benoni - Eloff

        List<GraphDisc> chartData = new List<GraphDisc>();
        foreach (DataRow dr in dt.Rows)
        {
            chartData.Add(new GraphDisc
            {
                w = Convert.ToString(dr["w"]),
                a = (Convert.ToInt32(dr["a"])),
                b = (Convert.ToInt32(dr["b"])),
                c = (Convert.ToInt32(dr["c"])),
                x = (Convert.ToInt32(dr["x"])),
                y = (Convert.ToInt32(dr["y"])),
                z = (Convert.ToInt32(dr["z"]))
               
            });
        }

        return chartData;
    }

    [WebMethod]
    public static List<IConnectGraphData> iConnectPastorRepGraph()
    {
        SqlConnMethod connect = new SqlConnMethod();


        string StartDate = HttpContext.Current.Session["StartDate"].ToString();
        string EndDate = HttpContext.Current.Session["EndDate"].ToString();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
                        new DataColumn("w", typeof(string)),
                        new DataColumn("a", typeof(int)),
                        new DataColumn("b", typeof(int))});


          #region FinalAttendActual
        int GetTotMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + HttpContext.Current.Session["Campus"].ToString() + "' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "'"));
        int GetAttendend = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = '" + HttpContext.Current.Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));
     
        double FinalAttendActual = 0;
        if (GetTotMember == 0)
        {
            FinalAttendActual = 0;
        }
        else if (GetAttendend == 0)
        {
            FinalAttendActual = 0;
        }
        else
        {
            FinalAttendActual = (double)GetAttendend / GetTotMember * 100;
        }
        #endregion


        #region FinalAttendActual
        int GetAttendendPossible = int.Parse(connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(invitesouls AS int)) is null then '0' Else SUM(CAST(invitesouls AS int)) END as 'Count'  FROM Attendance WHERE  TypeService = 'iConnectGroups' and  ChurchID = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = '" + HttpContext.Current.Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' "));
        double FinalAttendPossible = 0;
        if (GetTotMember == 0)
        {
            FinalAttendActual = 0;
        }
        else if (GetAttendendPossible == 0)
        {
            FinalAttendPossible = 0;
        }
        else
        {
            FinalAttendPossible = (double)GetAttendendPossible / GetTotMember * 100;
        }
        #endregion



        dt.Rows.Add(StartDate + " - " + EndDate, FinalAttendActual, FinalAttendPossible);//Delmas - Benoni - Eloff

        List<IConnectGraphData> chartData = new List<IConnectGraphData>();
        foreach (DataRow dr in dt.Rows)
        {
            chartData.Add(new IConnectGraphData
            {
                w = Convert.ToString(dr["w"]),
                a = (Convert.ToInt32(dr["a"])),
                b = (Convert.ToInt32(dr["b"]))
              
            });
        }

        return chartData;
    }

    public class GraphData
    {
        public string w { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int z1 { get; set; }
    }

    public class GraphDisc
    {
        public string w { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
      
    }


    public class IConnectGraphData
    {
        public string w { get; set; }
        public int a { get; set; }
        public int b { get; set; }
    
    }


    protected void btnSearchRep_ServerClick(object sender, EventArgs e)
    {
        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }










        GetStartDate.Value = StartDate;
        GetEndDate.Value = EndDate;

        Session["StartDate"] = StartDate;
        Session["EndDate"] = EndDate;


        if (Session["ReportType"].ToString() == "Kids")
        {
            BarLal.InnerText = "Builders Kidz Attendance Graph";
            BarGraphRep.Visible = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ KidsBarGraph(); },550);", true);
        }
        else if (Session["ReportType"].ToString() == "EvanPastor")
        {
          
        }
        else if (Session["ReportType"].ToString() == "DesicPastor")
        {

            BarLal.InnerText = "DISCIPLESHIP PASTOR Graph";
            BarGraphRep.Visible = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ DiscBarGraph(); },550);", true);
        }
        else if (Session["ReportType"].ToString() == "ConnectPastor")
        {
            BarLal.InnerText = "Connect Group Graph";
            BarGraphRep.Visible = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ IconnectBarGraph(); },550);", true);
        }





      

    }
}
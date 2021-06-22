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

public partial class ViewKids : System.Web.UI.Page
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



    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            if (Session["ShowAll"].ToString() == "Yes")
            {
                BtnSave.Visible = true;
                IsAdmin.Value = "1";
            }
            else
            {
                if (Session["KidsRights"].ToString() == "1")
                {
                    BtnSave.Visible = true;
                    IsAdmin.Value = "1";

                }
            }


         

            RunUSers();
        }
        else
        {
            IsAdmin.Value = "1";
            RunUSers();
          
        }

        populateTextboxes();
        RunMenus();
       
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


                if (MenuName.Contains("Builders Kidz"))
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

    void RunUSers()
    {

        string ChecIN = "";
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT  CASE WHEN isChecked  = 'Yes' THEN 'IN' ELSE 'OUT' END , convert(varchar(50),checkedDate,100),createdby,ParentName + '  - ' +  parentype,ParentCell,IsVisitor  FROM  KidsCheckedIn    WHERE KidID = '" + Session["KidsID"].ToString() + "' ORDER by intid DESC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                  //  "    <th>  </th> " +
              
               
                  
                      "    <th > Parent/Guardian </th> " +
                        "    <th > Cell No </th> " +
                          "    <th> Action by </th> " +
                     "    <th >  Date</th> " +
                     "    <th > Status </th> " +
                      "    <th > Type </th> " +
    
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {


           
                htmltext += " <tr> ";

                                 
                             htmltext +="   <td >" + Row[3].ToString() + "</td> " +
                                   "   <td >" + Row[4].ToString() + "</td> " +
                                        "   <td >" + Row[2].ToString() + "</td> " +
                             "   <td >" + Row[1].ToString() + "</td> " +

                              "   <td >" + Row[0].ToString() + "</td> " +
         "   <td >" + Row[5].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }


    void populateTextboxes()
    {


        DataTable table = connect.DTSQL("SELECT  surname,Name,Gender,Address,CONVERT(varchar(16),DOB,106),CellNo,IsSystem,MomName,MomCell,DadName,DadCell FROM Kids WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and intid = '" + Session["KidsID"].ToString() + "'");
        if (table.Rows.Count > 0)
        {
            foreach (DataRow rows in table.Rows)
            {
                txtSurname.Value =rows[0].ToString();
                txtName.Value =rows[1].ToString();
                CmdGender.Value =rows[2].ToString();
                txtAddress.Value = rows[3].ToString();
                txtDOB.Value =rows[4].ToString();
                txtCellNo.Value =rows[5].ToString();
                CmdOnSystem.SelectedValue =rows[6].ToString();

                if (rows[6].ToString() == "Yes")
                {
                    populateParents();
                    txtMomName.Attributes.Add("readonly", "readonly");
                    txtMomCell.Attributes.Add("readonly", "readonly");
                    txtDadName.Attributes.Add("readonly", "readonly");
                    txtDadCell.Attributes.Add("readonly", "readonly");
                  
                    #region Moms
                    DataTable Moms = connect.DTSQL("SELECT  Name + ' ' + Surname as [Name],Celno  FROM  Stats_Form WHERE intid = '" + rows[7].ToString() + "'");
                    if (Moms.Rows.Count > 0)
                    {
                        CmdMom.SelectedValue = rows[7].ToString();
                        foreach (DataRow MomsR in Moms.Rows)
                        {
                            txtMomName.Value = MomsR[0].ToString();
                            txtMomCell.Value = MomsR[1].ToString();
                      
                        }
                      
                    }
                    else
                    {
                        txtMomName.Value = "";
                        txtMomCell.Value = "";
                        PrentsInSystem.Visible = true;
                        MomDiv.Visible = true;
                    }

                    #endregion


                    #region Dads
                    DataTable Dads = connect.DTSQL("SELECT  Name + ' ' + Surname as [Name],Celno  FROM  Stats_Form WHERE intid = '" + rows[9].ToString() + "'");
                    if (Dads.Rows.Count > 0)
                    {
                        CmdDad.SelectedValue = rows[9].ToString();
                        foreach (DataRow DadsR in Dads.Rows)
                        {
                            txtDadName.Value = DadsR[0].ToString();
                            txtDadCell.Value = DadsR[1].ToString();

                           
                        }

                    }
                    else
                    {
                        txtDadName.Value = "";
                        txtDadCell.Value = "";

                        PrentsInSystem.Visible = true;
                        DadDiv.Visible = true;

                    }

                    #endregion
                }
                else
                {

                    PrentsInSystem.Visible = false;
                    txtMomName.Attributes.Remove("readonly");
                    txtMomCell.Attributes.Remove("readonly");
                    txtDadName.Attributes.Remove("readonly");
                    txtDadCell.Attributes.Remove("readonly");

                    txtMomName.Value = rows[7].ToString();
                    txtMomCell.Value = rows[8].ToString();
                    txtDadName.Value = rows[9].ToString();
                    txtDadCell.Value = rows[10].ToString();
                }

             

                     
            }


        }
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Kids.txt");
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

   

    

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Kids.aspx");
      
    }

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("UPDATE  Kids SET IsActive = '0' ,lastupdateby = '" + Session["FullName"].ToString() + "' ,LastUpdateDate = GETDATE() WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunUSers();
            RemoveNotie();
        }
    }


    void populateParents()
    {


        DataTable table = connect.DTSQL("SELECT intid, Name + ' ' + Surname as [Name]  FROM  Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' order by name asc");
        CmdMom.DataSource = table;
        CmdMom.DataTextField = "Name";
        CmdMom.DataValueField = "intid";
        CmdMom.DataBind();
        CmdMom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));


        CmdDad.DataSource = table;
        CmdDad.DataTextField = "Name";
        CmdDad.DataValueField = "intid";
        CmdDad.DataBind();
        CmdDad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
    }

    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

        string isOnSystem = CmdOnSystem.SelectedValue.ToString();
        string MomsName = "";
        string MomCellNo = "";
        string DadsName = "";
        string DadCellNo = "";



        if ((txtSurname.Value == "") || (txtName.Value == "") || (txtAddress.Value == "") || (CmdGender.Value == "None") || (txtDOB.Value == "") || (isOnSystem == "None"))
        {
            NotCompleteNotie();
            return;
        }

        if (isOnSystem == "Yes")
        {
            DadCellNo = "";
            MomCellNo = "";
            MomsName = CmdMom.SelectedValue.ToString();
            DadsName = CmdDad.SelectedValue.ToString();
        }
        else
        {
            DadCellNo = txtDadCell.Value;
            MomCellNo = txtMomCell.Value;
            MomsName = txtMomName.Value;
            DadsName = txtDadName.Value;
        
        }

        int complete = connect.SingleIntSQL("UPDATE  Kids SET surname = '" + txtSurname.Value + "',Name = '" + txtName.Value + "',Gender = '" + CmdGender.Value + "',Address = '" + txtAddress.Value + "',DOB = '" + txtDOB.Value + "',CellNo = '" + txtCellNo.Value + "' ,IsSystem = '" + isOnSystem + "',MomName = '" + MomsName + "',MomCell = '" + MomCellNo + "',DadName = '" + DadsName + "',DadCell = '" + DadCellNo + "', lastupdateby = '" + Session["FullName"].ToString() + "',LastUpdateDate= GETDATE() WHERE intid = '" + Session["KidsID"].ToString() + "'");
      
        if (complete > 0)
        {
         
            RunUSers();
            SaveNotie();
        }



    }

   
    protected void CmdOnSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmdOnSystem.SelectedValue.ToString() == "Yes")
        {
            populateParents();
            PrentNotInSystem.Visible = false;
            PrentsInSystem.Visible = true;
            DadDiv.Visible = true;
            MomDiv.Visible = true;
            txtMomName.Attributes.Add("readonly", "readonly");
            txtMomCell.Attributes.Add("readonly", "readonly");
            txtDadName.Attributes.Add("readonly", "readonly");
            txtDadCell.Attributes.Add("readonly", "readonly");
        }
        else
        {

            txtMomName.Attributes.Remove("readonly");
            txtMomCell.Attributes.Remove("readonly");
            txtDadName.Attributes.Remove("readonly");
            txtDadCell.Attributes.Remove("readonly");
            PrentNotInSystem.Visible = true;
          
            PrentsInSystem.Visible = false;
        }
    }
}
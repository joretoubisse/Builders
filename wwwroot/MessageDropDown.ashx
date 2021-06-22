<%@ WebHandler Language="C#" Class="MessageDropDown" %>

using System;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;


public class MessageDropDown : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();

    public void ProcessRequest (HttpContext context) {
        string id = context.Request["DropValue"];
        string ChurchID = context.Session["ChurchID"].ToString();
        string html = populateSingle(id, ChurchID);


       
        context.Response.Write(html);

    }

    public string populateSingle(string prject,string ChurchID)
    {
  
        string extraHTML = "";
        #region 
        DataTable det = new DataTable();
        det = connect.DTSQL("SELECT  Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '" + ChurchID + "'");
       // det = connect.DTSQL("SELECT  Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '" + HttpContext.Current.Session["CompanyID"].ToString() + "'");
        #endregion
        
        //extraHTML =  "<select class='form-control kt_selectpicker' data-live-search='true'>" +
        //             "<option value='0'>Please Select</option> ";

        //if (det.Rows.Count > 0)
        //{
        //    foreach (DataRow drt in det.Rows)
        //    {
        //        extraHTML += "<option value='" + drt[0].ToString() + "'>" + drt[1].ToString() + "</option>";
        //    }
        //}

        //extraHTML += "</select>";
        
        
        
        extraHTML = @"<div class='form-group'>
											<label>Name</label>
										
												<select class='form-control kt_selectpicker' data-live-search='true'>
													<option value='Test' >Jabual</option>
													<option   >Burger, Shake and a Smile</option>
													<option >Sugar, Spice and all things nice</option>
                                          
												</select>

                                               
								
										</div>";



        return extraHTML;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}


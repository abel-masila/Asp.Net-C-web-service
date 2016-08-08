using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SampleService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [WebMethod]
        public XmlElement GetUserInfo(string userName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblUserInfo where userName like @userName+'%'", con);
            cmd.Parameters.AddWithValue("@userName",userName);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            //Return the Dataset as XmlElement
            XmlDataDocument xmlData = new XmlDataDocument(ds);
            XmlElement xmlElement = xmlData.DocumentElement;
            return xmlElement;
        }
    }
}

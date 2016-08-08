# Asp.Net-C-web-service
To get started with creating a web service, open your SQL server management studio and create a table in your prefered database called tblUserInfo.

UserId-Int(Set Identity=true) primary key
UserName-Varchar(50)
FirstName-Varchar(50)
LastName-Varchar(50)
Location-Varchar(50).
Now open visual studio and create a blank web web project and add a web service to the blank project.
add the following code to your Service.asmx.cs
<script src="https://gist.github.com/abel-masila/bd63e87077658569e75f6f2b0e75a847.js"></script>

Now create add your database connection settings in web.config i.e
<connectionStrings>
<add name="dbconnection"  connectionString="Data Source=DESKTOP-ADMIN;Initial Catalog='UserInfo';Integrated Security=True"/>
</connectionStrings>
change the parameters in the connection string to match your local development variables.

Now, test your web service add if all was setup well, u will not run into any errors.

Finally, open a new instance of visual studio and create another Blank web project and add a web form i.e Default.aspx
add the following code in your default.aspx
<form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <b>Enter UserName:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
</div>
        <div>
            <asp:GridView ID="gvUserDetails" runat="server" EmptyDataText="No Record Found">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </form>
    
    Now add the following code in your default.aspx.cs to consume the service which you just created.
    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserDetails("");
        }
    }
    protected void BindUserDetails(string userName)
    {
        localhost.Service objUserInfo = new localhost.Service();
        DataSet dsRes = new DataSet();
        XmlElement exelement = objUserInfo.GetUserInfo(userName);
        if (exelement!=null)
        {
            XmlNodeReader nodeReader = new XmlNodeReader(exelement);
            dsRes.ReadXml(nodeReader, XmlReadMode.Auto);
            gvUserDetails.DataSource = dsRes;
            gvUserDetails.DataBind();
        }
        else
        {
            gvUserDetails.DataSource = null;
            gvUserDetails.DataBind(); 
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindUserDetails(txtUserName.Text);
    }
}
N/B
You need to add a web service referece to your new Project which is consuming the web service using the procedure found at https://goo.gl/kFU2bT


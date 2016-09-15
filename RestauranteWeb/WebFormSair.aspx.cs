using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebFormSair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Session["s"] = null;
            Response.Write("<script>window.alert('Logout concluído!'); self.location='LoginAdmRest.aspx'; </script>");
        }
    }
}
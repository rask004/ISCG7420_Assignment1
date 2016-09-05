using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class _Default : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private void Load_Categories()
    {
        PublicController controller = new PublicController();
        List<Category> categories =  controller.GetCategoriesWithCaps();
        dlstCategoriesWithProducts.DataSource = categories;
        dlstCategoriesWithProducts.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Categories();
        }
    }
}
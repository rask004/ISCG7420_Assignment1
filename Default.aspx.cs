using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

/// <summary>
/// 
/// </summary>
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

    private void LoadProducts(int categoryId)
    {
        if (categoryId < 1)
        {

        }
        else
        {
            
        }
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

            if (dlstCategoriesWithProducts.Items.Count == 0)
            {
                
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlstCategoriesWithProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            ImageButton img = (e.Item.FindControl("imgCategoryPicture") as ImageButton);
            int id = Convert.ToInt32((e.Item.FindControl("lblCategoryId") as Label).Text);
            string categoryName = (e.Item.FindControl("lblCategoryName") as Label).Text;
            PublicController controller = new PublicController();
            img.ImageUrl = controller.GetFirstCapImageByCategoryId(id);

            lblCentreHeader.Text = categoryName;

            List<Cap> caps = controller.GetAllCapsByCategoryId(id);
            dlstAvailableProducts.DataSource = caps;
            dlstAvailableProducts.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlstAvailableProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            int id = Convert.ToInt32((e.Item.FindControl("lblCapId") as Label).Text);
            PublicController controller = new PublicController();
        }
    }
}
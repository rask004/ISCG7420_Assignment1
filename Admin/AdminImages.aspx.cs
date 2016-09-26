using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonLogging;

/// <summary>
///     Admin page for orders management
///     Change log:
///     08-9-16     18:06       AskewR04 Created Page
///     20-9-16     18:06       AskewR04 Final review
/// </summary>
public partial class AdminImages : Page
{
    /// <summary>
    ///     Retrieve available images
    /// </summary>
    /// <returns>List, of images, by filename and server URL</returns>
    private List<ListItem> GetListOfUploadedImages()
    {
        var uploadedDirectoryInfo = new DirectoryInfo(Server.MapPath(GeneralConstants.ImagesUploadFolder));

        var imageUrls = new List<ListItem>();

        // permitted types are in MIME form. Cannot directly compare to extension.
        // but each type will include the extension.
        foreach (var file in uploadedDirectoryInfo.GetFiles())
        {
            foreach (var permittedMimeType in GeneralConstants.PermittedContentTypes)
            {
                if (permittedMimeType.Contains(file.Extension.Substring(1)))
                {
                    imageUrls.Add(new ListItem
                    {
                        Text = file.Name,
                        Value = GeneralConstants.ImagesUploadFolder + "/" + file.Name
                    });
                    break;
                }
            }
        }

        return imageUrls;
    }

    /// <summary>
    ///     Binder method for datalist.
    /// </summary>
    protected void Rebind()
    {
        dtlUploadedImages.DataSource = GetListOfUploadedImages();
        dtlUploadedImages.DataBind();
    }

    /// <summary>
    ///     page loader
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);

        if (!IsPostBack)
        {
            Rebind();
        }
    }

    /// <summary>
    ///     Called when submitting a file for upload.
    ///     There must be a file to upload.
    ///     The file must be less than 100K in size.
    ///     The file MIME type must be JPEG or PNG.
    ///     The file name must be unique.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadImage_OnClick(object sender, EventArgs e)
    {
        if (!fupImageUploader.HasFile)
        {
            lblStatusMessage.Text = "Please select a file to upload.";
        }
        else if (fupImageUploader.PostedFile.ContentLength > 100000)
        {
            lblStatusMessage.Text = "The file is too large. Please select a file less than 100K in size.";
        }
        else if (!GeneralConstants.PermittedContentTypes.Contains(fupImageUploader.PostedFile.ContentType))
        {
            var builder = new StringBuilder();
            builder.Append("Unpermitted file type. Permitted types are: ");
            foreach (var permittedContentType in GeneralConstants.PermittedContentTypes)
            {
                builder.Append(permittedContentType + " ");
            }

            lblStatusMessage.Text = builder.ToString();
        }
        foreach (var uploadedImage in GetListOfUploadedImages())
        {
            if (uploadedImage.Text.Equals(fupImageUploader.FileName))
            {
                lblStatusMessage.Text =
                    "The filename is already in use. Please give the file a unique name before uploading.";
                return;
            }
        }

        fupImageUploader.SaveAs(Server.MapPath(GeneralConstants.ImagesUploadFolder + "/" + fupImageUploader.FileName));

        Rebind();
    }

    /// <summary>
    ///     OnItem data bound method
    ///     DEPRECATED
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dtlUploadedImages_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            // don't do anything here
        }
    }

    /// <summary>
    ///     OnItem Command
    ///     if request is to delete, delete the file and rebind.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dtlUploadedImages_OnItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "deleteImage")
        {
            var fi = new FileInfo(Server.MapPath(e.CommandArgument.ToString()));
            fi.Delete();
            Rebind();
        }
    }
}
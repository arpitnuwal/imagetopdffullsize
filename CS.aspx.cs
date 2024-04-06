using System;
using System.IO;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class CS : System.Web.UI.Page
{
    protected void Upload(object sender, EventArgs e)
    {

        string filePath = Server.MapPath("~/Uploads/1.jpeg");

        using (Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(filePath);

                // Scale the image to fill the entire page
                img.ScaleAbsolute(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);

                pdfDoc.Add(img);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ImageExport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }



    }
}
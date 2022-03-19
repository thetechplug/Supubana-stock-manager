using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supubana_stock_manager
{
    public class PrintService
{
    public PrintService()
    {
        //
        // TODO: Add constructor logic here
        //
        this.docToPrint.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
    }//Add event handler to PrintDocument's PrintPage

    // Declare the PrintDocument object.
    PrintDocument docToPrint = new PrintDocument();//Create an instance of PrintDocument
 
        private string streamType;
    private string streamtxt;
    private Image streamima;

    // This method will set properties on the PrintDialog object and
    // then display the dialog.
    public void StartPrint(string txt, string streamType)
    {
        this.streamType = streamType;
        this.streamtxt = txt;
        // Allow the user to choose the page range he or she would
        // like to print.
        System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();//Create an instance of PrintDialog.
        PrintDialog1.AllowSomePages = true;

        // Show the help button.
        PrintDialog1.ShowHelp = true;

        // Set the Document property to the PrintDocument for 
        // which the PrintPage Event has been handled. To display the
        // dialog, either this property or the PrinterSettings property 
        // must be set 
        PrintDialog1.Document = docToPrint;//Set the Document property of PrintDialog to the instance of PrintDocument configured above

        //DialogResult result = PrintDialog1.ShowDialog();//Invoke PrintDialog's ShowDialog function to display the print dialog. If you don't want to comment, call docToPrint.Print() directly.
        //// If the result is OK then print the document.
        //if (result == DialogResult.OK)
        //{
        // docToPrint.Print();//Start printing
        //}
        docToPrint.Print();//Start printing
    }
    public void StartPrint(Image ima, string streamType)
    {
        this.streamType = streamType;
        this.streamima = ima;
        // Allow the user to choose the page range he or she would
        // like to print.
        System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();//Create an instance of PrintDialog.
        PrintDialog1.AllowSomePages = true;

        // Show the help button.
        PrintDialog1.ShowHelp = true;
        PrintDialog1.Document = docToPrint;//Set the Document property of PrintDialog to the instance of PrintDocument configured above

        DialogResult result = PrintDialog1.ShowDialog();//Invoke PrintDialog's ShowDialog function to display the print dialog. If you don't want to comment, call docToPrint.Print() directly.
                                                        // If the result is OK then print the document.
        if (result == DialogResult.OK)
        {
            docToPrint.Print();//Start printing
        }

        //docToPrint.Print();//Start printing
    }
    // The PrintDialog will print the document
    // by handling the document's PrintPage event.
    private void docToPrint_PrintPage(object sender,
               System.Drawing.Printing.PrintPageEventArgs e)//Set the event handler for the printer to start printing
    {

        // Insert code to render the page here.
        // This code will be called when the control is drawn.

        // The following code will render a simple
        // message on the printed document
        switch (this.streamType)
        {
            case "txt":
                string text = null;
                System.Drawing.Font printFont = new System.Drawing.Font
                                       ("Arial", 9, System.Drawing.FontStyle.Bold);//Set the print font and size here

                // Draw the content.

                text = streamtxt;
                //e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, e.MarginBounds.X, e.MarginBounds.Y);
                e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, 0, 10);//Set the print initial position
                break;
            case "image":
                System.Drawing.Image image = streamima;
                int x = e.MarginBounds.X;
                int y = e.MarginBounds.Y;
                int width = image.Width;
                int height = image.Height;
                if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
                {
                    width = e.MarginBounds.Width;
                    height = image.Height * e.MarginBounds.Width / image.Width;
                }
                else
                {
                    height = e.MarginBounds.Height;
                    width = image.Width * e.MarginBounds.Height / image.Height;
                }
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
                e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
                break;
            default:
                break;
        }

    }
}
}
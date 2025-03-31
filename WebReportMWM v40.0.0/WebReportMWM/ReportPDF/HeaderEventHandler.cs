using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTPDF
{
    public class HeaderEventHandler : IEventHandler
    {
        HeaderReportData HeaderData;
        

        public HeaderEventHandler(HeaderReportData headerData)
        {
            HeaderData = headerData;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = @event as PdfDocumentEvent;
            PdfDocument doc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas pdfCambas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), doc);
            Rectangle rootArea = new Rectangle(50, page.GetPageSize().GetTop() - 120, page.GetPageSize().GetWidth() - 75, 100);
            new Canvas(pdfCambas, doc, rootArea)
                .Add(GetTableHeader(docEvent));
            
        }

        private Table GetTableHeader(PdfDocumentEvent docEvent)
        {
            float[] cellsWidthPercent = { 20F,80F };
            Table tableHeader = new Table(UnitValue.CreatePercentArray(cellsWidthPercent)).UseAllAvailableWidth();

            Image logo = new Image(ImageDataFactory.Create(HeaderData.BusinessLogoPath));
            Cell cellLogo = new Cell().Add(logo.SetMaxWidth(60));
            cellLogo.SetBorder(Border.NO_BORDER);

            PdfFont fontTitle = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            Cell cellTitle = new Cell().Add(new Paragraph(HeaderData.Title));
            cellTitle.SetBorder(Border.NO_BORDER);
            cellTitle.SetTextAlignment(TextAlignment.CENTER);
            cellTitle.SetFont(fontTitle);
            cellTitle.SetFontSize(18);

            PdfFont fontSubTitle = PdfFontFactory.CreateFont(StandardFonts.COURIER);
            Cell cellSubTitleL1 = new Cell().Add(new Paragraph(HeaderData.SubTitleLine1))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(fontSubTitle)
                .SetFontSize(12);

            Cell cellSubTitleL2 = new Cell().Add(new Paragraph(HeaderData.SubTitleLine2))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(fontSubTitle)
                .SetFontSize(12);

            Cell cellDateReport = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
               // .SetFont(fontSubTitle)
                .SetFontSize(9);

            Cell cellVoid = new Cell().Add(new Paragraph(" "))
                .SetBorder(Border.NO_BORDER);

            tableHeader.AddCell(cellLogo);
            tableHeader.AddCell(cellTitle);
            tableHeader.AddCell(cellVoid);
            tableHeader.AddCell(cellSubTitleL1);
            tableHeader.AddCell(cellDateReport);
            tableHeader.AddCell(cellSubTitleL2);

            return tableHeader;
        }
    }
}

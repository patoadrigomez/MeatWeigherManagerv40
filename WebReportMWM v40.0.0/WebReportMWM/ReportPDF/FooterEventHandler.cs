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
    public class FooterEventHandler : IEventHandler
    {
        FooterReportData FooterData;
        

        public FooterEventHandler(FooterReportData footerData)
        {
            FooterData = footerData;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = @event as PdfDocumentEvent;
            PdfDocument doc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas pdfCambas = new PdfCanvas(page.NewContentStreamAfter(), page.GetResources(), doc);
            Rectangle rootArea = new Rectangle(20, 20, page.GetPageSize().GetWidth() - 50, 20);
            new Canvas(pdfCambas, doc, rootArea)
                .Add(GetTableFooter(docEvent));
            
        }

        private Table GetTableFooter(PdfDocumentEvent docEvent)
        {
            float[] cellsWidthPercent = { 70F,30F };
            Table tableHeader = new Table(UnitValue.CreatePercentArray(cellsWidthPercent)).UseAllAvailableWidth();

            PdfFont fonttextFooter = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            Cell cellTextFooter = new Cell().Add(new Paragraph(FooterData.TextLine))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFont(fonttextFooter)
                .SetFontSize(10);
            int pageNum = docEvent.GetDocument().GetPageNumber(docEvent.GetPage());

            Cell cellPageNumber = new Cell().Add(new Paragraph(String.Format("Pagina Nº: {0}",pageNum)))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFont(fonttextFooter)
                .SetFontSize(10);

            tableHeader.AddCell(cellTextFooter);
            tableHeader.AddCell(cellPageNumber);

            return tableHeader;
        }
    }
}

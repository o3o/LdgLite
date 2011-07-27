using IT = iTextSharp.text;
namespace Talaran.Ldg {
   public class ListBuilder: IBuilder {
      
      private IT.IDocListener document;
      private IT.pdf.PdfPTable table;
      private const int COLUMNS = 4;
      private const int SIZE_TITLE = 20;
      private const int SIZE_ROW = 10;
      private const float PAD_BOTTOM = 4F;
      public ListBuilder(IT.IDocListener document) {
         if (document == null) {
            throw new System.ArgumentNullException("document", "document cannot be null." ); 
         }
         this.document = document;

      }

      public void BeginDoc(int year) { 
         var title =
           new IT.Phrase("Trofeo Luca De Gerone",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, 26, IT.Font.BOLD)
                         );
         
         
         var par = new IT.Paragraph();
         par.SpacingAfter = 10;
         par.Alignment = IT.Element.ALIGN_CENTER;
         par.Add(title);
         par.Add(new IT.Phrase(year.ToString() ));
         
         document.Add(par);

         
      }
      public void BeginReport(string title) { 
         SetTable(title);

      }
      public void EndReport() {
         document.Add(table);
         document.NewPage();
      }

       
      public void Add(Athlete athete) {
         var data =
            new IT.Phrase(athete.Id.ToString() ,
                          IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));

          var cell = new IT.pdf.PdfPCell(data);
          cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
          cell.PaddingBottom = PAD_BOTTOM;
          table.AddCell(cell);
         
          data = new IT.Phrase(athete.Surname + " "  + athete.Name,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;

         table.AddCell(cell);
         table.AddCell(new IT.pdf.PdfPCell());
         
      }
      private void SetTable(string titleString) {
         
         var title =
           new IT.Phrase(titleString,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_TITLE, IT.Font.BOLD)
                         );
         //pett, nome ,  tempo
         float[] widths = {2f,6f,4f};
         table = new IT.pdf.PdfPTable(widths);
         table.HeaderRows = 2;
         table.DefaultCell.Border = IT.Rectangle.NO_BORDER;
         
         // intesatzione
         var cell = new IT.pdf.PdfPCell(title);
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER; 
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = PAD_BOTTOM;
         cell.GrayFill = 0.90F;
         table.AddCell(cell);
         
         // riga vuota
         //table.AddCell(GetEmptyRow());


         var data =
           new IT.Phrase("Pett.",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));

         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
         
         data = new IT.Phrase("Nome",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
        
         
         data = new IT.Phrase("Tempo",
                IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_RIGHT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
      }
      private IT.pdf.PdfPCell GetEmptyRow() {
         var cell = new IT.pdf.PdfPCell();
         cell.Border = IT.Rectangle.NO_BORDER;
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER; 
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = 20F;         
         return cell;
      }
   }
}
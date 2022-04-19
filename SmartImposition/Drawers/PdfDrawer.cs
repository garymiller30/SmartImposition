using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PDFlib_dotnet;
using SmartImposition.Handbook;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;
using SmartImposition.Models.Output;

namespace SmartImposition.Drawers
{
    public class PdfDrawer : IOutputDrawer
    {
        private const double mn = 2.83465;
        public void Save(string outputFileName, IOutputDocument document)
        {
            var p = new PDFlib();

            try
            {
                p.begin_document(outputFileName, "");

                foreach (var page in document.Pages)
                {
                    p.begin_page_ext(page.Format.Width * mn, page.Format.Height * mn, "");


                    DrawBackGroundMarks(p, page);
                    DrawPdfPages(p, page);

                    stubDrawForegroundMarks(p, page);

                    p.end_page_ext("");
                }


                p.end_document("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                p.Dispose();
            }
        }

        private void DrawBackGroundMarks(PDFlib p, OutputPage page)
        {
            
            if (page.Marks.Any())
            {
                var backgroundMarks = page.Marks.Where(x => !x.Foreground);

                foreach (var outputMark in backgroundMarks)
                {
                    if (outputMark is OutputPdfMark pdfMark)
                    {
                        DrawPdfMark(p, pdfMark);
                    }
                }

                //int gstate = p.create_gstate("overprintmode=1 overprintfill=true overprintstroke=true");

                //p.set_gstate(gstate);
                //p.setcolor("fillstroke", "cmyk", 0.79, 0, 0.44, 0.21);

                //int spot = p.makespotcolor("ProofColor");

                //p.setlinewidth(0.2);

                ///* Red rectangle */
                //p.setcolor("stroke", "spot", spot, 1.0, 0.0, 0.0);

                //foreach (var mark in page.Marks)
                //{
                //    p.rect(mark.Position.X * mn, mark.Position.Y * mn, mark.Format.Width * mn, mark.Format.Height * mn);
                //}
                //p.stroke();


            }

        }

        void DrawPdfMark(PDFlib p, OutputPdfMark mark)
        {
            var doc = p.open_pdi_document(mark.File, "");
            var docPage = p.open_pdi_page(doc, mark.PageNo, "");
            
            var opt = $"orientate={ImpositionHb.GetStringOrientateForPdfLib(mark.Rotate)} position={{{ImpositionHb.GetAnchorPositionForPdf(mark.MarkAnchor,mark.Rotate)}}}";
            p.fit_pdi_page(docPage, mark.Position.X * mn, mark.Position.Y * mn, opt);
            p.close_pdi_page(docPage);
            p.close_pdi_document(doc);
        }

        private void stubDrawForegroundMarks(PDFlib p, OutputPage page)
        {

        }
        private void DrawPdfPages(PDFlib p, OutputPage page)
        {


            foreach (var pdfPage in page.PdfPages)
            {
                if (pdfPage.AssignedDocument.Page != null)
                {
                    var pageRotate =
                        ImpositionHb.PageRotateAdd(pdfPage.Rotate, pdfPage.AssignedDocument.Page.Rotate);

                    Debug.WriteLine($"pageRotate: {pageRotate}");

                    var optlist =
                        $"orientate={ImpositionHb.GetStringOrientateForPdfLib(pageRotate)} " +
                        $"matchbox={{clipping={{{pdfPage.ClipBox.X * mn} {pdfPage.ClipBox.Y * mn} {pdfPage.ClipBox.Width * mn} {pdfPage.ClipBox.Height * mn}}}}}";

                    var doc = p.open_pdi_document(pdfPage.AssignedDocument.FileName, "");
                    var docPage = p.open_pdi_page(doc, pdfPage.AssignedDocument.Page.PageNo, "");

                    p.fit_pdi_page(docPage, pdfPage.Position.X * mn, pdfPage.Position.Y * mn, optlist);
                    p.close_pdi_page(docPage);
                    p.close_pdi_document(doc);

                    DrawGuttersBox(p, pdfPage);
                    DrawTrimbox(p, pdfPage);


                }
            }
        }



        private void DrawTrimbox(PDFlib p, OutputPdfPage page)
        {
            int gstate = p.create_gstate("overprintmode=1 overprintfill=true overprintstroke=true");

            p.set_gstate(gstate);
            p.setcolor("fillstroke", "cmyk", 0.79, 0, 0.44, 0.21);

            int spot = p.makespotcolor("ProofColor");

            p.setlinewidth(0.2);

            /* Red rectangle */
            p.setcolor("stroke", "spot", spot, 1.0, 0.0, 0.0);

            p.rect(
                (page.Position.X + page.TrimBox.X) * mn,
                (page.Position.Y + page.TrimBox.Y) * mn,
                page.TrimBox.Width * mn,
                page.TrimBox.Height * mn);


            p.stroke();
        }

        private void DrawGuttersBox(PDFlib p, OutputPdfPage pdfPage)
        {
            int gstate = p.create_gstate("overprintmode=1 overprintfill=true overprintstroke=true");

            p.set_gstate(gstate);
            p.setcolor("fillstroke", "cmyk", 0.79, 0, 0.44, 0.21);

            int spot = p.makespotcolor("ProofColor");

            p.setlinewidth(0.2);

            /* Red rectangle */
            p.setcolor("stroke", "spot", spot, 1.0, 0.0, 0.0);

            if (pdfPage.Rotate == RotateEnum._0 || pdfPage.Rotate == RotateEnum._180)
            {
                p.rect(pdfPage.Position.X * mn, pdfPage.Position.Y * mn, pdfPage.Format.Width * mn, pdfPage.Format.Height * mn);
            }
            else
            {
                p.rect(pdfPage.Position.X * mn, pdfPage.Position.Y * mn, pdfPage.Format.Height * mn, pdfPage.Format.Width * mn);
            }

            p.stroke();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PDFlib_dotnet;
using SmartImposition.Models.Enums;

namespace SmartImposition.Models.Input
{
    /// <summary>
    /// зберігає інформацію про PDF файл
    /// </summary>
    public class InputDocument
    {
        private const double mn = 2.83465;
        public string FileName { get; set; }

        public readonly List<InputPage> InputPages = new List<InputPage>();

        public InputDocument(string fileName)
        {
            FileName = fileName;
            LoadFile();
        }

        private void LoadFile()
        {
            if (File.Exists(FileName))
            {
                LoadPdf();
            }
            else
            {
                throw new Exception($"File does not exists: {FileName} ");
            }
        }

        private void LoadPdf()
        {
            var p = new PDFlib();

            try
            {
                var indoc = p.open_pdi_document(FileName, "");
                var cntPages = (int)p.pcos_get_number(indoc, "length:pages");

                for (var pageNo = 1; pageNo <= cntPages; pageNo++)
                {
                    var inputPage = new InputPage
                    {
                        PageNo = pageNo,
                        Rotate = GetRotateAngle(p, pageNo, indoc),
                        Format = GetFormats(p, pageNo, indoc)
                    };

                    InputPages.Add(inputPage);

                    Debug.WriteLine($"ImportPage {pageNo}: Rotate={inputPage.Rotate}, Format: {inputPage.Format.TrimBox}");
                }
                p.close_pdi_document(indoc);

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

        private PageFormat GetFormats(PDFlib p, int pageNo, int indoc)
        {
            var format = new PageFormat();

            var media = new double[4];
            var trim = new double[4];

            for (int i = 0; i < media.Length; i++)
                media[i] = p.pcos_get_number(indoc, $"pages[{pageNo-1}]/MediaBox[{i}]");
            
            format.MediaBox.X =      Math.Round(media[0] / mn,1);
            format.MediaBox.Y =       Math.Round(media[1] / mn,1);
            format.MediaBox.Width =   Math.Round(media[2] / mn,1);
            format.MediaBox.Height =  Math.Round(media[3] / mn,1);

            var trimStr = p.pcos_get_string(indoc, $"type:pages[{pageNo-1}]/TrimBox");

            if (trimStr == null)
            {
                for (int i = 0; i < trim.Length; i++)
                    trim[i] = media[i];
            }
            else
            {
                for (int i = 0; i < trim.Length; i++)
                    trim[i] = p.pcos_get_number(indoc, $"pages[{pageNo-1}]/TrimBox[{i}]");
            }

            format.TrimBox.X =       Math.Round((trim[0]-media[0]) / mn, 1);
            format.TrimBox.Y =       Math.Round((trim[1]-media[1]) / mn, 1);
            format.TrimBox.Width =   Math.Round((trim[2]-trim[0]) / mn, 1);
            format.TrimBox.Height =  Math.Round((trim[3]-trim[1]) / mn, 1);

            return format;
        }

        private RotateEnum GetRotateAngle(PDFlib p, int pageNo, int inDoc)
        {
            var rotated = p.pcos_get_string(inDoc, $"type:pages[{pageNo-1}]/Rotate");
            if (rotated == "number")
            {
                var angle = p.pcos_get_number(inDoc, $"pages[{pageNo-1}]/Rotate");

                if (angle == 180.0) return RotateEnum._180;
                if (angle == 90.0) return RotateEnum._90;
                if (angle == 270.0) return RotateEnum._270;
            }

            return RotateEnum._0;
        }

        public InputPage GetPageByIndex(int index)
        {
            if (index >= InputPages.Count) return null;

            return InputPages[index];
        }

        public InputPage GetPageByPageNo(int number)
        {
            return InputPages.FirstOrDefault(x => x.PageNo.Equals(number));
        }
    }
}

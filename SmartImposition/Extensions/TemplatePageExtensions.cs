using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Handbook;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;
using SmartImposition.Models.Output;
using SmartImposition.Models.Templates;

namespace SmartImposition.Extensions
{
    public static class TemplatePageExtensions
    {
        public static PageBox GetTrimBox(this TemplatePage page, OutputPdfPage outputPdfPage, RotateEnum assigned)
        {
            var trim = new PageBox();

            // взнаємо на скільки градусів потрібно повернути оригінал
            var angle = ImpositionHb.PageRotateAdd(outputPdfPage.Rotate, assigned);
            // взнаємо кут повороту
            angle = ImpositionHb.PageRotateAdd(angle, assigned);
            

            Debug.WriteLine($"OutputPdfPage.Rotate: {outputPdfPage.Rotate}, page.Rotated: {page.Rotated}, assignedPage: {assigned} = angle: {angle}");

            switch (angle)
            {
                case RotateEnum._0:

                    trim.X = page.Gutters.Left;
                    trim.Y = page.Gutters.Bottom;
                    trim.Width = page.Format.Width;
                    trim.Height = page.Format.Height;

                    break;

                case RotateEnum._270:

                    trim.X = page.Gutters.Top;
                    trim.Y = page.Gutters.Left;
                    trim.Width = page.Format.Height;
                    trim.Height = page.Format.Width;

                    break;

                case RotateEnum._180:

                    trim.X = page.Gutters.Right;
                    trim.Y = page.Gutters.Top;
                    trim.Width = page.Format.Width;
                    trim.Height = page.Format.Height;

                    break;

                case RotateEnum._90:

                    trim.X = page.Gutters.Bottom;
                    trim.Y = page.Gutters.Right;
                    trim.Width = page.Format.Height;
                    trim.Height = page.Format.Width;

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return trim;
        }


        public static PageBox GetHorMirrorTrimBox(this TemplatePage page, OutputPdfPage outputPdfPage, RotateEnum assigned)
        {
            var trim = new PageBox();
            var pageformat = page.GetFormatWithGutters();
            // взнаємо на скільки градусів потрібно повернути оригінал
            var angle = ImpositionHb.PageRotateAdd(outputPdfPage.Rotate, assigned);
            // взнаємо кут повороту
            angle = ImpositionHb.PageRotateAdd(angle, assigned);
            angle = ImpositionHb.PageRotateAdd(angle, page.Rotated);

            switch (angle)
            {
                case RotateEnum._0:

                    trim.X = page.Gutters.Right;
                    trim.Y = page.Gutters.Bottom;
                    trim.Width = page.Format.Width;
                    trim.Height = page.Format.Height;

                    break;

                case RotateEnum._90:

                    trim.X = page.Gutters.Bottom;
                    trim.Y = page.Gutters.Left;
                    trim.Width = page.Format.Height;
                    trim.Height = page.Format.Width;

                    break;

                case RotateEnum._180:

                    trim.X = page.Gutters.Left;
                    trim.Y = page.Gutters.Top;
                    trim.Width = page.Format.Width;
                    trim.Height = page.Format.Height;

                    break;

                case RotateEnum._270:

                    trim.X = page.Gutters.Top;
                    trim.Y = page.Gutters.Right;
                    trim.Width = page.Format.Height;
                    trim.Height = page.Format.Width;

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return trim;
        }


    }
}

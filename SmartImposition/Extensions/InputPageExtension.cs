using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Input;
using SmartImposition.Models.Templates;

namespace SmartImposition.Extensions
{
    public static class InputPageExtension
    {
        public static PageBox GetCorrectClipBox(this InputPage page, TemplateGutters gutters, RotateEnum rotate)
        {

            var clipBox = new PageBox();

            switch (rotate)
            {
                case RotateEnum._0:

                    clipBox.X = page.Format.TrimBox.X - gutters.Left;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Bottom;
                    clipBox.Width = page.Format.TrimBox.X +
                             page.Format.TrimBox.Width + gutters.Right;
                    clipBox.Height = page.Format.TrimBox.Y +
                             page.Format.TrimBox.Height + gutters.Top;

                    //Debug.WriteLine($"Clip: Left: {left},Bottom:{bottom},left: {width},top {height}");

                    break;
                case RotateEnum._90:

                    clipBox.X =page.Format.TrimBox.X - gutters.Bottom;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Right;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Height + gutters.Top;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Width + gutters.Left;
                   

                    break;
                case RotateEnum._180:

                    clipBox.X = page.Format.TrimBox.X - gutters.Right;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Top;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Width + gutters.Left;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Height + gutters.Bottom;
                    

                    break;
                case RotateEnum._270:
                    clipBox.X = page.Format.TrimBox.X - gutters.Top;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Left;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Height + gutters.Bottom;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Width + gutters.Right;
                    

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return clipBox;
        }

        public static PageBox GetCorrectHorMirroredClipBox(this InputPage page, TemplateGutters gutters,
            RotateEnum rotate)
        {
            var clipBox = new PageBox();

            switch (rotate)
            {
                case RotateEnum._0:

                    clipBox.X = page.Format.TrimBox.X - gutters.Right;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Bottom;
                    clipBox.Width = page.Format.TrimBox.X +
                             page.Format.TrimBox.Width + gutters.Left;
                    clipBox.Height = page.Format.TrimBox.Y +
                             page.Format.TrimBox.Height + gutters.Top;

                    //Debug.WriteLine($"Clip: Left: {left},Bottom:{bottom},left: {width},top {height}");

                    break;
                case RotateEnum._90:

                    clipBox.X =page.Format.TrimBox.X - gutters.Bottom;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Left;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Height + gutters.Top;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Width + gutters.Right;
                   // Debug.WriteLine($"Clip: Left: {left},Bottom:{bottom},left: {width},top {height}");

                    break;
                case RotateEnum._180:

                    clipBox.X = page.Format.TrimBox.X - gutters.Left;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Top;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Width + gutters.Right;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Height + gutters.Bottom;
                    //Debug.WriteLine($"Clip: Left: {left},Bottom:{bottom},Right: {width},top {height}");

                    break;
                case RotateEnum._270:
                    clipBox.X = page.Format.TrimBox.X - gutters.Top;
                    clipBox.Y = page.Format.TrimBox.Y - gutters.Right;
                    clipBox.Width = page.Format.TrimBox.X +
                                    page.Format.TrimBox.Height + gutters.Bottom;
                    clipBox.Height = page.Format.TrimBox.Y +
                                     page.Format.TrimBox.Width + gutters.Left;
                    //Debug.WriteLine($"Clip: Left: {left},Bottom:{bottom},left: {width},top {height}");

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return clipBox;
        }


        

    }
}

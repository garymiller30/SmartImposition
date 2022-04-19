using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Extensions
{
    public static class TemplateBlockExtension
    {
        public static TemplatePosition GetPagePosition(this TemplatePageBlock pageBlock, TemplatePageIndex pageIndex, RotateEnum rotate)
        {
            TemplatePosition tp = new TemplatePosition();

            switch (rotate)
            {
                case RotateEnum._0:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X < pageIndex.X && x.PageIndex.Y == pageIndex.Y)
                        .Sum(y=>y.GetFormatWithGutters().Width);
                    // виборка сторінок по Y
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y < pageIndex.Y && x.PageIndex.X == pageIndex.X)
                        .Sum(y=>y.GetFormatWithGutters().Height);

                    break;
                case RotateEnum._270:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y < pageIndex.Y && x.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X < pageIndex.X && x.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);

                    break;
                case RotateEnum._180:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X > pageIndex.X && x.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y > pageIndex.Y && x.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    break;
                case RotateEnum._90:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y < pageIndex.Y && x.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X > pageIndex.X && x.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotate), rotate, null);
            }

            return tp;
        }

        public static TemplatePosition GetHorMirrorPagePosition(this TemplatePageBlock pageBlock, TemplatePageIndex pageIndex, RotateEnum rotate)
        {
            TemplatePosition tp = new TemplatePosition();

            
            switch (rotate)
            {
                case RotateEnum._0:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X > pageIndex.X)
                        .Where(y=>y.PageIndex.Y == pageIndex.Y)
                        .Sum(y=>y.GetFormatWithGutters().Width);

                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y < pageIndex.Y && x.PageIndex.X == pageIndex.X)
                        .Sum(y=>y.GetFormatWithGutters().Height);

                    break;

                case RotateEnum._90:

                    tp.X = pageBlock.TemplatePages
                        .Where(y => y.PageIndex.Y > pageIndex.Y)
                        .Where(x=> x.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X > pageIndex.X)
                        .Where(y=>y.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);

                    break;
                case RotateEnum._180:

                    tp.X = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X < pageIndex.X)
                        .Where(y=>y.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.Y < pageIndex.Y)
                        .Where(y=> y.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    break;
                case RotateEnum._270:

                    tp.X = pageBlock.TemplatePages
                        .Where(y => y.PageIndex.Y < pageIndex.Y)
                        .Where(x=> x.PageIndex.X == pageIndex.X)
                        .Sum(y => y.GetFormatWithGutters().Height);
                    tp.Y = pageBlock.TemplatePages
                        .Where(x => x.PageIndex.X < pageIndex.X)
                        .Where(y=> y.PageIndex.Y == pageIndex.Y)
                        .Sum(y => y.GetFormatWithGutters().Width);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotate), rotate, null);
            }

            return tp;
        }


        public static RotateEnum GetOutputPageRotate(this TemplatePageBlock templateBlock, TemplatePage templatePage, RotateEnum rotate)
        {
            var angle = (int) rotate + (templatePage.Rotated ? 180 : 0);

            if (angle % 360 != 0) angle %= 360;

            return (RotateEnum) angle;
        }

        public static RotateEnum GetHorMirrorOutputPageRotate(this TemplatePageBlock templateBlock, TemplatePage templatePage, RotateEnum rotate)
        {
            int angle;

            if (rotate == RotateEnum._270 || rotate == RotateEnum._90)
            {
                angle = (int) rotate + (templatePage.Rotated ? 180 : 0) + 180;

            }
            else
            {
                angle = (int) rotate + (templatePage.Rotated ? 180 : 0);
            }
           
            if (angle % 360 != 0) angle %= 360;
            return (RotateEnum) angle;
        }

    }
}

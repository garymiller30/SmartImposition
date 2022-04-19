using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class AutomateSimpleNone
    {
        public void Place(TemplatePressSheet templateSheet, TemplatePageBlock pageBlock)
        {
            var printableFormat = templateSheet.GetPrintableFormat();
            var pageBlockFormat = pageBlock.GetTemplatePageBlockFormat();

            var rotated0Cnt = GetRotate0Cnt(templateSheet,printableFormat, pageBlockFormat);
            var rotated90Cnt = GetRotate90Cnt(templateSheet,printableFormat, pageBlockFormat);

            if (rotated0Cnt.TotalCnt == 0 && rotated90Cnt.TotalCnt == 0)
            {
                throw new Exception("No pages on press sheet");
            }
            
            templateSheet.TemplateBlocks.Clear();

            if (rotated0Cnt.TotalCnt > rotated90Cnt.TotalCnt)
            {
                FillPages(pageBlock, rotated0Cnt,RotateEnum._0);
            }
            else if (rotated0Cnt.TotalCnt == rotated90Cnt.TotalCnt)
            {
                // вибрати по більшому вільному простору навколо блоку сторінок
                if (rotated0Cnt.FreeSpaceSquare > rotated90Cnt.FreeSpaceSquare)
                {
                    FillPages(pageBlock, rotated0Cnt,RotateEnum._0);
                }
                else
                {
                    FillPages(pageBlock, rotated90Cnt,RotateEnum._90);
                }
            }
            else
            {
                FillPages(pageBlock, rotated90Cnt,RotateEnum._90);
            }

            templateSheet.TemplateBlocks.Add(pageBlock);
        }

        private void FillPages(TemplatePageBlock pageBlock, ImpositionResult result, RotateEnum rotateEnum)
        {
            pageBlock.Rotate = rotateEnum;
            pageBlock.CntXPages = result.CntX;
            pageBlock.CntYPages = result.CntY;

            var blockFormat = pageBlock.GetTemplatePageBlockFormat();

            pageBlock.TemplatePages.Clear();
            int idx = 0;

            double x = 0;

            for (int i = 0; i < pageBlock.CntXPages; i++)
            {
                double y = 0;

                for (int j = 0; j < pageBlock.CntYPages; j++)
                {
                    var templatePage = new TemplatePage
                    {
                        Format = new TemplateFormat
                        {
                            Width = pageBlock.Settings.TemplatePageFormat.Width,
                            Height = pageBlock.Settings.TemplatePageFormat.Height
                        },
                        Gutters = new TemplateGutters
                        {
                            Bottom = pageBlock.Settings.TemplatePageGutters.Bottom,
                            Top = pageBlock.Settings.TemplatePageGutters.Top,
                            Left = pageBlock.Settings.TemplatePageGutters.Left,
                            Right = pageBlock.Settings.TemplatePageGutters.Right
                        },
                        //Position = {X = x, Y = y},
                        PageIndex = new TemplatePageIndex{X = i, Y = j}
                    };


                    y += blockFormat.Height;

                    idx++;
                    pageBlock.TemplatePages.Add(templatePage);

                    //Debug.WriteLine($"TemplatePage: Idx={templatePage.PageIndex.X},{templatePage.PageIndex.Y}");
                }

                x += blockFormat.Width;

            }
        }

        private ImpositionResult GetRotate90Cnt(TemplatePressSheet templateSheet, TemplateFormat printableFormat, TemplateFormat pageBlockFormat)
        {

            ImpositionResult imposResult;

            if (templateSheet.Settings.PrintType == TemplatePressSheetPrintTypeEnum.WorkAndTurn)
            {
                imposResult = new ImpositionResult
                {
                    CntX = (int) (printableFormat.Height / pageBlockFormat.Width),
                    CntY = (int) ((printableFormat.Width/2) / pageBlockFormat.Height)
                
                };
                imposResult.FreeSpaceSquare = (printableFormat.Height - (imposResult.CntX*pageBlockFormat.Width))
                                              *(printableFormat.Width/2 - (imposResult.CntY*pageBlockFormat.Height));
            }
            else
            {
                imposResult = new ImpositionResult
                {
                    CntX = (int) (printableFormat.Height / pageBlockFormat.Width),
                    CntY = (int) (printableFormat.Width / pageBlockFormat.Height)
                
                };

                imposResult.FreeSpaceSquare = (printableFormat.Height - (imposResult.CntX*pageBlockFormat.Width))
                                              *(printableFormat.Width - (imposResult.CntY*pageBlockFormat.Height));
            }
           

            return imposResult;
        }

        private ImpositionResult GetRotate0Cnt(TemplatePressSheet templateSheet, TemplateFormat printableFormat, TemplateFormat pageBlockFormat)
        {
            ImpositionResult imposResult;


            if (templateSheet.Settings.PrintType == TemplatePressSheetPrintTypeEnum.WorkAndTurn)
            {
                imposResult = new ImpositionResult
                {
                    CntX = (int) ((printableFormat.Width/2) / pageBlockFormat.Width),
                    CntY= (int) (printableFormat.Height / pageBlockFormat.Height),
                
                };
                imposResult.FreeSpaceSquare = (printableFormat.Width/2 - (imposResult.CntX*pageBlockFormat.Width))
                                              *(printableFormat.Height - (imposResult.CntY*pageBlockFormat.Height));
                
            }
            else
            {
                imposResult = new ImpositionResult
                {
                    CntX = (int) (printableFormat.Width / pageBlockFormat.Width),
                    CntY= (int) (printableFormat.Height / pageBlockFormat.Height),
                
                };
                imposResult.FreeSpaceSquare = (printableFormat.Width - (imposResult.CntX*pageBlockFormat.Width))
                                              *(printableFormat.Height - (imposResult.CntY*pageBlockFormat.Height));
            }
            
            
            
            return imposResult;
        }


        class ImpositionResult
        {
            public int CntX { get; set; }
            public int CntY { get; set; }
            public double FreeSpaceSquare { get; set; }
            public int TotalCnt => CntX * CntY;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartImposition.Controllers;
using SmartImposition.Drawers;
using SmartImposition.Interfaces;
using SmartImposition.Models;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImpositionTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var smartImpos = new SmartImposition.SmartImposition(BindingTypeEnum.None);

            smartImpos.AddFile("input.pdf");

            var templateSheet = smartImpos.CreateTemplateSheet(new TemplatePressSheetSettings
            {
                Format = new TemplateFormat
                {
                    Width = 640,
                    Height = 450
                },
                NonPrintableGutters = new TemplateGutters { Top = 0, Bottom = 10, Left = 0, Right = 0 },
                PlaceAnchorPoint = AnchorPointEnum.Center,
                PrintType = TemplatePressSheetPrintTypeEnum.SheetWise
            });
            TemplatePageBlock pageBlock = smartImpos.CreateTemplatePageBlock(new TemplatePageBlockSettings
            {
                UseFormatFromFile = true,
                TemplatePageGutters = new TemplateGutters
                {
                    All = 0
                },
                AnchorPoint = AnchorPointEnum.Center
            });

            //ITemplateMark pdfMark = smartImpos.Marks.AddPdf("640.pdf");

            //pdfMark.MarkAnchor = AnchorPointEnum.BottomCenter;
            //pdfMark.SubjectAnchor = AnchorPointEnum.TopCenter;
            //pdfMark.OffsetPosition.Y = 3;
            //pdfMark.Rotate = RotateEnum._0;
            //pageBlock.Marks.Add(pdfMark);

            smartImpos.Imposition(new AutomateSimple(), templateSheet, pageBlock);
            
            smartImpos.TemplateSave("template.json");
            //Thread.Sleep(1000);
            //smartImpos.TemplateLoad("template.json");
            smartImpos.Save("output.pdf", new PdfDrawer());
            Process.Start("output.pdf");
        }
    }
}

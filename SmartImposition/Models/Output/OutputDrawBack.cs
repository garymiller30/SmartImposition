using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Controllers;
using SmartImposition.Extensions;
using SmartImposition.Handbook;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Output
{
    public class OutputDrawBack : IOutputDraw
    {
        private readonly IInputFileController _inputFileController;
        private readonly TemplatePressSheet _templatePressSheet;
        //private readonly TemplateRunList _runList;
        private readonly TemplateContainer _container;
        public OutputDrawBack(TemplateContainer container, IInputFileController inputFileController, TemplatePressSheet templatePressSheet)
        {
            _inputFileController = inputFileController;
            _templatePressSheet = templatePressSheet;
            //_runList = runList;
            _container = container;
        }

        public OutputPage Draw()
        {
            var page = new OutputPage
            {
                Format = {
                    Width = _templatePressSheet.Settings.Format.Width,
                    Height = _templatePressSheet.Settings.Format.Height
                }
            };


            foreach (var templateBlock in _templatePressSheet.TemplateBlocks)
            {
                var pos = ImpositionHb.GetHorMirrorBlockPosition(_templatePressSheet, templateBlock);

                var ofs = new TemplatePosition();

                foreach (var templatePage in templateBlock.TemplatePages)
                {
                    var runItem = _container.RunList.GetRunItem(templatePage.Numeration.Back);
                    var document = _inputFileController.GetDocument(runItem.InputFileId);
                    var assignedPage = document.GetPageByPageNo(runItem.InputFilePageNo);

                    var obj = new OutputPdfPage
                    {
                        Format = templatePage.Format + templatePage.Gutters,
                        Position = pos + templateBlock.GetHorMirrorPagePosition(templatePage.PageIndex, templateBlock.Rotate),
                        Rotate = templateBlock.GetHorMirrorOutputPageRotate(templatePage, templateBlock.Rotate),
                        AssignedDocument =
                        {
                            FileName = document.FileName,
                            Page = assignedPage
                        },
                        Gutters = new TemplateGutters
                        {
                            Left = templatePage.Gutters.Left,
                            Bottom = templatePage.Gutters.Bottom,
                            Right = templatePage.Gutters.Right,
                            Top = templatePage.Gutters.Top,

                        },

                    };
                    obj.TrimBox = templatePage.GetHorMirrorTrimBox(obj, assignedPage.Rotate);
                    obj.ClipBox = assignedPage.GetCorrectHorMirroredClipBox(obj.Gutters, assignedPage.Rotate);

                    page.PdfPages.Add(obj);
                }
            }

            return page;
        }
    }
}

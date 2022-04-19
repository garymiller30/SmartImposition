using System.Linq;
using SmartImposition.Extensions;
using SmartImposition.Handbook;
using SmartImposition.Interfaces;
using SmartImposition.Models.Templates;

namespace SmartImposition.Models.Output
{
    public class OutputDrawWorkAndTurn : IOutputDraw
    {
        private readonly IInputFileController _inputFileController;
        private readonly TemplatePressSheet _templatePressSheet;
        private readonly TemplateContainer _container;
        public OutputDrawWorkAndTurn(TemplateContainer container, IInputFileController inputFileController, TemplatePressSheet templatePressSheet)
        {
            _inputFileController = inputFileController;
            _templatePressSheet = templatePressSheet;
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


            //todo: draw background marks
            stubDrawBackgroundMarks(page,_templatePressSheet);

            foreach (var templateBlock in _templatePressSheet.TemplateBlocks)
            {
                var pos = ImpositionHb.GetBlockPosition(_templatePressSheet, templateBlock);

                DrawBackgroundPageBlockMarks(page,templateBlock, _templatePressSheet);


                foreach (var templatePage in templateBlock.TemplatePages)
                {
                    var runItem = _container.RunList.GetRunItem(templatePage.Numeration.Front);
                    var document = _inputFileController.GetDocument(runItem.InputFileId);
                    var assignedPage = document.GetPageByPageNo(runItem.InputFilePageNo);

                    var obj = new OutputPdfPage
                    {
                        Format = templatePage.Format + templatePage.Gutters,
                        Position = pos + templateBlock.GetPagePosition(templatePage.PageIndex,templateBlock.Rotate),
                        Rotate = templateBlock.GetOutputPageRotate(templatePage,templateBlock.Rotate),
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
                        ClipBox = assignedPage.GetCorrectClipBox(templatePage.Gutters,assignedPage.Rotate),

                        
                    };
                    obj.TrimBox = templatePage.GetTrimBox(obj, assignedPage.Rotate);
					DrawCutMarks(obj,templatePage);
                    page.PdfPages.Add(obj);
                }
            }

            //todo: draw foreground marks

            return page;
        }
        		
		private void DrawCutMarks(OutputPdfPage outputPdfPage, TemplatePage templatePage)
        {
            if (templatePage.CutMarks.Any())
            {
                foreach (var cutMark in templatePage.CutMarks)
                {
                    var outMark = new OutputLineMark();

                    //outputPdfPage.CutMarks.Add(outMark);
                    
                }
            }
        }
        private void DrawBackgroundPageBlockMarks(OutputPage page,TemplatePageBlock templateBlock, TemplatePressSheet templatePressSheet)
        {

            // get all background marks

            foreach (var mark in templateBlock.Marks)
            {
                if (!mark.Foreground)
                {
                    if (mark is TemplateMarkPdf pdfMark)
                    {
                        var outMark = new OutputPdfMark
                        {
                            Foreground = mark.Foreground,
                            Format = new TemplateFormat
                            {
                                Width = mark.Format.Width,
                                Height = mark.Format.Height
                            }
                        };

                        var blockPosition = ImpositionHb.GetBlockPosition(templatePressSheet, templateBlock);
                        outMark.Position = ImpositionHb.GetMarkPosition(mark, blockPosition, templateBlock);
                        outMark.Rotate = pdfMark.Rotate;
                        outMark.MarkAnchor = pdfMark.MarkAnchor;
                        outMark.File = _inputFileController.GetDocument(pdfMark.DocumentId).FileName;
                        outMark.PageNo = pdfMark.DocumentPageNo;

                        page.Marks.Add(outMark);

                    }
                }
            }
        }

        private void stubDrawBackgroundMarks(OutputPage page, TemplatePressSheet templatePressSheet)
        {
            foreach (var mark in templatePressSheet.Marks)
            {
                
            }
        }
    }
}
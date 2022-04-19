using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    public class MarkController : IMarkController
    {
        private readonly IInputFileController _fileController;


        public MarkController(IInputFileController inputFileController)
        {
            _fileController = inputFileController;
        }

        public ITemplateMark AddPdf(string pdf)
        {
            var id = _fileController.Load(pdf);
            var document = _fileController.GetDocument(id);

            TemplateMarkPdf mark = new TemplateMarkPdf
            {
                DocumentId = id,
                Format = new TemplateFormat
                {
                    Width = document.InputPages[0].Format.TrimBox.Width,
                    Height = document.InputPages[0].Format.TrimBox.Height
                }

            };

            return mark;
        }
        



        public void CreateCutMarks(TemplatePageBlock pageBlock,double lenght, double width)
        {
            foreach (var page in pageBlock.TemplatePages)
            {
                page.CutMarks.Clear();

                if (page.PageIndex.X == 0)
                {
                    var offsetPositionX = -pageBlock.Settings.TemplatePageGutters.Left;

                    var downLeftHor = new TemplateLineMark
                    {
                        Lenght = lenght, Width = width, OffsetPosition = {X = offsetPositionX},SubjectAnchor = AnchorPointEnum.BottomLeft
                    };
                    page.CutMarks.Add(downLeftHor);

                    var topLeftHor = new TemplateLineMark
                    {
                        Lenght = lenght, Width = width, OffsetPosition = {X = offsetPositionX},SubjectAnchor = AnchorPointEnum.TopLeft
                    };
                    page.CutMarks.Add(topLeftHor);
                }
                else if (page.PageIndex.X == pageBlock.CntXPages - 1)
                {
                    var offsetPositionX = pageBlock.Settings.TemplatePageGutters.Left;

                    var downLeftHor = new TemplateLineMark
                    {
                        Lenght = lenght, Width = width, OffsetPosition = {X = offsetPositionX},SubjectAnchor = AnchorPointEnum.BottomRight
                    };
                    page.CutMarks.Add(downLeftHor);

                    var topLeftHor = new TemplateLineMark
                    {
                        Lenght = lenght, Width = width, OffsetPosition = {X = offsetPositionX},SubjectAnchor = AnchorPointEnum.TopRight
                    };
                    page.CutMarks.Add(topLeftHor);


                }
                if (page.PageIndex.Y == 0)
                {

                }
                else if (page.PageIndex.Y == pageBlock.CntYPages - 1)
                {

                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Controllers;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition.Handbook
{
    public static class ImpositionHb
    {
        public static readonly Dictionary<AnchorPointEnum, TemplatePosition> AnchorMultiply = new Dictionary<AnchorPointEnum, TemplatePosition>()
        {
            {AnchorPointEnum.BottomCenter,new TemplatePosition{X=0.5,Y=0.0}},
            {AnchorPointEnum.BottomLeft,  new TemplatePosition{X=0.0,Y=0.0}},
            {AnchorPointEnum.BottomRight, new TemplatePosition{X=1.0,Y=0.0}},
            {AnchorPointEnum.Center,      new TemplatePosition{X=0.5,Y=0.5}},
            {AnchorPointEnum.CenterLeft,  new TemplatePosition{X=0.0,Y=0.5}},
            {AnchorPointEnum.CenterRight, new TemplatePosition{X=1.0,Y=0.5}},
            {AnchorPointEnum.TopCenter,   new TemplatePosition{X=0.5,Y=1.0}},
            {AnchorPointEnum.TopLeft,     new TemplatePosition{X=0.0,Y=1.0}},
            {AnchorPointEnum.TopRight,    new TemplatePosition{X=1.0,Y=1.0}},
        };


        private static readonly Dictionary<RotateEnum,string> pdfLibRotate = new Dictionary<RotateEnum, string>
        {
            {RotateEnum._0,"north"},
            {RotateEnum._90,"east"},
            {RotateEnum._180,"south"},
            {RotateEnum._270,"west"}
        };

        public static string GetStringOrientateForPdfLib(RotateEnum rotate)
        {
           // return "north";
            return pdfLibRotate[rotate];
        }

        public static RotateEnum PageRotateAdd(RotateEnum a, RotateEnum b)
        {
            var result = (int) a + (int) b;

            if (result >= 360)
            {
                result %= 360;
            }
            //else if (result == 360)
            //{
            //    result = 0;
            //}
            return (RotateEnum) result;
        }

        public static RotateEnum PageRotateAdd(RotateEnum a, bool b)
        {
            return PageRotateAdd(a, b ? RotateEnum._180 : RotateEnum._0);
        }

        public static TemplatePosition GetBlockPosition(TemplatePressSheet pressSheet, TemplatePageBlock templateBlock)
        {
            var pos = new TemplatePosition();

            var printableFormat = pressSheet.GetPrintableFormat();

            // відносна точка листа
            pos.X = printableFormat.Width *
                    ImpositionHb.AnchorMultiply[pressSheet.Settings.PlaceAnchorPoint].X +
                    pressSheet.Settings.NonPrintableGutters.Left;
            pos.Y = printableFormat.Height *
                    ImpositionHb.AnchorMultiply[pressSheet.Settings.PlaceAnchorPoint].Y +
                    pressSheet.Settings.NonPrintableGutters.Bottom;

            // відносна точка блоку
            var blockFormat = templateBlock.GetPageBlockFormat();

            pos.X -= blockFormat.Width *
                     ImpositionHb.AnchorMultiply[templateBlock.Settings.AnchorPoint].X +
                     templateBlock.OffsetPosition.X;
            pos.Y -= blockFormat.Height *
                     ImpositionHb.AnchorMultiply[templateBlock.Settings.AnchorPoint].Y +
                     templateBlock.OffsetPosition.Y;

            return pos;
        }

        public static TemplatePosition GetHorMirrorBlockPosition(TemplatePressSheet pressSheet, TemplatePageBlock templateBlock)
        {
            var pos = new TemplatePosition();

            var printableFormat = pressSheet.GetPrintableFormat();

            // відносна точка листа
            pos.X = printableFormat.Width *
                    ImpositionHb.AnchorMultiply[pressSheet.Settings.PlaceAnchorPoint].X +
                    pressSheet.Settings.NonPrintableGutters.Right;
            pos.Y = printableFormat.Height *
                    ImpositionHb.AnchorMultiply[pressSheet.Settings.PlaceAnchorPoint].Y +
                    pressSheet.Settings.NonPrintableGutters.Bottom;

            // відносна точка блоку
            var blockFormat = templateBlock.GetPageBlockFormat();

            pos.X -= blockFormat.Width *
                     ImpositionHb.AnchorMultiply[templateBlock.Settings.AnchorPoint].X +
                     templateBlock.OffsetPosition.X;
            pos.Y -= blockFormat.Height *
                     ImpositionHb.AnchorMultiply[templateBlock.Settings.AnchorPoint].Y +
                     templateBlock.OffsetPosition.Y;

            return pos;
        }

        public static TemplatePosition GetMarkPosition(Interfaces.ITemplateMark mark, TemplatePosition blockPosition,TemplatePageBlock templateBlock)
        {
            var position = new TemplatePosition();

            var blockFormat = templateBlock.GetPageBlockFormat();

            //відносна точка блоку
            position.X = blockFormat.Width *
                         ImpositionHb.AnchorMultiply[mark.SubjectAnchor].X;
            position.Y = blockFormat.Height *
                         ImpositionHb.AnchorMultiply[mark.SubjectAnchor].Y;

            //// відносна точка мітки
            //position.X -= mark.Format.Width *
            //              AnchorMultiply[mark.MarkAnchor].X;
            //position.Y -= mark.Format.Height * 
            //              AnchorMultiply[mark.MarkAnchor].Y;

            position.X += mark.OffsetPosition.X;
            position.Y += mark.OffsetPosition.Y;

            return position + blockPosition;
        }

        //static Dictionary<int,AnchorPointEnum> IntToAnchorTable = new Dictionary<int, AnchorPointEnum>
        //{
        //    {1,AnchorPointEnum.BottomLeft},
        //    {2,AnchorPointEnum.BottomCenter},
        //    {3,AnchorPointEnum.BottomRight},
        //    {4,AnchorPointEnum.CenterLeft},
        //    {5,AnchorPointEnum.Center},
        //    {6,AnchorPointEnum.CenterRight},
        //    {7,AnchorPointEnum.TopLeft},
        //    {8,AnchorPointEnum.TopCenter},
        //    {9,AnchorPointEnum.TopRight}
        //};

        static readonly Dictionary<AnchorPointEnum,int> AnchorToIntTable = new Dictionary<AnchorPointEnum, int>
        {
            {AnchorPointEnum.BottomLeft,1},
            {AnchorPointEnum.BottomCenter,2},
            {AnchorPointEnum.BottomRight,3},
            {AnchorPointEnum.CenterLeft,4},
            {AnchorPointEnum.Center,5},
            {AnchorPointEnum.CenterRight,6},
            {AnchorPointEnum.TopLeft,7},
            {AnchorPointEnum.TopCenter,8},
            {AnchorPointEnum.TopRight,9}
        };

        static readonly Dictionary<int, string> IntToStringAnchorPdfTable = new Dictionary<int, string>
        {
            {1,"0 0"},
            {2,"50 0"},
            {3,"100 0"},
            {4,"0 50"},
            {5,"50 50"},
            {6,"100 50"},
            {7,"0 100"},
            {8,"50 100"},
            {9,"100 100"}
        };
        /// <summary>
        /// 
        /// </summary>
        static readonly Dictionary<RotateEnum,int[]> AnchorPointByAngle = new Dictionary<RotateEnum, int[]>
        {
            {RotateEnum._0,    new [] {1,2,3,4,5,6,7,8,9}},
            {RotateEnum._90,   new [] {7,4,1,8,5,2,9,6,3}},
            {RotateEnum._180,  new [] {9,8,7,6,5,4,3,2,1}},
            {RotateEnum._270,  new [] {3,6,9,2,5,8,1,4,7}}
        };

        public static string GetAnchorPositionForPdf(AnchorPointEnum anchorPoint, RotateEnum rotate)
        {
            var rotateInt = AnchorPointByAngle[rotate];

            var intAnchor = AnchorToIntTable[anchorPoint];

            var rotatedInt = rotateInt[intAnchor - 1];

            return IntToStringAnchorPdfTable[rotatedInt];

        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartImposition.Controllers;
using SmartImposition.Interfaces;
using SmartImposition.Models;
using SmartImposition.Models.Enums;
using SmartImposition.Models.Templates;

namespace SmartImposition
{
    /// <summary>
    /// Виконує контроль над спуском.
    /// </summary>
    public class SmartImposition
    {

        private TemplateContainer _container = new TemplateContainer();

        private BindingTypeEnum _bindingType;

        private IInputFileController _fileController;
        private TemplateController _templateController;
        private ImpositionController _impositionController;
        private OutputController _outputController;
        public IMarkController Marks { get; set; }

        public SmartImposition(BindingTypeEnum bindingType)
        {
            _bindingType = bindingType;
            InitControllers();
           

        }

        private void InitControllers()
        {
            _fileController = new InputFileController(_container);
            _templateController = new TemplateController(_container);
            _impositionController = new ImpositionController(_container);
            _outputController = new OutputController(_container);
            Marks = new MarkController(_fileController);
        }

        public void AddFile(string file)
        {
            var id = _fileController.Load(file);

            foreach (var inputPage in _fileController.GetDocument(id).InputPages)
            {
                _container.RunList.Add(id,inputPage.PageNo);
            }

            
        }

        public TemplatePressSheet CreateTemplateSheet(TemplatePressSheetSettings templatePressSheetSettings)
        {
            return _templateController.CreateTemplateSheet(templatePressSheetSettings);
        }

        public void Imposition(IImposer imposer, TemplatePressSheet templateSheet,TemplatePageBlock pageBlock)
        {
            imposer.Impose(new ImpositionSettings
            {
                BindingType = _bindingType
            },
                templateSheet, 
                pageBlock);

            Marks.CreateCutMarks(pageBlock,0.2,3);
           
        }

        public void Save(string outputPdf, IOutputDrawer drawer)
        {
            //Marks.LoadPdfMarks(_templateController);

            var doc = _outputController.CreateOutputDocument(_container,new OutputSimpleController(), _fileController, _templateController);

            drawer.Save(outputPdf, doc);
        }

        public void TemplateSave(string templateName)
        {
            string output = JsonConvert.SerializeObject(_container, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                }
            );
            File.WriteAllText(templateName,output);
        }

        public void TemplateLoad(string templateName)
        {
            var str = File.ReadAllText(templateName);
            _container = JsonConvert.DeserializeObject<TemplateContainer>(str,new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            InitControllers();

        }

        public TemplatePageBlock CreateTemplatePageBlock(TemplatePageBlockSettings templatePageBlockSettings)
        {
            if (templatePageBlockSettings.UseFormatFromFile)
            {
                
                templatePageBlockSettings.TemplatePageFormat.Width =
                    _fileController.GetDocument( _container.RunList.TemplateRunPages[0].InputFileId)
                        .InputPages[0].Format.TrimBox.Width;
                templatePageBlockSettings.TemplatePageFormat.Height =
                    _fileController.GetDocument(_container.RunList.TemplateRunPages[0].InputFileId)
                        .InputPages[0].Format.TrimBox.Height;
            }
            return _templateController.CreateTemplatePageBlock(templatePageBlockSettings);
        }

       
    }
}

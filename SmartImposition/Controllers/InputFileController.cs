using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartImposition.Interfaces;
using SmartImposition.Models.Input;
using SmartImposition.Models.Templates;

namespace SmartImposition.Controllers
{
    /// <summary>
    /// контроль над файлами
    /// </summary>
    public class InputFileController : IInputFileController
    {
       // private readonly Dictionary<int, InputDocument> _documents = new Dictionary<int, InputDocument>();
       private TemplateContainer _container;

       public InputFileController(TemplateContainer container)
       {
           _container = container;

       }
        /// <summary>
        /// завантажити PDF 
        /// </summary>
        /// <param name="inputPdf"></param>
        /// <returns>вертає id документа</returns>
        public int Load(string inputPdf)
        {
            var document = new InputDocument(inputPdf);

            int id;

            if (_container.InputDocuments.Any())
            {
                id = _container.InputDocuments.Keys.Max()+1;
            }
            else
            {
                id = 1;
            }
            
            _container.InputDocuments.Add(id,document);

            return id;
        }
        /// <summary>
        /// отримати документ по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>вертає InputDocument</returns>
        /// <exception cref="Exception"></exception>
        public InputDocument GetDocument( int id)
        {
            if (_container.InputDocuments.ContainsKey(id))
            {
                return _container.InputDocuments[id];
            }

            throw new Exception($"No id: {id}");
        }

        public void LoadPdfMarks(TemplateController templateController)
        {
            //get all pdf mark in imposition (maybe duplicated)
            var marks = templateController.GetMarkPdfs();

            //todo: load file info, get size
            foreach (var markPdf in marks)
            {
                
            }

        }
    }
}

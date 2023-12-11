using Autodesk.Fabrication;
using Autodesk.Fabrication.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Command : IExternalApplication
    {
        //Use Execute method to as the entry point to the Addin
        public void Execute()
        {

            FileUltil fileUltil = new FileUltil();
            fileUltil.UpdateRandomFileName();
            fileUltil.HanldeAndExportData();
            fileUltil.UploadFileToServer();
        }

        //Use Terminate method to clean any resources used by the Addin
        public void Terminate()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp2023.Utilities
{
    public class FileService
    {
        private static void CopyFileToFolder(string sourceLocation, string destinationLocation, string fileNameToCopy)
        {
            if (File.Exists(Path.Combine(destinationLocation, fileNameToCopy)))
                File.Delete(Path.Combine(destinationLocation, fileNameToCopy));
            File.Copy(Path.Combine(sourceLocation, fileNameToCopy), Path.Combine(destinationLocation, fileNameToCopy));
            //File.Delete(Path.Combine(Application.StartupPath, fileNameToCopy)); //For move folder functions.
        }
    }
}

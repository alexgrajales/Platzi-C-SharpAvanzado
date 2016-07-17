using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librerias
{
    public class FileHelper
    {
        public List<FileObjectInfo> GetFileSystemObjects(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            var listaInfo = di.EnumerateFileSystemInfos();
            var fileObject = new List<FileObjectInfo>();
            foreach (var fso in listaInfo)
            {
                FileObjectType tipo = FileObjectType.Directory;
                if (fso.Attributes == FileAttributes.Archive)
                {
                    tipo = FileObjectType.File;
                }
                fileObject.Add(new FileObjectInfo() { Path = fso.FullName, Name = fso.Name, FileType = tipo });

            }

            return fileObject;
        }
    }
}

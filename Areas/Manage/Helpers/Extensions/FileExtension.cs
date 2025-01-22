using System.Drawing;

namespace KiderApp.Areas.Manage.Helpers.Extensions
{
    public static class FileExtension
    {
        public static string Upload(this IFormFile file,string root,string folder)
        {
            string fileName=Guid.NewGuid() +file.FileName;
            if (fileName.Length > 100)
            {
                fileName=fileName.Substring(0,100);
            }
            string path=Path.Combine(root,folder,fileName);
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static bool Delete(string root, string folder, string fileName)
        {
            string path = Path.Combine(root, folder, fileName);
            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return true;
        }
    }
}


namespace NewsReader.Helpers
{
    public class FileManager
    {
        public IWebHostEnvironment hostingEnvironment;
        public FileManager(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public void DeleteFile(string fileName, string path)
        {
            var fullPath = Path.Combine(hostingEnvironment.ContentRootPath, path) + fileName;
            if (System.IO.File.Exists(fullPath))
            {
                try
                {
                    System.IO.File.Delete(fullPath);
                }
                catch (System.Exception)
                {
                }
            }
        }

        public void DeleteFileWithThumb(string fileName, string path)
        {
            var fullPath = Path.Combine(hostingEnvironment.ContentRootPath, path) + fileName;
            if (System.IO.File.Exists(fullPath))
            {
                try
                {
                    System.IO.File.Delete(fullPath);
                }
                catch (System.Exception)
                {
                }
            }

            var fullPathThumb = Path.Combine(hostingEnvironment.ContentRootPath, path) + "thumb-" + fileName;
            if (System.IO.File.Exists(fullPathThumb))
            {
                try
                {
                    System.IO.File.Delete(fullPathThumb);
                }
                catch (System.Exception)
                {
                }
            }
        }

        public string Upload(IFormFile file, string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hostingEnvironment.ContentRootPath))
                    hostingEnvironment.ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");

                var uploadPath = Path.Combine(hostingEnvironment.ContentRootPath, path);
                string ext = Path.GetExtension(file.FileName);
                Guid guid = Guid.NewGuid();
                var uniqueFileName = guid.ToString() + ext;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);
                var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);
                fileStream.Close();
                return uniqueFileName;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public string UploadWithName(IFormFile file, string path, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hostingEnvironment.ContentRootPath))
                {
                    hostingEnvironment.ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
                }
                var uploadPath = Path.Combine(hostingEnvironment.ContentRootPath, path);
                string ext = Path.GetExtension(file.FileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, name + ext);
                var fileStream = new FileStream(filePath, FileMode.Create);


                file.CopyTo(fileStream);
                fileStream.Close();
                return name + ext;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public bool Move(string fileName, string SourceFolder, string DestFolder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hostingEnvironment.ContentRootPath))
                {
                    hostingEnvironment.ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
                }
                var SourceFolderPath = Path.Combine(hostingEnvironment.ContentRootPath, SourceFolder);
                var DestFolderPath = Path.Combine(hostingEnvironment.ContentRootPath, DestFolder);
                if (!Directory.Exists(DestFolderPath))
                {
                    Directory.CreateDirectory(DestFolderPath);
                }
                var filePath = Path.Combine(SourceFolderPath, fileName);

                var destFilePath = Path.Combine(DestFolderPath, fileName);

                if (System.IO.File.Exists(destFilePath))
                {
                    try
                    {
                        System.IO.File.Delete(destFilePath);
                    }
                    catch (System.Exception)
                    {
                    }
                }
                System.IO.File.Move(filePath, destFilePath);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public void DeleteDirectory(string path)
        {
            var fullPath = Path.Combine(hostingEnvironment.ContentRootPath, path);
            if (System.IO.Directory.Exists(fullPath))
            {
                try
                {
                    System.IO.Directory.Delete(fullPath, true);
                }
                catch (System.Exception)
                {
                }
            }
        }
    }

}
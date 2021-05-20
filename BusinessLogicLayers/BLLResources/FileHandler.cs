using BusinessLogicLayer.Resources;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.BLLResources
{
    public class FileHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInBytes">The file being uploaded in Bytes</param>
        /// <param name="filename">This is the name of the file and extension</param>
        /// <param name="folderName">The name of the folder where the file will be saved</param>
        /// <returns></returns>
        public static async Task<FileUploadDTO> SaveFileFromByte(byte[] fileInBytes, string filename,string folderName)
        {
            var imageUrl = ""; //variable for storing image to be set to the database url field

            try
            {
                var imageFolderName = Path.Combine("images",folderName);
                var imagePathToSave = Path.Combine(Directory.GetCurrentDirectory(), imageFolderName);
                var artworkFileName = filename; var imageFullPath = "";
                if (fileInBytes.Length > 0)
                {

                    imageFullPath = Path.Combine(imagePathToSave, filename);
                    if (File.Exists(imageFullPath))
                    {
                        return new FileUploadDTO
                        {
                            IsErrorOccured = true,
                            IsErrorKnown = true,
                            Message = "Filename already Exist, to avoid overwriting existing files please choose another name for the file"
                        };
                    }

                    File.WriteAllBytes(imageFullPath, fileInBytes);
                    imageUrl = Path.Combine($"images/{folderName}/", artworkFileName);
                }

            }
            catch (System.Exception ex)
            {
                return new FileUploadDTO
                {
                    IsErrorOccured = false,
                    ImageUrl = imageUrl
                };
            }

            return new FileUploadDTO
            {
                IsErrorOccured = false,
                ImageUrl = imageUrl
            };
        }

        public static async Task<byte[]> ConvertFileToByte( string filename)
        {

            var replaceForwardSlash = filename.Replace("/", @"\");
                //var imageFolderName = Path.Combine("images");
                var imagePathToSave = Path.Combine(Directory.GetCurrentDirectory());
                var artworkFileName = replaceForwardSlash; var imageFullPath = "";
                  
                 imageFullPath = Path.Combine(imagePathToSave, replaceForwardSlash);
                 //Convert the file to transfer it over to the API
                 using (FileStream fs = new FileStream(imageFullPath, FileMode.Open, FileAccess.Read))
                 {
                     // Create a byte array of file stream length
                     byte[] bytes = System.IO.File.ReadAllBytes(imageFullPath);
                     //Read block of bytes from stream into the byte array
                     fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                     //Close the File Stream
                     fs.Close();
                     return bytes; //return the byte data
                 }
               

        }

        public static async Task<OutputHandler> DeleteFileFromFolder(string Oldfilename, string folderName )
        {

            try
            {
                var replaceForwardSlash = Oldfilename.Replace("/", @"\");
                var imageFolderName = Path.Combine("images",folderName);
                var imagePathToSave = Path.Combine(Directory.GetCurrentDirectory());
                var artworkFileName = replaceForwardSlash; var imageFullPath = "";

                imageFullPath = Path.Combine(imagePathToSave, replaceForwardSlash);
                if (System.IO.File.Exists(imageFullPath))
                {
                    System.IO.File.Delete(imageFullPath);

                    return new OutputHandler { IsErrorOccured = false };
                }
                else
                {
                    return new OutputHandler { IsErrorOccured = true, Message = "File could not be found" };
                }
                
            }
            catch (System.Exception ex)
            {
                return new OutputHandler { IsErrorOccured = true };

            }


        }

        public static async Task<OutputHandler> GetFileSize(string imageUrl)
        {
            try
            {
                var storageSize = "";
                if (File.Exists(imageUrl))
                {
                    FileInfo fileInfo = new FileInfo(Path.Combine(imageUrl));
                    var size = fileInfo.Length / 1024f;
                    if (size < 1024)
                    {
                        if (size >= 100)
                        {
                            storageSize = size.ToString("000.00" + " KB");
                        }
                        else
                        {
                            storageSize = size.ToString("00.00" + " KB");
                        }

                    }
                    else
                    {
                        var sizeInMB = (size / 1024f);
                        //less than 9MB display single digit in MB
                        //greater than 9MB and less than 100 display 2 digits in MB( e.g. 20MB)
                        //else greater than 100 dispay 3 digit in MB (e.g 100MB)
                        if (sizeInMB <= 9)
                        {
                            storageSize = sizeInMB.ToString("0.00" + " " + " MB");
                        }
                        else if (sizeInMB > 9 && sizeInMB < 100)
                        {
                            storageSize = sizeInMB.ToString("00.00" + " " + " MB");
                        }
                        else
                        {
                            storageSize = sizeInMB.ToString("000.00" + " " + " MB");
                        }
                    }
                }
                else
                {
                    storageSize = "File Missing";
                }

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Result = storageSize
                };
            }
            catch (System.Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
            }
        }

    }
}

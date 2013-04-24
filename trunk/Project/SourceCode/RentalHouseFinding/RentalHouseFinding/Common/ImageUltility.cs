using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace RentalHouseFinding.Common
{
    public static class ImageUtility
    {
        public static SaveImageResult SaveImage(string path, int maxSize, string allowedExtensions, HttpPostedFileBase image, HttpServerUtilityBase server)
        {
            var result = new SaveImageResult { Success = false };

            if (image == null || image.ContentLength == 0)
            {
                result.Errors.Add("There was problem with sending image.");
                return result;
            }

            // Check image size
            if (image.ContentLength > maxSize)
                result.Errors.Add("Image is too big.");

            // Check image extension
            var extension = Path.GetExtension(image.FileName).Substring(1).ToLower();
            if (!allowedExtensions.Contains(extension))
                result.Errors.Add(string.Format("'{0}' format is not allowed.", extension));

            // If there are no errors save image
            if (!result.Errors.Any())
            {
                // Generate unique name for safety reasons
                var newName = Guid.NewGuid().ToString("N") + "." + extension;
                var serverPath = server.MapPath("~" + path + newName);
                image.SaveAs(serverPath);

                result.Success = true;
            }

            return result;
        }
    }

    public class SaveImageResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public SaveImageResult()
        {
            Errors = new List<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace RentalHouseFinding.Common
{
    class PNGHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "GET")
            {
                string requestedFile = context.Server.MapPath(context.Request.FilePath);
                FileInfo fileinfo = new FileInfo(requestedFile);
                string contentType = "";
                if (fileinfo.Exists && fileinfo.Extension.Remove(0, 1).ToUpper() == "PNG")
                {
                    contentType = "image/png";
                    context.Response.ContentType = contentType;
                    context.Response.TransmitFile(requestedFile);
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }
    }
}
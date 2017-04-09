using System;
using System.IO;
using System.Web;

namespace Visions.IntegrationTests.Helpers
{
    internal class CustomHttpPostedFileBase : HttpPostedFileBase
    {
        private Stream stream;
        private string contentType;
        private string fileName;

        public CustomHttpPostedFileBase(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override int ContentLength
        {
            get
            {
                return (int)stream.Length;
            }
        }

        public override string ContentType
        {
            get
            {
                return contentType;
            }
        }

        public override string FileName
        {
            get
            {
                return fileName;
            }
        }

        public override Stream InputStream
        {
            get
            {
                return stream;
            }
        }

        public override void SaveAs(string filename)
        {
            throw new NotImplementedException();
        }
    }
}

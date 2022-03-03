using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Base.Api.Filters
{
    public class CompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var content = actionExecutedContext.Response.Content;
            var bytes = content?.ReadAsByteArrayAsync().Result;
            if (bytes != null && bytes.Length > 0)
            {
                var acceptEncoding = actionExecutedContext.Request.Headers.AcceptEncoding.Where(x => x.Value == "gzip" || x.Value == "deflate").ToList();
                byte[] zlibbedContent;
                if (acceptEncoding.FirstOrDefault()?.Value == "deflate")
                {
                    zlibbedContent = DeflateByte(bytes);
                    actionExecutedContext.Response.Content = new ByteArrayContent(zlibbedContent);
                    actionExecutedContext.Response.Content.Headers.Add("Content-Encoding", "deflate");
                }
                else
                {
                    zlibbedContent = GZipByte(bytes);
                    actionExecutedContext.Response.Content = new ByteArrayContent(zlibbedContent);
                    actionExecutedContext.Response.Content.Headers.Add("Content-Encoding", "gzip");
                }
            }
            actionExecutedContext.Response.Content.Headers.Add("Content-Type", "application/json");

            base.OnActionExecuted(actionExecutedContext);
        }

        private byte[] GZipByte(byte[] data)
        {
            using (var output = new MemoryStream())
            {
                using (var compressor = new GZipStream(output, CompressionMode.Compress))
                {
                    compressor.Write(data, 0, data.Length);
                    compressor.Close();
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// Deflate the byte.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private byte[] DeflateByte(byte[] data)
        {
            using (var output = new MemoryStream())
            {
                using (var compressor = new DeflateStream(output, CompressionMode.Compress))
                {
                    compressor.Write(data, 0, data.Length);
                    compressor.Close();
                }

                return output.ToArray();
            }
        }
    }
}
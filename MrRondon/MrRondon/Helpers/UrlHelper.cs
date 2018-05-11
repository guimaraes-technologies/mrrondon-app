using System;
using System.IO;
using System.Net;

namespace MrRondon.Helpers
{
    public class UrlHelper
    {
        public static string ConvertImageUrlToBase64(string url)
        {
            var _byte = GetImage(url);

            var value = $"{Convert.ToBase64String(_byte, 0, _byte.Length)}==";

            return value;
        }

        private static byte[] GetImage(string url)
        {
            byte[] buf;

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)req.GetResponse();
                var stream = response.GetResponseStream();

                using (var br = new BinaryReader(stream))
                {
                    var len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                buf = null;
            }

            return buf;
        }
    }
}
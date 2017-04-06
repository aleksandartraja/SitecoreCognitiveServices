﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.SharedSource.CognitiveServices.Models;

namespace Microsoft.SharedSource.CognitiveServices.Repositories
{
    public class RepositoryClient : IRepositoryClient
    {
        public RepositoryClient()
        {
        }

        public virtual async Task<string> SendPostMultiPartAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "multipart/form-data", "POST");
        }

        public virtual async Task<string> SendEncodedFormPostAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "application/x-www-form-urlencoded", "POST");
        }

        public virtual async Task<string> SendTextPostAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "text/plain", "POST");
        }

        public virtual async Task<string> SendJsonPostAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "application/json", "POST");
        }

        public virtual async Task<string> SendJsonPutAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "application/json", "PUT");
        }

        public virtual async Task<string> SendJsonPatchAsync(string apiKey, string url, string data) {
            return await SendAsync(apiKey, url, data, "application/json", "PATCH");
        }

        public virtual async Task<string> SendJsonDeleteAsync(string apiKey, string url)
        {
            return await SendAsync(apiKey, url, "", "application/json", "DELETE");
        }

        public virtual async Task<string> SendOctetStreamUpdateAsync(string apiKey, string url, Stream stream)
        {
            return await SendAsync(apiKey, url, GetStreamString(stream), "application/octet-stream", "UPDATE");
        }

        public virtual async Task<string> SendOctetStreamPostAsync(string apiKey, string url, Stream stream)
        {
            return await SendAsync(apiKey, url, GetStreamString(stream), "application/octet-stream", "POST");
        }

        public virtual async Task<string> SendJsonUpdateAsync(string apiKey, string url, string data)
        {
            return await SendAsync(apiKey, url, data, "application/json", "UPDATE");
        }

        public virtual async Task<string> SendImagePostAsync(string apiKey, string url, Stream stream)
        {
            return await SendAsync(apiKey, url, GetStreamString(stream), GetImageStreamContentType(stream), "POST");
        }
        
        public virtual async Task<string> SendAsync(string apiKey, string url, string data, string contentType, string method, string token = "", bool sendChunked = false, string host = "")
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("url");
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("ApiKey");
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("authorization", $"Bearer {token}");
            }

            request.ContentType = contentType;
            request.Accept = contentType;
            request.Method = method;

            if(sendChunked)
                request.SendChunked = true;
            if(!string.IsNullOrEmpty(host))
                request.Host = host;

            if (!string.IsNullOrEmpty(data))
            {
                byte[] reqData = Encoding.UTF8.GetBytes(data);

                request.ContentLength = (long) reqData.Length;

                Stream requestStreamAsync = await request.GetRequestStreamAsync();
                requestStreamAsync.Write(reqData, 0, reqData.Length);
                requestStreamAsync.Close();
            }
            else
            {
                request.ContentLength = 0;
            }

            string end = "";
            WebResponse responseAsync = null;
            StreamReader streamReader = null;
            Exception exHolder = null;
            try
            {
                responseAsync = await request.GetResponseAsync();
                streamReader = new StreamReader(responseAsync.GetResponseStream());
                end = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                exHolder = ex;
            }
            finally
            {
                streamReader?.Close();
                responseAsync?.Close();
                if (exHolder != null)
                    throw exHolder;
            }
            

            return end;
        }
        
        public virtual async Task<string> SendOperationPostAsync(string apiKey, string url, string data)
        {

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("url");
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("ApiKey");
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("data");

            byte[] reqData = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = (long)reqData.Length;
            request.Method = "POST";

            Stream requestStreamAsync = await request.GetRequestStreamAsync();
            requestStreamAsync.Write(reqData, 0, reqData.Length);
            requestStreamAsync.Close();

            HttpWebResponse responseAsync = (HttpWebResponse)await request.GetResponseAsync();
            var opLocation = responseAsync.GetResponseHeader("operation-location");
            responseAsync.Close();

            return opLocation;
        }

        public virtual TokenResponse SendContentModeratorTokenRequest(string privateKey, string clientId)
        {
            byte[] reqData = Encoding.UTF8.GetBytes($"resource=https%3A%2F%2Fapi.contentmoderator.cognitive.microsoft.com%2Freview&client_id={clientId}&client_secret={privateKey}&grant_type=client_credentials");

            WebRequest request = WebRequest.Create("https://login.microsoftonline.com/contentmoderatorprod.onmicrosoft.com/oauth2/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = (long)reqData.Length;

            Stream requestStreamAsync = request.GetRequestStream();
            requestStreamAsync.Write(reqData, 0, reqData.Length);
            requestStreamAsync.Close();

            WebResponse response = request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string end = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            TokenResponse t = new JavaScriptSerializer().Deserialize<TokenResponse>(end);

            return t;
        }

        public virtual string SendBingSpeechTokenRequest(string apiKey) {

            WebRequest request = WebRequest.Create("https://api.cognitive.microsoft.com/sts/v1.0/issueToken");
            request.Method = "POST";
            request.Headers["Ocp-Apim-Subscription-Key"] = apiKey;
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string end = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            return end;
        }
        
        public virtual string GetImageStreamContentType(Stream stream)
        {
            var image = Image.FromStream(stream);
            if (ImageFormat.Jpeg.Equals(image.RawFormat))
                return "image/jpeg";
            else if (ImageFormat.Png.Equals(image.RawFormat))
                return "image/png";
            else if (ImageFormat.Gif.Equals(image.RawFormat))
                return "image/gif";
            else if (ImageFormat.Bmp.Equals(image.RawFormat))
                return "image/bmp";

            throw new BadImageFormatException("The image stream provided for cognitive analysis wasn't an allowed format (jpeg, png, gif or bmp)");
        }

        public virtual string GetStreamString(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                sb.Append(reader.ReadToEnd());
            }

            return sb.ToString();
        }
    }
}
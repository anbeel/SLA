using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace stockassistant
{
    public class BaiduPan
    {

        CookieCollection cookies = new CookieCollection();
        CookieContainer cc = new CookieContainer();

        string username = ConfigurationSettings.AppSettings["baiduuser"];
        string password = ConfigurationSettings.AppSettings["baidupwd"];

        string Token = "";
        string CodeString = "";

        private string Accept = "*/*";
        private string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
        private string Referer = "";

        private string ContentType = "application/x-www-form-urlencoded";

        private string SkyDrive_app_id = ConfigurationSettings.AppSettings["baiduappid"];

        public BaiduPan()
        {
            GetPageByGet("http://www.baidu.com", Encoding.UTF8);

            GetTokenAndCodeString();

            Login(username, password);
        }

        private void Login(string username, string password)
        {
            string loginUrl = "https://passport.baidu.com/v2/api/?login";

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("staticpage", "http://www.baidu.com/cache/user/html/v3Jump.html");
            postData.Add("charset", "GBK");
            postData.Add("token", Token);
            postData.Add("tpl", "ik");
            postData.Add("apiver", "v3");
            postData.Add("tt", Utility.GetCurrentTimeStamp().ToString());
            postData.Add("codestring", "");
            postData.Add("isPhone", "true");
            postData.Add("safeflg", "0");
            postData.Add("u", "http://www.baidu.com/");
            postData.Add("username", username);
            postData.Add("password", password);
            postData.Add("verifycode", "");
            postData.Add("mem_pass", "on");
            postData.Add("ppui_logintime", "22429");
            postData.Add("callback", "parent.bd__pcbs__7doo5q");

            string html = GetPageByPost(loginUrl, postData, Encoding.UTF8);
            Console.WriteLine(html);
        }

        private string GetPageByPost(string url, Dictionary<string, string> postData, Encoding encoder)
        {
            string html = "";

            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.CookieContainer = cc;
            webReq.Method = "POST";

            webReq.Accept = this.Accept;
            webReq.UserAgent = this.UserAgent;
            webReq.Referer = this.Referer;

            Stream reqStream = null;
            if (postData != null && postData.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in postData)
                {
                    sb.Append(HttpUtility.UrlEncode(kv.Key));
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(kv.Value));
                    sb.Append("&");
                }

                byte[] data = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd('&'));

                webReq.ContentType = ContentType;
                webReq.ContentLength = data.Length;
                reqStream = webReq.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                if (reqStream != null)
                {
                    reqStream.Close();
                }
            }

            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            cookies.Add(webResp.Cookies);
            Stream stream = webResp.GetResponseStream();
            StreamReader sr = new StreamReader(stream, encoder);
            html = sr.ReadToEnd();

            sr.Close();
            stream.Close();

            return html;
        }

        private string GetPageByGet(string url, Encoding encoder)
        {
            string html = "";

            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.CookieContainer = cc;
            webReq.Method = "GET";

            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            cookies.Add(webResp.Cookies);
            Stream stream = webResp.GetResponseStream();
            StreamReader sr = new StreamReader(stream, encoder);
            html = sr.ReadToEnd();

            sr.Close();
            stream.Close();

            return html;
        }

        /// <summary>
        /// 获取 Token & CodeString
        /// </summary>
        private void GetTokenAndCodeString()
        {
            Console.WriteLine("---------------------------------Get Token--------------------------------------");

            string url = string.Format("https://passport.baidu.com/v2/api/?getapi&tpl=ik&apiver=v3&tt={0}&class=login", Utility.GetCurrentTimeStamp());
            string html = GetPageByGet(url, Encoding.UTF8);
            Console.WriteLine(url);

            ResultData result = JsonConvert.DeserializeObject<ResultData>(html);
            if (result.ErrInfo.no == "0")
            {
                Token = result.Data.Token;
                CodeString = result.Data.CodeString;
            }

            Console.WriteLine("Token:{0}", Token);
            Console.WriteLine("CodeString:{0}", CodeString);

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        public bool DownloadFile(string pathinPan, string pathinLocal)
        {
            string url = "http://c.pcs.baidu.com/rest/2.0/pcs/file?";
            url += "method=download&path=" + pathinPan + "&app_id=" + SkyDrive_app_id;
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.CookieContainer = cc;
                //GetAllCookies(requestCookie);
                webReq.Method = "GET";
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                cookies.Add(webResp.Cookies);
                using (Stream st = webResp.GetResponseStream())
                {
                    using (Stream so = new System.IO.FileStream(pathinLocal, System.IO.FileMode.Create))
                    {
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, (int)by.Length);
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            osize = st.Read(by, 0, (int)by.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        private string GetBoundary()
        {
            return "----------" + DateTime.Now.Ticks.ToString("x");
        }

        private string GetContextType(FileInfo fileInfo)
        {
            string result = "";
            switch (fileInfo.Extension.ToLower())
            {
                case ".jpeg": result = "image/jpeg"; break;
                case ".jpg": result = "image/jpeg"; break;
                case ".txt": result = "text/plain"; break;
                case ".png": result = "image/png"; break;
                default: result = "text/plain"; break;
            }
            return result;
        }

        public bool UploadFile(string pathinPan, string pathinLocal)
        {
            try
            {
                //上传文件url
                string url = "http://c.pcs.baidu.com/rest/2.0/pcs/file?";
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("method", "upload");//方法
                data.Add("path", pathinPan);//上传到网盘的路径
                data.Add("ondup", "overwrite");//模式，覆盖
                data.Add("app_id", SkyDrive_app_id.ToString());
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in data)
                {
                    sb.Append(HttpUtility.UrlEncode(kv.Key));
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(kv.Value));
                    sb.Append("&");
                }
                string uploadUrl = url + sb.ToString();

                string boundary = GetBoundary();
                UrolsPage page = new UrolsPage();
                page.RequestCookie = cc;
                FileInfo fileInfo = new FileInfo(pathinLocal);
                if (fileInfo.Exists)
                {
                    using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                    {
                        string contenttype = GetContextType(fileInfo);

                        StringBuilder sbHeader = new StringBuilder();
                        sbHeader.Append("--");
                        sbHeader.Append(boundary);
                        sbHeader.Append("\r\n");
                        sbHeader.Append("Content-Disposition: form-data;filename=\"" + Path.GetFileName(pathinLocal) + "\"");
                        sbHeader.Append("\r\n");
                        sbHeader.Append("Content-Type: ");
                        sbHeader.Append(contenttype);
                        sbHeader.Append("\r\n");
                        sbHeader.Append("\r\n");

                        string postHeader = sbHeader.ToString();
                        byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

                        string result = page.POST(uploadUrl, boundary, postHeaderBytes, fileStream);
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    [Serializable]
    class ErrInfo
    {
        [JsonProperty("no")]
        public string no { get; set; }
    }

    [Serializable]
    class Data
    {
        [JsonProperty("codeString")]
        public string CodeString { get; set; }

        [JsonProperty("rememberedUserName")]
        public string RememberedUserName { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    [Serializable]
    class ResultData
    {
        [JsonProperty("errInfo")]
        public ErrInfo ErrInfo { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

    }

    public class UrolsPage
    {

        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string method;
        public string Method
        {
            get
            {
                if (string.IsNullOrEmpty(method))
                    method = "GET";
                method = method.ToUpper();
                if (method != "GET" && method != "POST")
                    method = "GET";
                return method;
            }
            set
            {
                method = value;
            }
        }

        private Encoding encoder;
        public Encoding Encoder
        {
            get
            {
                if (encoder == null)
                    encoder = Encoding.UTF8;
                return encoder;
            }
            set { encoder = value; }
        }

        private string html;
        public string Html { get { return html; } }

        private CookieContainer requestCookie;
        public CookieContainer RequestCookie
        {
            get
            {
                if (requestCookie == null)
                    requestCookie = new CookieContainer();
                return requestCookie;
            }
            set { requestCookie = value; }
        }

        private CookieCollection responseCookie;
        public CookieCollection ResponseCookie
        {
            get
            {
                if (responseCookie == null)
                    responseCookie = new CookieCollection();
                return responseCookie;
            }
            set { responseCookie = value; }
        }

        private Dictionary<string, string> data;
        public Dictionary<string, string> Data
        {
            get
            {
                if (data == null)
                    data = new Dictionary<string, string>();
                return data;
            }
            set { data = value; }
        }

        private string contentType;
        public string ContentType
        {
            get
            {
                if (string.IsNullOrEmpty(contentType))
                    contentType = "application/x-www-form-urlencoded";
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }

        private string accept;
        public string Accept
        {
            get
            {
                if (string.IsNullOrEmpty(accept))
                    accept = "*/*";
                return accept;
            }
            set
            {
                accept = value;
            }
        }

        private string userAgent;
        public string UserAgent
        {
            get
            {
                if (string.IsNullOrEmpty(userAgent))
                    userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36";
                return userAgent;
            }
            set
            {
                userAgent = value;
            }
        }

        private string referer;
        public string Referer
        {
            get
            {
                if (string.IsNullOrEmpty(referer))
                    referer = "";
                return referer;
            }
            set
            {
                referer = value;
            }
        }

        private long contentLength;
        public long ContentLength
        {
            get { return contentLength; }
            set { contentLength = value; }
        }

        public UrolsPage()
        {

        }
        public UrolsPage(string _url, Encoding _encoder)
        {
            url = _url;
            encoder = _encoder;
            GET();
        }

        public void GET()
        {
            //request 请求
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.CookieContainer = RequestCookie;//存储网页返回的cookie值
            webReq.Method = Method;
            //response 返回
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            ResponseCookie.Add(webResp.Cookies);
            //获取返回的response流
            using (Stream stream = webResp.GetResponseStream())
            {
                using (StreamReader read = new StreamReader(stream, Encoder))
                {
                    html = read.ReadToEnd();
                }
            }
        }

        public string POST(string url, string boundary, byte[] postHeaderBytes, Stream stram)
        {
            string result = "";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.CookieContainer = RequestCookie;
            if (!string.IsNullOrEmpty(boundary))
                webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            else
                webrequest.ContentType = "application/x-www-form-urlencoded";

            webrequest.Method = "POST";
            //生成header
            //string postHeader = sbHeader.ToString();
            //byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            //header长度+字节流长度=内容长度
            long length = 0;
            if (postHeaderBytes != null)
                length += postHeaderBytes.Length;
            length += stram.Length + boundaryBytes.Length;
            webrequest.ContentLength = length;//header的长度
            //想request中写入数据
            using (Stream requestStream = webrequest.GetRequestStream())
            {
                if (postHeaderBytes != null)
                {
                    //将header写入request请求
                    requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                }
                //将字节流写入request请求
                byte[] buffer = new Byte[(int)stram.Length];
                int bytesRead = 0;
                while ((bytesRead = stram.Read(buffer, 0, buffer.Length)) != 0)
                    requestStream.Write(buffer, 0, bytesRead);
                //将分隔符写入request请求
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);//分隔符
            }

            //获取返回响应
            using (HttpWebResponse webResp = (HttpWebResponse)webrequest.GetResponse())
            {
                using (Stream stream = webResp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoder))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}

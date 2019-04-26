using System;
using System.IO;
using System.Net;
using System.Threading;
using static DeezerSync.Deezer.API.Model.UserDataModel;

namespace DeezerSync.Deezer.API
{
    public class Login
    {
        public Login()
        {
            GetDeezerAPILogin();
        }

        private readonly string apiurl = "https://www.deezer.com/ajax/gw-light.php";
        private readonly string actionurl = "https://www.deezer.com/ajax/action.php";
        private readonly string api_version = "api_version=1.0";
        private readonly string api_input = "input=3";
        private readonly string api_token = "api_token=";
        private readonly string method = "method=";
        private readonly string cid = "cid=";

        internal string userid;
        private string apiKey;
        private string csrfsid;
        private string secret = "";


        internal string DeezerRequest(string Dmethod, string payload = null)
        {

            var request = (dynamic)null;
            if (string.IsNullOrEmpty(apiKey))
            {
                request = (HttpWebRequest)WebRequest.Create(apiurl + "?" + api_version + "&" + api_token + "&" + api_input + "&" + method + Dmethod + "&" + cid + GenCid());
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(apiurl + "?" + api_version + "&" + apiKey + "&" + api_input + "&" + method + Dmethod + "&" + cid + GenCid());
            }

            request.Method = "POST";

            if (!string.IsNullOrEmpty(payload))
            {
                request.ContentType = "application/json; charset=utf-8";
            }

            // User Agent: Chrome Version 72.0.3626.121
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
            request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
            request.Headers["Cache-Control"] = "max-age=0";
            request.Headers["accept-language"] = "en-US,en;q=0.9,en-US;q=0.8,en;q=0.7";
            request.Headers["accept-charset"] = "utf-8,ISO-8859-1;q=0.8,*;q=0.7";
            request.Headers["cookie"] = "arl=" + secret+ "; sid="+csrfsid;

            var content = string.Empty;
            request.CookieContainer = new CookieContainer();

            try
            {
                if (!string.IsNullOrEmpty(payload))
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(payload);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (string.IsNullOrEmpty(csrfsid))
                    {
                        foreach (Cookie cook in response.Cookies)
                        {
                            if (cook.Name.Equals("sid"))
                            {
                                csrfsid = cook.Value;
                            }
                        }
                    }
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            content = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException we)
            {
                // Wait before the next request
                Thread.Sleep(1000);
                // Converting Data
                int errorCode = (int)((HttpWebResponse)we.Response).StatusCode;
                int startsWith = 5;
                // Retry HTTP Request on HTTPError 5xx
                if (errorCode.ToString().StartsWith(startsWith.ToString()))
                {
                    throw new WebException("ERROR during HTTP Request: " + we.Message + " Error Code: " + (int)((HttpWebResponse)we.Response).StatusCode);
                }
                else
                {
                    throw new WebException("ERROR during HTTP Request: " + we.Message + " Error Code: " + (int)((HttpWebResponse)we.Response).StatusCode);
                }
            }
            return content;
        }

        private int GenCid()
        {
            Random rnd = new Random();
            return rnd.Next(100000000, 999999999);
        }

        protected void GetDeezerAPILogin()
        {
            string webresult = DeezerRequest("deezer.getUserData");
            var welcome = Welcome.FromJson(webresult);
            // Check for Valid User ID
            if (welcome.Results.User.UserId > 0)
            {
                // Check for Valid API Key
                if (!string.IsNullOrEmpty(welcome.Results.CheckForm) && welcome.Results.User.UserId != 0)
                {
                    // Write API Key to Var
                    apiKey = api_token + welcome.Results.CheckForm;
                    // Write UserID to Var
                    userid = welcome.Results.User.UserId.ToString();
                }
                else
                {
                    throw new Exception("Wrong User Information");
                }
            }
            else
            {
                throw new Exception("Cannot get Deezer API Key");
            }
        }
    }
}

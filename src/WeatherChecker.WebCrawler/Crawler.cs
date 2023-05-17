using System;
using System.Net.Http;
using System.Text;

namespace WeatherChecker.WebCrawler
{
    /// <summary>
    /// Class used to make the web requests 
    /// </summary>
    internal class Crawler
    {
        /// <summary>
        /// Simple GET REQUEST METHOD 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Plain text of html response of the request (UTF8 encoded)</returns>
        public static string Get(string url, string refer = null)
        {
            int tries = 0;

            while (true)
            {
                //Try to get the URL for five times (this is necessary sometimes due infra instabilities like timeouts and bad internet conections)
                if (tries > 5)
                    break;

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //The Australian government website don't like webcrawlers, so you have to put this headers to camouflage your request and mimic a browser request, so please, do not use this code to production purposes
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
                        if(!string.IsNullOrEmpty(refer))
                            client.DefaultRequestHeaders.Add("Referer", refer);
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");

                        using (HttpResponseMessage res = client.GetAsync(url).Result)
                        using (HttpContent content = res.Content)
                        {
                            var byteArray = content.ReadAsByteArrayAsync().Result;
                            var data = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                            if (data != null)
                            {
                                return (data);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch
                {
                    tries++;
                }
            }

            //In case of non sucessfull tries
            return null;
        }
    }
}

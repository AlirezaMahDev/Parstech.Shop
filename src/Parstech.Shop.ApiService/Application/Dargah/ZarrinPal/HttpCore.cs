using Newtonsoft.Json;

using System.Net;


namespace Parstech.Shop.ApiService.Application.Dargah.ZarrinPal;

internal class HttpCore
{
    public string URL { get; set; }
    public Method Method { get; set; }
    public object Raw { get; set; }


    public HttpCore(string URL)
    {
        this.URL = URL;
    }

    public HttpCore()
    {
        //TODO....
    }


    public string Get()
    {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = Method.ToString();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                               SecurityProtocolType.Tls11 |
                                               SecurityProtocolType.Tls |
                                               SecurityProtocolType.Ssl3;

        using (StreamWriter streamWriter = new(httpWebRequest.GetRequestStream()))
        {
            if (Method == Method.POST)
            {
                string json = JsonConvert.SerializeObject(Raw);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (StreamReader streamReader = new(httpResponse.GetResponseStream()))
        {
            string result = streamReader.ReadToEnd();
            return result.ToString();
        }
    }
}

public enum Method
{
    GET,
    POST
}
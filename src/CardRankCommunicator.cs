using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Nancy.Simple
{
    public class CardRankCommunicator
    {
        public static int CallRainMan()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://rainman.leanpoker.org/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsync("rank", new StringContent("cards=[{'rank':'5', 'suit':'diamonds'},{'rank':'6', 'suit':'diamonds'},{'rank':'7', 'suit':'diamonds'},{'rank':'8', 'suit':'diamonds'},{'rank':'9', 'suit':'diamonds'}]")).Result;
                    Console.Error.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return 0;
        }
    }
}

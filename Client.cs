using System;
using System.Net.Http;

namespace ServicePosilka
{
    public class Client : HttpClient
    {
        public Client(string token, string address)
        {
            BaseAddress = new Uri(address);
            DefaultRequestHeaders.Add("X-Authorization-Token", token);
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
        public Client(string address)
        {
            BaseAddress = new Uri(address);
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}

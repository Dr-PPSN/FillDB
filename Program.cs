using RestSharp;
using System;

namespace FillDB
{
    internal class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            Console.WriteLine("Welcome to the FillDB program!");
            Console.WriteLine("Please enter the URL of the API (string)");
            string url = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please chose how many request should send (int)");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Please chose the method:");
            Console.WriteLine("1 for GET");
            Console.WriteLine("2 for POST");
            Console.WriteLine("3 for PUT");
            Console.WriteLine("4 for DELETE");

            var method = Console.ReadLine();
            switch (method)
            {
                case "1":
                    Get(url, count);
                    break;
                case "2":
                    Post(url, count);
                    break;
                case "3":
                    Put(url, count);
                    break;
                case "4":
                    Delete(url, count);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
            Console.WriteLine("Please Wait...");
            Console.ReadLine();
            Main(null);
            void Get(string url, int count)
            {

            }
            async void Post(string url, int count)
            {
                var values = new Dictionary<string, string>
                {
                    { "un", RandomString(10) },
                    { "pw", RandomString(6) }
                };

                var content = new FormUrlEncodedContent(values);

                for (int i = 0; i < count; i++)
                {
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    Output(responseString);
                }
                
                Console.ReadLine();
                Main(null);
            }
            void Put(string url, int count)
            {

            }
            void Delete(string url, int count)
            {

            }
            async void Output(string txt)
            {
                Console.WriteLine("");
                Console.WriteLine("Response:");
                Console.WriteLine(txt);
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
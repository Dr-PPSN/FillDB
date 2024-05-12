using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace FillDB
{
    internal class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            var allTasks = new List<Task>();
            var values = new Dictionary<string, string> {};
            
            Console.WriteLine("Welcome to the FillDB program!");
            Console.WriteLine("Please enter the URL of the API (string)");
            string? url = Console.ReadLine();
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
                    break;
                case "2":
                    for (int i = 0; i < count; i++)
                    {
                        values.Add("cch", RandomString(5) + "+" + RandomString(7));
                        values.Add("ccn", RandomNumber(4,4).ToString() + "+" + RandomNumber(4,4).ToString() + "+" + RandomNumber(4,4).ToString() + "+" + RandomNumber(4,4).ToString());
                        values.Add("exp", RandomNumber(1,12).ToString() + "%2F" + RandomNumber(24,30).ToString());
                        values.Add("cw", RandomNumber(100,999).ToString());
                        var content = new FormUrlEncodedContent(values);
                        allTasks.Add(Task.Run(() => Post(url, content)));
                        values.Clear();
                    }
                    Task.WhenAll(allTasks);
                    break;
                case "3":
                    Put(url);
                    break;
                case "4":
                    Delete(url);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
            Console.WriteLine("Please Wait...");
            Console.ReadLine();
            Main(args);

            async void Post(string? url, FormUrlEncodedContent content)
            {
                Console.WriteLine(url);
                try
                {
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler: " + e);
                }
            }
            void Put(string? url)
            {

            }
            void Delete(string? url)
            {

            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
namespace mvc_project.Data;
using System;
using System.Net.Http.Headers;
using Microsoft.Net.Http;
using mvc_project.Models;

public class ApiMethodes 
{
    public static  HttpClient apiClient {get;set;} = new HttpClient();

    public static void runApiRequest()
    {
        
        getApiInfo().Wait();
    }
    public async static Task<User> getApiInfo()
    {
        apiClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        apiClient.DefaultRequestHeaders.Accept.Clear();
        apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await apiClient.GetAsync("todos/1");
        User _user = new User()
        {
            id = 1 ,
            userId = 1,
            title = "null",
            completed = false

        };

        if (response.IsSuccessStatusCode)
        {
            _user = await response.Content.ReadFromJsonAsync<User>(); 
            //Console.WriteLine("{0}\t${1}\t{2}", _user.id, _user.title, _user.completed);
        }
         return _user;
    }
    public async static Task<User> findMeaning()
    {
        apiClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        apiClient.DefaultRequestHeaders.Accept.Clear();
        apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await apiClient.GetAsync("todos/1");
        User _user = new User()
        {
            id = 1 ,
            userId = 1,
            title = "null",
            completed = false

        };

        if (response.IsSuccessStatusCode)
        {
            _user = await response.Content.ReadFromJsonAsync<User>(); 
            //Console.WriteLine("{0}\t${1}\t{2}", _user.id, _user.title, _user.completed);
        }
         return _user;
    }
}
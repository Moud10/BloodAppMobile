using BloodAPP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BloodAPP.Services
{
    public class ApiServices
    {
        static string acces_token = "";
        public async Task<bool> RegisterUser(string email,string password,string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email=email,
                Password=password,
                ConfirmPassword=confirmPassword
            };
            var httpClient = httpClientF();
            var json=JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response=await httpClient.PostAsync("https://192.168.0.108:45457/api/Account/register", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalue = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("username",email),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("grant_type", "password")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "https://192.168.0.108:45457/token");
            request.Content = new FormUrlEncodedContent(keyvalue);
            var httpClient = httpClientF();
            var response = await httpClient.SendAsync(request);
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic stuff = JsonConvert.DeserializeObject(content);
            acces_token = stuff.access_token;
            Settings.AccessToken = acces_token;
            Settings.UserName = email;
            Settings.Password = password;
            return response.IsSuccessStatusCode;
        }
        public List<BloodUser>FindBlood(string country,string bloodType)
        {
            var httpClient = httpClientF();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var bloodApiUrl = "https://192.168.0.108:45457/api/BloodUsers";
            var json = httpClient.GetStringAsync($"{bloodApiUrl}?bloodGroup={bloodType}&country={country}").GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }
        public List<BloodUser> LatestBloodUser()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(httpClientHandler);
            var res=httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", acces_token);
            var bloodApiUrl = "https://192.168.0.108:45457/api/BloodUsers";
            var json = httpClient.GetStringAsync($"{bloodApiUrl}").GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }
        public async Task<bool> RegisterDonar(BloodUser bloodUser)
        {
            var json = JsonConvert.SerializeObject(bloodUser);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "access token");
            var bloodApiUrl = "https://192.168.0.108:45456/api/BloodUsers";
            var response = await httpClient.PostAsync($"{bloodApiUrl}",content);
            return response.IsSuccessStatusCode;
        }
        public static HttpClient httpClientF()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(httpClientHandler);
            return httpClient;
        }
    }
}

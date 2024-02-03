using System.Text;

namespace API.Services;

public class FirebaseService
{
    private readonly string _firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
    private readonly HttpClient _httpClient = new();

    public async Task<string> GetAsync(string path)
    {
        var response = await _httpClient.GetAsync(_firebaseUrl + path);
        if (!response.IsSuccessStatusCode)
        {
            return "";
        }
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> PutAsync(string path, string data)
    {
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine("|");
        System.Console.WriteLine(_firebaseUrl + path);
        var response = await _httpClient.PutAsync(_firebaseUrl + path, content);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteAsync(string path)
    {
        // TODO : test firebase service and fazaa deletion
        var response = await _httpClient.DeleteAsync(_firebaseUrl + path);
        if (!response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine("||");
            System.Console.WriteLine(response);
            return false;
        }
        return true;
    }


}
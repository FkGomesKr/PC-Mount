using System;
using System.Collections.Generic;  // Added to support List<>
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlazorWinForms
{
    public partial class FormWindow : Form
    {
        private readonly List<string> Urls = new List<string>
        {
            "http://localhost:5106",
            "http://localhost:7061",
            "https://localhost:5106",
            "https://localhost:7061"
        };

        private const int Threshold = 800; // Waiting time between retries in milliseconds
        private readonly HttpClient _httpClient;

        public FormWindow()
        {
            InitializeComponent();

            // Optional: Ignore SSL certificate validation (useful in development with self-signed certs)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)  // Set custom timeout for the HTTP client
            };
            _ = CheckServerAndLoadAsync();
        }

        // Check if a server is available and load the corresponding page
        private async Task CheckServerAndLoadAsync()
        {
            foreach (var url in Urls)
            {
                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        Invoke(new Action(() => LoadPage(url)));
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while connecting to {url}: {ex.Message}");
                }
                await Task.Delay(Threshold);
            }
        }

        private void LoadPage(string url)
        {
            webView2.Source = new Uri(url);
            //Console.WriteLine($"Loading page: {url}");
        }
    }
}
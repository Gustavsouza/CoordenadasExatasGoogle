using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        // Sua chave de API do Google Maps Geocoding
        string apiKey = "";

        // Endereço completo que você deseja geocodificar
        string enderecoCompleto = "Rua Aloísio Sepulcri Júnior, 20, Guaraciaba, Serra, Espírito Santo";

        // Codifica o endereço para formato de URL
        string enderecoCodificado = Uri.EscapeDataString(enderecoCompleto);

        // URL da API do Google Maps Geocoding
        string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={enderecoCodificado}&key={apiKey}";

        await Console.Out.WriteLineAsync(apiUrl);

        using (HttpClient client = new HttpClient())
        {
            // Faz a solicitação GET à API
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Lê a resposta JSON
                string json = await response.Content.ReadAsStringAsync();

                // Desserializa o JSON para um objeto C#
                var resultado = JsonConvert.DeserializeObject<GoogleMapsGeocodingResponse>(json);

                // Verifique se há resultados
                if (resultado.status == "OK" && resultado.results.Length > 0)
                {
                    var resultadoGeocodificado = resultado.results[0];
                    var localizacao = resultadoGeocodificado.geometry.location;

                    Console.WriteLine($"Latitude: {localizacao.lat}");
                    Console.WriteLine($"Longitude: {localizacao.lng}");
                }
                else
                {
                    Console.WriteLine("Nenhum resultado encontrado.");
                }
            }
        }
    }
}
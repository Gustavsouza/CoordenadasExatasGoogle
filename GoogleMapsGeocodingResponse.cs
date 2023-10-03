public partial class Program
{
    // Classes de modelo para desserialização
    public class GoogleMapsGeocodingResponse
    {
        public string status { get; set; }
        public ResultadoGeocodificado[] results { get; set; }
    }
}
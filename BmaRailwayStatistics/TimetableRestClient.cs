using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BmaRailwayStatistics
{
    class TimetableRestClient
    {
        private readonly HttpClient client;

        public TimetableRestClient()
        {
            client = new HttpClient();
        }

        public async Task<string> GetTimetableBetweenEndpoints(string start, string end)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var url = $"";
            var response = await client.GetAsync(url);
            return Encoding.GetEncoding("windows-1250").GetString(await response.Content.ReadAsByteArrayAsync());
        }
    }
}

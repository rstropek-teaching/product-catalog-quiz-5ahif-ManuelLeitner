using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog {
    public class JsonContent<T> {

        private HttpContent Content { get; set; }

        public JsonContent(T obj) {
            Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public JsonContent(HttpContent content) {
            Content = content;
        }

        public async Task<T> GetObjectAsync() {
            return JsonConvert.DeserializeObject<T>(await Content.ReadAsStringAsync());
        }

        public static explicit operator JsonContent<T>(HttpContent content) {
            return new JsonContent<T>(content);
        }

        public static implicit operator HttpContent(JsonContent<T> jsonContent) {
            return jsonContent.Content;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace XF.AplicativoFIAP.Model {
    public static class ProfessorRepository {
        private static string apiURL = "http://apiaplicativofiap.azurewebsites.net";
        private static List<Professor> professoresSqlAzure;

        public static async Task<List<Professor>> GetProfessoresSqlAzureAsync() {
            if (professoresSqlAzure != null) return professoresSqlAzure;

            var httpRequest = new HttpClient();
            var stream = await httpRequest.GetStreamAsync($"{apiURL}/api/professors");

            var professorSerializer = new DataContractJsonSerializer(typeof(List<Professor>));
            professoresSqlAzure = (List<Professor>)professorSerializer.ReadObject(stream);

            return professoresSqlAzure;
        }

        public static async Task<bool> PostProfessorSqlAzureAsync(Professor profAdd) {
            if (profAdd == null) return false;

            var httpRequest = new HttpClient();
            httpRequest.BaseAddress = new Uri(apiURL);
            httpRequest.DefaultRequestHeaders.Accept.Clear();
            httpRequest.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string profJson = Newtonsoft.Json.JsonConvert.SerializeObject(profAdd);

            var response = await httpRequest.PostAsync("api/professors", new StringContent(profJson, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteProfessorSqlAzureAsync(string profId) {
            if (string.IsNullOrWhiteSpace(profId)) return false;

            var httpRequest = new HttpClient();
            httpRequest.BaseAddress = new Uri(apiURL);
            httpRequest.DefaultRequestHeaders.Accept.Clear();
            httpRequest.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpRequest.DeleteAsync(string.Format("api/professors/{0}", profId));

            return response.IsSuccessStatusCode;
        }
    }
}

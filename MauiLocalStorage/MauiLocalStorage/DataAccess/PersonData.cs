using SQLite;
using MauiLocalStorage.Models;
using Newtonsoft.Json;

namespace MauiLocalStorage.DataAccess
{
    public class PersonData
    {

        public async Task<List<Person>> GetPeopleAsync()
        {
            HttpClient client;

            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                List<Person> people = new List<Person>();

                var response = await client.GetAsync("http://localhost:24565/api/Person");
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    if (!String.IsNullOrEmpty(content))
                    {
                        people = JsonConvert.DeserializeObject<List<Person>>(content);
                    }
                }
                return people;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SavePersonAsync(Person person)
        {
            HttpClient client;

            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.TryAddWithoutValidation("Accpet", "application/json");

                var content = JsonConvert.SerializeObject(person);
                var buff = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buff);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PostAsync("http://localhost:24565/api/Person", byteContent).Result;
                return response.IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.API.Models;
using System.Net.Http;

namespace Phonebook.API.Services.Implementations
{
    public class ContactsApiService : IContactsApiService
    {
        private const int UsersCount = 1000;

        private readonly HttpClient _httpClient;

        public ContactsApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<ContactModel>> GetContacts()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Constants.BaseUrl}?results={UsersCount}"))
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var parseResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultModel<IEnumerable<ContactModel>>>(content);

                    return parseResult.Results;
                }
                else
                {
                    throw new HttpRequestException($"{responseMessage.StatusCode} {responseMessage.ReasonPhrase}");
                }
            }
        }
    }
}

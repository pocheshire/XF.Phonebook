using Newtonsoft.Json;

namespace Phonebook.API.Models
{
    public class ContactModel
    {
        [JsonProperty("picture")]
        public ImageModel Image { get; set; }

        public NameModel Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}

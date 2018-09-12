using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.API.Models;

namespace Phonebook.API.Services
{
    public interface IContactsApiService
    {
        Task<IEnumerable<ContactModel>> GetContacts();
    }
}

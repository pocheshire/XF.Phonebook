using System;
namespace Phonebook.API.Models
{
    public class ResultModel<T>
    {
        public T Results { get; set; }
    }
}

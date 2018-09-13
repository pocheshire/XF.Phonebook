using System.Text;

namespace Phonebook.API.Models
{
    public class NameModel
    {
        public string Title { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        private string GetFullName()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Title))
            {
                sb.Append(Title);
                sb.Append(" ");
            }
            if (!string.IsNullOrEmpty(First))
            {
                sb.Append(First);
                sb.Append(" ");
            }
            if (!string.IsNullOrEmpty(Last))
            {
                sb.Append(Last);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return GetFullName();
        }
    }
}
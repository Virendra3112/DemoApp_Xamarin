using System.Collections.Generic;

namespace DemoAppXamarin.Models
{
    public partial class UserData
    {
        public List<Person> People { get; set; }
    }

    public partial class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

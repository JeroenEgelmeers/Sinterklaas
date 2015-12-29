using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Person hasPerson { get; private set; }
        public Person cantHave { get; set; }

        public Person suggestedPerson { get; set; }

        public Person(string firstName, string lastName)
        {
            FirstName   = firstName;
            LastName    = lastName;

            hasPerson   = null;
            cantHave    = null;
            Console.WriteLine("\t" + firstName + " \t\t" + lastName);
        }

        public void setCantHavePerson(Person p)
        {
            cantHave = p;
            //Console.WriteLine(FirstName + " \t heeft zeker niet \t" + p.FirstName);
        }

        public void setHasPerson(Person p)
        {
            hasPerson = p;
            //Console.WriteLine(FirstName + " \t heeft zeker \t\t" + p.FirstName);
        }
    }
}

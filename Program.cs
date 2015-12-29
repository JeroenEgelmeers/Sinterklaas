using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> person = new List<Person>();

            Console.WriteLine("****************************************************");
            Console.WriteLine("*            Personen worden toegevoegd..          *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("\tVoornaam\tAchternaam");
            Console.WriteLine("____________________________________________________");
            person.Add(new Person("P1",   "")); // L
            person.Add(new Person("P2",   "")); // M
            person.Add(new Person("P3",   "")); // K
            person.Add(new Person("P4",   "")); // C
            person.Add(new Person("P5",   "")); // B
            person.Add(new Person("P6",   "")); // S
            person.Add(new Person("P7",   "")); // D

            Console.WriteLine("");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                Bepalingen defineren..            *");
            Console.WriteLine("****************************************************");
            
            // VUL IN: GEBRUIKERS DIE ELKAAR NIET KUNNEN HEBBEN:
            person[0].setCantHavePerson(person[5]);
            person[3].setCantHavePerson(person[6]);
            person[5].setCantHavePerson(person[0]);
            person[6].setCantHavePerson(person[3]);

            // VUL IN: GEBRUIKERS DIE JE ZEKER WEET:
            person[1].setHasPerson(person[3]);
            person[4].setHasPerson(person[6]);
            person[6].setHasPerson(person[1]);

            // VUL IN: GEBRUIKERS DIE JE VERMOED GOED TE HEBBEN: { Waarschuwing: Maakt kans op fouten groter! }
            person[0].suggestedPerson = person[4];
            person[2].suggestedPerson = person[0];

            int unpossibleTakes = 0;
            int surelytaken     = 0;
            foreach (Person p in person)
            {
                if (p.cantHave != null)     { unpossibleTakes++;    }
                if (p.hasPerson != null)    { surelytaken++;        }
            }

            Console.WriteLine("\tOnmogelijkheden gevonden:\t {0}", unpossibleTakes);
            Console.WriteLine("\t100% zeker goed: \t\t {0}", surelytaken);

            Console.WriteLine("");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*           ~ Lootjestrekken.nl berekenen ~        *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("\tBezig.. een moment geduld a.u.b.");

            List<Person> tempPerson_hasNobody = new List<Person>();
            List<Person> tempPerson_taken = new List<Person>();
            foreach(Person p in person)
            {
                if (p.hasPerson != null) { tempPerson_taken.Add(p.hasPerson);
                }else if (p.suggestedPerson != null) {
                    tempPerson_taken.Add(p.suggestedPerson);
                }else { tempPerson_hasNobody.Add(p); }
            }

            List<Person> tempPerson_available = new List<Person>();

            bool done = false;
            int possibleTrys = 0;
            int tryMax = 50;
            while (!done && possibleTrys < tryMax)
            {
                Thread.Sleep(250);
                possibleTrys++;
                Console.WriteLine("\tOptie {0} proberen...", possibleTrys);
                tempPerson_available = null;
                tempPerson_available = person.ToList();
                tempPerson_available.RemoveAll(item => tempPerson_taken.Contains(item));
                var rnd = new Random();
                var result = tempPerson_available.OrderBy(item => rnd.Next());
                tempPerson_available = result.ToList();

                // Those people have to be filled in.
                foreach (Person p in tempPerson_hasNobody)
                {
                    Person personTaken = null;
                    foreach (Person p1 in tempPerson_available)
                    {
                        if (p.cantHave != p1 && p.FirstName != p1.FirstName)
                        {
                            foreach (Person p2 in person)
                            {
                                if (p2.FirstName == p.FirstName)
                                {
                                    p2.suggestedPerson = p1;
                                    personTaken = p1;
                                }
                            }
                        }
                    }
                    if (personTaken != null) { tempPerson_available.Remove(personTaken); }
                }
                if (tempPerson_available.Count == 0) { done = true; }
            }

            if (possibleTrys == tryMax)
            {
                Console.WriteLine("");
                Console.WriteLine("****************************************************");
                Console.WriteLine("*                    ERROR GEVONDEN                *");
                Console.WriteLine("****************************************************");
                Console.WriteLine("");
                Console.WriteLine("\tEr is geen mogelijkheid gevonden. Probeer het opnieuw.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("\tGelukt! Lijst opmaken..");
                Thread.Sleep(250);
                Console.WriteLine("");
                Console.WriteLine("****************************************************");
                Console.WriteLine("*\t\t  Na {0} poginging(en):              *", possibleTrys);
                Console.WriteLine("****************************************************");

                foreach (Person p in person)
                {
                    string heeftPersoon = "onbekend";
                    bool suggestion = false;

                    if (p.hasPerson != null) { heeftPersoon = p.hasPerson.FirstName; }
                    else if (p.suggestedPerson != null) {
                        heeftPersoon = p.suggestedPerson.FirstName;
                        suggestion = true;
                    }

                    if (suggestion) { Console.WriteLine("\t{0} \theeft waarschijnlijk\t {1}", p.FirstName, heeftPersoon); }
                    else { Console.WriteLine("\t{0} \theeft\t\t\t {1}", p.FirstName, heeftPersoon); }
                }
            }

            while (true)
            {
                // Keep the system alive!
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace HashAlgorithm
{
    class HashTable
    {
        private readonly Person[] _table;
        private readonly int _size;
        public HashTable(int newSize)
        {
            this._size = newSize;
            this._table = new Person[_size];
        }

        public void AddPerson(Person p)
        {
            int hash = GenerateHash(p.GetLastName());
            bool searching = true;

            while (searching)
            {
                if (_table[hash] != null)
                {
                    hash += 1;
                    if (hash == this._size) {hash = 0;}
                    Console.WriteLine($"\nCollision occured, trying hash {hash}\n");
                }
                else
                {
                    _table[hash] = p;

                    searching = false;
                }
            }
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("\nFirst Name - Last Name - Age - Coolness");
            
            for (int i = 0; i < this._size; i++)
            {
                if (this._table[i] != null)
                {
                    string age = this._table[i].GetAge();

                    string firstName = this._table[i].GetFirstName();
                    string lastName = this._table[i].GetLastName();
                    string coolRating = this._table[i].GetCoolRating();

                    Console.WriteLine($"{i} {firstName} - {lastName} - {age} - {coolRating}");
                }
                else
                {
                    Console.WriteLine($"{i} - null");
                }
            }
        }
        public List<Person> RetrieveLastName(string s)
        {
            List<Person> result = new List<Person>();
            int hash = GenerateHash(s);

            if (this._table[hash] != null)
            {
                result.Add(this._table[hash]);
                bool searching = true;
                while (searching)
                {
                    if (this._table[hash].GetLastName() == this._table[hash + 1].GetLastName())
                    {
                        result.Add(this._table[hash + 1]);
                        hash += 1;
                    }
                    else
                    {
                        searching = false;
                    }
                    
                    if (this._table[hash + 1] == null) {searching = false;}
                }

                return result;
            }
            return null;
        }
        
        private int GenerateHash(string s)
        {
            var result = 0;
            for (var i = 0; i < s.Length; i++)
            {
                int x = s[i];
                int y = s[s.Length - (i + 1)]; //from end of index - same as s.Length - i
                result += x * y * i;
            }

            return result % this._size;
        }
    }

    class Person
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private float _coolRating;
        private readonly DateTime _dob;
        
        public Person(string newFirstName, string newLastName, string newDob, float newCoolRating)
        {
            this._firstName = newFirstName;
            this._lastName = newLastName;
            this._coolRating = newCoolRating;
            this._dob = Convert.ToDateTime(newDob);
        }

        public string GetFirstName(){return this._firstName;}
        public string GetLastName(){return this._lastName;}
        public string GetDOB(){return this._dob.ToLongDateString();}

        public string GetAge()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan ts = currentDate - this._dob;
            DateTime age = DateTime.MinValue.AddDays(ts.Days);
            return $"{age.Year - 1} Years, {age.Month - 1} Months, {age.Day - 1} Days";
        }

        public string GetCoolRating(){return this._coolRating.ToString("0.##");}
        
        public void SetCoolRating(float newCoolRating) {this._coolRating = newCoolRating;}
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HashTable h = new HashTable(100);
            
            Person one = new Person("George", "Waller", "2002, 11, 17", 5.2f);
            Person two = new Person("Richard", "Waller", "1972, 9, 7", 4.7f);
            Person three = new Person("Kieron", "Goh", "2003, 7, 14", 6.1f);
            
            //test identities from four onwards from: https://www.fakenamegenerator.com/
            Person four = new Person("Amelie", "Stone", "1995, 8, 26", 2.2f);
            Person five = new Person("Christopher", "Miller", "1953, 6, 26", 3.5f);
            Person six = new Person("Noah", "Hardy", "1990, 2, 13", 9.9f);
            Person seven = new Person("Lola", "Bryan", "1961, 9, 21", 3.6f);
            Person eight = new Person("Reece", "Hewitt", "1981, 9, 27", 2.6f);
            Person nine = new Person("Jamie", "Fontenot", "1991, 4, 30", 8.7f);
            Person ten = new Person("Karl", "Henke", "1996, 8, 18", 2.0f);
            Person eleven = new Person("Estrella", "Pringle", "2005, 11, 28", 8.2f);
            Person twelve = new Person("Edwin", "Largo", "2000, 3, 30", 8.0f);
            Person thirteen = new Person("Robert", "Cranor", "1997, 4, 21", 2.3f);
            Person fourteen = new Person("Guadalupe", "Alexander", "2002, 11, 25", 2.1f);
            Person fifteen = new Person("Fern", "Meier", "1995, 2, 1", 4.7f);
            
            
            h.AddPerson(one);
            h.AddPerson(two);
            h.AddPerson(three);
            h.AddPerson(four);
            h.AddPerson(five);
            h.AddPerson(six);
            h.AddPerson(seven);
            h.AddPerson(eight);
            h.AddPerson(nine);
            h.AddPerson(ten);
            h.AddPerson(eleven);
            h.AddPerson(twelve);
            h.AddPerson(thirteen);
            h.AddPerson(fourteen);
            h.AddPerson(fifteen);
            
            h.Show();

            List<Person> retrieval = h.RetrieveLastName("Waller");
            Console.WriteLine("\n\nRetrieval for 'Waller':");
            foreach (var p in retrieval)
            {
                Console.WriteLine($"{p.GetFirstName()} {p.GetLastName()} -- {p.GetAge()} - {p.GetCoolRating()}");
            }
        }
    }
}
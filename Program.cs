using System;
using Rhythym.Models;
using System.Linq;

namespace Rhythym
{
  class Program
  {
    static void Main(string[] args)
    {

      static void PopulateDatabase()
      {
        // check if there are any cohorts,
        var db = new DatabaseContext();
        if (!db.Bands.Any())
        {
          // if none then add a few
          db.Bands.Add(new Band
          {
            Name = "Khruangbin",
            CountryOfOrigin = "USA",
            NumberOfMembers = "3",
            Website = "www.khruangbin.com",
            Style = "Thai Funk",
            IsSigned = true,
            PersonOfContact = "Laura Lee",
            ContactPhoneNumber = "727-785-0299"

          });
          db.Bands.Add(new Band
          {
            Name = "Glass Animals",
            CountryOfOrigin = "UK",
            NumberOfMembers = "4",
            Website = "www.glassanimals.com",
            Style = "Psychedelia",
            IsSigned = true,
            PersonOfContact = "Dave Bayley",
            ContactPhoneNumber = "727-409-4246"
          });
          db.SaveChanges();
        }
        // if there are, then do nothing
      }
      Console.WriteLine("Welcome to Rhythm App");
      PopulateDatabase();


    }
  }
}

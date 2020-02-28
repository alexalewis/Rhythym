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

        var db = new DatabaseContext();

        if (!db.Bands.Any())
        {
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

      var isRunning = true;

      while (isRunning == true)
      {
        var db = new DatabaseContext();
        Console.WriteLine("Would you like to (SIGN), (PRODUCE), (UNSIGN), (RESIGN), (VIEW) a band or (QUIT)?");
        var input = Console.ReadLine().ToLower();

        var newBand = new Band();

        if (input == "sign")
        {
          Console.WriteLine("What is the name of the band you want to sign?");
          newBand.Name = Console.ReadLine().ToLower();

          Console.WriteLine("Where is the country of origin of your band?");
          newBand.CountryOfOrigin = Console.ReadLine().ToLower();

          Console.WriteLine("How many members does your band have?");
          newBand.NumberOfMembers = Console.ReadLine();

          Console.WriteLine("What is the bands website?");
          newBand.Website = Console.ReadLine().ToLower();

          Console.WriteLine("What is the style of the band?");
          newBand.Style = Console.ReadLine().ToLower();

          Console.WriteLine("Is your band signed? Type in (TRUE) for yes or (FALSE) for no.");
          newBand.IsSigned = bool.Parse(Console.ReadLine().ToLower());

          Console.WriteLine("Who is the person of contact for the band?");
          newBand.PersonOfContact = Console.ReadLine();

          Console.WriteLine("What is the phone number for the person of contact");
          newBand.ContactPhoneNumber = Console.ReadLine();

          db.Bands.Add(newBand);

          db.SaveChanges();

        }
        else if (input == "unsign")
        {
          Console.WriteLine("Which band do you want to unsign?");
          var band = Console.ReadLine().ToLower();

          var bandToUnsign = db.Bands.FirstOrDefault(b => b.Name == band);
          bandToUnsign.IsSigned = false;

          Console.WriteLine($"You have unsigned {band}.");
          db.SaveChanges();

        }
        else if (input == "produce")
        {
          Console.WriteLine("Do you want to (CREATE) a new album or (ADD) a song?");
          var produceInput = Console.ReadLine().ToLower();
          if (produceInput == "create")
          {
            var newAlbum = new Album();

            Console.WriteLine("Which band are you producing an album for? Choose by Id");
            newAlbum.BandId = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the name of the album you want to produce?");
            newAlbum.Title = Console.ReadLine().ToLower();

            Console.WriteLine("Is the album explicit? (TRUE) for yes, (FALSE) for no");
            newAlbum.IsExplicit = bool.Parse(Console.ReadLine().ToLower());

            newAlbum.ReleaseDate = DateTime.Now;

            db.Albums.Add(newAlbum);
            db.SaveChanges();

          }

          if (produceInput == "add")
          {
            var newSong = new Song();

            Console.WriteLine("Which album are you adding a song to? Choose by Id");
            newSong.AlbumId = int.Parse(Console.ReadLine());

            Console.WriteLine("What song do you want to add?");
            newSong.Title = Console.ReadLine().ToLower();

            Console.WriteLine("What is the length of the song?");
            newSong.Length = Console.ReadLine();

            Console.WriteLine("What is the genre of the song?");
            newSong.Genre = Console.ReadLine().ToLower();

            Console.WriteLine("What are the lyrics to the song?");
            newSong.Lyrics = Console.ReadLine().ToLower();

            db.Songs.Add(newSong);
            db.SaveChanges();
          }

        }
        else if (input == "resign")
        {
          Console.WriteLine("Which band do you want to resign?");
          var band = Console.ReadLine().ToLower();

          var bandToResign = db.Bands.FirstOrDefault(b => b.Name == band);
          bandToResign.IsSigned = true;

          Console.WriteLine($"You have resigned {band}.");
          db.SaveChanges();
        }
        else if (input == "view")
        {
          // view options 
        }
        else if (input == "quit")
        {
          isRunning = false;
        }
      }

    }
  }
}

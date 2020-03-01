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
            Name = "khruangbin",
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
            Name = "glass animals",
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
        Console.WriteLine("Would you like to (SIGN), (PRODUCE), (UNSIGN), (RESIGN), (VIEW) or (QUIT)?");
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
          var viewBands = db.Bands.OrderBy(b => b.IsSigned == true);
          foreach (var seeBand in viewBands)
          {
            Console.WriteLine($"{seeBand.Name}");
          }
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

            var viewBands = db.Bands.OrderBy(b => b.Id);
            foreach (var seeBand in viewBands)
            {
              Console.WriteLine($" {seeBand.Id} : {seeBand.Name}");
            }

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

            var viewAlbums = db.Albums.OrderBy(a => a.Id);
            foreach (var seeAlbum in viewAlbums)
            {
              Console.WriteLine($" {seeAlbum.Id} : {seeAlbum.Title}");
            }

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
          var viewBands = db.Bands.OrderBy(b => b.IsSigned == false);
          foreach (var seeBand in viewBands)
          {
            Console.WriteLine($"{seeBand.Name}");
          }
          Console.WriteLine("Which band do you want to resign?");
          var band = Console.ReadLine().ToLower();

          var bandToResign = db.Bands.FirstOrDefault(b => b.Name == band);
          bandToResign.IsSigned = true;

          Console.WriteLine($"You have resigned {band}.");
          db.SaveChanges();
        }
        else if (input == "view")
        {
          Console.WriteLine("Do you want to view all (ALBUMS)for a band, (ORDER) by release date, (SIGNED) bands, (NOT) signed bands, or (SONGS) from an album?");
          var viewInput = Console.ReadLine().ToLower();

          if (viewInput == "albums")
          {

            Console.WriteLine("Which band do you want to see albums for?");
            var bandInput = Console.ReadLine().ToLower();

            var viewBand = db.Bands.FirstOrDefault(b => b.Name == bandInput);
            var viewAlbums = db.Albums.Where(a => a.BandId == viewBand.Id);

            foreach (var album in viewAlbums)
            {
              Console.WriteLine($"{album.Title} are the albums for {viewBand.Name}.");
            }
          }
          else if (viewInput == "order")
          {
            var orderedAlbums = db.Albums.OrderBy(a => a.ReleaseDate);
            foreach (var order in orderedAlbums)
            {
              Console.WriteLine($"{order.Title} was released {order.ReleaseDate}");
            }
          }
          else if (viewInput == "signed")
          {
            var signedBands = db.Bands.Where(b => b.IsSigned == true);
            foreach (var signed in signedBands)
            {
              Console.WriteLine($"{signed.Name} is signed");
            }
          }
          else if (viewInput == "not")
          {
            var notSignedBands = db.Bands.Where(b => b.IsSigned == false);
            foreach (var notSigned in notSignedBands)
            {
              Console.WriteLine($"{notSigned.Name} is not signed");
            }
          }
          else if (viewInput == "songs")
          {

            var albums = db.Albums.OrderBy(a => a.Id);
            foreach (var seeAlbums in albums)
            {
              Console.WriteLine($"{seeAlbums.Title}");
            }
            Console.WriteLine("Which album do you want to see songs for?");
            var songs = Console.ReadLine().ToLower();

            var viewAlbums = db.Albums.FirstOrDefault(a => a.Title == songs);
            var viewSongs = db.Songs.Where(s => s.AlbumId == viewAlbums.Id);

            Console.WriteLine($"The songs in {viewAlbums.Title} : ");

            foreach (var song in viewSongs)
            {
              Console.WriteLine($"{song.Title}");
            }

          }

        }
        else if (input == "quit")
        {
          isRunning = false;
        }
      }

    }
  }
}

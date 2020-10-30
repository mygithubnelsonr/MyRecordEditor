using DbRecordEditor.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DbRecordEditor.BLL
{
    public class DataGetSet
    {
        public static List<Genre> GetGenres()
        {
            List<Genre> list = new List<Genre>();
            using (var context = new MyJukeboxEntities())
            {
                var genres = context.tGenres
                            .OrderBy(g => g.ID)
                            .Select(g => g)
                            .ToList();

                foreach (var g in genres)
                {
                    list.Add(new Genre { ID = g.ID, Name = g.Name });
                }

                return list;
            }
        }

        public static string GetGenreFromID(int id)
        {
            using (var context = new MyJukeboxEntities())
            {
                var genres = context.tGenres
                            .FirstOrDefault(g => g.ID == id);

                return genres.Name;
            }
        }

        public static List<Catalog> GetCatalogs()
        {
            List<Catalog> list = new List<Catalog>();
            using (var context = new MyJukeboxEntities())
            {
                var catalogs = context.tCatalogs
                            .OrderBy(c => c.ID)
                            .Select(c => c)
                            .ToList();

                foreach (var c in catalogs)
                {
                    list.Add(new Catalog { ID = c.ID, Name = c.Name });
                }

                return list;
            }
        }

        internal static MP3Record GetRecord(int id)
        {
            MP3Record record = new MP3Record();

            try
            {
                var context = new MyJukeboxEntities();
                var song = context.tSongs
                    .Where(s => s.ID == id)
                    .Select(s => s)
                    .ToList();

                if (song.Count == 0)
                    throw new Exception($"Record with ID {id} not found!");

                record.Album = song[0].Album;
                record.Artist = song[0].Artist;
                record.Titel = song[0].Titel;
                record.Path = song[0].Pfad;
                record.FileName = song[0].FileName;
                record.ID_Genre = (int)song[0].ID_Genre;
                record.ID_Catalog = (int)song[0].ID_Catalog;

                return record;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return null;
            }
        }

        public static bool EditSaveSongChanges(int id, MP3Record record)
        {
            try
            {
                var context = new MyJukeboxEntities();
                var songs = context.tSongs.Find(id);

                if (songs.ID_Genre != (int)record.ID_Genre)
                    songs.ID_Genre = (int)record.ID_Genre;

                if (songs.ID_Catalog != (int)record.ID_Catalog)
                    songs.ID_Catalog = (int)record.ID_Catalog;

                if (songs.Album != record.Album) songs.Album = record.Album;
                if (songs.Artist != record.Artist) songs.Artist = record.Artist;
                if (songs.Titel != record.Titel) songs.Titel = record.Titel;
                if (songs.Pfad != record.Path) songs.Pfad = record.Path;
                if (songs.FileName != record.FileName) songs.FileName = record.FileName;

                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int GetGenreFromString(string genre)
        {
            List<int> genres;
            var context = new MyJukeboxEntities();
            genres = context.tGenres
                        .Where(g => g.Name == genre)
                        .Select(g => g.ID).ToList();

            return genres[0];
        }

    }
}

using System;

namespace DbRecordEditor.BLL
{
    public class MP3Record
    {
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Titel { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int ID_Genre { get; set; }
        public int ID_Catalog { get; set; }
        public int FileSize { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime FileDate { get; set; }
        public string MD5 { get; set; }
        public int Played { get; set; }
        public int Rating { get; set; }
        public string Beat { get; set; }
        public string Comment { get; set; }
        public int Media { get; set; }
        public bool IsSample { get; set; }
        public bool Error { get; set; }
        public bool Hide { get; set; }

        public void Initialize()
        {
            Album = ""; Artist = ""; Titel = "";
            Path = ""; FileName = ""; ID_Genre = -1;
            ID_Catalog = -1; FileSize = -1; MD5 = "";
            Played = -1; Rating = -1;
            Beat = ""; Comment = ""; Media = -1;
            IsSample = false; Error = false; Hide = false;
        }
    }

}

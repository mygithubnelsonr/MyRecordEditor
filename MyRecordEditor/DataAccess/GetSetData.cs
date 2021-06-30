using Dapper;
using MyRecordEditor.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MyRecordEditor.DataAccess
{
    public class GetSetData
    {
        public static List<tGenresModel> GetGenres()
        {
            string connection = ConnectionTools.GetConnectionString("MyJukebox");

            using (IDbConnection conn = new SqlConnection(connection))
            {
                string sql = $"select * from tGenres";
                var result = conn.Query<tGenresModel>(sql).ToList();
                return result;
            }
        }

        public static List<tCatalogsModel> GetCatalogs()
        {
            string connection = ConnectionTools.GetConnectionString("MyJukebox");

            using (IDbConnection conn = new SqlConnection(connection))
            {
                string sql = $"select * from tCatalogs";
                var result = conn.Query<tCatalogsModel>(sql).ToList();
                return result;
            }
        }

        public static List<tMediasModel> GetMedias()
        {
            string connection = ConnectionTools.GetConnectionString("MyJukebox");

            using (IDbConnection conn = new SqlConnection(connection))
            {
                string sql = $"select * from tMedia";
                var result = conn.Query<tMediasModel>(sql).ToList();
                return result;
            }
        }

        //public static List<tSongsModel> GetRecord(int id)
        //{
        //    string connection = ConnectionTools.GetConnectionString("MyJukebox");

        //    using (IDbConnection conn = new SqlConnection(connection))
        //    {
        //        string sql = $"select * from tSongs where id={id}";
        //        var result = conn.Query<tSongsModel>(sql).ToList();
        //        return result;
        //    }
        //}

        public static tSongsModel GetSongModel(int id)
        {
            string connection = ConnectionTools.GetConnectionString("MyJukebox");

            using (IDbConnection conn = new SqlConnection(connection))
            {
                string sql = $"select * from tSongs where id={id}";
                tSongsModel record = conn.QuerySingle<tSongsModel>(sql);
                return record;
            }
        }

        public static bool SaveChanges(tSongsModel record)
        {
            string connection = ConnectionTools.GetConnectionString("MyJukebox");

            using (IDbConnection conn = new SqlConnection(connection))
            {
                var p = new DynamicParameters();

                p.Add("@ID", record.ID);
                p.Add("@ID_Genre", record.ID_Genre);
                p.Add("@ID_Catalog", record.ID_Catalog);
                p.Add("@ID_Media", record.ID_Media);
                p.Add("@Album", record.Album);
                p.Add("@Artist", record.Artist);
                p.Add("@Title", record.Title);
                p.Add("@Path", record.Path);
                p.Add("@FileName", record.FileName);

                string sql = "dbo.spMyEditor_UpdateTitle";
                var result = conn.Execute(sql, p, commandType: CommandType.StoredProcedure);

                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}

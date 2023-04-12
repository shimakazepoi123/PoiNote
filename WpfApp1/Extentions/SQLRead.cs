using System.Data.SQLite;
using System.Collections;


namespace WpfApp1.Extentions
{
    class SQLRead
    {
        public SQLRead() { }
        public SQLRead(string path) { filePath = path; }

        protected string filePath { get; set; }
        /*追加表列*/

        private void SQL_NoneSearch(string command_string)
        {
            string source = string.Format(@"Data Source={0}", filePath);
            using (var connection = new SQLiteConnection(source))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format(@"{0}", command_string);
                command.ExecuteReader();
            }

        }

        public IEnumerable SQL_Search(string command_string, string mode = null)
        {
            string source = string.Format(@"Data Source={0}", filePath);
            using (var connection = new SQLiteConnection(source))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = string.Format(@"{0}", command_string);
                if (mode == "read")
                {
                    using (var read = command.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            yield return read;
                        }
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                }
                connection.Clone();
            }
        }


        public void SQL_Create()
        {
            /*创建表命令*/
            string create = "CREATE TABLE Wenxian(" +
                "ID TEXT," +
                "Type TEXT," +
                "Title TEXT," +
                "Author TEXT," +
                "Jounral TEXT," +
                "Year TEXT," +
                "Period TEXT," +
                "Page TEXT," +
                "Volume TEXT," +
                "Tag TEXT," +
                "Keyword TEXT," +
                "DOI TEXT," +
                "FilePath TEXT" +
                ")" +
                ";";
            SQL_NoneSearch(create);
        }

        public void SQL_Other(string commond_string)
        {
            SQL_NoneSearch(commond_string);
        }

        public void SQL_CreatDatabase(string filename)
        {
            SQLiteConnection.CreateFile(filename);
        }
    }
}

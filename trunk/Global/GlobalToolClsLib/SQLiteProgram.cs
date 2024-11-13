using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{

    public class SQLData
    {
        public object Data { get; set; }

        public SQLDataType type { get; set; }


        public SQLData(object Data, SQLDataType type)
        {
            this.Data = Data;
            this.type = type;
        }

    }

    public enum SQLDataType
    {
        INT,
        FLOAT,
        STRING,
    }

    public class SQLiteProgram
    {

        private static readonly object _lockObj = new object();
        private static volatile SQLiteProgram _instance = null;
        public static SQLiteProgram Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SQLiteProgram();
                        }
                    }
                }
                return _instance;
            }
        }

        SQLiteConnection m_dbConnection;

        public bool Init()
        {
            try
            {
                string DatabasePath = "D:/GuWell/Logs/SQLiteData/ColdWeldDatabase.db";
                CreateNewDatabase(DatabasePath);
                connectToDatabase(DatabasePath);

                string tablename = "VacuumsData";
                Dictionary<string, SQLData> tableDictionarys = new Dictionary<string, SQLData>
            {
                { "Date", new SQLData("", SQLDataType.STRING) },
                { "Time", new SQLData("", SQLDataType.STRING) },
                { "Vacuum1", new SQLData(0, SQLDataType.FLOAT) },
                { "Vacuum2", new SQLData(0, SQLDataType.FLOAT) },
                { "Vacuum3", new SQLData(0, SQLDataType.FLOAT) },
            };
                createTable(tablename, tableDictionarys);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建一个空的数据库
        /// </summary>
        /// <param name="DatabasePath"></param>
        /// <returns></returns>
        public bool CreateNewDatabase(string DatabasePath)
        {
            try
            {
                if (File.Exists(DatabasePath))
                {
                    Console.WriteLine("数据库已存在，未进行覆盖。");

                }
                else
                {
                    // 创建数据库  
                    SQLiteConnection.CreateFile(DatabasePath);
                    
                }
                return true;

            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 创建一个连接到指定数据库
        /// </summary>
        /// <param name="DatabasePath"></param>
        public bool connectToDatabase(string DatabasePath)
        {
            try
            {
                m_dbConnection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;");
                m_dbConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }




        /// <summary>
        /// 在指定数据库中创建一个table
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="tableDictionarys"></param>
        /// <returns></returns>
        public bool createTable(string tablename, Dictionary<string, SQLData> tableDictionarys)
        {
            try
            {
                if(m_dbConnection.State == System.Data.ConnectionState.Open)
                {
                    string sql = $"CREATE TABLE IF NOT EXISTS {tablename} (Id INTEGER PRIMARY KEY AUTOINCREMENT";
                    foreach (var kvp in tableDictionarys)
                    {
                        if(kvp.Value.type == SQLDataType.STRING)
                        {
                            sql += $", {kvp.Key} varchar(20)";
                        }
                        else if (kvp.Value.type == SQLDataType.INT)
                        {
                            sql += $", {kvp.Key} int";
                        }
                        else if (kvp.Value.type == SQLDataType.FLOAT)
                        {
                            sql += $", {kvp.Key} float";
                        }
                        
                    }
                    sql += ")";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


        /// <summary>
        /// 在指定的table中添加数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="tableDictionarys"></param>
        /// <returns></returns>
        public bool AddData(string tablename, Dictionary<string, SQLData> tableDictionarys, int datasize = 512000)
        {
            try
            {
                
                if (m_dbConnection.State == System.Data.ConnectionState.Open)
                {
                    string insertQuery = $"INSERT INTO {tablename} ({tableDictionarys.Keys.First()}";
                    int i = 0;
                    foreach (var kvp in tableDictionarys)
                    {
                        if(i > 0)
                        {
                            insertQuery += $", {kvp.Key}";
                        }
                        i++;

                    }
                    insertQuery += $") VALUES (@{tableDictionarys.Keys.First()}";
                    i = 0;
                    foreach (var kvp1 in tableDictionarys)
                    {
                        if (i > 0)
                        {
                            insertQuery += $", @{kvp1.Key}";
                        }

                        i++;
                    }
                    insertQuery += $");";

                    using (var insertCommand = new SQLiteCommand(insertQuery, m_dbConnection))
                    {
                        // 检查记录数量  
                        string countQuery = $"SELECT COUNT(*) FROM {tablename};";
                        using (var countCommand = new SQLiteCommand(countQuery, m_dbConnection))
                        {
                            int count = Convert.ToInt32(countCommand.ExecuteScalar());
                            if (count >= datasize) // 假设限制为512000条记录 大概100MB 355天的数据  
                            {
                                // 删除最早的记录  
                                string deleteQuery = $"DELETE FROM {tablename} WHERE Id = (SELECT MIN(Id) FROM {tablename});";
                                using (var deleteCommand = new SQLiteCommand(deleteQuery, m_dbConnection))
                                {
                                    deleteCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        foreach (var kvp in tableDictionarys)
                        {
                            insertQuery += $", {kvp.Key}";
                            insertCommand.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value.Data);

                        }
                        insertCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 读取指定的table
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="tableDictionarys"></param>
        /// <returns></returns>
        public bool ReadData(string tablename, ref List<Dictionary<string, SQLData>> tableDictionarys)
        {
            try
            {
                tableDictionarys = new List<Dictionary<string, SQLData>>();

                if (m_dbConnection.State == System.Data.ConnectionState.Open)
                {
                    Dictionary<string, SQLData> tableDictionary_type = new Dictionary<string, SQLData>();

                    int L = 0;

                    string sql = $"PRAGMA table_info({tablename})";

                    using (var command = new SQLiteCommand(sql, m_dbConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string columnName = reader.GetString(1); // 列名  
                                string dataType = reader.GetString(2); // 数据类型  
                                int isPrimaryKey = reader.GetInt32(5); // 是否为主键（0或1）  

                                if (isPrimaryKey == 0)
                                {
                                    if (dataType == "varchar(20)")
                                    {
                                        tableDictionary_type.Add(columnName, new SQLData("", SQLDataType.STRING));
                                    }
                                    else if (dataType == "int")
                                    {
                                        tableDictionary_type.Add(columnName, new SQLData(0, SQLDataType.INT));
                                    }
                                    else if (dataType == "float")
                                    {
                                        tableDictionary_type.Add(columnName, new SQLData(0, SQLDataType.FLOAT));
                                    }

                                }
                            }
                        }
                    }

                    sql = $"SELECT * FROM {tablename}";
                    

                    using (var command = new SQLiteCommand(sql, m_dbConnection))
                    {
                        // 执行查询并读取数据  
                        using (var reader = command.ExecuteReader())
                        {
                            // 遍历结果集  
                            while (reader.Read())
                            {
                                Dictionary<string, SQLData> tableDictionary_data = new Dictionary<string, SQLData>();
                                int i = 1;
                                foreach(var kvp in tableDictionary_type)
                                {
                                    if(kvp.Value.type == SQLDataType.STRING)
                                    {
                                        tableDictionary_data.Add(kvp.Key, new SQLData(reader.GetString(i), kvp.Value.type));
                                    }
                                    else if(kvp.Value.type == SQLDataType.INT)
                                    {
                                        tableDictionary_data.Add(kvp.Key, new SQLData(reader.GetInt32(i), kvp.Value.type));
                                    }
                                    else if (kvp.Value.type == SQLDataType.FLOAT)
                                    {
                                        tableDictionary_data.Add(kvp.Key, new SQLData(reader.GetFloat(i), kvp.Value.type));
                                    }

                                    i++;

                                }

                                tableDictionarys.Add(tableDictionary_data);


                            }
                        }
                    }
                }
                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        //插入一些数据
        public void fillTable()
        {


            string tablename = "VacuumsData";



            Random random = new Random();

            // 设置上下限  
            double lowerLimit = 0.0001;
            double upperLimit = 10000;
            float vacuum1 = 100000;
            float vacuum2 = 100000;
            float vacuum3 = 100000;


            int i = 0;
            while(i < 0)
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                double randomValue = random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
                vacuum1 = (float)randomValue;
                randomValue = random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
                vacuum2 = (float)randomValue;
                randomValue = random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
                vacuum3 = (float)randomValue;

                Dictionary<string, SQLData> tableDictionarys_0 = new Dictionary<string, SQLData>
                {
                    { "Date", new SQLData(currentDate, SQLDataType.STRING) },
                    { "Time", new SQLData(currentTime, SQLDataType.STRING) },
                    { "Vacuum1", new SQLData(vacuum1, SQLDataType.FLOAT) },
                    { "Vacuum2", new SQLData(vacuum2, SQLDataType.FLOAT) },
                    { "Vacuum3", new SQLData(vacuum3, SQLDataType.FLOAT) },
                };

                AddData(tablename, tableDictionarys_0);

                i++;

                Thread.Sleep(5);
            }

            List<Dictionary<string, SQLData>> N = new List<Dictionary<string, SQLData>>();

            ReadData(tablename, ref N);
        }




        //使用sql查询语句，并显示结果
        void printHighscores()
        {
            string sql = "select * from Vacuums order by Vacuum1 desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Date: " + reader["Date"] + "\tTime: " + reader["Time"] + "\tV1: " + reader["Vacuum1"] + "\tV2: " + reader["Vacuum2"] + "\tV3: " + reader["Vacuum3"]);
            Console.ReadLine();
        }


    }
}

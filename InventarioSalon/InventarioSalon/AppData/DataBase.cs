using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSalon.AppData
{
    internal class DataBase
    {
        private Connection connection = Connection.Instance;

        public DataTable RunQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = this.connection.ConnectionDB())
                {
                    if (connection.State != ConnectionState.Open) { connection.Open(); }

                    var command = new MySqlCommand(query, connection);
                    command.CommandTimeout = 180;
                    var adapter = new MySqlDataAdapter(command);
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        public DataTable RunQuery(MySqlCommand command)
        {
            try
            {
                using (MySqlConnection connection = this.connection.ConnectionDB())
                {
                    if (connection.State != ConnectionState.Open) { connection.Open(); }
                    command.Connection = connection;
                    command.CommandTimeout = 180;
                    var adapter = new MySqlDataAdapter(command);
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
            }
            catch (Exception ) { throw; }
        }

        public void ExecuteQuery(MySqlCommand command)
        {
            try
            {
                using (MySqlConnection connection = this.connection.ConnectionDB())
                {
                    if (connection.State != ConnectionState.Open) { connection.Open(); }
                    command.Connection = connection;
                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception) { throw; }
        }

        public dynamic RunQueryForSingleValue(string query, dynamic defaultValue)
        {
            try
            {
                using (MySqlConnection connection = this.connection.ConnectionDB())
                {
                    if (connection.State != ConnectionState.Open) { connection.Open(); }

                    var command = new MySqlCommand(query, connection);
                    command.CommandTimeout = 180;
                    var adapter = new MySqlDataAdapter(command);
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    var table = dataSet.Tables[0];

                    dynamic result;
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        result = row[0];
                    }
                    else
                    {
                        result = default;
                    }

                    return result;
                }
            }
            catch (Exception) { throw; }
        }

        public dynamic RunQueryForSingleValue(MySqlCommand command, dynamic defaultValue)
        {
            try
            {
                using (MySqlConnection connection = this.connection.ConnectionDB())
                {
                    if (connection.State != ConnectionState.Open) { connection.Open(); }
                    command.Connection = connection;
                    command.CommandTimeout = 180;
                    var adapter = new MySqlDataAdapter(command);
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    var table = dataSet.Tables[0];

                    dynamic result;
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        result = row[0];
                    }
                    else
                    {
                        result = defaultValue;
                    }

                    return result;
                }
            }
            catch (Exception) { throw; }
        }

        //TRANSACCIONES DE BASES DE LA BASE DE DATOS

        public MySqlTransaction DBTransaction { get; set; }
        public MySqlConnection TransConnection { get; set; }

        public void PrepareForTransaction()
        {
            TransConnection = this.connection.ConnectionDB();
            DBTransaction = TransConnection.BeginTransaction();
        }

        public DataTable RunTransactionQuery(string query)
        {
            try
            {
                DataTable table = new DataTable();
                MySqlCommand command = new MySqlCommand(query, TransConnection);
                command.CommandTimeout = 180;
                command.Transaction = DBTransaction;
                MySqlDataAdapter adapter = new MySqlDataAdapter (command);
                adapter.Fill(table);
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable RunTransactionQuery(MySqlCommand command)
        {
            try
            {
                DataTable table = new DataTable();
                command.Connection = TransConnection;
                command.CommandTimeout = 180;
                command.Transaction = DBTransaction;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic RunTransactionQueryForSingleValue(string query, dynamic defaultValue)
        {
            try
            {
                DataTable table = new DataTable();
                MySqlCommand command = new MySqlCommand(query, TransConnection);
                command.CommandTimeout = 180;
                command.Transaction = DBTransaction;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);

                dynamic result;
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    result = row[0];
                }
                else
                {
                    result = defaultValue;
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic RunTransactionQueryForSingleValue(MySqlCommand command, dynamic defaultValue)
        {
            try
            {
                DataTable table = new DataTable();
                command.Connection = TransConnection;
                command.CommandTimeout = 180;
                command.Transaction = DBTransaction;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);

                dynamic result;
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    result = row[0];
                }
                else
                {
                    result = defaultValue;
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EndTransaction()
        {
            DBTransaction.Commit();
            if (TransConnection.State == ConnectionState.Open)
                TransConnection.Close();
        }
        public void RollBackTransaction()
        {
            DBTransaction.Rollback();
            if (TransConnection.State == ConnectionState.Open)
                TransConnection.Close();
        }

    }
}

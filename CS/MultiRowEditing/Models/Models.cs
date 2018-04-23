using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace MultiRowEditing.Models {
    public class ConnectionRepository {
        public static OleDbConnection GetDataConnection() {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", HttpContext.Current.Server.MapPath("~/App_Data/data.mdb"));
            return connection;
        }
    }
    public static class ProductRepository {
        const string updateCommandTemplate = "UPDATE [Products] SET {0} WHERE [ProductID] = {1}";
        const string fieldValueString = "[{0}] = '{1}'";
        const string fieldValueInt = "[{0}] = {1}";
        public static DataTable GetProducts() {
            DataTable dataTableProducts = new DataTable();
            using (OleDbConnection connection = ConnectionRepository.GetDataConnection()) {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
                adapter.SelectCommand.CommandText = "SELECT [ProductID], [ProductName], [CategoryID] FROM [Products]";
                adapter.Fill(dataTableProducts);
            }
            return dataTableProducts;
        }
        public static bool UpdateValues(Dictionary<string, object> changedValues) {
            bool updateResult = true;
            try {
                foreach (string row in changedValues.Keys) {
                    string fieldValues = string.Empty;
                    Dictionary<string, object> fields = changedValues[row] as Dictionary<string, object>;
                    bool firstItemPresent = false;
                    foreach (string field in fields.Keys) {
                        fieldValues += ((firstItemPresent ? ", " : string.Empty) + string.Format(GetTemplateByFieldName(field), field, fields[field]));
                        if (!firstItemPresent)
                            firstItemPresent = true;
                    }
                    string commandText = string.Format(updateCommandTemplate, fieldValues, row);
                    UpdateValue(commandText);
                }
            }
            catch {
                updateResult = false;
            }
            return updateResult;
        }
        private static void UpdateValue(string commandText) {
            using (OleDbConnection connection = ConnectionRepository.GetDataConnection()) {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(commandText, connection);
                cmd.ExecuteNonQuery();
            }
        }
        private static string GetTemplateByFieldName(string fieldName) {
            string res = string.Empty;
            switch (fieldName) {
                case "ProductName": {
                        res = fieldValueString;
                        break;
                    }
                case "CategoryID": {
                        res = fieldValueInt;
                        break;
                    }
            }
            return res;
        }
    }
    public class CategoryRepository {
        public static DataTable GetCategories() {
            DataTable dataTableCategories = new DataTable();
            using (OleDbConnection connection = ConnectionRepository.GetDataConnection()) {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
                adapter.SelectCommand.CommandText = "SELECT [CategoryID], [CategoryName] FROM [Categories]";
                adapter.Fill(dataTableCategories);
            }
            return dataTableCategories;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


    public class Position: Connection
    {
        #region attributes
        private string _id;
        private string _description;     
        #endregion

        #region Properties

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion

        #region Constructors

        public Position()
        {
            _id = "";
            _description = "";
        }

        public Position(string id)
        {
            string query = "select pos_id, pos_description from position where pos_id = @ID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.Add(new SqlParameter("@ID",id));
            DataRow row = GetFirstRow(command);
            if(row != null)
            {
                _id = id;
                _description = row["pos_description"].ToString();
            }
            else
            {
                _id = "";
                _description = "";
            }
        }
        #endregion
    }


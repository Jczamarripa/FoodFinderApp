using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

public class Player : Connection
{
    #region attributes
    private int _id;
    private DateTime _dateOfBirth;
    private string _firstName, _lastName;
    private double _heightInInches, _weightInPounds;
    private byte[] _picture;
    private string _position;

    #endregion

    #region Properties
    public int Age
    {

        get
        {
            int Edad = DateTime.Today.Year - _dateOfBirth.Year;
            if (DateTime.Today.DayOfYear < _dateOfBirth.DayOfYear)
                Edad = Edad - 1; 

            return Edad;
        }
    }

    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string FullName
    {
        get { return _lastName + ", " + _firstName; }
    }

    public string HeightInFeet
    {
        get
        {
            int feet, feetTwo = 0;
            feet = (int)_heightInInches / 12;
            feetTwo = (int)_heightInInches % 12;
            return feet + "'" + feetTwo + "\"";
        }
    }

    public double HeightInInches
    {
        get { return _heightInInches; }
        set { _heightInInches = value; }
    }

    public string HeightInMeters
    {
        get
        {
            double meters = 0;
            meters = _heightInInches / 39.370;
            return meters.ToString("n2");
        }
    }

    public int Id
    {
        get { return _id; }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public double WeightInKilograms
    {
        get
        {
            double kilograms = 0;
            kilograms = _weightInPounds / 2.2046;
            return kilograms;
        }
    }

    public double WeightInPounds
    {
        get { return _weightInPounds; }
        set { _weightInPounds = value; }
    } 

    public BitmapImage Picture
    {
        get
        {
            return ImageConverter.ByteToBitmapImage(_picture);
        }
    }

    public string PictureUri
    {
        set { _picture = ImageConverter.FileToByte(value); }
    }

    public string Position
    {
        get { return _position; }
        set { _position = value; }
    }
    #endregion

    #region Constructors
    public Player()
    {
        _dateOfBirth = DateTime.MinValue;
        _firstName = "";
        _lastName = "";
        _heightInInches = 0;
        _id = 0;
        _weightInPounds = 0;
        _position = "";
    }
    public Player(int id)
    {
        //query
        string query = @"select pla_date_of_birth,pla_first_name,
                        pla_last_name,pla_image,pla_height_inches,
                        pla_weight_pounds,pos_description from players,position where pla_id=@ID and pla_id_position=pos_id";
        //sql command
        SqlCommand command = new SqlCommand(query);
        command.Parameters.Add(new SqlParameter("@ID", id));
        //execute command
        DataRow row = GetFirstRow(command);
        //check if row has data
        if (row != null)
        {
            //pass field values to class attributes
            _dateOfBirth = (DateTime)row["pla_date_of_birth"];
            _firstName = row["pla_first_name"].ToString();
            _lastName = row["pla_last_name"].ToString();
            _heightInInches = (int)row["pla_height_inches"];
            _id = id;
            _weightInPounds = (int)row["pla_weight_pounds"];
            _picture = (byte[])row["pla_image"];
            _position = row["pos_description"].ToString();
        }
        else
        {
            throw new RecordNotFoundException();
        }
    }
    #endregion

    #region Methods
    public bool Add(string idTeam, string idPosition)
    {
        
        //insert
        string insert = @"insert into players (pla_first_name, pla_last_name,
                        pla_date_of_birth, pla_height_inches, pla_weight_pounds,
                        pla_image,pla_team_id,pla_id_position)
                        values(@NAME,@LNAME,@DOB,@PHIN,@PWPO,@PPIC,@PTID,@PIP);";
        //command
        SqlCommand command = new SqlCommand(insert);
        //parameters
        
        command.Parameters.Add(new SqlParameter("@NAME", _firstName));
        command.Parameters.Add(new SqlParameter("@LNAME", _lastName));
        command.Parameters.Add(new SqlParameter("@DOB", _dateOfBirth));
        command.Parameters.Add(new SqlParameter("@PHIN", _heightInInches));
        command.Parameters.Add(new SqlParameter("@PWPO", _weightInPounds));
        command.Parameters.Add(new SqlParameter("@PPIC",_picture));
        command.Parameters.Add(new SqlParameter("@PTID", idTeam));
        command.Parameters.Add(new SqlParameter("@PIP", idPosition));

        //execute command
        return ExecuteNonQuery(command);
    }


    public override string ToString()
    {
        return _firstName + " " + _lastName + " " + _dateOfBirth + " " + _heightInInches + " " + _weightInPounds + " " + _position;
    }
    /* public bool Delete()
     {
         //delete
         string delete = @"delete from players where pla_id=@ID;";
         //command
         SqlCommand command = new SqlCommand(delete);
         //parameters
         command.Parameters.Add(new SqlParameter("@ID", _id));
         //execute command
         return ExecuteNonQuery(command);
     }
     public bool Update()
     {
         //update
         string update = @"update players 
                 set pla_first_name=@NAME, pla_last_name=@LNAME, pla_date_of_birth=@DOB, pla_height_inches=@PHIN, pla_weight_pounds=@PWPO
                 where pla_id=@ID";
         //command
         SqlCommand command = new SqlCommand(update);

         //parameters
         command.Parameters.Add(new SqlParameter("@ID", _id));
         command.Parameters.Add(new SqlParameter("@NAME", _firstName));
         command.Parameters.Add(new SqlParameter("@LNAME", _lastName));
         command.Parameters.Add(new SqlParameter("@DOB", _dateOfBirth));
         command.Parameters.Add(new SqlParameter("@PHIN", _heightInInches));
         command.Parameters.Add(new SqlParameter("@PWPO", _weightInPounds));
         //execute command
         return ExecuteNonQuery(command);
     }*/
    #endregion
}


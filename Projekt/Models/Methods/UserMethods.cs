using Projekt.Models.Details;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace Projekt.Models.Methods
{
    public class UserMethods
    {

        public UserMethods() { }

    

        public string GenerateHash(string password)
        {
             return Crypto.HashPassword(password);   
        }

        /*
        private bool VerifyPassword(string hashedPassword, string password)
        {
            if(Crypto.Hash(password) == hashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        public int RegisterUserHash(UserDetails ud, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            string hashedPass = Crypto.HashPassword(ud.Password);

            //Connection to SQL server

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "INSERT INTO Tbl_User (Us_Username, Us_Password, Us_Image, Us_Mail) VALUES (@username, @password, 14, @mail)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("username", SqlDbType.NVarChar, 30).Value = ud.Username;
            dbCommand.Parameters.Add("password", SqlDbType.NVarChar, 200).Value = hashedPass;
            dbCommand.Parameters.Add("mail", SqlDbType.NVarChar, 200).Value = ud.Mail;


            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "No user was created!";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public UserDetails loginUserHash(UserDetails ud, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

          
            String sqlString = "SELECT* FROM Tbl_User WHERE Tbl_User.Us_Username = @username";
            
            


            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

           
             dbCommand.Parameters.Add("username", SqlDbType.NVarChar, 30).Value = ud.Username;
            
            
            

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myUser");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myUser"].Rows.Count;

                if (count > 0)
                {

                    //Read data from data set
                    UserDetails ud2 = new UserDetails();

                    ud2.Username = myDS.Tables["myUser"].Rows[i]["Us_Username"].ToString();
                    ud2.Password = myDS.Tables["myUser"].Rows[i]["Us_Password"].ToString();
                    ud2.Id = Convert.ToInt32(myDS.Tables["myUser"].Rows[i]["Us_Id"]);


                    errormsg = "";

                    if (Crypto.VerifyHashedPassword(ud2.Password, ud.Password)) {
                        return ud2;
                    }
                   else
                    {
                        errormsg = "No account was found!";
                        return null;
                    }
                }
                else
                {
                    errormsg = "No account was found!";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int RegisterUser(UserDetails ud, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            string hashedPass = GenerateHash(ud.Password);

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
           // dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "INSERT INTO Tbl_User (Us_Username, Us_Password, Us_Image) VALUES (@username, @password, 14)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("username", SqlDbType.NVarChar, 30).Value = ud.Username;
            dbCommand.Parameters.Add("password", SqlDbType.NVarChar, 30).Value = ud.Password;


            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "No user was created!";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }


        public UserDetails loginUser(UserDetails ud, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_User WHERE Tbl_User.Us_Username = @username AND Tbl_User.Us_Password = @password";


            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("username", SqlDbType.NVarChar, 30).Value = ud.Username;
            dbCommand.Parameters.Add("password", SqlDbType.NVarChar, 30).Value = ud.Password;


            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myUser");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myUser"].Rows.Count;

                if (count > 0)
                {

                    //Read data from data set
                    UserDetails ud2 = new UserDetails();
                    ud2.Username = myDS.Tables["myUser"].Rows[i]["Us_Username"].ToString();
                    ud2.Password = myDS.Tables["myUser"].Rows[i]["Us_Password"].ToString();
                    ud2.Id = Convert.ToInt32(myDS.Tables["myUser"].Rows[i]["Us_Id"]);


                    errormsg = "";
                    return ud2;
                }
                else
                {
                    errormsg = "No account was found!";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int addUserDescription(UserDetails ud, int userID, out string errormsg)
        {
            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "UPDATE Tbl_User SET Tbl_User.Us_Description = @descString WHERE Tbl_User.Us_Id = @userID";
            //String sqlString = "INSERT INTO Tbl_Movies (Mo_Id, Mo_Title, Mo_ReleaseYear) VALUES (@id, @title, @releaseyear); INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film) VALUES (@grade, @description, @id)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("descString", SqlDbType.NVarChar, 100).Value = ud.Description;
            dbCommand.Parameters.Add("userID", SqlDbType.Int).Value = userID;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "No description was added!";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public UserDetails GetUser(int userID, out string errormsg) { 
        //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_User WHERE Tbl_User.Us_Id = @userID";
        //String sqlString = "SELECT * FROM Tbl_Movies, Tbl_Review WHERE Mo_Title = @reviewTitle AND Tbl_Review.Re_Film = Tbl_Movies.Mo_Id";
        SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

        dbCommand.Parameters.Add("userID", SqlDbType.Int).Value = userID;

            //Create adapter
        SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
        DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myUser");

                int count = 0;
                int i = 0;
                   count = myDS.Tables["myUser"].Rows.Count;

                if (count > 0)
                {

                    //Read data from data set
                    UserDetails uDetails = new UserDetails();

                    uDetails.Id = Convert.ToInt16(myDS.Tables["myUser"].Rows[i]["Us_Id"]);
                    uDetails.Username = myDS.Tables["myUser"].Rows[i]["Us_Username"].ToString();
                    uDetails.Image = Convert.ToInt16(myDS.Tables["myUser"].Rows[i]["Us_Image"]);
                    uDetails.Description = myDS.Tables["myUser"].Rows[i]["Us_Description"].ToString();
                    uDetails.Mail = myDS.Tables["myUser"].Rows[i]["Us_Mail"].ToString();

                    errormsg = "";

                    return uDetails;
                }
                else
                {
                    errormsg = "No review was collected!";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int ChangePassword(int userID, UserDetails ud, out string errormsg)
        {
            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            string hashedPass = GenerateHash(ud.Password);

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "UPDATE Tbl_User SET Tbl_User.Us_Password = @password WHERE Tbl_User.Us_Id = @userID ";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("userID", SqlDbType.Int).Value = userID;
            //dbCommand.Parameters.Add("username", SqlDbType.NVarChar, 30).Value = ud.Username;
            dbCommand.Parameters.Add("password", SqlDbType.NVarChar, 200).Value = hashedPass;


            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 2)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "No review was updated!";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }


        }

    }
    
}

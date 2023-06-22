using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models.Methods
{
    public class GenreMethods
    {

        public GenreMethods() { }

        public List<GenreDetails> GetGenres(out string errormsg)
        {
            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_Genres";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<GenreDetails> GenreList = new List<GenreDetails>();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myGenre");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myGenre"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Read data from data set
                        GenreDetails gd = new GenreDetails();
                        gd.Name = myDS.Tables["myGenre"].Rows[i]["Ge_Name"].ToString();
                        gd.Id = Convert.ToInt16(myDS.Tables["myGenre"].Rows[i]["Ge_Id"]);

                        i++;
                        GenreList.Add(gd);
                    }
                    errormsg = "";
                    return GenreList;
                }
                else
                {
                    errormsg = "No genre was collected!";
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
        public int InsertGenre(GenreDetails gd, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "INSERT INTO Tbl_Genres (Ge_Name) VALUES (@name)";
            //String sqlString = "INSERT INTO Tbl_Movies (Mo_Id, Mo_Title, Mo_ReleaseYear) VALUES (@id, @title, @releaseyear); INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film) VALUES (@grade, @description, @id)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("name", SqlDbType.NVarChar, 30).Value = gd.Name;

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
                    errormsg = "No genre was inserted!";
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Projekt.Models.Details;

namespace Projekt.Models.Methods
{
    public class MovieMethods
    {
        public MovieMethods() { }

        public List<MovieDetail> GetMoviesWithDataSet(out string errormsg)
        {

            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_Movies";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<MovieDetail> MovieList = new List<MovieDetail>();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myMovie");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myMovie"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Read data from data set
                        MovieDetail md = new MovieDetail();
                        md.Title = myDS.Tables["myMovie"].Rows[i]["Mo_Title"].ToString();
                        md.ReleaseYear = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_ReleaseYear"]);
                        md.Image = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Image"]);
                        md.User = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_User"]);



                        i++;
                        MovieList.Add(md);
                    }
                    errormsg = "";
                    return MovieList;
                }
                else
                {
                    errormsg = "No movie was collected!";
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
    }
}

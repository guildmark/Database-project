using Projekt.Models.Details;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models.Methods
{
    public class MovieReviewMethods
    {

        public MovieReviewMethods() { }

        public int InsertMovieReview(MovieReviewDetail mrd, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "BEGIN TRANSACTION InsertReview INSERT INTO Tbl_Movies (Mo_Title, Mo_ReleaseYear, Mo_Image) VALUES (@title, @releaseyear, 15) INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film, Re_User) VALUES (@grade, @description, (SELECT Tbl_Movies.Mo_Id FROM Tbl_Movies WHERE Tbl_Movies.Mo_Title = @title), @user) COMMIT TRANSACTION InsertReview";
            //String sqlString = "INSERT INTO Tbl_Movies (Mo_Id, Mo_Title, Mo_ReleaseYear) VALUES (@id, @title, @releaseyear); INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film) VALUES (@grade, @description, @id)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = mrd.Id;
            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 30).Value = mrd.Title;
            dbCommand.Parameters.Add("releaseyear", SqlDbType.Int).Value = mrd.ReleaseYear;
            dbCommand.Parameters.Add("grade", SqlDbType.Int).Value = mrd.Grade;
            dbCommand.Parameters.Add("description", SqlDbType.NVarChar, 100).Value = mrd.Description;
            dbCommand.Parameters.Add("user", SqlDbType.Int).Value = mrd.User;



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
                    errormsg = "No review was inserted into the database!";
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
        public int InsertMovieReview2(int genreID, MovieReviewDetail mrd, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "BEGIN TRANSACTION InsertReview INSERT INTO Tbl_Movies (Mo_Title, Mo_ReleaseYear, Mo_Image) VALUES (@title, @releaseyear, 15) INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film, Re_User) VALUES (@grade, @description, (SELECT Tbl_Movies.Mo_Id FROM Tbl_Movies WHERE Tbl_Movies.Mo_Title = @title), @user) INSERT INTO Tbl_Actor(Act_Name) VALUES (@actor) INSERT INTO Tbl_IsInMovies (II_Actor, II_Movie) VALUES ((SELECT Tbl_Actor.Act_Id FROM Tbl_Actor WHERE Tbl_Actor.Act_Name = @actor), (SELECT Tbl_Movies.Mo_Id FROM Tbl_Movies WHERE Tbl_Movies.Mo_Title = @title))INSERT INTO Tbl_PartOfGenre (PG_Movie, PG_Genre) VALUES ((SELECT Tbl_Movies.Mo_Id FROM Tbl_Movies WHERE Tbl_Movies.Mo_Title = @title), @genreID)  COMMIT TRANSACTION InsertReview";
            //String sqlString = "INSERT INTO Tbl_Movies (Mo_Id, Mo_Title, Mo_ReleaseYear) VALUES (@id, @title, @releaseyear); INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film) VALUES (@grade, @description, @id)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = mrd.Id;
            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 30).Value = mrd.Title;
            dbCommand.Parameters.Add("releaseyear", SqlDbType.Int).Value = mrd.ReleaseYear;
            dbCommand.Parameters.Add("grade", SqlDbType.Int).Value = mrd.Grade;
            dbCommand.Parameters.Add("description", SqlDbType.NVarChar, 100).Value = mrd.Description;
            dbCommand.Parameters.Add("user", SqlDbType.Int).Value = mrd.User;
            dbCommand.Parameters.Add("genreID", SqlDbType.Int).Value = genreID;
            dbCommand.Parameters.Add("actor", SqlDbType.NVarChar, 100).Value = mrd.Actor;




            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 5)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "No review was inserted into the database!";
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

        public MovieReviewDetail GetSingleMovieReview(string reviewTitle, out string errormsg)
        {

            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM ((((Tbl_Movies INNER JOIN Tbl_PartOfGenre ON Tbl_Movies.Mo_Id = Tbl_PartOfGenre.PG_Movie) INNER JOIN Tbl_Genres ON Tbl_PartOfGenre.PG_Genre = Tbl_Genres.Ge_Id) INNER JOIN Tbl_IsInMovies ON Tbl_Movies.Mo_Id = Tbl_IsInMovies.II_Movie) INNER JOIN Tbl_Actor ON Tbl_IsInMovies.II_Actor = Tbl_Actor.Act_Id), Tbl_Review, Tbl_User WHERE Mo_Title = @reviewTitle AND Tbl_Review.Re_Film = Tbl_Movies.Mo_Id AND Tbl_Review.Re_User = Tbl_User.Us_Id";
            //String sqlString = "SELECT * FROM Tbl_Movies, Tbl_Review WHERE Mo_Title = @reviewTitle AND Tbl_Review.Re_Film = Tbl_Movies.Mo_Id";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("reviewTitle", SqlDbType.NVarChar, 40).Value = reviewTitle;

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myReview");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myReview"].Rows.Count;

                if (count > 0)
                {

                    //Read data from data set
                    MovieReviewDetail mrd = new MovieReviewDetail();

                    mrd.Id = Convert.ToInt16(myDS.Tables["myReview"].Rows[i]["Mo_Id"]);
                    mrd.Title = myDS.Tables["myReview"].Rows[i]["Mo_Title"].ToString();
                    mrd.ReleaseYear = Convert.ToInt16(myDS.Tables["myReview"].Rows[i]["Mo_ReleaseYear"]);
                    mrd.User = Convert.ToInt16(myDS.Tables["myReview"].Rows[i]["Re_User"]);
                    mrd.Image = Convert.ToInt16(myDS.Tables["myReview"].Rows[i]["Mo_Image"]);
                    mrd.Grade = Convert.ToInt16(myDS.Tables["myReview"].Rows[i]["Re_Grade"]);
                    mrd.Description = myDS.Tables["myReview"].Rows[i]["Re_Description"].ToString();
                    mrd.Actor = myDS.Tables["myReview"].Rows[i]["Act_Name"].ToString();
                    mrd.Genre = myDS.Tables["myReview"].Rows[i]["Ge_Name"].ToString();
                    mrd.Username = myDS.Tables["myReview"].Rows[i]["Us_Username"].ToString();

                    errormsg = "";

                    return mrd;
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
    
        public List<MovieReviewDetail> GetMovieReviews(out string errormsg)
        {

            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT Tbl_User.Us_Username, Tbl_Review.Re_User, Tbl_Movies.Mo_Image, Tbl_Movies.Mo_Id, Tbl_Movies.Mo_Title, Tbl_Movies.Mo_ReleaseYear, Tbl_Review.Re_Grade, Tbl_Review.Re_Description FROM Tbl_Movies, Tbl_Review, Tbl_User, Tbl_Images WHERE Tbl_Movies.Mo_Id = Tbl_Review.Re_Film AND Tbl_Movies.Mo_Image = Tbl_Images.Im_Id AND Tbl_Review.Re_User = Tbl_User.Us_Id";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);


            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<MovieReviewDetail> MovieReviewList = new List<MovieReviewDetail>();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myReview
                myAdapter.Fill(myDS, "myMovie");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myMovie"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Read data from data set
                        MovieReviewDetail mr = new MovieReviewDetail();
                        mr.Image = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Image"]);
                        mr.User = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Re_User"]);
                        mr.Id = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Id"]);
                        mr.Title = myDS.Tables["myMovie"].Rows[i]["Mo_Title"].ToString();
                        mr.ReleaseYear = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_ReleaseYear"]);
                        mr.Grade = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Re_Grade"]);
                        mr.Description = myDS.Tables["myMovie"].Rows[i]["Re_Description"].ToString();
                        mr.Username = myDS.Tables["myMovie"].Rows[i]["Us_Username"].ToString();


                        i++;
                        MovieReviewList.Add(mr);
                    }
                    errormsg = "";
                    return MovieReviewList;
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

        public List<MovieReviewDetail> GetMovieReviews(int userID, out string errormsg)
        {

            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT Tbl_User.Us_Username, Tbl_Review.Re_User, Tbl_Movies.Mo_Image, Tbl_Movies.Mo_Id, Tbl_Movies.Mo_Title, Tbl_Movies.Mo_ReleaseYear, Tbl_Review.Re_Grade, Tbl_Review.Re_Description FROM Tbl_Movies, Tbl_Review, Tbl_User, Tbl_Images WHERE Tbl_Movies.Mo_Id = Tbl_Review.Re_Film AND Tbl_Movies.Mo_Image = Tbl_Images.Im_Id AND Tbl_Review.Re_User = Tbl_User.Us_Id AND Tbl_User.Us_Id = @userID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("userID", SqlDbType.Int).Value = userID;


            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<MovieReviewDetail> MovieReviewList = new List<MovieReviewDetail>();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myReview
                myAdapter.Fill(myDS, "myMovie");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myMovie"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Read data from data set
                        MovieReviewDetail mr = new MovieReviewDetail();
                        mr.Image = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Image"]);
                        mr.User = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Re_User"]);
                        mr.Id = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Id"]);
                        mr.Title = myDS.Tables["myMovie"].Rows[i]["Mo_Title"].ToString();
                        mr.ReleaseYear = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_ReleaseYear"]);
                        mr.Grade = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Re_Grade"]);
                        mr.Description = myDS.Tables["myMovie"].Rows[i]["Re_Description"].ToString();
                        mr.Username = myDS.Tables["myMovie"].Rows[i]["Us_Username"].ToString();


                        i++;
                        MovieReviewList.Add(mr);
                    }
                    errormsg = "";
                    return MovieReviewList;
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

        public int UpdateMovieReviews(int filmID, MovieReviewDetail mrd, out string errormsg)
        {
             //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "UPDATE Tbl_Movies SET Tbl_Movies.Mo_Title = @title, Tbl_Movies.Mo_ReleaseYear = @releaseyear WHERE Tbl_Movies.Mo_Id = @filmID; UPDATE Tbl_Review SET Tbl_Review.Re_Grade = @grade, Tbl_Review.Re_Description = @description WHERE Tbl_Review.Re_Film = @filmID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            
            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 30).Value = mrd.Title;
            dbCommand.Parameters.Add("releaseyear", SqlDbType.Int).Value = mrd.ReleaseYear;
            dbCommand.Parameters.Add("grade", SqlDbType.Int).Value = mrd.Grade;
            dbCommand.Parameters.Add("description", SqlDbType.NVarChar, 200).Value = mrd.Description;
            dbCommand.Parameters.Add("filmID", SqlDbType.NVarChar, 30).Value = filmID;

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
  

        public int DeleteMovieReviewID(int id, out string errormsg)
        {
            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "DELETE FROM Tbl_Review WHERE Tbl_Review.Re_Film = @id";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;


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

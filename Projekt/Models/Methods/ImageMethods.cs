using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Projekt.Models.Methods
{
    public class ImageMethods
    {

        public ImageMethods() { }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public ImageDetails getImage(int id, out string errormsg)
        {
            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_Images WHERE Tbl_Images.Im_Id = @id";

            //String sqlString = "SELECT * FROM Tbl_Movies, Tbl_Review WHERE Mo_Title = @reviewTitle AND Tbl_Review.Re_Film = Tbl_Movies.Mo_Id";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.NVarChar, 40).Value = id;

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myMovie
                myAdapter.Fill(myDS, "myImage");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myImage"].Rows.Count;

                if (count > 0)
                {

                    //Read data from data set
                    ImageDetails imd = new ImageDetails();

                    imd.Id = Convert.ToInt16(myDS.Tables["myImage"].Rows[i]["Im_Id"]);
                    //Cast varbinary that the images are stored in to byte array
                    imd.Image = (byte[])myDS.Tables["myImage"].Rows[i]["Im_Image"];


                    errormsg = "";

                    return imd;
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

        public List<ImageDetails> GetImageList(out string errormsg)
        {

            //Create SQL connection
            SqlConnection dbConnection = new SqlConnection();

            //Connect to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_Images";

            //String sqlString = "SELECT * FROM Tbl_Movies, Tbl_Review WHERE Mo_Title = @reviewTitle AND Tbl_Review.Re_Film = Tbl_Movies.Mo_Id";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            //Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<ImageDetails> ImageList = new List<ImageDetails>();

            try
            {
                dbConnection.Open();

                //Fill data set in table known as myReview
                myAdapter.Fill(myDS, "myImage");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myImage"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Read data from data set
                        ImageDetails imd = new ImageDetails();
                        //mr.Image = Convert.ToInt16(myDS.Tables["myMovie"].Rows[i]["Mo_Image"]);
                        imd.Id = Convert.ToInt16(myDS.Tables["myImage"].Rows[i]["Im_Id"]);
                        imd.Image = (byte[])myDS.Tables["myImage"].Rows[i]["Im_Image"];

                        i++;
                        ImageList.Add(imd);
                    }
                    errormsg = "";
                    return ImageList;
                }
                else
                {
                    errormsg = "No image was collected!";
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

        public int InsertImage(ImageDetails id, out string errormsg)
        {

            //Create sql connection
            SqlConnection dbConnection = new SqlConnection();

            //Connection to SQL server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\ProjektDB.mdf;Integrated Security=True";
            //dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjektDB;Integrated Security=True";

            String sqlString = "insert into Tbl_Images(Im_Image) SELECT BulkColumn FROM Openrowset(Bulk '@imagePath', Single_Blob) as img";
            //String sqlString = "INSERT INTO Tbl_Movies (Mo_Id, Mo_Title, Mo_ReleaseYear) VALUES (@id, @title, @releaseyear); INSERT INTO Tbl_Review (Re_Grade, Re_Description, Re_Film) VALUES (@grade, @description, @id)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("imagePath", SqlDbType.Int).Value = id.ImagePath;
  


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

    }

}

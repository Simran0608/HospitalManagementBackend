using Hospital_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Configuration;

namespace Hospital_Management.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
         readonly SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = Hospital_Management_System;Integrated Security = True; MultipleActiveResultSets=True");

        PaymentBil pay = new PaymentBil();

        // GET api/values
        public List<PaymentBil> Get()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("ListPayment", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<PaymentBil> listPayment = new List<PaymentBil>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        PaymentBil pay = new PaymentBil();
                        pay.Patient_Bill_Id = Convert.ToInt32(dt.Rows[i]["Patient_Bill_Id"]);
                        pay.Payment_id = Convert.ToInt32(dt.Rows[i]["Payment_id"]);
                        pay.Patient_Name = dt.Rows[i]["Patient_Name"].ToString();
                        pay.Doctor_Name = dt.Rows[i]["Doctor_Name"].ToString();
                        pay.Room_Bill = Convert.ToInt32(dt.Rows[i]["Room_Bill"]);
                        pay.Doctor_Bill = Convert.ToInt32(dt.Rows[i]["Doctor_Bill"]);
                        pay.Medicine_Bill = Convert.ToInt32(dt.Rows[i]["Medicine_Bill"]);
                        pay.Total_Bill = Convert.ToInt32(dt.Rows[i]["Total_Bill"]);
                        pay.Status = dt.Rows[i]["Status"].ToString();

                        listPayment.Add(pay);
                    }
                }
                if (listPayment.Count > 0)
                {
                    return listPayment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET api/values/5
        public PaymentBil Get(int id)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SearchPayment", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Payment_id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                PaymentBil payment = new PaymentBil();
                if (dt.Rows.Count > 0)
                {
                    payment.Payment_id = Convert.ToInt32(dt.Rows[0]["Payment_id"].ToString());
                    payment.Patient_Bill_Id = Convert.ToInt32(dt.Rows[0]["Patient_Bill_Id"]);
                    payment.Patient_Name = dt.Rows[0]["Patient_Name"].ToString();
                    payment.Doctor_Name = dt.Rows[0]["Doctor_Name"].ToString();
                    payment.Total_Bill = Convert.ToInt32(dt.Rows[0]["Total_Bill"]);
                    payment.Status = dt.Rows[0]["Status"].ToString();
                }
                if (payment != null)
                {
                    return payment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/values
        public string Post(PaymentBil payment)
        {
            try
            {
                string msg = "";
                if (payment != null)
                {
                    SqlCommand cmd = new SqlCommand("GeneratePayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Patient_Bill_Id", payment.Patient_Bill_Id);
                    cmd.Parameters.AddWithValue("@status", payment.Status);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        msg = "Data is inserted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
                return msg;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/values/5
        public string Put(int id, PaymentBil payment)
        {
            try
            {
                string msg = "";
                if (payment != null)
                {
                    SqlCommand cmd = new SqlCommand("UpdatePaymentDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", payment.Status);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        msg = "Data has been Updated";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
                return msg;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            try
            {
                string msg = "";
                SqlCommand cmd = new SqlCommand("DeletePaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentId", id);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been Deleted";
                }
                else
                {
                    msg = "Error";
                }
                return msg;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace AppointmentProject.Pages
{
    public class viewModel : PageModel
    {
        private readonly ILogger<viewModel> _logger;

        public viewModel(ILogger<viewModel> logger)
        {
            _logger = logger;
        }

        public List<view> viewParameterList = new List<view>();

        public void OnGet()
        {
            view view = new view();
            viewParameterList = view.GetviewParameter();
        }

        public class view
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Doctor { get; set; }
            public string Specialization { get; set; }

            public List<view> GetviewParameter()
            {
                List<view> viewParameterList = new List<view>();
                string connectionString = "Data Source=yash-laptop\\sqlexpress; Integrated Security=True;TrustServerCertificate=True; DATABASE=CareLink";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT ID, Name, Date, Doctor, Specialization FROM Appointments";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    view viewParameter = new view
                                    {
                                        Id = Convert.ToInt32(dr["ID"]),
                                        Name = dr["Name"].ToString(),
                                        Date = dr["Date"].ToString(),
                                        Doctor = dr["Doctor"].ToString(),
                                        Specialization = dr["Specialization"].ToString()
                                    };
                                    viewParameterList.Add(viewParameter);
                                }
                            }
                        }
                    }
                }
                return viewParameterList;
            }
        }
    }
}

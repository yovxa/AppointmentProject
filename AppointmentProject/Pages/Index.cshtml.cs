using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AppointmentProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            string name = Request.Form["Name"];
            string date = Request.Form["Date"];
            string doctor = Request.Form["Doctor"];
            string Specialization = Request.Form["Specialization"];

            string connectionString = "Data Source=yash-laptop\\sqlexpress; Integrated Security=True;TrustServerCertificate=True; DATABASE=CareLink";
            string sqlQuery = "INSERT INTO Appointments (Name, Date, Doctor, Specialization) VALUES(@Name, @Date, @Doctor, @Specialization)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Doctor", doctor);
                    cmd.Parameters.AddWithValue("@Specialization", Specialization);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            OnGet();
        }
    }
}

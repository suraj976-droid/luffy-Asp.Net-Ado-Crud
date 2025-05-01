using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud
{
    public partial class EmployeeCrud : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            if (!IsPostBack)
            {
                BindEmployeeList();
                btnUpdate.Visible = false;

                if (Request.QueryString["Id"] != null && Request.QueryString["Action"] != null)
                {
                    int empId = int.Parse(Request.QueryString["Id"]);
                    string action = Request.QueryString["Action"].ToString();

                    if (action == "Edit")
                    {
                        LoadEmployeeForEdit(empId);
                    }
                    else if (action == "Delete")
                    {
                        DeleteEmployee(empId);
                    }
                }
            }
        }

        private void BindEmployeeList()
        {
            string q = "SELECT * FROM Employees";
            SqlDataAdapter ada = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
               

                string name = txtName.Text;
                string gender = rbMale.Checked ? "Male" : "Female";
                bool isActive = chkActive.Checked;
                string dept = DropDownList1.SelectedValue;

                // Check for duplicate name
                string checkQuery = $"SELECT COUNT(*) FROM Employees WHERE Name = '{txtName.Text}'";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    lblError.Text = "Duplicate entry: Employee name already exists!";
                    return;
                }

                else
                {
                    string q = $"INSERT INTO Employees (Name, Gender, IsActive, Department) VALUES ('{name}', '{gender}', '{isActive}', '{dept}')";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    cmd.ExecuteNonQuery();

                    ClearForm();
                    BindEmployeeList();
                    lblError.Text = "Employee added successfully!";
                }
                    
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                int empId = int.Parse(Request.QueryString["Id"]);
                string name = txtName.Text;
                string gender = rbMale.Checked ? "Male" : "Female";
                bool isActive = chkActive.Checked;
                string dept = DropDownList1.SelectedValue;

            
                string checkQuery = $"SELECT COUNT(*) FROM Employees WHERE Name = '{name}' AND Id != {empId}";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                int countDuplicate = (int)checkCmd.ExecuteScalar();

                if (countDuplicate > 0)
                {
                    lblError.Text = "Duplicate entry: Employee name already exists!";
                    return;
                }

              else
                {
                    string q = $"UPDATE Employees SET Name='{name}', Gender='{gender}', IsActive='{isActive}', Department='{dept}' WHERE Id={empId}";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    cmd.ExecuteNonQuery();

                    ClearForm();
                    BindEmployeeList();
                    lblError.Text = "Employee updated successfully!";
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    Response.Redirect("EmployeeCrud.aspx");
                }

                
            }
        }

        private void LoadEmployeeForEdit(int empId)
        {
            string q = $"SELECT * FROM Employees WHERE Id={empId}";
            SqlDataAdapter ada = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                rbMale.Checked = dt.Rows[0]["Gender"].ToString() == "Male";
                rbFemale.Checked = !rbMale.Checked;
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                DropDownList1.SelectedValue = dt.Rows[0]["Department"].ToString();

                btnSave.Visible = false;
                btnUpdate.Visible = true;
                lblError.Text = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text.Trim();
            string q = "SELECT * FROM Employees WHERE Name LIKE @name";
            SqlDataAdapter ada = new SqlDataAdapter(q, conn);
            ada.SelectCommand.Parameters.AddWithValue("@name", "%" + name + "%");

            DataTable dt = new DataTable();
            ada.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            Response.Redirect("EmployeeCrud.aspx");
        }
        private void DeleteEmployee(int empId)
        {
            string q = $"DELETE FROM Employees WHERE Id={empId}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();

            BindEmployeeList();
            lblError.Text = "Employee deleted successfully!";
            Response.Redirect("EmployeeCrud.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            BindEmployeeList(); // We're keeping this method to avoid compilation errors, but not using pagination


        }

        private void ClearForm()
        {
            txtName.Text = "";
            rbMale.Checked = true;
            rbFemale.Checked = false;
            chkActive.Checked = false;
            DropDownList1.SelectedValue = "0";
            lblError.Text = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblError.Text = "Please enter employee name";
                return false;
            }
            if (DropDownList1.SelectedValue == "0")
            {
                lblError.Text = "Please select a department";
                return false;
            }
            return true;
        }
    }
}
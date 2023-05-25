using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAB03_SCD.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LAB03_SCD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataTable EmpStatus = new DataTable();
            EmpStatus.Columns.Add("Id");
            EmpStatus.Columns.Add("Name");
            EmpStatus.AcceptChanges();
            dr = EmpStatus.NewRow();
            dr["Id"] = 2;
            dr["Name"] = "Disable";
            EmpStatus.Rows.Add(dr);
            dr = EmpStatus.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Active";
            EmpStatus.Rows.Add(dr);
            EmpStatus.AcceptChanges();
            comboStatus.DataSource = null;
            comboStatus.DisplayMember = "Name";
            comboStatus.ValueMember = "Id";
            comboStatus.DataSource = EmpStatus;
            comboStatus.SelectedIndex = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        string img_location = string.Empty;
        DataRow dr;
        employee employee = new employee();
        long empid = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //Image Browse Code:
            #region upload image file
            try
            {
                Image imageFile;
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "png files(*.png)|*.png|jpg files(*.jpg)" +
                   "|*.jpg|All files(*.*)|*.*"
                };
                //This ^^ is same as:
                //OpenFileDialog dialog = new OpenFileDialog()
                //dialog.Filter = "png files ....

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageFile = Image.FromFile(dialog.FileName);
                    int imgHeight = imageFile.Height;
                    if (imgHeight > 350)
                        MessageBox.Show("Maximum Image can be 350x350" +
                            " Image", "Image size is too large."
                            , MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    else
                    {
                        img_location = dialog.FileName.ToString();
                        picEmployee.ImageLocation = img_location;
                    }
                }
                dialog.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            #endregion
            try
            {
                byte[] images = null;
                if (img_location != string.Empty)
                {
                    FileSream stream = new FileStream(img_location, FileMode.Open, FileAcceess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);
                } else
                {
                    Image img = picEmployee.Image;

                    ImageConverter converter = new ImageConverter();
                    images = (byte[])converter.ConvertTo(img, typeof(byte[]));
                }
                employee.EmpName = txtName.Text.Trim();
                employee.EmpUsername.Text.Trim();
                employee.EmpPassworde.Text.Trim();
                employee.DptId = int.Parse(comboDepartment.SelectedValue.ToString());
                employee.RoleID = int.Parse(comboDepartment.SelectedValue.ToString());
                employee.EmpJoiningDate = dateTimePicker.Value;
                employee.EmpStatus = short.Parse(comboStatus.SelectedValue.ToString());
                employee.EmpImage = images;

            }
            using (var db = new SCDEntities())

            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            ClearData();
            SetdataInGridView();
            MessageBox.Show("Record SaveSuccessfully");

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            employee.EmpName = txtName.Text.Trim();
            employee.EmpUsername = txtUsername.Text.Trim();
            employee.EmpPassword.Text.Trim();
            employee.DptID = int.Parse(comboDepartment.SelectedValue.ToString());
            employee.RoleID = int.Parse(comboDepartment.SelectedValue.ToString());
            employee.EmpJoiningDate = dateTimePicker.Value;
            employee.EmpStatus = short.Parse(comboStatus.SelectedValue.ToString());
            employee.EmpImage = images;

            using (var db = new SCDEntities())
                var entry = db.Entry(employee);
            if (entry.State == EntityState.Detached)
                db.Employees.Atach


        } }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ARE YOU SURE YOU WANT TO DELETE THIS RECORD ?", "DELETE ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db=new SCDEntities())
                    {
                        var entry=db.Entry(employee);
                        if (entry.State == EntityState.Detached)
                            db.Employees.Attach(employee);
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                    }
                    ClearData();
                    SetDataInGridView();
                    MessageBox.Show("Record Deleted Sucessfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(Exception ex)
                    MessageBox.Show(ex.Message);

            }

        

        }

        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();
            try
            {
                if(e.RowIndex >= 0)
                {
                    empid = long.Parse(dgvEmployee.Rows[e.RowIndex].Cells["EmpID"].Value.ToString());
                    // load emp data
                    using (var db = new SCDEntities())
                    {
                        employee = db.Employees.Where(y => y.EmpID == empid).First();
                    }

                    txtName = dgvEmployee.Rows[e.RowIndex].Cells["EmpName"].Value.ToString();
                    txtUsername = dgvEmployee.Rows[e.RowIndex].Cells["EmployeeUsername"].Value.ToString();
                    dateTimePicker.Value = Convert.ToDateTime(dgvEmployee.Rows[e.RowIndex].Cells["EmployeeJoiningDate"].Value.ToString());
                    comboDepartment.SelectedValue = int.Parse(dgvEmployee.Rows[e.RowIndex].Cells["DptID"].Value.ToString());
                    comboRole.SelectedValue = int.Parse(dgvEmployee.Rows[e.RowIndex].Cells["RoleID"].Value.ToString());
                    if (dgvEmployee.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Active")
                        comboStatus.SelectedIndex = 1;
                    else
                        comboStatus.SelectedIndex = 0;

                    comboStatus.Enabled = false;
                    txtName.Enabled = false;
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                    dateTimePicker.Enabled = false;
                    comboDepartment.Enabled = false;
                    picEmployee.Enabled = false;
                    comboRole.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnBrowse.Enabled = false;
                    btnAdd.Enabled = false;
                    btnClear.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    Image img = Properties.Resources.icons8_no_image_100;
                    MemoryStream ms = new MemoryStream(employee.EmpImage);
                    img = Image.FromStream(ms);
                    picEmployee.Image = img;

                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SetDataInGridView()
        {
        var db = new SCDEntities();
        var employee = (from emp in db.Employees
                        join role in db.Roles
                        on emp.RoleID equals role.RoleID
                        join dpt in db.Departments
                        on emp.DptID equals dpt.DptID
                        where emp.EmpStatus == 1
                        select new
                        {
                            EmpID = emp.EmpID,
                            EmpName = emp.EmpName,
                            EmpUsername = emp.EmpUsername,
                            EmpJoiningDate = emp.EmpJoiningDate,
                            EmpStatus = emp.EmpStatus,
                            RoleID = role.RoleID,
                            DptID = dpt.DptID,
                            DptName = dpt.DptName,
                            RoleName = role.RoleName,

                        }).ToList();
        dgvEmployee.Rows.Clear();


        }

    }
}

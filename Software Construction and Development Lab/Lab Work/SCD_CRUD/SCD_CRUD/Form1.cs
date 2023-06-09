﻿using SCD_CRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Entity;

namespace SCD_CRUD
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

        private void ClearData()
        {
            txtName.Text = "";
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            picEmployee.Image = Properties.Resources.icons8_no_image_100;
            img_location = string.Empty;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            comboDepartment.SelectedIndex = 0;
            comboRole.SelectedIndex = 0;
            dateTimePicker.Value = DateTime.Now;

            comboStatus.Enabled = true;
            txtName.Enabled = true;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            dateTimePicker.Enabled = true;
            comboDepartment.Enabled = true;
            picEmployee.Enabled = true;
            comboRole.Enabled = true;

            btnClear.Enabled = true;
            btnBrowse.Enabled = true;

            btnAdd.Enabled = true;

            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public void SetDataInGridView()
        {
            var db = new SCDEntities();
                var employee = (from emp in db.Employees
                            join role in db.Roles

                            on emp.RoleID equals role.RoleID

                            join dpt in db.Departments
                            on emp.DpID equals dpt.DptID
                            select new
                            {
                                EmpID = emp.EmpID,
                                EmpName = emp.EmpName,
                                EmpUsername = emp.EmpUsername,
                                EmpJoiningDate = emp.EmpJoiningDate,
                                EmpStatus = emp.EmpStatus,
                                RoleID = emp.RoleID,
                                DptID = emp.DpID,
                                DptName = dpt.DptName,
                                RoleName = role.RoleName,
                            }).ToList();

            dgvEmployee.Rows.Clear();
            dgvEmployee.DataSource = null;
            foreach (var emp in employee)
            {
                int RowIndex = dgvEmployee.Rows.Add(); // Add new Row

                dgvEmployee.Rows[RowIndex].Cells["EmpID"].Value = emp.EmpID;
                dgvEmployee.Rows[RowIndex].Cells["EmpName"].Value = emp.EmpName;
                dgvEmployee.Rows[RowIndex].Cells["EmpUsername"].Value = emp.EmpUsername;
                dgvEmployee.Rows[RowIndex].Cells["RoleID"].Value = emp.RoleID;
                dgvEmployee.Rows[RowIndex].Cells["RoleName"].Value = emp.RoleName;
                dgvEmployee.Rows[RowIndex].Cells["DptID"].Value = emp.DptID;
                dgvEmployee.Rows[RowIndex].Cells["DptName"].Value = emp.DptName;
                dgvEmployee.Rows[RowIndex].Cells["EmpJoiningDate"].Value
                    = emp.EmpJoiningDate.ToString("dd-MMM-yyyy");
                if (emp.EmpStatus == 1)
                    dgvEmployee.Rows[RowIndex].Cells["Status"].Value = "Active";
                else
                    dgvEmployee.Rows[RowIndex].Cells["Status"].Value = "Disable";
            }
            dgvEmployee.Update();
            dgvEmployee.Refresh();


        }

        void getDepartment()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();

            DataRow dr;

            dataTable.Columns.Add("DptID");
            dataTable.Columns.Add("DptName");

            using (var db = new SCDEntities())
            {
                var departments = db.Departments.ToList<Department>();

                foreach (Department dpt in departments)
                {
                    //Setting column names as Property names
                    dr = dataTable.NewRow();

                    dr["DptID"] = dpt.DptID;
                    dr["DptName"] = dpt.DptName;
                    dataTable.Rows.Add(dr);
                }
            }

            dataTable.AcceptChanges();

            comboDepartment.DataSource = null;

            comboDepartment.ValueMember = "DptID";
            comboDepartment.DisplayMember = "DptName";
            comboDepartment.DataSource = dataTable;
            //MessageBox.Show(db.Departments.ToList<Department>().ToList().ToString());

        }

        void getRole()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();

            DataRow dr;
            dataTable.Columns.Add("RoleID");
            dataTable.Columns.Add("RoleName");
            using (var db = new SCDEntities())
            {
                var roles = db.Roles.ToList<Role>();
                foreach (Role role in roles)
                {

                    //Setting column names as Property names

                    dr = dataTable.NewRow();
                    dr["RoleID"] = role.RoleID;
                    dr["RoleName"] = role.RoleName;
                    dataTable.Rows.Add(dr);

                }
            }
            dataTable.AcceptChanges();
            comboRole.DataSource = null;
            comboRole.ValueMember = "RoleID";
            comboRole.DisplayMember = "RoleName";
            comboRole.DataSource = dataTable;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //string img_location1 = "";
        DataRow dr;
        Employee employee = new Employee();
        long empId = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                if (img_location != string.Empty)
                {
                    FileStream stream =
                        new FileStream(img_location, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);

                }
                else
                {
                    Image img = picEmployee.Image;

                    ImageConverter converter = new ImageConverter();
                    images = (byte[])converter.ConvertTo(img, typeof(byte[]));
                }
                employee.EmpName = txtName.Text.Trim();
                employee.EmpUsername = txtUsername.Text.Trim();
                employee.EmpPassword = txtPassword.Text.Trim();
                employee.DpID = int.Parse(comboDepartment.SelectedValue.ToString());
                employee.RoleID = int.Parse(comboRole.SelectedValue.ToString());
                employee.EmpJoiningDate = dateTimePicker.Value;
                employee.EmpStatus = short.Parse(comboRole.SelectedValue.ToString());
                employee.EmpImage = images;


                //for crud operation
                using (var db = new SCDEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }

                ClearData();
                SetDataInGridView();
                MessageBox.Show("Record Save Successfully");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        string img_location = String.Empty;

        private void btnBrowse_Click(object sender, EventArgs e)
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
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageFile = Image.FromFile(dialog.FileName);
                    int imgHeight = imageFile.Height;
                    if (imgHeight > 350)
                        MessageBox.Show("Maximum Image can be 350x350" +
                            " Image", "Image size is too large..!!"
                            , MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    else
                    {
                        img_location = dialog.FileName.ToString();
                        picEmployee.ImageLocation = img_location;
                    }
                }
                dialog.Dispose();    //object destroy
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                if (img_location != string.Empty)
                {
                    FileStream stream =
                        new FileStream(img_location, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);

                }
                else
                {
                    Image img = picEmployee.Image;

                    ImageConverter converter = new ImageConverter();
                    images = (byte[])converter.ConvertTo(img, typeof(byte[]));
                }
                employee.EmpName = txtName.Text.Trim();
                employee.EmpUsername = txtUsername.Text.Trim();
                employee.EmpPassword = txtPassword.Text.Trim();
                employee.DpID = int.Parse(comboDepartment.SelectedValue.ToString());
                employee.RoleID = int.Parse(comboRole.SelectedValue.ToString());
                employee.EmpJoiningDate = dateTimePicker.Value;
                employee.EmpStatus = short.Parse(comboRole.SelectedValue.ToString());
                employee.EmpImage = images;


                //for crud operation
                using (var db = new SCDEntities())
                {
                    var entry = db.Entry(employee);

                    if (entry.State == EntityState.Detached)
                        db.Employees.Attach(employee);

                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }

                ClearData();
                SetDataInGridView();
                MessageBox.Show("Record Added Successfully");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db = new SCDEntities())
                    {
                        var entry = db.Entry(employee);
                        if (entry.State == EntityState.Detached)
                            db.Employees.Attach(employee);

                        db.Employees.Remove(employee);
                        db.SaveChanges();
                    }
                    ClearData();
                    SetDataInGridView();

                    MessageBox.Show("Record Deleted Successfully", "Deleted ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();
            try
            {
                if (e.RowIndex >= 0)
                {
                    empId = long.Parse(dgvEmployee.Rows[e.RowIndex].Cells["EmpID"].Value.ToString());
                    //load Employee Data
                    using (var db = new SCDEntities())
                    {
                        employee = db.Employees.Where(x => x.EmpID == empId).First();
                    }

                    txtName.Text = dgvEmployee.Rows[e.RowIndex].Cells["EmpName"].Value.ToString();
                    txtUsername.Text = dgvEmployee.Rows[e.RowIndex].Cells["EmpUsername"].Value.ToString();
                    dateTimePicker.Value = Convert.ToDateTime(dgvEmployee.Rows[e.RowIndex].Cells["EmpJoiningDate"].Value.ToString());
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDataInGridView();
            getDepartment();
            getRole();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            comboStatus.Enabled = false;
            txtName.Enabled = false;
            txtPassword.Enabled = false;
            txtUsername.Enabled = false;
            dateTimePicker.Enabled = false;
            comboDepartment.Enabled = false;
            picEmployee.Enabled = false;
            comboRole.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
            btnBrowse.Enabled = true;

            btnAdd.Enabled = true;

            btnEdit.Enabled = true;
        }
    }
}

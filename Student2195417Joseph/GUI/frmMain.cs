using Student2195417Joseph.Business;
using Student2195417Joseph.DataAccess;
using Student2195417Joseph.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student2195417Joseph
{
    public partial class frmMain : Form
    {
        List<Student> listC = new List<Student>();

        public frmMain()
        {
            InitializeComponent();
            btnList.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtStudentId.Text != "" && txtFirstName.Text != "" && txtLastName.Text != "")
            {
                if (Validator.IsSevenDigits(txtStudentId.Text))
                { 
                    if (Validator.IsUniqueID(listC, Convert.ToInt32(txtStudentId.Text)))
                    {
                        Student aStudent = new Student();
                        if ((Validator.IsSevenDigits(txtStudentId.Text)) && (Validator.IsValidName(txtFirstName)) && (Validator.IsValidName(txtLastName)))
                        {
                            aStudent.StudentId = Convert.ToInt32(txtStudentId.Text);
                            aStudent.FirstName = txtFirstName.Text;
                            aStudent.LastName = txtLastName.Text;
                            aStudent.PhoneNumber = mtxtPhone.Text;                            
                            listC.Add(aStudent);                           
                            btnList.Enabled = true;
                            ClearAll();

                            ListViewItem item = new ListViewItem(aStudent.StudentId.ToString());
                            item.SubItems.Add(aStudent.FirstName);
                            item.SubItems.Add(aStudent.LastName);
                            item.SubItems.Add(aStudent.PhoneNumber);
                            listViewStudent.Items.Add(item);
                        }
                    }
                }

            }
            else
                MessageBox.Show("Please fill the customer data");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            

            if (txtStudentId.Text != "" && txtFirstName.Text != "" && txtLastName.Text != "")
            {
                if (Validator.IsSevenDigits(txtStudentId.Text) && (Validator.IsValidName(txtFirstName)) && (Validator.IsValidName(txtLastName)))
                {
                    if(StudentDA.Search(Convert.ToInt32(txtStudentId.Text)) == null) 
                    {                            

                        Student aStudent = new Student();

                        aStudent.StudentId = Convert.ToInt32(txtStudentId.Text);
                        aStudent.FirstName = txtFirstName.Text;
                        aStudent.LastName = txtLastName.Text;
                        aStudent.PhoneNumber = mtxtPhone.Text;

                        StudentDA.Save(aStudent);

                        ClearAll();
                    }
                    else
                        MessageBox.Show("Duplicate ID, please enter a unique one.");
                }                 
            }
            else
                MessageBox.Show("Please fill the Student data");
        }

        private void ClearAll()
        {
            txtStudentId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            mtxtPhone.Clear();  
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure to exit the application?", "Confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)            
                Application.Exit();            
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listViewStudent.Items.Clear();
            StudentDA.ListStudents(listViewStudent);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Validator.IsSevenDigits(txtStudentId.Text))
            {
                if(StudentDA.Delete(Convert.ToInt32(txtStudentId.Text)))
                    MessageBox.Show("Student record has been deleted successfully", "Delete");
                else
                    MessageBox.Show("Student not found");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtInputInfo.Text != "")
            {
                if (Validator.IsSevenDigits(txtInputInfo.Text))
                {
                    int choice = cmbSearchBy.SelectedIndex;
                    Student cust;
                    switch (choice)
                    {
                        case -1: 

                            MessageBox.Show("Please, select at least one Search Option");
                            break;

                        case 0:
                            cust = StudentDA.Search(Convert.ToInt32(txtInputInfo.Text));

                            if (cust != null)
                            {
                                txtStudentId.Text = (cust.StudentId).ToString();
                                txtFirstName.Text = cust.FirstName;
                                txtLastName.Text = cust.LastName;
                                mtxtPhone.Text = cust.PhoneNumber;
                            }
                            else
                            {
                                MessageBox.Show("Customer not found!");
                            }
                            break;
                        default:  
                            break;
                    }
                }
            }
            else
                MessageBox.Show("Please fill the input info");
        }
    }
}

using Student2195417Joseph.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Student2195417Joseph.DataAccess
{
    public static class StudentDA
    {
        private static string filePath = Application.StartupPath + @"\Students.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save(Student stud)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(stud.StudentId + "," + stud.FirstName + "," + stud.LastName + "," + stud.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Student Data has been saved.");
        }

        public static void ListStudents(ListView listViewStudent)
        {
            if (File.Exists(filePath))
            {                
                StreamReader sReader = new StreamReader(filePath);
                listViewStudent.Items.Clear();                
                string line = sReader.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    ListViewItem item = new ListViewItem(fields[0]);
                    item.SubItems.Add(fields[1]);
                    item.SubItems.Add(fields[2]);
                    item.SubItems.Add(fields[3]);
                    listViewStudent.Items.Add(item);
                    line = sReader.ReadLine();
                }
                sReader.Close();
            }
            else
                MessageBox.Show("File not found.");
        }

        public static List<Student> ListStudents()
        {
            if (File.Exists(filePath))
            {
                List<Student> listS = new List<Student>();               
                StreamReader sReader = new StreamReader(filePath);              
                string line = sReader.ReadLine();
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    Student stud = new Student();
                    stud.StudentId = Convert.ToInt32(fields[0]);
                    stud.FirstName = fields[1];
                    stud.LastName = fields[2];
                    stud.PhoneNumber = fields[3];
                    listS.Add(stud);
                    line = sReader.ReadLine();
                }
                sReader.Close(); 
                return listS;
            }
            else
            {
                MessageBox.Show("File not found.");
                return null;
            }
        }


        public static Student Search(int studId)
        {
            if (File.Exists(filePath))
            {
                Student stud = new Student();
                StreamReader sReader = new StreamReader(filePath);
                string line = sReader.ReadLine();

                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (studId == Convert.ToInt32(fields[0]))
                    {
                        stud.StudentId = Convert.ToInt32(fields[0]);
                        stud.FirstName = fields[1];
                        stud.LastName = fields[2];
                        stud.PhoneNumber = fields[3];
                        sReader.Close();
                        return stud;
                    }
                    line = sReader.ReadLine();
                }
                sReader.Close();
                return null;
            }
            else
            {
                MessageBox.Show("File not found.");
                return null;
            }
        }


        public static bool Delete(int studId)
        {
            bool found = false;
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((studId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }
                else
                    found = true;
                line = sReader.ReadLine(); 
            }
            sReader.Close();
            sWriter.Close();            
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
            return found;
        }
    }
}

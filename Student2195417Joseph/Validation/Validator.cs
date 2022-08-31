using Student2195417Joseph.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student2195417Joseph.Validation
{
    public static class Validator
    {
        public static bool IsValidName(TextBox text)
        {
            for (int i = 0; i < text.TextLength; i++)
            {                
                if (!char.IsLetter(text.Text, i))
                {
                    MessageBox.Show("Invalid Name, Please enter only text data" , "INVALID NAME");
                    text.Clear();
                    text.Focus();
                    return false;
                }
            }
            return true;
        }

        public static bool IsSevenDigits(string text)
        {
            int tempID;
            if ((text.Length != 7) || !((Int32.TryParse(text, out tempID))))
            {
                MessageBox.Show("Invalid StudentID, it must be a 7 digit number");
                return false;
            }
            return true;
        }

        public static bool IsUniqueID(List<Student> listS, int id)
        {            
            foreach (Student s in listS)
            {
                if (s.StudentId == id)
                {
                    MessageBox.Show("Duplicate ID, please enter a unique one.");
                    return false;
                }
            }
            return true;
        }
    }
}


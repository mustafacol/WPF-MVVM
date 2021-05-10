using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustafaCol.Model
{
    class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int studentId;

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; OnPropertyChanged("StudentId"); }
        }
        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; OnPropertyChanged("FullName"); }
        }

        private string grade;

        public string Grade
        {
            get { return grade; }
            set { grade = value; OnPropertyChanged("Grade"); }
        }

        private int numberofVotes;

        public int NumberOfVotes
        {
            get { return numberofVotes; }
            set { numberofVotes = value; OnPropertyChanged("NumberOfVotes"); }
        }




    }
}

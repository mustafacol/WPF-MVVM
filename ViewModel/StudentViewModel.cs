using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MustafaCol.Commands;
using MustafaCol.Model;
namespace MustafaCol.ViewModel
{
    class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Student currentStudent;

        public Student CurrentStudent
        {
            get { return currentStudent; }
            set { currentStudent = value; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value;OnPropertyChanged("Message"); }
        }

        StudentService studentService;

        public StudentViewModel()
        {
            studentService = new StudentService();
            LoadData();
            addCommand = new CrudCommand(Add);
            findCommand = new CrudCommand(Find);
            updateCommand = new CrudCommand(Update);
            deleteCommand = new CrudCommand(Delete);
            voteCommand = new CrudCommand(Vote);
            completeElectionCommand = new CrudCommand(CompleteElection);
            CurrentStudent = new Student();

        }



        #region DisplayOperations
        private ObservableCollection<Student> stuList;

        public ObservableCollection<Student> StuList
        {
            get { return stuList; }
            set { stuList = value;  OnPropertyChanged("StuList"); }
        }

        private void LoadData()
        {
            //CurrentStudent = new Student();
            StuList =new ObservableCollection<Student>( studentService.GetAllStudents());
        }
        #endregion



        #region AddOperations
        private CrudCommand addCommand;

        public CrudCommand AddCommand
        {
            get { return addCommand; }
            
        }
        public void Add()
        {
            try
            {
                if(currentStudent.StudentId!=0&&currentStudent.FullName!="" && currentStudent.Grade != "")
                {
                    Student newStudent = new Student
                    {
                        FullName = currentStudent.FullName,
                        Grade = currentStudent.Grade,
                        StudentId = currentStudent.StudentId,
                        NumberOfVotes = 0


                    };

                    var isAdded = studentService.AddStudent(newStudent);
                    LoadData();
                    if (isAdded)
                    {
                        Message = "New student is added.";
                    }
                    else
                    {
                        Message = "Student could not added.";
                    }
                }
                else
                {
                    Message = "Please fill the empty fields.";
                }
               

                
            }
            catch (Exception e)
            {

                Message = e.Message;
            }
        }
        #endregion

        #region FindOperations
        private CrudCommand findCommand;

        public CrudCommand FindCommand
        {
            get { return findCommand; }
        }

        public void Find() {
            try
            {
                var student= studentService.Search(currentStudent.StudentId);
                if (student != null)
                {
                    CurrentStudent.StudentId = student.StudentId;
                    CurrentStudent.FullName = student.FullName;
                    CurrentStudent.Grade = student.Grade;
                }
                else
                {
                    Message = "Student could not found.";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region UpdateOperations

        private CrudCommand updateCommand;

        public CrudCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                var isUpdated = studentService.UpdateStudent(CurrentStudent);
                if (isUpdated)
                {
                    Message = "Student is updated";
                    LoadData();
                }
                else
                {
                    Message = "Student does not exist with the specified id.";
                }
            }
            catch (Exception e)
            {

                Message = e.Message;
            }   
        }
        #endregion

        #region DeleteOperations
        private CrudCommand deleteCommand;

        public CrudCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                var isDelete = studentService.DeleteStudent(currentStudent.StudentId);
                if (isDelete)
                {
                    Message = "Student is deleted.";
                    LoadData();
                }
                else
                {
                    Message = "Student does not exist with the specified id.";
                }
            }
            catch (Exception e)
            {

                Message = e.Message;
            }
        }

        #endregion

        #region VoteOperations

        private CrudCommand voteCommand;

        public CrudCommand VoteCommand
        {
            get { return voteCommand; }
        }
        public void Vote()
        {
            try
            {
                var isVoted = studentService.VoteStudent(currentStudent.StudentId);
                if (isVoted)
                {
                    Message = "Student is voted.";
                    LoadData();
                }
                else
                {
                    Message = "Student does not exist with the specified id.";
                }
            }
            catch (Exception e)
            {

                Message = e.Message;
            }
          
        }


        #endregion

        #region ElectionOperations
        private CrudCommand completeElectionCommand;        

        public CrudCommand CompleteElectionCommand
        {
            get { return completeElectionCommand; }
        }
        private void CompleteElection()
        {
            Student winningStudent = studentService.CompleteElection();

            if (winningStudent != null)
            {
                Message = winningStudent.FullName + " wins the election with the " + winningStudent.NumberOfVotes + " votes.";
            }
            
        }

        #endregion
    }
}

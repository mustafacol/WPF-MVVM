using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustafaCol.Model
{
    class StudentService
    {
        private static List<Student> studentList;

        public StudentService()
        {
            studentList = new List<Student>()
            {
                new Student{StudentId=21602625,FullName="Mustafa Col", Grade="4th", NumberOfVotes=2},
                new Student{StudentId=21509635,FullName="Arda Ersoy", Grade="3th", NumberOfVotes=3},
                new Student{StudentId=21901452,FullName="Ali Veli", Grade="2th", NumberOfVotes=4},
            };

        }

        public List<Student> GetAllStudents()
        {
            return studentList;
        }
        public bool AddStudent(Student student)
        {
           
            studentList.Add(student);
            return true;
        }
        public bool UpdateStudent(Student student)
        {
            bool isUpdated = false;
            int index;
            index = FindStudent(student.StudentId);
            if (index != -1)
            {
                studentList[index].FullName = student.FullName;
                studentList[index].Grade = student.Grade;
                studentList[index].StudentId = student.StudentId;
                isUpdated = true;
            }

            return isUpdated;

        }
        public bool DeleteStudent(int id)
        {
            bool isDeleted = false;
            int index = FindStudent(id);
            if (index != -1)
            {
                studentList.RemoveAt(index);
                isDeleted = true;
            }
            return isDeleted;
            
        }
        public bool VoteStudent(int id)
        {
            int index = FindStudent(id);
            if (index != -1)
            {
                studentList[index].NumberOfVotes++;
                return true;
            }
            return false;
        }

        public Student Search(int id)
        {
            return studentList.FirstOrDefault(e => e.StudentId == id);
        }
        public int FindStudent(int id)
        {
            for(int i = 0; i < studentList.Count; i++)
            {
                if (studentList[i].StudentId == id)
                {
                    return  i;
                }
            }
            return -1;
        }
        public Student CompleteElection()
        {
            Student maxVoteStudent=studentList[0];
            for(int i = 1; i<studentList.Count; i++)
            {
                if (studentList[i].NumberOfVotes > maxVoteStudent.NumberOfVotes)
                {
                    maxVoteStudent = studentList[i];
                }
            }
            return maxVoteStudent;
        }

        
    }
}

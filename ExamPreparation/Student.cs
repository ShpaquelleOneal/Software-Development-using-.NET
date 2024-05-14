using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = SQLite.ColumnAttribute;
using TableAttribute = SQLite.TableAttribute;

namespace ExamPreparation
{
    [Table("Student")]
    public class Student
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("student_id")]
        public int StudentId { get; set; }
        [Column("student_name")]
        public string Name { get; set; }
        [Column("student_surname")]
        public string Surname { get; set; }
        [Column("enrolment_date")]
        public DateTime EnrolmentDate { get; set; }
    }
}

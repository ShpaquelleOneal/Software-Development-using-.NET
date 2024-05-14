using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPreparation
{
    public class LocalDBService
    {
        private const string DB_NAME = "EXAM_PREPARATION.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService ()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Student>().Wait();
        }

        // Get a list of customers
        public async Task<List<Student>> GetStudents()
        {
            return await _connection.Table<Student>().ToListAsync();
        }

        // get student by id
        public async Task<Student> GetById(int studentId)
        {
            return await _connection.Table<Student>().Where(x => x.StudentId == studentId).FirstOrDefaultAsync();
        }

        // create student
        public async Task Create (Student student)
        {
            await _connection.InsertAsync(student);
        }

        // update student
        public async Task Update (Student student)
        {
            await _connection.UpdateAsync(student);
        }

        // delete student
        public async Task Delete(Student student)
        {
            await _connection.DeleteAsync(student);
        }
    }
}

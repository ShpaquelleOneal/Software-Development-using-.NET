namespace ExamPreparation
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDBService _localDBService;
        private int _editStudentId;

        public MainPage(LocalDBService dBService)
        {
            InitializeComponent();
            _localDBService = dBService;
            Task.Run( async () => listView.ItemsSource = await _localDBService.GetStudents());
        }

        private async void saveButton_Clicked (object sender, EventArgs e) 
        {
            if (_editStudentId == 0)
            {
                // add customer
                await _localDBService.Create(new Student
                {
                    Name = nameEntryField.Text,
                    Surname = surnameEntryField.Text,
                    EnrolmentDate = DateTime.Parse(enrolmentDateEntryField.Text)
                });
            }
            else
            {
                // edit customer
                await _localDBService.Update(new Student
                {
                    StudentId = _editStudentId,
                    Name = nameEntryField.Text,
                    Surname = surnameEntryField.Text,
                    EnrolmentDate = DateTime.Parse(enrolmentDateEntryField.Text)
                });

                _editStudentId = 0;
            }

            nameEntryField.Text = string.Empty;
            surnameEntryField.Text = string.Empty;
            enrolmentDateEntryField.Text = string.Empty;

            listView.ItemsSource = await _localDBService.GetStudents();
        }
        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var student = (Student)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editStudentId = student.StudentId;
                    nameEntryField.Text = student.Name;
                    surnameEntryField.Text = student.Surname;
                    enrolmentDateEntryField.Text = student.EnrolmentDate.ToString();
                    break;
                case "Delete":
                    await _localDBService.Delete(student);
                    listView.ItemsSource = await _localDBService.GetStudents();
                    break;
            }
        }
    }

}

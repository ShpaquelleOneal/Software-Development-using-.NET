namespace MAUIAppHomework2;

public partial class AddOrderPage : ContentPage
{
	public AddOrderPage()
	{
		InitializeComponent();
	}

    private async void AddOrderButton_Clicked(object sender, EventArgs e)
    {
        // retrieve input values from the fields
        string orderDate = OrderDateEntry.Text;
        string state = OrderStatePicker.SelectedItem as string;
        string customerName = CustomerNameEntry.Text;
        string customerSurname = CustomerSurnameEntry.Text;
        string employeeName = EmployeeNameEntry.Text;
        string employeeSurname = EmployeeSurnameEntry.Text;


        // validate input
        if (string.IsNullOrWhiteSpace(orderDate) ||
            string.IsNullOrWhiteSpace(state) ||
            string.IsNullOrWhiteSpace(customerName) ||
            string.IsNullOrWhiteSpace(customerSurname) ||
            string.IsNullOrWhiteSpace(employeeName) ||
            string.IsNullOrWhiteSpace(employeeSurname)
            )
        {
            // return alert message to user if not all fields are filled
            await DisplayAlert("Alert", "Please fill all fields.", "OK");
            return;
        }

        string customerFullname = customerName + " " + customerSurname;
        string employeeFullname = employeeName + " " + employeeSurname;

        // add the order
        App.dm.AddOrder(orderDate, state, customerFullname, employeeFullname);

        // show success message
        await DisplayAlert("Success", "Order added successfully.", "OK");

        // navigate back
        await Shell.Current.GoToAsync("..");
    }
}
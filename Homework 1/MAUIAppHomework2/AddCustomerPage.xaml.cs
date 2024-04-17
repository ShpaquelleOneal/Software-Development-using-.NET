namespace MAUIAppHomework2;

public partial class AddCustomerPage : ContentPage
{
	public AddCustomerPage()
	{
		InitializeComponent();
	}

    private async void AddCustomerButton_Clicked(object sender, EventArgs e)
    {
        string customerName = CustomerNameEntry.Text;
        string customerSurname = CustomerSurnameEntry.Text;
        string customerEmail = CustomerEmailEntry.Text;

        // input validation
        if (customerName != "" && customerSurname != "" && customerEmail != "")
        {
            // add new person
            App.dm.AddPerson(customerName + " " + customerSurname, customerEmail);

            // display confirmation message
            await DisplayAlert("Alert", "Customer has been added.", "OK");

            // return one step back
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            // if input is invalid, display alert message
            await DisplayAlert("Alert", "Please enter all data.", "OK");
        }

    }
}
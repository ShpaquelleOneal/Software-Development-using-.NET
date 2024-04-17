namespace MAUIAppHomework2;

public partial class ManageDataPage : ContentPage
{
	public ManageDataPage()
	{
		InitializeComponent();
	}

    // methods for page navigation
	private async void ViewStoredData_Clicked (object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(ViewDataPage));
    }

    private async void AddNewProduct_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddProductPage));
    }

    private async void AddNewCustomer_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddCustomerPage));
    }

    private async void AddNewOrder_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddOrderPage));
    }
    private async void CreateTestData_Clicked(object sender, EventArgs e)
    {
        App.dm.CreateTestData();
        await DisplayAlert("Message", "Test data successfully created.", "OK");
    }

}
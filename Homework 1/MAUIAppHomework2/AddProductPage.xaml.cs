namespace MAUIAppHomework2;

public partial class AddProductPage : ContentPage
{
	public AddProductPage()
	{
		InitializeComponent();
	}

    private async void AddProductButton_Clicked(object sender, EventArgs e)
	{
		// retrieve input data
		string productName = ProductNameEntry.Text;
        string productPrice = ProductPriceEntry.Text;

		// input validation 
		if (productName != "" && productPrice != "") 
		{
			// add new product and show confirmation message
            App.dm.AddProduct(productName, productPrice);
            
            await DisplayAlert("Alert", "Product has been added.", "OK");

			// go back on a previous page
            await Shell.Current.GoToAsync("..");
        }
		else
		{
            // display error message in case of some fields not filled
            await DisplayAlert("Alert", "Please enter all data.", "OK");
        }
		
    }
}
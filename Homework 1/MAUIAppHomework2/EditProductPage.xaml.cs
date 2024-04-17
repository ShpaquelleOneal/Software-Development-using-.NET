using Homework1;

namespace MAUIAppHomework2;

public partial class EditProductPage : ContentPage
{
	public EditProductPage(Product product)
	{
		InitializeComponent();

        // create labels to display old data
        var nameLabel = new Label
        {
            Text = $"Old Name: {product.ProductName}",
            FontSize = 12,
            HorizontalOptions = LayoutOptions.Center
        };

        var priceLabel = new Label
        {
            Text = $"Old Price: {product.Price}",
            FontSize = 12,
            HorizontalOptions = LayoutOptions.Center
        };

        // create input fields for editing
        var newNameEntry = new Entry
        {
            Placeholder = "Enter new name",
            HorizontalOptions = LayoutOptions.Center
        };

        var newPriceEntry = new Entry
        {
            Placeholder = "Enter new price",
            HorizontalOptions = LayoutOptions.Center
        };

        // create a button to save changes
        var saveButton = new Button
        {
            Text = "Save Changes",
            FontSize = 12,
            HorizontalOptions = LayoutOptions.Center
        };

        saveButton.Clicked += (sender, e) =>
        {
            // update product with new data
            if (!string.IsNullOrEmpty(newNameEntry.Text))
            {
                product.ProductName = newNameEntry.Text;
            }

            if (!string.IsNullOrEmpty(newPriceEntry.Text))
            {
                product.Price = double.Parse(newPriceEntry.Text);
            }

            // navigate back to the previous page
            Navigation.PopAsync();
        };

        // add labels and input fields to the layout
        Content = new StackLayout
        {
            Children =
            {
                nameLabel,
                priceLabel,
                newNameEntry,
                newPriceEntry,
                saveButton
            }
        };
    }
}
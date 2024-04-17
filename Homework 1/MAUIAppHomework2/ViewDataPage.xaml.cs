using Homework1;

namespace MAUIAppHomework2;

public partial class ViewDataPage : ContentPage
{
    public ViewDataPage()
    {
        InitializeComponent();
    }

    private async void ViewProductData_Clicked(object sender, EventArgs e)
    {
        ViewProductButton.IsEnabled = false;


        // retrieve all available data
        string products = App.dm.Print();

        // show a message if no records are stored
        if (products == "")
        {
            var label = new Label
            {
                Text = $"No data available.",
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center
            };
            ProductStack.Children.Add(label);
        }

        string[] lines = products.Split(new[] { '\n', '\r' });
        // iterate over each line
        foreach (string line in lines)
        {
            string[] parts = line.Split(":");
            if (parts.Length == 2)
            {
                string type = parts[0]; // identificator of Object
                string data = parts[1]; // data

                // if Product line is read
                if (type == "PR")
                {
                    // split product data based on semicolon, if the format is correct
                    string[] productData = data.Split(";");
                    if (productData.Length == 2)
                    {
                        string result = productData[0] + ' ' + productData[1];

                        // create a new label for each item
                        var label = new Label
                        {
                            Text = $"{result}",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };

                        // create a new button for deletion
                        var deleteButton = new Button
                        {
                            Text = "Delete",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };

                        // create a new button for edit
                        var editButton = new Button
                        {
                            Text = "Edit",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };

                        // attach an event handler for the button click
                        deleteButton.Clicked += async (sender, e) =>
                        {
                            // if the value exists, then confirm with user and delete the entry
                            if (App.dm.products.TryGetValue(productData[0], out Product product))
                            {
                                bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this item?", "Yes", "No");
                                if (answer)
                                {
                                    // remove product and all elements in UI
                                    App.dm.products.Remove(productData[0]);
                                    ProductStack.Children.Remove(label);
                                    ProductStack.Children.Remove(deleteButton);
                                    ProductStack.Children.Remove(editButton);
                                }
                            }
                        };

                        // attach an event handler for the edit button click
                        editButton.Clicked += async (sender, e) =>
                        {
                            if (App.dm.products.TryGetValue(productData[0], out Product product))
                            {
                                // navigate to change Product data page
                                await Navigation.PushAsync(new EditProductPage(product));
                            }
                        };
                        
                        // display all needed elements
                        ProductStack.Children.Add(label);
                        ProductStack.Children.Add(deleteButton);
                        ProductStack.Children.Add(editButton);
                    }
                }
            }
        }
    }

    // method to view all person data
    private async void ViewPersonsData_Clicked(object sender, EventArgs e)
    {
        // remove button press ability to not show duplicate data on screen
        ViewPersonButton.IsEnabled = false;

        // retrieve all available data
        string person = App.dm.Print();

        // show a message if no records are stored
        if (person == "")
        {
            var label = new Label
            {
                Text = $"No data available.",
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center
            };
            PersonStack.Children.Add(label);
        }

        string[] lines = person.Split(new[] { '\n', '\r' });
        // iterate over each line
        foreach (string line in lines)
        {
            string[] parts = line.Split(":");
            if (parts.Length == 2)
            {
                string type = parts[0]; // identificator of Object
                string data = parts[1]; // data

                // if Person line is read
                if (type == "PE")
                {
                    // split person data based on semicolon
                    string[] personData = data.Split(";");

                    // a Customer if line contains 2 strings
                    if (personData.Length == 2)
                    {
                        string result = personData[0] + ' ' + personData[1];

                        // create a new label for each item
                        var label = new Label
                        {
                            Text = $"Customer: {result}",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        PersonStack.Children.Add(label);
                    }
                    // an Employee if line contains 4 strings
                    else if (personData.Length == 4)
                    {
                        string result = personData[0] + ' ' + personData[1];
                        // create a new label for each item
                        var label = new Label
                        {
                            Text = $"Employee: {result}",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        PersonStack.Children.Add(label);
                    }
                }
            }
        }
    }

    private async void ViewOrdersData_Clicked(object sender, EventArgs e)
    {
        // remove button press ability to not show duplicate data on screen
        ViewOrderButton.IsEnabled = false;

        // retrieve all available data
        string order = App.dm.Print();

        // show a message if no records are stored
        if (order == "")
        {
            var label = new Label
            {
                Text = $"No data available.",
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center
            };
            OrderStack.Children.Add(label);
        }

        string[] lines = order.Split(new[] { '\n', '\r' });
        // iterate over each line
        foreach (string line in lines)
        {
            string[] parts = line.Split(":");
            if (parts.Length == 2)
            {
                string type = parts[0]; // identificator of Object
                string data = parts[1]; // data

                // if Order line is read
                if (type == "OR")
                {
                    string[] orderData = data.Split(";");
                    if (orderData.Length == 5)
                    {
                        string result = string.Join(' ', orderData[0], orderData[1], orderData[2], orderData[3], orderData[4]);
                        // create a new label for each item
                        var label = new Label
                        {
                            Text = $"Order: {result}",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        OrderStack.Children.Add(label);

                    }
                }
                // if OrderDetails line is read
                else if (type == "OD")
                {
                    string[] orderData = data.Split(";");
                    if (orderData.Length == 3)
                    {
                        string result = string.Join(' ', orderData[0], orderData[2]);
                        // create a new label for each item
                        var label = new Label
                        {
                            Text = $"Detail: {result}",
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        OrderStack.Children.Add(label);

                    }
                }
            }
        }
    }

}
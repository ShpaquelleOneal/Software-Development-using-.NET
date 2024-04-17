namespace MAUIAppHomework2
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void DataManagement_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ManageDataPage));
        }

        private async void LoadData_Clicked(object sender, EventArgs e)
        {
            if(App.dm.Load("C:\\Test\\data.txt"))
            {
                await DisplayAlert("Alert", "Data successfully loaded.", "OK");
            } 
            else
            {
                await DisplayAlert("Alert", "Data was not loaded.", "OK");
            }
        }

        private async void SaveData_Clicked(object sender, EventArgs e)
        {
            if (App.dm.Save("C:\\Test\\data.txt"))
            {
                await DisplayAlert("Alert", "Data successfully saved.", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Data was not saved.", "OK");
            }
        }

        private async void ClearData_Clicked(object sender, EventArgs e)
        {
            
            if (App.dm.Reset())
            {
                await DisplayAlert("Alert", "Data successfully cleared.", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Data was not cleared.", "OK");
            }

        }
    }

}

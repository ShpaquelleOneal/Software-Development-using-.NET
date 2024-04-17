using Homework1;

namespace MAUIAppHomework2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // register new routes for all pages
            Routing.RegisterRoute(nameof(ManageDataPage), typeof(ManageDataPage));
            Routing.RegisterRoute(nameof(ViewDataPage), typeof(ViewDataPage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(AddCustomerPage), typeof(AddCustomerPage));
            Routing.RegisterRoute(nameof(AddOrderPage), typeof(AddOrderPage));
            Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));

            MainPage = new AppShell();

            dm = new ManagementSystem();
        }

        public static ManagementSystem dm { get; set; }
    }
}

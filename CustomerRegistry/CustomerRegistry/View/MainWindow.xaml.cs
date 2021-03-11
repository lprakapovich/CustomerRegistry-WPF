using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomerRegistry.Model;
using CustomerRegistry.View;
using CustomerRegistry.ViewModel;

namespace CustomerRegistry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _dataContext;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            _dataContext = (MainViewModel) this.DataContext;
        }

        private void OnAddCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {
            CustomerEditorViewModel customerEditorViewModel = new CustomerEditorViewModel(new Customer("Liza", "P", null));
            _dataContext.CustomerEditorViewModel = customerEditorViewModel;

            CustomerEditorView editor = new CustomerEditorView(customerEditorViewModel);
            editor.Show();
        }
    }
}

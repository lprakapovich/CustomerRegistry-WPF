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
using System.Windows.Shapes;
using CustomerRegistry.ViewModel;

namespace CustomerRegistry.View
{
    /// <summary>
    /// Interaction logic for CustomerEditorView.xaml
    /// </summary>
    public partial class CustomerEditorView : Window
    {
        private readonly CustomerEditorViewModel _dataContext;  
        public CustomerEditorView()
        {
            InitializeComponent();
        }

        public CustomerEditorView(CustomerEditorViewModel dataContext) : this()
        {
            this.DataContext = dataContext;
            _dataContext = dataContext;
            _dataContext.CloseWindow += OnCancel;
        }

        void OnCancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}

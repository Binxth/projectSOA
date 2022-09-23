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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for ServicesScreen.xaml
    /// </summary>
    public partial class ServicesScreen : Window
    {
        UserModel user;
        
        public ServicesScreen()
        {
            InitializeComponent();
        }

        public ServicesScreen(UserModel user)
        {
            this.user = user;
            InitializeComponent();
        }
    }
}

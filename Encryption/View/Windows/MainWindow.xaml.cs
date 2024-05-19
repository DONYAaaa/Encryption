using Encryption.ViewModel;
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

namespace Encryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Lab2VM lab2VM = new Lab2VM();
            Lab2.DataContext = lab2VM;
            
            Lab3VM lab3VM = new Lab3VM();
            Lab3.DataContext = lab3VM;
            DG.ItemsSource = lab3VM.Strochechki;
            
            Lab4VM lab4VM = new Lab4VM();
            Lab4.DataContext = lab4VM;

            Lab5VM lab5VM = new Lab5VM();
            Lab5.DataContext = lab5VM;
            DG.ItemsSource = lab5VM.Strochechki;

            Tab.SelectedIndex = 4;
        }
    }
}

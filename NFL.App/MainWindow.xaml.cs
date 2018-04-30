using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NFL.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load conferences
            cmbConference.ItemsSource = Catalogs.GetConferences();
            cmbDivision.IsEnabled = false;
            cmbTeam.IsEnabled = false;
        }

        private void cmbConference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int selectedIndex = cmbConference.SelectedIndex;
            if(cmbConference.SelectedItem != null)
            {
                Conference c = (Conference)cmbConference.SelectedItem;
                cmbDivision.ItemsSource = c.Divisions;
                cmbDivision.IsEnabled = true;
                cmbTeam.IsEnabled = false;
                cmbTeam.ItemsSource = null;
            }
        }

        private void cmbDivision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDivision.SelectedItem != null)
            {
                Division d = (Division)cmbDivision.SelectedItem;
                cmbTeam.ItemsSource = d.Teams;
                cmbTeam.IsEnabled = true;
            }
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewPlayer np = new NewPlayer();
            np.ShowDialog();
        }

        private void cmbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTeam.SelectedItem != null)
            {
                Team t = (Team)cmbTeam.SelectedItem;
                dtgPlayers.ItemsSource = t.GetPlayers(t.Id);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                this.Close();
        }

        
    }
}

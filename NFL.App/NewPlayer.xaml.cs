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
using System.Windows.Shapes;
//added
using Microsoft.Win32;
namespace NFL.App
{
    /// <summary>
    /// Interaction logic for NewPlayer.xaml
    /// </summary>
    public partial class NewPlayer : Window
    {
        public NewPlayer()
        {
            InitializeComponent();
            //load conferences
            cmbConference.ItemsSource = Catalogs.GetConferences();
            cmbPosition.ItemsSource = Catalogs.GetPositions();
            cmbDivision.IsEnabled = false;
            cmbTeam.IsEnabled = false;
            cmbPosition.IsEnabled = false;
        }

        private Player p = new Player();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
         
            OpenFileDialog dlgPicture = new OpenFileDialog(); 
            dlgPicture.Title = "Load Picture"; 
            dlgPicture.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg"; 
            if (dlgPicture.ShowDialog() == true) 
            {
                picture.Source = new BitmapImage(new Uri(dlgPicture.FileName)); 
                p.PictureUri = dlgPicture.FileName; 
            }
        }

        private void cmbConference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmbConference.SelectedItem != null)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           
            int dd, mm, yyyy;
            dd = int.Parse(txtdd.Text);
            mm = int.Parse(txtmm.Text);
            yyyy = int.Parse(txtyyyy.Text);
            p.DateOfBirth = DateTime.Parse(dd + "/" + mm + "/" + yyyy);
            p.FirstName = txtFN.Text;
            p.LastName = txtLN.Text;
            p.HeightInInches = double.Parse(txtHI.Text);
            p.WeightInPounds = double.Parse(txtWP.Text);


            Team t = (Team)cmbTeam.SelectedItem;
            Position po = (Position)cmbPosition.SelectedItem;
            
            
            if (p.Add(t.Id,po.Id))
                MessageBox.Show("Player added");
            else
                MessageBox.Show("Error adding player");

        }

        private void cmbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTeam.SelectedItem != null)
            {
                cmbPosition.IsEnabled = true;
            }
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            cmbConference.SelectedItem = null;
            cmbDivision.SelectedItem = null;
            cmbPosition.SelectedItem = null;
            cmbTeam.SelectedItem = null;
            txtdd.Text = "";
            txtFN.Text = "";
            txtHI.Text = "";
            txtLN.Text = "";
            txtmm.Text = "";
            txtWP.Text = "";
            txtyyyy.Text = "";
            picture.Source = null;
        }
    }
}
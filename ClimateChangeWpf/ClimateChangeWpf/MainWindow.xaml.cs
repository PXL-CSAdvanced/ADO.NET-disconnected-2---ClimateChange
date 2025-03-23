using ClimateChangeClassLibrary.DataAccess;
using ClimateChangeClassLibrary.Entities;
using System.Diagnostics.Metrics;
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

namespace ClimateChangeWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // TODO: koppel DataGrid's

        LoadComboBoxItems();
        CountriesComboBox.SelectedIndex = 0;

        // TODO: vul WorstYearsListBox met een gepaste List van TempChange objecten
    }

    private void LoadComboBoxItems()
    {
        throw new NotImplementedException();
    }

    private void CountriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void LoadListBoxItemsByCountry(Country country)
    {
        throw new NotImplementedException();
    }

    private ListBoxItem CreateListBoxItem(TempChange tc, bool isAddingCountryName)
    {
        throw new NotImplementedException();
    }
}
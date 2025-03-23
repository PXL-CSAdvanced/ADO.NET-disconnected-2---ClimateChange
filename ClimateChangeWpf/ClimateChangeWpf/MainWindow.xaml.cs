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

        //DataProcessor.InitializeDataSet();
        TempChangeDataGrid.ItemsSource = DataProcessor.GetTempChangeDataView();
        CountriesDataGrid.ItemsSource = DataProcessor.GetCountriesDataView();

        LoadComboBoxItems();
        CountriesComboBox.SelectedIndex = 0;

        WorstYearsListBox.Items.Clear();
        List<TempChange> worstYearsAfter2000 = DataProcessor.GetWorstYearsAfter2000();
        worstYearsAfter2000.ForEach(x =>
        {
            if (x.Change != null)
            {
                ListBoxItem item = CreateListBoxItem(x, true);
                WorstYearsListBox.Items.Add(item);
            }
        }
        );
    }

    private void LoadComboBoxItems()
    {
        DataProcessor.GetCountries().ForEach(c => CountriesComboBox.Items.Add(c));
    }

    private void CountriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CountriesComboBox.SelectedIndex != -1)
        {
            Country selectedCountry = (Country)CountriesComboBox.SelectedItem;

            CountryNameTextBlock.Text = selectedCountry.CountryName;
            CountryRegionTextBlock.Text = selectedCountry.Region;
            CountrySubRegionTextBlock.Text = selectedCountry.SubRegion;
            CountryFlagImage.Source = new BitmapImage(new Uri("images/flags/" + selectedCountry.ImageFilePath, UriKind.Relative));

            LoadListBoxItemsByCountry(selectedCountry);
        }
    }

    private void LoadListBoxItemsByCountry(Country country)
    {
        ListBoxTempChange.Items.Clear();
        var filteredTemp = DataProcessor.GetTempChangesByCountryName(country);
        filteredTemp.ForEach(x =>
        {
            if (x.Change != null)
            {
                ListBoxItem item = CreateListBoxItem(x, false);
                ListBoxTempChange.Items.Add(item);
            }
        }
        );
    }

    private ListBoxItem CreateListBoxItem(TempChange tc, bool isAddingCountryName)
    {
        ListBoxItem item = new ListBoxItem();
        if (tc.Change < 0.5)
        {
            item.Background = Brushes.GreenYellow;
        }
        else if (tc.Change < 1)
        {
            item.Background = Brushes.Yellow;
        }
        else if (tc.Change < 1.5)
        {
            item.Background = Brushes.Orange;
        }
        else if (tc.Change < 2)
        {
            item.Background = Brushes.OrangeRed;
        }
        else if (tc.Change < 2.5)
        {
            item.Background = Brushes.Red;
        }
        else
        {
            item.Background = Brushes.Purple;
        }
        if (isAddingCountryName)
        {
            item.Content = $"{tc.CountryName} {tc.Year}: {tc.Change}";
        }
        else
        {
            item.Content = $"{tc.Year}: {tc.Change}";
        }
        return item;
    }

    private void ExportXMLButton_Click(object sender, RoutedEventArgs e)
    {
        DataProcessor.ClimateChangeDataSet.WriteXml("climateChange.xml");
    }
}
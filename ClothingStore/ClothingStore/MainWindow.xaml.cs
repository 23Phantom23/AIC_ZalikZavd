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
using Microsoft.EntityFrameworkCore;

namespace ClothingStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void ClothesItemList_SelectedChanget(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private DataContext context { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            context = new DataContext();
            UpdateData();
        }

        public void UpdateData()
        {
            List<Type> DatabaseTypes = context.Types.Include(type => type.Clothes).ToList();
            TypesItemList.ItemsSource = DatabaseTypes;
            List<Clothing> DatabaseClothes = context.Clothes.Include(clothing => clothing.Type).ToList();
            ClothesItemList.ItemsSource = DatabaseClothes;

            TypeComboBox.ItemsSource = DatabaseTypes;

        }
        private bool IsEmpty(string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        private void CreateClothing(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var price = PriceTextBox.Text;
            var type = TypeComboBox.Text;


            if (IsEmpty(name) || IsEmpty(price) || type == null)
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                context.Clothes.Add(new Clothing() 
                {
                    Clothes_Name = name,
                    Price = Convert.ToInt32(price),
                    Id = Convert.ToInt32(type)
                });
                context.SaveChanges();
                UpdateData();
            }
            NameTextBox.Clear();
            PriceTextBox.Clear();
        }

        private void UpdateClothing(object sender, RoutedEventArgs e)
        {
            Clothing selectedClothes = ClothesItemList.SelectedItem as Clothing;
            var name = NameTextBox.Text;
            var price = PriceTextBox.Text;
            var type = TypeComboBox.Text;

            if (IsEmpty(name) == false && IsEmpty(price) == false && type != null)
            {
                Clothing clothing = context.Clothes.Find(selectedClothes.Clothes_number);
                clothing.Clothes_Name = name;
                clothing.Price = Convert.ToInt32(price);
                clothing.Id = Convert.ToInt32(type);

                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            NameTextBox.Clear();
            PriceTextBox.Clear();
            TypeComboBox.SelectedItem = " ";

        }

        private void DeleteClothing(object sender, RoutedEventArgs e)
        {
            if (ClothesItemList.SelectedItem is Clothing selectedClothing)
            {
                Clothing clothing = selectedClothing;
                context.Clothes.Remove(clothing);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateType(object sender, RoutedEventArgs e)
        {
            var type = TypeTextBox.Text;


            if (IsEmpty(type))
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                context.Types.Add(new Type()
                {
                    Type_Name = type

                });
                context.SaveChanges();
                UpdateData();
            }
            TypeTextBox.Clear();
        }

        private void UpdateType(object sender, RoutedEventArgs e)
        {
            Type selectedType = TypesItemList.SelectedItem as Type;
            var type = TypeTextBox.Text;


            if (IsEmpty(type) == false)
            {
                Type Type = context.Types.Find(selectedType.Type_Id);
                Type.Type_Name = type;

                context.SaveChanges();
                UpdateData();

            }
            else
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            TypeTextBox.Clear();
        }

        private void DeleteType(object sender, RoutedEventArgs e)
        {
            if (TypesItemList.SelectedItem is Type selectedType)
            {
                Type type = selectedType;
                context.Types.Remove(type);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search_Type(object sender, RoutedEventArgs e)
        {
            var types = "";
            var type = TypeComboBox.Text;
            foreach (Clothing clothing in ClothesItemList.Items)
            {

                if (Convert.ToInt32(clothing.Id) == Convert.ToInt32(type))
                {
                    types = types + clothing.Clothes_number + "\t" + clothing.Clothes_Name + "\t" + clothing.Price + "\t" + clothing.Id + "\n";
                }

            }
            if (types == "")
            {
                types = "Not Found";
            }
            MessageBox.Show($"result:\n{types}", "Result of Search", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Max(object sender, RoutedEventArgs e)
        {
            int sum = 0, a = -1;
            foreach (Clothing clothing in ClothesItemList.Items)
            {
                a++;
            }
            int[] arr = new int[a];
            int b = a;
            a = 0;
            foreach (Clothing clothing in ClothesItemList.Items)
            {
                if (a < b)
                {
                    arr[a] = clothing.Price;
                    a++;
                }
            }
            int MaxV = arr.Max();
            MessageBox.Show($"Max values = {MaxV}", "Sum of prices", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Search_Name(object sender, RoutedEventArgs e)
        {
            var names = "";
            var name = NameTextBox.Text;
            foreach (Clothing clothing in ClothesItemList.Items)
            {

                if (clothing.Clothes_Name.IndexOf(name) != -1)
                {
                    names = names + clothing.Clothes_number + "\t" + clothing.Clothes_Name + "\t" + clothing.Price + "\t" + clothing.Id + "\n";
                }

            }
            if (names == "")
            {
                names = "Not Found";
            }
            MessageBox.Show($"result:\n{names}", "Result of Search", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
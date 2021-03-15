using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFEntityCoreExample.Entityes;

namespace WPFEntityCoreExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Category> Catsource ;
        ObservableCollection<Element> ELsource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            // создаем два объекта
            Category category = new() { Name = "Основное", Description = "Тест описания" };
            Category category2 = new() { Name = "Дополнительное" };

            Element element = new() { Name = "Элемент 1", Category = category };

            // добавляем их в бд
            db.Categories.Add(category);
            db.Categories.Add(category2);

            db.Elements.Add(element);
            db.SaveChanges();

            Console.WriteLine("Объекты успешно сохранены");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            Catsource = new(db.Categories);
            dataGrid.ItemsSource = Catsource;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            ELsource = new(db.Elements);
            dataGrid.ItemsSource = ELsource;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            db.Categories.RemoveRange(db.Categories);
            db.SaveChanges();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            db.Categories.UpdateRange(Catsource);
            db.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        readonly Context WindowDB = new(); // Плохо!

        public MainWindow()
        {
            InitializeComponent();

            Closed += (o, e) => WindowDB.Dispose();
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
            // TODO: dataGrid.ItemsSource = db.Categories.Local.ToObservableCollection();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            using Context db = new();
            ELsource = new(db.Elements.Include(c => c.Category).AsNoTracking()); // Явная подгрузка и без кеширования
            //ELsource = new(db.Elements); // Ленивая подгрузка глючная жопа
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
            WindowDB.SaveChanges();
            WindowDB.Categories.Load();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //dataGrid.ItemsSource = CatalogView.SelectAll(); // Не тру Вью

            using Context db = new();
            //dataGrid.ItemsSource = Element.ElementsByCategories(db, "%Основное%").ToList(); // Из хранимого запроса

            dataGrid.ItemsSource = db.CatalogTrueViews.ToList(); // тру Вью
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowDB.Categories.Load();
            dataGrid.ItemsSource = WindowDB.Categories.Local.ToObservableCollection();

        }
    }
}

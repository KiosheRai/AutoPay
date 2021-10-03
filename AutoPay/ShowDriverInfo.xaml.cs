using System;
using System.Collections.Generic;
using System.Data;
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

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для ShowDriverInfo.xaml
    /// </summary>
    public partial class ShowDriverInfo : Page
    {
        private int id;
        private int childCount;

        public ShowDriverInfo()
        {
            InitializeComponent();
            MessageBox.Show("Тестовая ветка.");
        }

        public ShowDriverInfo(int _id)
        {
            InitializeComponent();
            this.id = _id;

            showName();
            showInfo();
        }
        private void showName()
        {
            DataTable table = SQLbase.Select($"select FIO, children from Driver where id = {id}");
            NameOfDriver.Text = table.Rows[0][0].ToString();
            childCount = int.Parse(table.Rows[0][1].ToString());
        }
        private void showInfo()
        {   

            DataTable table = SQLbase.Select($"select FIO, birthday from Child where driver = {id}");
            string s = table.Rows[0][0].ToString();
            
            listChildren.ItemsSource = table.DefaultView;
        }

        private void Button_Main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Button_Drivers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Drivers.xaml", UriKind.Relative));
        }

        private void Button_Mark(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void DeleteChild(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddChild(object sender, RoutedEventArgs e)
        {
            string name = formName.Text.Trim();
            string surname = formSurnameName.Text.Trim();
            string secondname = formSecondName.Text.Trim();

            bool isGood = true;

            if (name == "")
            {
                isGood = false;

                formName.ToolTip = "Заполните поле!";
                formName.Foreground = Brushes.Red;
            }
            else
            {
                formName.ToolTip = "";
                formName.Foreground = Brushes.Black;
            }

            if (surname == "")
            {
                isGood = false;

                formSurnameName.ToolTip = "Заполните поле!";
                formSurnameName.Foreground = Brushes.Red;
            }
            else
            {
                formSurnameName.ToolTip = "";
                formSurnameName.Foreground = Brushes.Black;
            }

            if (secondname == "")
            {
                isGood = false;

                formSecondName.ToolTip = "Заполните поле!";
                formSecondName.Foreground = Brushes.Red;
            }
            else
            {
                formSecondName.ToolTip = "";
                formSecondName.Foreground = Brushes.Black;
            }

            foreach (Char x in name)
            {
                if (char.IsDigit(x))
                {
                    formName.ToolTip = "Требуется символьное значение!";
                    formName.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formName.ToolTip = "";
                    formName.Foreground = Brushes.Black;
                }
            }

            foreach (Char x in surname)
            {
                if (char.IsDigit(x))
                {
                    formSurnameName.ToolTip = "Требуется символьное значение!";
                    formSurnameName.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formSurnameName.ToolTip = "";
                    formSurnameName.Foreground = Brushes.Black;
                }
            }

            foreach (Char x in secondname)
            {
                if (char.IsDigit(x))
                {
                    formSecondName.ToolTip = "Требуется символьное значение!";
                    formSecondName.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formSecondName.ToolTip = "";
                    formSecondName.Foreground = Brushes.Black;
                }
            }

            if (DateT.SelectedDate == null)
            {
                AlarmMessageChildAdd.Visibility = Visibility.Visible;
                isGood = false;
            }
            else
            {
                AlarmMessageChildAdd.Visibility = Visibility.Hidden;
            }

            if (isGood)
            {
                DateTime d = DateT.SelectedDate.Value;
                SQLbase.Insert($"insert into Child(driver , FIO, birthday) values ({id}, '{name} {surname} {secondname}', '{d.Day}-{d.Month}-{d.Year}')");
                childCount += 1;
                SQLbase.Insert($"UPDATE driver SET children = '{childCount}' where id = {id}");
                MessageBox.Show("Ребёнок добавлен!");

                formName.Text = "";
                formSurnameName.Text = "";
                formSecondName.Text = "";
                DateT.DisplayDate = DateTime.Today;

                showInfo();
            }
        }
    }
}

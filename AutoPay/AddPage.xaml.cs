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

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
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
            NavigationService.Navigate(new Uri("/Mark.xaml", UriKind.Relative));
        }

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            string name = formName.Text.Trim();
            string surname = formSurnameName.Text.Trim();
            string secondname = formSecondName.Text.Trim();
            string experience = formExperience.Text.Trim();
            string rate = formRate.Text.Trim();

            bool isGood = true;

            if(name == "")
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

            if (experience == "")
            {
                isGood = false;

                formExperience.ToolTip = "Заполните поле!";
                formExperience.Foreground = Brushes.Red;
            }
            else
            {
                formExperience.ToolTip = "";
                formExperience.Foreground = Brushes.Black;
            }

            if (rate == "")
            {
                isGood = false;

                formRate.ToolTip = "Заполните поле!";
                formRate.Foreground = Brushes.Red;
            }
            else
            {
                formRate.ToolTip = "";
                formRate.Foreground = Brushes.Black;
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

            foreach (Char x in experience)
            {
                if (!char.IsDigit(x))
                {
                    formExperience.ToolTip = "Требуется числовое значение!";
                    formExperience.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formExperience.ToolTip = "";
                    formExperience.Foreground = Brushes.Black;
                }
            }

            foreach (Char x in rate)
            {
                if (!char.IsDigit(x))
                {
                    formRate.ToolTip = "Требуется числовое значение!";
                    formRate.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formRate.ToolTip = "";
                    formRate.Foreground = Brushes.Black;
                }
            }



            if (isGood)
            {
                SQLbase.Insert($"INSERT INTO Driver VALUES ('{name} {surname} {secondname}', {rate}, {experience}, 0)");

                MessageBox.Show("Водитель добавлен.");

                formName.Text = "";
                formSecondName.Text = "";
                formSurnameName.Text = "";
                formExperience.Text = "";
                formRate.Text = "";
            }
        }
    }
}

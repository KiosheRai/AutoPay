using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using Word = Microsoft.Office.Interop.Word;

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для CulcPage.xaml
    /// </summary>
    public partial class CulcPage : Page
    {
        private int id;
        private bool isCanWord = false;

        private string name;
        private string fDate;
        private string sDate;
        private string summa;
        private string b;
        private string d;

        public CulcPage()
        {
            InitializeComponent();
            MessageBox.Show("Отладка!");
        }

        public CulcPage(int _id)
        {
            this.id = _id;
            InitializeComponent();

            showName();
        }

        private void showName()
        {
            DataTable table = SQLbase.Select($"select FIO from Driver where id = {id}");
            showNameStr.Text = table.Rows[0][0].ToString();
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
            NavigationService.Navigate(new Uri("/PayMent.xaml", UriKind.Relative));
        }

        private void GoWord(object sender, RoutedEventArgs e)
        {
            if(isCanWord == false)
            {
                AlarmMessage.Content = "Создайте чек!";
                return;
            }

            AlarmMessage.Content = "";

            Report();
        }

        private void CuclButton(object sender, RoutedEventArgs e)
        {
            DateTime StartDate;
            DateTime EndDate;

            if (DateStart.SelectedDate == null)
            {
                AlarmMessage.Content = "Введите начальную дату!";
                return;
            }
            
            if (DateEnd.SelectedDate == null)
            {
                AlarmMessage.Content = "Введите конечную дату!";
                return;
            }

            StartDate = DateStart.SelectedDate.Value;
            EndDate = DateEnd.SelectedDate.Value;

            if(StartDate >= EndDate)
            {
                AlarmMessage.Content = "Введите верную дату!";
                return;
            }

            AlarmMessage.Content = "";

            if(showBonus.Text == "")
            {
                showBonus.Text = "0";
            }

            if(showDecrease.Text == "")
            {
                showDecrease.Text = "0";
            }

            DataTable summ = SQLbase.Select($"select sum(shiftt) from WorkDay where driver = {id} and datee > '{StartDate}' and datee < '{EndDate}' ");

            SQLbase.Insert($"insert into Bonus(datee, summa) values (GETDATE(), {showBonus.Text})");
            SQLbase.Insert($"insert into Decrease(datee,summa) values (GETDATE(), {showDecrease.Text})");
            DataTable table = SQLbase.Select("SELECT MAX(id) FROM Bonus");
            DataTable table1 = SQLbase.Select("SELECT MAX(id) FROM Decrease");

            showSummaBefore.Text = (Int32.Parse(summ.Rows[0][0].ToString()) * 15).ToString();
            showSumma.Text = (Int32.Parse(showSummaBefore.Text) - Int32.Parse(showDecrease.Text) + Int32.Parse(showBonus.Text)).ToString();

            SQLbase.Insert($"insert into Paycheck(driver, starte, ende, summa, decrease, bonus) values ({id}, '{StartDate}', '{EndDate}', {showSumma.Text}, {table.Rows[0][0]}, {table1.Rows[0][0]})");

            name = showNameStr.Text;
            fDate = $"{StartDate.Day}:{StartDate.Month}:{StartDate.Year}";
            sDate = $"{EndDate.Day}:{EndDate.Month}:{EndDate.Year}";
            summa = showSumma.Text;
            b = showBonus.Text;
            d = showDecrease.Text;

            isCanWord = true;
        }

        private void showBonus_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
        }

        private void Report()
        {
            Thread t = new Thread(ReportGo);
            t.Start();
        }

        private void ReplaceWordStub(String stubToReplace, String Text, Word.Document word)
        {
            var range = word.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: Text);
        }

        private void ReportGo()
        {
            try
            {
                var word = new Word.Application();
                word.Visible = false;

                var worddoc = word.Documents.Open($"{Environment.CurrentDirectory}/PaymentMaket.DOCX");

                ReplaceWordStub("{name}", name, worddoc);
                ReplaceWordStub("{dateO}", fDate, worddoc);
                ReplaceWordStub("{dateT}", sDate, worddoc);
                ReplaceWordStub("{summ}", summa, worddoc);
                ReplaceWordStub("{bon}", b, worddoc);
                ReplaceWordStub("{decr}", d, worddoc);


                worddoc.SaveAs2($"{Environment.CurrentDirectory}/Payment.docx");
                MessageBox.Show("Отчёт создан!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

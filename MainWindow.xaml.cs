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
using System.IO;

namespace StrakhFed
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();
        bool truecapcha = false;
        

        public MainWindow()
        {
            for (int i = 1; i <= 5; i++)
            {
                people.Add(new Person
                {
                    id = i,
                    Name = $"User{i}",
                    Login = $"Log{i}",
                    Password = $"Passw{i}"
                }
                ); 
            }

            InitializeComponent();
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string[] LogPas = streamReader.ReadToEnd().Split(' ');
                    tbLog.Text = LogPas[0].Trim();
                    tbPass.Text = LogPas[1].Trim();
                }
            }
        }

        string path = @"C:\Users\WSR\Desktop\LogPas.txt";


        private string capcha()
        {
            String allowchar = " ";

            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";

            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] a = { ',' };

            String[] ar = allowchar.Split(a);

            String pwd = " ";

            string temp = " ";

            Random r = new Random();

            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;

            }

            txtCapch.Text = pwd;

            return pwd;
        }
        

        private void btnCapch_Click(object sender, RoutedEventArgs e)
        {
            tbCapch.Text = capcha();
        }


        private void btnSign_Click(object sender, RoutedEventArgs e)
        {

            var user = people.Where(i => i.Login == tbLog.Text && i.Password == tbPass.Text).FirstOrDefault();

            if (user != null && !truecapcha)
            {
                WindowSing windowSing = new WindowSing(user);
                windowSing.ShowDialog();

                if (chbRememb.IsChecked == true)
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        streamWriter.Write(tbLog.Text + " " + tbPass.Text);
                        streamWriter.Close();
                    }
                }
                return;

                
            }
            else if (user != null && !truecapcha)
            {
                if (capcha() == tbCapch.Text)
                {
                    WindowSing windowSing = new WindowSing();
                    windowSing.ShowDialog();
                }
                else
                {
                    txtCapch.Text = capcha();
                }
            }

            else
            {
                MessageBox.Show("Пользователь не найден.");

                tbCapch.Visibility = Visibility.Visible;
                txtCapch1.Visibility = Visibility.Visible;
                txtCapch.Visibility = Visibility.Visible;
                imgCapcha.Visibility = Visibility.Visible;
                btnCapch.Visibility = Visibility.Visible;
                txtCapch.Text = capcha();
                truecapcha = true;
            }
           



            //if ((user.Login != tbLog.Text) || (user.Password != tbPass.Text) || (capcha() != tbCapch.Text))
            //{
            //    txtCapch.Text = capcha();
            //}
            //else if ((user.Login == tbLog.Text) & (user.Password == tbPass.Text) & (capcha() == tbCapch.Text))
            //{
            //    WindowSing windowSing = new WindowSing();
            //    windowSing.ShowDialog();
            //}

           

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void chbRememb_Checked(object sender, RoutedEventArgs e)
        {
            StreamReader streamReader = new StreamReader(path);
           

        }
    }
}

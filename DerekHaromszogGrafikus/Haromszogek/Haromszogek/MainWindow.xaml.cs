using System.Windows;
using Microsoft.Win32;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Generic;
using System;
using System.Windows.Controls;
namespace haromszog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DHaromszog> haromszogek;
        public MainWindow()
        {
            InitializeComponent();
            haromszogek = new List<DHaromszog>();
        }
        List<DHaromszog> Haromszogek { get => haromszogek; set => haromszogek = value; }
        private void Btn_1_betolt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                gbox_1.Items.Clear();
                gbox_2.Items.Clear();
                StreamReader sr = new StreamReader(fileDialog.FileName);
                int sorszam = 1;
                while (!sr.EndOfStream)
                {
                    try
                    {
                        DHaromszog dHaromszog = new DHaromszog(sr.ReadLine(), sorszam);
                        gbox_2.Items.Add(String.Format("{0}. sor a={1} b={2} c={3}", sorszam, dHaromszog.AOldal, dHaromszog.BOldal, dHaromszog.COldal));
                        Haromszogek.Add(dHaromszog);
                    }
                    catch (AOldalhiba o)
                    {
                        gbox_1.Items.Add(o.HibaFormat(sorszam));
                    }
                    catch (BOldalhiba o)
                    {
                        gbox_1.Items.Add(o.HibaFormat(sorszam));
                    }
                    catch (COldalhiba o)
                    {
                        gbox_1.Items.Add(o.HibaFormat(sorszam));
                    }
                    catch (Novekvohiba n)
                    {
                        gbox_1.Items.Add(n.HibaFormat(sorszam));
                    }
                    catch (Derekszoghiba d)
                    {
                        gbox_1.Items.Add(d.HibaFormat(sorszam));
                    }
                    catch (Megszerkeszthetohiba m)
                    {
                        gbox_1.Items.Add(m.HibaFormat(sorszam));
                    }
                    sorszam++;
                }
                sr.Close();
            }
        }
        private void Gbox_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kerulet.Text = String.Format("Kerület = {0}", Haromszogek[(sender as ListBox).SelectedIndex].Kerulet);
            Terulet.Text = String.Format("Terület = {0}", Haromszogek[(sender as ListBox).SelectedIndex].Terulet);
        }

        class DHaromszog
        {
            private double aOldal;
            private double bOldal;
            private double cOldal;
            public double AOldal { get => aOldal; set => aOldal = value; }
            public double BOldal { get => bOldal; set => bOldal = value; }
            public double COldal { get => cOldal; set => cOldal = value; }
            public DHaromszog(string sor, int sorSzáma)
            {
                string[] sorE = sor.Split(' ');
                AOldal = double.Parse(sorE[0]);
                BOldal = double.Parse(sorE[1]);
                COldal = double.Parse(sorE[2]);
                SorSzama = sorSzáma;
                if (AOldal <= 0)
                    throw new AOldalhiba(sorSzáma);
                if (BOldal <= 0)
                    throw new BOldalhiba(sorSzáma);
                if (COldal <= 0)
                    throw new COldalhiba(sorSzáma);
                if (!this.EllNovekvoSorrend)
                    throw new Novekvohiba(sorSzáma);
                if (!this.EllMegszerkesztheto)
                    throw new Megszerkeszthetohiba(sorSzáma);
                if (!this.EllDerekszogu)
                    throw new Derekszoghiba(sorSzáma);
            }
            private bool EllNovekvoSorrend
            {
                get
                {
                    if (AOldal <= BOldal && BOldal <= COldal)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            private bool EllMegszerkesztheto
            {
                get
                {
                    if (AOldal + BOldal > COldal)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            private bool EllDerekszogu
            {
                get
                {
                    double a = Math.Pow(AOldal, 2);
                    double b = Math.Pow(BOldal, 2);
                    double c = Math.Pow(COldal, 2);

                    if (a + b == c)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            public int SorSzama
            {
                get;
                set;
            }
            public double Terulet
            {
                get { return (AOldal * BOldal) / 2; }
            }
            public double Kerulet
            {
                get { return AOldal + BOldal + COldal; }
            }
        }
    }
}

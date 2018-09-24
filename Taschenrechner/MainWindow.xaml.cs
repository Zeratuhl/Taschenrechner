using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Taschenrechner
{   // Partielle Classe Mainwindow erbt von Window
    // Partiel erlaubt es eine Klasse zu teilen und diese an mehrere stellen zu deklarieren
    public partial class MainWindow : Window
    {

        public MainWindow()
        {   // erstellt und läd das Interface
            InitializeComponent();
            // Startseite auswählen
            MainFrame.Navigate(new Uri("XAML/AnsichtStandard.xaml", UriKind.Relative));
            // Breite reduzieren um History zu verstecken
            this.Width = 375;
        }


        // schaltet die Ansichten durch
        private void Menü_Click(object sender, RoutedEventArgs e)
        {   // Da wir wissen, dass in diesem sender-Objekt nur Objekte vom Typ MenuItem sind, geben wir dies an und
            // lesen so gezielt den Header als string aus
            string menüauswahl = ((MenuItem)sender).Header.ToString();

            switch (menüauswahl)
            {
                case "Standard":
                    // Läde die Ansicht des Standardrechners
                    MainFrame.Navigate(new Uri("XAML/AnsichtStandard.xaml", UriKind.Relative));                                                  
                    break;

                case "Wissenschaftlich":
                    MainFrame.Navigate(new Uri("XAML/AnsichtWissenschaftlich.xaml", UriKind.Relative));
                    break;

                case "Programmierung":
                    MainFrame.Navigate(new Uri("XAML/AnsichtProgrammierung.xaml", UriKind.Relative));
                    break;

                case "History":
                    // Prüfung auf Fenstergröße um zu sehen ob History an oder aus ist. Dies ermöglicht ein ein- und
                    // ausblenden der History
                    if (this.Width == 375)
                    {
                        // Ändert die Fenstergröße
                        this.Width = 575;
                    }
                    else
                    {
                        this.Width = 375;
                    }
                    break;

            }
        }     
    }
}
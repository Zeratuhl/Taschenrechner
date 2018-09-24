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

namespace Taschenrechner.XAML
{
    /// <summary>
    /// Interaktionslogik für AnsichtWissenschaftlich.xaml
    /// </summary>
    public partial class AnsichtWissenschaftlich : Page
    {
        // Definition der Variablen
        // Beinhaltet den Wert den aktuellen Buttons
        string content = "";
        // Beinhaltet die aktuelle Zahl
        double aktuellerWert = 0;
        // String welche die Eingabe aufnimmt
        string eingabe = "";
        // beinhaltet die zuletzt gewählte Rechenoperation
        string letzterechenOperation = "=";
        // Erst Zahl oder Ergebnis
        double letztesErgebnis = 0.0;
        // Soll prüfen ob bereits ein Komma im Wert ist
        bool komma = false;

        public AnsichtWissenschaftlich()
        {
            InitializeComponent();
        }


        // Wird aufgerufen wenn ein Button von 0 - 9 oder das Komma gedrückt wird
        private void button_Click(object sender, RoutedEventArgs e)
        {
            content = ((Button)sender).Content.ToString();

            if (komma && content.Equals(","))
            {
                buttonLöschen_Click(sender, e);
                MessageBox.Show("Ehrlich? 2 Kommata in einem Wert?", "Netter Versuch", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!komma && content.Equals(","))
            {   // markierung, das Komma gesetzt wurde
                komma = true;
                // setzt den string eingabe aus den einzelnen Buttons die geklickt werden zusammen
                eingabe += content;
                // schreibt die eingegeben Zahlen/buchstaben in die Hauptanzeige
                Ausgabe.Text = eingabe;
                // Umwandlung von string in int um später zu rechnen
                aktuellerWert = double.Parse(eingabe);
            }
            else
            {
                // setzt den string eingabe aus den einzelnen Buttons die geklickt werden zusammen
                eingabe += content;
                // schreibt die eingegeben Zahlen/buchstaben in die Hauptanzeige
                Ausgabe.Text = eingabe;
                // Umwandlung von string in int um später zu rechnen
                aktuellerWert = double.Parse(eingabe);
            }
        }


        // Wird ausgeführt wenn ein Funktionsbutton verwendet wird
        private void buttonFunktion_Click(object sender, RoutedEventArgs e)
        {
            string x = ((string)((Button)sender).Content);
            // Prüft beim ersten start ob schon eine Zahl eingegeben wurde. Mit Ausnahme für Rechenopertationen 
            // die nur einen Wert brauchen
            if (!content.Equals("") || x.Equals("√") || x.Equals("x²")
                 || x.Equals("sin") || x.Equals("cos") || x.Equals("tan")
                 || x.Equals("sinh") || x.Equals("cosh") || x.Equals("tanh"))
            {
                Ausgabe.Text = "0";
                eingabe = "";
                
                // ruft die Methode zur Berechnung auf
                LetzteRechenoperation(x);
            }
        }


        // Berechnungen
        private void LetzteRechenoperation(string rechenoperation)
        {   // Speichert im aktuellen durchlauf das vorangegangen letzte ergebnis.
            double zahl1 = letztesErgebnis;
            // Auswahl aufgrund der letzten Rechenoperation. Weil die aktuelle bereits das Gleichzeichen sein könnte
            switch (letzterechenOperation)
            {
                case "%":  // x % von Wert y                
                    letztesErgebnis = letztesErgebnis * (aktuellerWert / 100);
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "√": // Zahl eingeben, dann Wurzel ziehen                    
                    letztesErgebnis = Math.Sqrt(aktuellerWert);// - aktuellerWert;
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "y ͯ":   // erster Wert ist die Basis, zweiter der Exponent                
                    letztesErgebnis = Math.Pow(letztesErgebnis,aktuellerWert);
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "sin": //Bogenmaß!
                    letztesErgebnis = Math.Sin(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "cos":
                    letztesErgebnis = Math.Cos(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "tan":
                    letztesErgebnis = Math.Tan(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "cosh":
                    letztesErgebnis = Math.Cosh(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "sinh":
                    letztesErgebnis = Math.Sinh(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "tanh":
                    letztesErgebnis = Math.Tanh(aktuellerWert);
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "x²": // Zahl eingeben, dann quadrieren
                    letztesErgebnis = aktuellerWert * aktuellerWert;
                    Ausgaben(aktuellerWert, rechenoperation);
                    break;

                case "+":
                    letztesErgebnis = letztesErgebnis + aktuellerWert;
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "-":
                    letztesErgebnis = letztesErgebnis - aktuellerWert;
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "x":
                    letztesErgebnis = letztesErgebnis * aktuellerWert;
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "/":
                    if (aktuellerWert == 0)
                    {
                        letztesErgebnis = 0;
                            MessageBox.Show("Durch 0 teilen? Mir fehlen die Worte...", "Netter Versuch", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                    letztesErgebnis = letztesErgebnis / aktuellerWert;
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;

                case "=":// letzterechenOperation wird mit = initialisiert, damit der Inhalt von aktuellerWert zum 
                         // letztenErgenis wird.
                         // Danach wird die gewählte rechenopertion zur letztenrechenOperation.
                         // Bei erneutem Klick auf eine rechenoperation, wird die Erste dann ausgeführt.
                    letztesErgebnis = aktuellerWert;
                    Ausgaben(zahl1, aktuellerWert, rechenoperation);
                    break;
            }        

            // Gleichsetzung nach der Ausgabe damit mit dem errechenten Wert weiter gerechent werden kann
            aktuellerWert = letztesErgebnis;
            letzterechenOperation = rechenoperation;
            // zurecksetzen der Kommavariablen um wieder eins bei der 2. Zahl nutzen zu können
            komma = false;
        }


        public void Ausgaben(double zahl1, double aktuellerWert, string rechenoperation)
        {   // Wandelt den inhalt von Variable letztesErgebnis um

            Ausgabe.Text = letztesErgebnis.ToString();

            //prüft ob das letzen Reichenzeichen ein = war
            if (letzterechenOperation.Equals("="))
            {   // Wandelt das letze letzteErgebnis und für das aktuelle Rechenzeichen hinzu
                AusgabeFormel.Text = letztesErgebnis.ToString() + rechenoperation;
            }
            else
            {   // wenn das letze Rechenzeichen kein = war, werden die Werte ins gewünschte Zahlenformat gewandelt
                // und die Rechenzeichen zur Ausgabe hinzugefügt
                AusgabeFormel.Text = zahl1.ToString() + letzterechenOperation + aktuellerWert.ToString() + "=" + letztesErgebnis.ToString();
                // Speichert die fertige Formel zusätzlich in der History
                HistoryTextBox.Text = HistoryTextBox.Text + "\n" + AusgabeFormel.Text;
            }
        }


        // Wenn nur 1 wert benötigt wird, nutzen wir diese Methode zur Ausgabe
        public void Ausgaben(double aktuellerwert, string rechenoperation)
        {
            Ausgabe.Text = letztesErgebnis.ToString();
            AusgabeFormel.Text = letzterechenOperation + aktuellerWert.ToString() + "=" + letztesErgebnis.ToString();
            HistoryTextBox.Text = HistoryTextBox.Text + "\n" + AusgabeFormel.Text;
        }


        //löscht die letzte Stelle der aktuellen Eingabe oder des Ergebnises
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            // prüft oder mehr als eine Stelle im aktuellen Wert ist aktuellerWert
            if (Ausgabe.Text.Length > 1)
            {
                // Prüft ob der Wert in der Ausgabe genau 2 Stellen hat und ob der Wert mit - beginnt
                // ohne diese Anweisung, würde beim löschen der letzten Stelle nur das - bleiben, was zu einem Fehler
                // beim Parsing führt
                if (Ausgabe.Text.Length == 2 && Ausgabe.Text.StartsWith("-"))
                {
                    Ausgabe.Text = "0";
                    aktuellerWert = 0;
                    eingabe = "";
                }

                // Prüfung oder der zu löschende Wert ein Komma ist. Falls ja, Komma freigeben                
                if (Ausgabe.Text.Substring(Ausgabe.Text.Length - 1).Equals(","))
                {
                    komma = false;
                }

                // Entfernt den letzen index aus der Ausgabebox
                Ausgabe.Text = Ausgabe.Text.Remove(Ausgabe.Text.Length - 1, 1);

                // Durch eingabe des Kommas, sind die Längen der Ausgabe.Text und die Länge von aktuellerWert ungleich.
                // Daher muss erneut geprüft werden, ob aktuellerWert noch lang genug zum kürzen ist.
                if (aktuellerWert.ToString().Length > 1)
                {
                    // Die Variable aktuellerWert, wird zum String, dann wir der letze index entfernt und anschließend wird
                    // die Variable als double geparsed und gespeichert
                    aktuellerWert = double.Parse(aktuellerWert.ToString().Remove(aktuellerWert.ToString().Length - 1, 1));
                }

                // Wenn nur ein Ergebnis gekürzt werden soll, ist die Variable eingabe leer. Daher muss diese geprüft werden
                // um einen Fehler zu vermeiden
                if (eingabe.Length > 1)
                {
                    eingabe = eingabe.Remove(eingabe.Length - 1, 1);
                }

            }
            else
            {   // sollte nur noch eine Stelle in der Varialblen aktuellerWert sein, so wird einfach alles zur 0, oder geleert
                Ausgabe.Text = "0";
                aktuellerWert = 0;
                eingabe = "";
            }
        }


        // Löscht alle Eingaben
        private void buttonLöschen_Click(object sender, RoutedEventArgs e)
        {
            eingabe = "";
            aktuellerWert = 0;
            letzterechenOperation = "=";
            letztesErgebnis = 0;
            AusgabeFormel.Text = "0";
            Ausgabe.Text = "0";
            HistoryTextBox.Text = "";
            komma = false;
        }


        // Abfangen der Tastatureingabe
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.NumPad0)
            {   // Ansprechen von Button1, Erzeugung eines neuen Events, 
                // instanzierung eines RoutedEventArgs bassierende auf der Klasse
                // Button und Ausführung von ClickEvent.                
                button0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                // versucht den Fokus auf das Element zu legen. So wirkt der Button als würde er betätigt
                button0.Focus();
            }
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                button1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button1.Focus();
            }
            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                button2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button2.Focus();
            }
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                button3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button3.Focus();
            }
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                button4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button4.Focus();
            }
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
            {
                button5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button5.Focus();
            }
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
            {
                button6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button6.Focus();
            }
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                button7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button7.Focus();
            }
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
            {
                button8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button8.Focus();
            }
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
            {
                button9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button9.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                Backspace.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Backspace.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                buttonLöschen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                buttonLöschen.Focus();
            }
            else if (e.Key == Key.Enter)
            {
                buttongleich.Focus();
                //buttongleich.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            }
            else if (e.Key == Key.Add)
            {
                buttonPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                buttonPlus.Focus();
            }
            else if (e.Key == Key.Subtract)
            {
                buttonMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                buttonMinus.Focus();
            }
            else if (e.Key == Key.Multiply)
            {
                buttonMal.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                buttonMal.Focus();
            }
            else if (e.Key == Key.Divide)
            {
                buttonTeilen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                buttonTeilen.Focus();
            }
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal)
            {
                button_Komma.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                button_Komma.Focus();
            }
        }
    }
}
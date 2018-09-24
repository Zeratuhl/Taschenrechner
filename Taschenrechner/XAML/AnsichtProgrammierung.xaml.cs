﻿using System;
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
    /// Interaktionslogik für AnsichtProgrammierung.xaml
    /// </summary>
    public partial class AnsichtProgrammierung : Page
    {
        // Definition der Variablen
        // Beinhaltet den Wert den aktuellen Buttons
        string content = "";
        // Beinhaltet die aktuelle Zahl
        double aktuellerWert = 0;
        // String welche die Eingabe aufnimmt
        string eingabe = "";
        // beinhaltet die zuletzt gewählte Rechenoperation
        char letzterechenOperation = '=';
        // Erst Zahl oder Ergebnis
        double letztesErgebnis = 0.0;
        // Ausgewähltes Zahlensystem
        string inhaltRadiobutton = "";


        public AnsichtProgrammierung()
        {   // erstellt und läd das Interface
            InitializeComponent();
            ButtonSteuerungFunktionsButtons(false);
            ButtonSteuerungBuchstaben(false);
            ButtonSteuerungZahlen(false, false, false);
        }


        // Aktiviert, oder deaktiviert die Funktionbuttons
        public void ButtonSteuerungFunktionsButtons(bool an)
        {
            buttongleich.IsEnabled = an;
            buttonLöschen.IsEnabled = an;
            buttonMal.IsEnabled = an;
            buttonMinus.IsEnabled = an;
            buttonPlus.IsEnabled = an;
            buttonTeilen.IsEnabled = an;
            Backspace.IsEnabled = an;
            if (an)
            {
                button_Komma.IsEnabled = !an;
            }
            else
            {
                button_Komma.IsEnabled = an;
            }
        }


        // Aktiviert oder deaktiviert die Buchstaben
        public void ButtonSteuerungBuchstaben(bool an)
        {
            buttonA.IsEnabled = an;
            buttonB.IsEnabled = an;
            buttonC.IsEnabled = an;
            buttonD.IsEnabled = an;
            buttonE.IsEnabled = an;
            buttonF.IsEnabled = an;
        }


        // aktiviert oder deaktiviert die Zahlen abhängig ob binär gewählt wieder nicht
        public void ButtonSteuerungZahlen(bool bin, bool oct, bool an)
        {
            if (bin)
            {
                button0.IsEnabled = an;
                button1.IsEnabled = an;
                button2.IsEnabled = !an;
                button3.IsEnabled = !an;
                button4.IsEnabled = !an;
                button5.IsEnabled = !an;
                button6.IsEnabled = !an;
                button7.IsEnabled = !an;
                button8.IsEnabled = !an;
                button9.IsEnabled = !an;
            }
            else if (oct)
            {
                button0.IsEnabled = an;
                button1.IsEnabled = an;
                button2.IsEnabled = an;
                button3.IsEnabled = an;
                button4.IsEnabled = an;
                button5.IsEnabled = an;
                button6.IsEnabled = an;
                button7.IsEnabled = an;
                button8.IsEnabled = !an;
                button9.IsEnabled = !an;
            }
            else
            {
                button0.IsEnabled = an;
                button1.IsEnabled = an;
                button2.IsEnabled = an;
                button3.IsEnabled = an;
                button4.IsEnabled = an;
                button5.IsEnabled = an;
                button6.IsEnabled = an;
                button7.IsEnabled = an;
                button8.IsEnabled = an;
                button9.IsEnabled = an;
            }
        }


        // Wird ausgeführt sobald ein Zahlensystem gewählt wird
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            inhaltRadiobutton = ((RadioButton)sender).Content.ToString();

            switch (inhaltRadiobutton)
            {
                case "HEX":
                    // löscht alle Eingaben
                    buttonLöschen_Click(sender, e);
                    ButtonSteuerungFunktionsButtons(true);
                    ButtonSteuerungBuchstaben(true);
                    ButtonSteuerungZahlen(false, false, true);
                    break;

                case "DEC":
                    buttonLöschen_Click(sender, e);
                    ButtonSteuerungFunktionsButtons(true);
                    ButtonSteuerungBuchstaben(false);
                    ButtonSteuerungZahlen(false, false, true);
                    break;

                case "OCT":
                    buttonLöschen_Click(sender, e);
                    ButtonSteuerungFunktionsButtons(true);
                    ButtonSteuerungBuchstaben(false);
                    ButtonSteuerungZahlen(false, true, true);
                    break;

                case "BIN":
                    buttonLöschen_Click(sender, e);
                    ButtonSteuerungFunktionsButtons(true);
                    ButtonSteuerungBuchstaben(false);
                    ButtonSteuerungZahlen(true, false, true);
                    break;
            }
        }


        // Wird aufgerufen wenn ein Button von 0 - F gedrückt wird
        private void button_Click(object sender, RoutedEventArgs e)
        {
            content = ((Button)sender).Content.ToString();
            // setzt den string eingabe aus den einzelnen Buttons die geklickt werden zusammen
            eingabe += content;
            // schreibt die eingegeben Zahlen/buchstaben in die Hauptanzeige
            Ausgabe.Text = eingabe;

            // Auswahl aufgrund der Einstellung DEC, OCT, BIN, HEX
            switch (inhaltRadiobutton)
            {
                case "DEC":
                    // Konvertierung und Anzage der Werte in den jeweiligen Ausgabefeldern
                    Ausgabe_BIN.Text = Convert.ToString(Convert.ToInt64(eingabe), 2);
                    Ausgabe_DEC.Text = eingabe;
                    Ausgabe_HEX.Text = Convert.ToString(Convert.ToInt64(eingabe), 16);
                    Ausgabe_OCT.Text = Convert.ToString(Convert.ToInt64(eingabe), 8);
                    // Umwandlung von string in int um später zu rechnen
                    aktuellerWert = double.Parse(eingabe);
                    break;

                case "BIN":
                    Ausgabe_BIN.Text = eingabe;
                    Ausgabe_DEC.Text = Convert.ToInt64(eingabe, 2).ToString();
                    Ausgabe_HEX.Text = Convert.ToInt64(eingabe, 2).ToString("X");
                    Ausgabe_OCT.Text = Convert.ToString((Convert.ToInt64(eingabe, 2)), 8);
                    // Konvertierung des eingegebenen Formats in das DEC Format
                    // um mit der Zahl später zu rechnen
                    aktuellerWert = Convert.ToInt64(eingabe, 2);
                    break;

                case "OCT":
                    Ausgabe_BIN.Text = Convert.ToString((Convert.ToInt64(eingabe, 8)), 2);
                    Ausgabe_DEC.Text = Convert.ToString((Convert.ToInt64(eingabe, 8)), 10);
                    Ausgabe_HEX.Text = Convert.ToInt64(eingabe, 8).ToString("X");
                    Ausgabe_OCT.Text = eingabe;
                    aktuellerWert = Convert.ToInt64(eingabe, 8);
                    break;

                case "HEX":
                    Ausgabe_BIN.Text = Convert.ToString((Convert.ToInt64(eingabe, 16)), 2);
                    Ausgabe_DEC.Text = Convert.ToString((Convert.ToInt64(eingabe, 16)), 10);
                    Ausgabe_HEX.Text = eingabe;
                    Ausgabe_OCT.Text = Convert.ToString((Convert.ToInt64(eingabe, 16)), 8);
                    aktuellerWert = Convert.ToInt64(eingabe, 16);
                    break;
            }
        }


        // Wird ausgeführt wenn ein Funktionsbutton verwendet wird
        private void buttonFunktion_Click(object sender, RoutedEventArgs e)
        {   // Prüft ob schon eine Zahl eingegeben wurde
            if (!content.Equals(""))
            {
                Ausgabe.Text = "0";
                eingabe = "";
                char x = char.Parse((string)((Button)sender).Content);
                // ruft die Methode zur Berechnung auf
                LetzteRechenoperation(x);
            }
        }


        // Berechnungen
        private void LetzteRechenoperation(char rechenoperation)
        {   // Speichert im aktuellen durchlauf das vorangegangen letzte ergebnis.
            double zahl1 = letztesErgebnis;
            // Auswahl aufgrund der letzten Rechenoperation. Weil die aktuelle bereits das Gleichzeichen sein könnte
            switch (letzterechenOperation)
            {
                case '+':
                    letztesErgebnis = letztesErgebnis + aktuellerWert;
                    break;

                case '-':
                    letztesErgebnis = letztesErgebnis - aktuellerWert;
                    break;

                case 'x':
                    letztesErgebnis = letztesErgebnis * aktuellerWert;
                    break;

                case '/':
                    if (aktuellerWert == 0)
                    {
                        letztesErgebnis = 0;
                        MessageBox.Show("Durch 0 teilen? Mir fehlen die Worte...", "Netter Versuch", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }
                    letztesErgebnis = letztesErgebnis / aktuellerWert;
                    break;

                case '=':// letzterechenOperation wird mit = initialisiert, damit der Inhalt von aktuellerWert zum 
                    // letztenErgenis wird.
                    // Danach wird die gewählte rechenopertion zur letztenrechenOperation.
                    // Bei erneutem Klick auf eine rechenoperation, wird die Erste dann ausgeführt.
                    letztesErgebnis = aktuellerWert;
                    break;
            }


            // Zur gezielten Ausgabe in der Hauptausgabe eine Auswahl abhängig der Einstellung          
            switch (inhaltRadiobutton)
            {
                case "HEX":
                    // Methodenaufruf um die ausgabefelder zu befüllen
                    Ausgaben(16, zahl1, aktuellerWert, rechenoperation);
                    break;

                case "DEC":
                    Ausgaben(10, zahl1, aktuellerWert, rechenoperation);
                    break;

                case "BIN":
                    Ausgaben(2, zahl1, aktuellerWert, rechenoperation);
                    break;

                case "OCT":
                    Ausgaben(8, zahl1, aktuellerWert, rechenoperation);
                    break;
            }

            // Gleichsetzung nach der Ausgabe damit mit dem errechenten Wert weiter gerechent werden kann
            aktuellerWert = letztesErgebnis;
            letzterechenOperation = rechenoperation;
        }


        public void Ausgaben(int zahlensystem, double zahl1, double aktuellerWert, char rechenoperation)
        {   // Wandelt den inhalt von Variable letztesErgebnis bassierend auf dem Inhalt der Variablen zahlensystem um
            
            Ausgabe.Text = Convert.ToString(Convert.ToInt64(letztesErgebnis), zahlensystem).ToUpper();
            //prüft ob das letzen Reichenzeichen ein = war
            if (letzterechenOperation.Equals('='))
            {   // Wandelt das letze letzteErgebnis ins gewünschte Zahlenformat und für das aktuelle Rechenzeichen hinzu
                AusgabeFormel.Text = Convert.ToString(Convert.ToInt64(letztesErgebnis), zahlensystem).ToUpper() + rechenoperation;
            }
            else
            {   // wenn das letze Rechenzeichen kein = war, werden die Werte ins gewünschte Zahlenformat gewandelt
                // und die Rechenzeichen zur Ausgabe hinzugefügt
                AusgabeFormel.Text = Convert.ToString(Convert.ToInt64(zahl1), zahlensystem).ToUpper() + letzterechenOperation + Convert.ToString(Convert.ToInt64(aktuellerWert), zahlensystem).ToUpper() + "=" + Convert.ToString(Convert.ToInt64(letztesErgebnis), zahlensystem).ToUpper();
                // Speichert die fertige Formel zusätzlich in der History
                HistoryTextBox.Text = HistoryTextBox.Text + "\n" + AusgabeFormel.Text;
            }

            //Befüllt die Ausgabefelder mit den umgewandelten Inhalten aus der Variable letztesErgebnis
            Ausgabe_BIN.Text = Convert.ToString(Convert.ToInt64(letztesErgebnis), 2);
            Ausgabe_DEC.Text = letztesErgebnis.ToString();
            Ausgabe_HEX.Text = Convert.ToString(Convert.ToInt64(letztesErgebnis), 16).ToUpper();
            Ausgabe_OCT.Text = Convert.ToString(Convert.ToInt64(letztesErgebnis), 8);

        }


        //löscht die letzte Stelle der aktuellen Eingabe oder des Ergebnises
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            // prüft oder mehr als eine Stelle im aktuellen Wert ist
            if (aktuellerWert.ToString().Length > 1)
            {   // Entfernt den letzen index aus der Ausgabebox
                Ausgabe.Text = Ausgabe.Text.Remove(Ausgabe.Text.Length - 1, 1);
                Ausgabe_BIN.Text = Ausgabe_BIN.Text.Remove(Ausgabe_BIN.Text.Length - 1, 1);
                Ausgabe_HEX.Text = Ausgabe_HEX.Text.Remove(Ausgabe_HEX.Text.Length - 1, 1);
                Ausgabe_OCT.Text = Ausgabe_OCT.Text.Remove(Ausgabe_OCT.Text.Length - 1, 1);
                Ausgabe_DEC.Text = Ausgabe_DEC.Text.Remove(Ausgabe_DEC.Text.Length - 1, 1);
                // Die Variable aktuellerWert, wird zum String, dann wir der letze index entfernt und anschließend wird
                // die Variable als double geparsed und gespeichert
                aktuellerWert = double.Parse(aktuellerWert.ToString().Remove(aktuellerWert.ToString().Length - 1, 1));
                // Wenn ein ergebnis gekürzt werden soll, ist die Variable eingabe leer. Daher muss diese geprüft werden
                // um einen Fehler zu vermeiden
                if (eingabe.Length > 1)
                {
                    eingabe = eingabe.Remove(eingabe.Length - 1, 1);
                }

            }
            else
            {   // sollte nur noch eine Stelle in der Varialblen aktuellerWert sein, so wird einfach alles zur 0, oder geleert
                Ausgabe.Text = "0";
                Ausgabe_BIN.Text = "0";
                Ausgabe_HEX.Text = "0";
                Ausgabe_OCT.Text = "0";
                Ausgabe_DEC.Text = "0";
                aktuellerWert = 0;
                eingabe = "";
            }
        }


        // Löscht alle Eingaben
        private void buttonLöschen_Click(object sender, RoutedEventArgs e)
        {
            eingabe = "";
            aktuellerWert = 0;
            letzterechenOperation = '=';
            letztesErgebnis = 0;
            AusgabeFormel.Text = "0";
            Ausgabe.Text = "0";
            Ausgabe_BIN.Text = "0";
            Ausgabe_DEC.Text = "0";
            Ausgabe_HEX.Text = "0";
            Ausgabe_OCT.Text = "0";
            HistoryTextBox.Text = "";
        }


        // Abfangen der Tastatureingabe
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {   // Prüfung ob ein Zahlensystem gewählt wurde
            if (!inhaltRadiobutton.Equals(""))
            {   // Abhängig vom gewählten Zahlensystem werden nur bestimmte Tasten abgefangen
                switch (inhaltRadiobutton)
                {
                    case "BIN":
                        if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                        {   // Ansprechen von Button1, Erzeugung eines neuen Events, 
                            // instanzierung eines RoutedEventArgs bassierende auf der Klasse 
                            // Button und Ausführung von ClickEvent. 
                            button0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            button0.Focus();
                        }
                        else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                        {
                            button1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            button1.Focus();
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
                            // buttongleich.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttongleich.Focus();
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
                        break;

                    case "DEC":
                        if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                        {
                            button0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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
                            // buttongleich.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttongleich.Focus();
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
                        break;

                    case "OCT":
                        if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                        {
                            button0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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
                            // buttongleich.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttongleich.Focus();
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
                        break;

                    case "HEX":
                        if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                        {
                            button0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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
                        else if (e.Key == Key.A)
                        {
                            buttonA.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonA.Focus();
                        }
                        else if (e.Key == Key.B)
                        {
                            buttonB.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonB.Focus();
                        }
                        else if (e.Key == Key.C)
                        {
                            buttonC.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonC.Focus();
                        }
                        else if (e.Key == Key.D)
                        {
                            buttonD.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonD.Focus();
                        }
                        else if (e.Key == Key.E)
                        {
                            buttonE.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonE.Focus();
                        }
                        else if (e.Key == Key.F)
                        {
                            buttonF.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttonF.Focus();
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
                            // buttongleich.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            buttongleich.Focus();
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

                        break;

                    default:
                        break;
                }
            }
        }
    }
}

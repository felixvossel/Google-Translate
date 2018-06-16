
using System;
using System.Net;
using System.Diagnostics;

namespace googletranslate
{
    class Program
    {
        
        private static String GetResultPage(string KeyWord,string OutLang,string TargLang) //Methode, die die Google Übersetzer-Ergebnisseite als String herunterlädt.
        {

            string GoogleTranslateUrl = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", KeyWord, OutLang+"|"+TargLang); // Erstellen des URL-Strings mit "String.Format".

            WebClient DownloadHTMLString = new WebClient(); //"WebClient" wird benutzt, um die Webseite als String herunterzuladen.

            DownloadHTMLString.Encoding = System.Text.Encoding.UTF8; // Encoding wird auf UTF8 gesetzt, um auch umlaute mit übersetzen zu können.

            string Result = DownloadHTMLString.DownloadString(GoogleTranslateUrl); // Die Webseite wird in der Variable "Result" gespeichert.

            Result = Result.Substring(Result.IndexOf("<span title=\"") + "<span title=\"".Length);
            /*
             * Es wird nach bestimmten HTML-Tags gesucht, um die Übersetzung im HTML-Text zu finden.
             */

            Result = Result.Substring(Result.IndexOf(">") + 1); //Die Übersetzung wird weiter eingegränzt. 

            Result = Result.Substring(0, Result.IndexOf("</span>")); // Die Übersetzung wird komplett isoliert.

            return Result; //Die Übersetzung wird an die Main Funktion zurückgegeben.


        }

        static void Main(string[] args)
        {
            if (args.Length==3)                     
            {
                /*
                 * Drei Parameter sollen an das Programm übergeben werden, der erste Parameter ist das Schlüsselwort,
                 * das übersetz werden soll, der zweite Parameter sind die ersten zwei Buchstaben der Ausgangssprache,
                 * der dritte Parameter sind die ersten zwei Buchstaben der Zielsprache.
                 */
                string KeyWord = args[0];  //Der String, der übersetz werden soll.
                string OutLang = args[1];  //Der String, der die Ausgangssprache darstellt.
                string TargLang = args[2]; //Der String, der die Zielsprache darstellt.
                string Translation = GetResultPage(KeyWord,OutLang,TargLang); // Aufruf der Methode, die die Übersetzung zurückgibt.
                Console.Write("Die Überseztung von \""+KeyWord+"\" lautet: "+Translation+"\n\n"); // Ausgabe der Übersetzung in der Konsole.
                Console.ReadKey();// Es wird gewartet, dass der Benutzer ein Taste drückt, dann wird das Programm beendet.

            }

            else // Wenn nicht die richtigen Parameter angegeben werden, wird die Syntax erklärt.
            {
                Console.Write("Syntax: \n googletranslate.exe \"Suchbegriff\" \"Die ersten zwei Buchstaben der Ausgangssprache\" \"Die ersten zwei Buchstaben der Zielsprache\""); //Erklärung der Syntax.
                Console.ReadKey();// Es wird gewartet, dass der Benutzer ein Taste drückt, dann wird das Programm beendet.

            }
        }
    }
}

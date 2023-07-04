using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Die_ersten_Versuche
{

    public partial class Form1 : Form
    {
        string recheninput = ""; // Damit sollte eigentlich gerechnent werden
        string input = ""; // Variable fuers Autoscaling
        string rechensymbol = ""; // Zum bestimmen des Operators
        double wert = 0; //ZS für Rechenprozess        
        double zweitezahl = 0; // fuer das weiterrechnen mit =
        bool rechnen_press = false;
        bool neueZahl = false; // Rechnen mit neuer Zahl
        bool weiterrechnen = false; // fuer das eingeben neuer Zahlen nach dem =
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (rechnen_press) //Startnull wird überschrieben
            {
                
                recheninput = string.Empty;
            }

            if (weiterrechnen)
            {
                recheninput = string.Empty;
                weiterrechnen = false;
            }
            rechnen_press = false;
            Button b = (Button)sender;
            recheninput += b.Text; //Die box wird als box mit symbolen gewertet
            double tempdouble = 0;
            if (!double.TryParse(recheninput, out tempdouble)) return;

            textBox1.Text = tempdouble.ToString(CultureInfo.GetCultureInfo("es-ES"));// nochmal einer variablen zugewiesen UND!!!!!Das Format für die Ausgabe
            // N = numeric / N0 = Eigentlich gewuenschtes Format aber ohne 10e+ und keine Kommarechnung

            //"#,##0.###############", CultureInfo.GetCultureInfo("es-ES")
        }


        private void Buttonclear_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; // Es wird alles auf Null zurückgesetzt
            recheninput = string.Empty;
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                recheninput = ""; 
                input = "";
                rechensymbol = "";
                wert = 0;
                zweitezahl = 0;
                rechnen_press = false;
                neueZahl = false; 
                weiterrechnen = false;
                Zwischenspeicher.Text = string.Empty;
            }
        }


        private void Operator_Symbol(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) // Der Operator bug zwar beseitigt aber zum verlust der startnull
            {
                return;
            }
            else
            {
                Button b = (Button)sender;
                rechensymbol = b.Text;
                wert = double.Parse(recheninput);
                rechnen_press = true;
                neueZahl = true;
                Zwischenspeicher.Text = wert + " " + rechensymbol;
            }
        }

        
        private void Ergebniss(object sender, EventArgs e)
        {
            Zwischenspeicher.Text = "";
            double ergebnis = 0;

            if (neueZahl)
            {
                if (!double.TryParse(recheninput, out zweitezahl))// Zweite Zahl wird zugewiesen
                {
                    return;
                }
            }

            switch (rechensymbol)//welches rechensymbol soll was tun
            {
                
                case "+":
                    ergebnis = wert + zweitezahl;
                    break;

                case "-":
                    ergebnis = wert - zweitezahl;
                    break;

                case "*":
                    ergebnis = wert * zweitezahl;
                    break;

                case "/":
                    ergebnis = wert / zweitezahl;
                    break;
            }

            textBox1.Text = ergebnis.ToString(CultureInfo.GetCultureInfo("es-ES")); //Ergebnis wird ausgegeben // Formatausgabe
            recheninput = ergebnis.ToString(); // mit ergebnis weiterrechen
            wert = ergebnis;
            neueZahl = false;
            weiterrechnen = true;
        }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) { return; }
            input = (textBox1.Text);
            if (input.Length > 15)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, 12);// Autoscaling für groessere Zahlen
                textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            }
            else if (input.Length > 12)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, 14);
                textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            }
            else if (input.Length > 9)
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, 16);
                textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            }
            
            if (input.Length < 9)//Text wird wieder groesser bei kleiner werdenden Zahlen
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, 18);
                textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            }
        }
    }  
}

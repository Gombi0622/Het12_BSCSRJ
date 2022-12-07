using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Het12_BSCSRJ.Models;

namespace Het12_BSCSRJ
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB;     // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void CreateTable()
        {
            xlSheet.Cells[1, 1] = "Kérdés";
            xlSheet.Cells[1, 2] = "1. Válasz";
            xlSheet.Cells[1, 3] = "2. Válasz";
            xlSheet.Cells[1, 4] = "3. Válasz";
            xlSheet.Cells[1, 5] = "Helyes válasz";

            HajosContext context = new HajosContext();
            var adatok = context.Questions.ToList();

            object[,] adattomb = new object[adatok.Count, 6];

            for (int i = 0; i < adatok.Count(); i++)
            {
                adattomb[i, 0] = adatok[i].Question1;
                adattomb[i, 1] = adatok[i].Answer1;
                adattomb[i, 2] = adatok[i].Answer2;
                adattomb[i, 3] = adatok[i].Answer3;
                adattomb[i, 4] = adatok[i].CorrectAnswer;
                adattomb[i, 5] = adatok[i].Image;
            }

            int sorokSzáma = adattomb.GetLength(0);
            int oszlopokSzáma = adattomb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);
            adatRange.Value2 = adattomb;

            adatRange.Columns.AutoFit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
    }
}
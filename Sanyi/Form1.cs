namespace Sanyi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Szoft2.Week10.BigBrother.BigBrother bigBrother = new Szoft2.Week10.BigBrother.BigBrother();

            bigBrother.AblakV�lt�s += BigBrother_AblakV�lt�s;
        }

        private void BigBrother_AblakV�lt�s(object sender, Szoft2.Week10.BigBrother.Alkalmaz�sHaszn�latEventArgs e)
        {
            
        }
    }
}
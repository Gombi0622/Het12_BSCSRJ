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

            bigBrother.AblakVáltás += BigBrother_AblakVáltás;
        }

        private void BigBrother_AblakVáltás(object sender, Szoft2.Week10.BigBrother.AlkalmazásHasználatEventArgs e)
        {
            
        }
    }
}
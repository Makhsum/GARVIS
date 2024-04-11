using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GARVIS
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
            StartDelayedForm();


        }

        private async void StartDelayedForm()
        {   
            await Task.Delay(8000);
            GarvisUi secondForm = new GarvisUi();
            secondForm.Show();
           // this.Close();
             this.Hide();
          
           
        }
    }
}

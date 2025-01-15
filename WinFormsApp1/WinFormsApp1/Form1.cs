using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SeeResult(object sender, EventArgs e)
        {
            MessageBox.Show($"Ваша анкета:\n\tФамилия {TBFam.Text}\n\t Имя: {TBName.Text}\n\t Отчество: {TBOt4.Text}\n\t Страна: {TBStr.Text}\n\t Город: {TBCity.Text}\n\t Телефон: {TBPhone.Text}\n\t Дата рождения: {dateTimePicker1.Text}\n\t Пол: ");
        }
    }
}

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
            MessageBox.Show($"���� ������:\n\t������� {TBFam.Text}\n\t ���: {TBName.Text}\n\t ��������: {TBOt4.Text}\n\t ������: {TBStr.Text}\n\t �����: {TBCity.Text}\n\t �������: {TBPhone.Text}\n\t ���� ��������: {dateTimePicker1.Text}\n\t ���: ");
        }
    }
}

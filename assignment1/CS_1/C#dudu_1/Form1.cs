using System;
using System.Windows.Forms;
namespace C_dudu_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "+", "-", "*", "/" }); // �ֶ�������������ѡ��
            comboBox1.SelectedIndex = 0; // Ĭ��ѡ���һ��������
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//ʹ������ֻ��ѡ�񣬲�����д

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num1, num2;
            if (!double.TryParse(textBox1.Text, out num1) || !double.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("��������Ч�����֣�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            char operation = comboBox1.SelectedItem.ToString()[0];
            double result = 0;

            switch (operation)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        MessageBox.Show("���󣺳�������Ϊ�㣡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("������Ч�Ĳ�������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            MessageBox.Show("�����" + result, "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
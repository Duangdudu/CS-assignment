using System;
using System.Windows.Forms;
namespace C_dudu_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "+", "-", "*", "/" }); // 手动添加四种运算符选项
            comboBox1.SelectedIndex = 0; // 默认选择第一个操作符
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;//使操作符只能选择，不能填写

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num1, num2;
            if (!double.TryParse(textBox1.Text, out num1) || !double.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("请输入有效的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("错误：除数不能为零！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("错误：无效的操作符！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            MessageBox.Show("结果：" + result, "计算结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
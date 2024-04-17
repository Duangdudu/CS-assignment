using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsOrderManage
{
    public partial class ModifyOrder : Form
    {
        public Order NewOrder { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public ModifyOrder(Order order)
        {
            InitializeComponent();
            NewOrder = order;
        }

        private void ModifyOrder_Load(object sender, EventArgs e)
        {
            customerName.DataBindings.Add("Text", this, "CustomerName");
            address.DataBindings.Add("Text", this, "Address");
            CustomerName = NewOrder.CustomerName;
            Address = NewOrder.Address;
            NewOrder.TotalPrice = 0;
            itemBindingSource.DataSource = NewOrder.OrderItemList;
        }

        private void dgvOrderItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("请填写正确的数据", "ERROR");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CustomerName == "")
            {
                MessageBox.Show("请填写对应的用户名");
                return;
            }

            if (Address == "")
            {
                MessageBox.Show("请填写对应的地址");
                return;
            }
            NewOrder.CustomerName = CustomerName;
            NewOrder.Address = Address;

            foreach (OrderItem item in NewOrder.OrderItemList)
            {
                NewOrder.TotalPrice += item.ProductPrice * item.ProductAmount;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
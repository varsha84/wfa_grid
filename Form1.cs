
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace testCasePool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataBaseConnection();
        }
        

        private void DataBaseConnection()
        {

            string fullPath = @"URI=file:C:\work\csharpproject\testCasePool\headsets.db";
            SQLiteConnection conread = new SQLiteConnection(fullPath);
            conread.Open();

            string selectSQL = "SELECT * FROM test";
            SQLiteCommand selectCommand = new SQLiteCommand(selectSQL, conread);
            SQLiteDataReader dataReader = selectCommand.ExecuteReader();
            
            while (dataReader.Read())
            {
               
               var Id = Convert.ToInt32(dataReader["test_id"]);
               var title = dataReader["title"].ToString();
               //var price = Convert.ToInt32(dataReader["price"]);

                var n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = Id;
                dataGridView1.Rows[n].Cells[1].Value = title;
                //dataGridView1.Rows[n].Cells[2].Value = price;


            }

            conread.Close();

        }    
        private void button1_Click(object sender, EventArgs e)
        {
             TreeNode t1 = treeView1.Nodes.Add("ON_oFF");
             TreeNode t2 = treeView1.Nodes.Add("USB");
             TreeNode t3 = treeView1.Nodes.Add("play_pause");
             t1.Nodes.Add("ON_testCase_1");
             t1.Nodes.Add("ON_testcase_2");
             t1.Nodes.Add("ON_testCase_3");
             t1.Nodes.Add("OFF_testCase_4");
             t1.Nodes.Add("OFF_testcase_1");
             t1.Nodes.Add("OFF_testCase_2");
             t2.Nodes.Add("USB_testCase_1");
             t2.Nodes.Add("USB_testcase_2");
             t2.Nodes.Add("USB_testCase_3");
             t2.Nodes.Add("USB_testCase_4");


             t3.Nodes.Add("Pause_testCase_1");
             t3.Nodes.Add("Pause_testCase_2");
             t3.Nodes.Add("Pause_testCase_3");
             t3.Nodes.Add("Play_testCase_1");
             t3.Nodes.Add("Play_testCase_2");
             t3.Nodes.Add("Play_testCase_3"); 

            

        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //treeView1.SelectedNode.Remove();
            RemoveCheckedNodes(treeView1.Nodes);
        }

        List<TreeNode> checkedNodes = new List<TreeNode>();

        void RemoveCheckedNodes(TreeNodeCollection nodes)
        {
            foreach(TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);

                }
                else
                {
                    RemoveCheckedNodes(node.Nodes);
                }
            }
            foreach (TreeNode checkedNode in checkedNodes)
            {
                nodes.Remove(checkedNode);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow) dataGridView1.Rows.Remove(row);


            }
                    
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(textBox1.Text);
            try
            {
                treeView1.SelectedNode.Nodes.Add(node);
            }
            catch (Exception ex)
            {
                treeView1.SelectedNode.Nodes.Add(node);
            }

        }

        private void mouseDouble_click(object sender, MouseEventArgs e)
        {

            try
            {
                string text = treeView1.SelectedNode.Text;
                //MessageBox.Show(text);
                string fullPath = @"URI=file:C:\work\csharpproject\testCasePool\headsets.db";
                SQLiteConnection conread = new SQLiteConnection(fullPath);
                conread.Open();

                string selectSQL = string.Format("SELECT * FROM test WHERE label1='{0}'", text.ToLower());

                SQLiteCommand selectCommand = new SQLiteCommand(selectSQL, conread);
                SQLiteDataReader dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                {

                    var Id = Convert.ToInt32(dataReader["test_id"]);
                    var title = dataReader["title"].ToString();
                    //var price = Convert.ToInt32(dataReader["price"]);
                    dataGridView1.Rows.Clear();
                    var n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = Id;
                    dataGridView1.Rows[n].Cells[1].Value = title;
                    //dataGridView1.Rows[n].Cells[2].Value = price;


                }
            }

            catch(Exception ex)

            {
                MessageBox.Show("exeception happened");
            }
        }
    }
}

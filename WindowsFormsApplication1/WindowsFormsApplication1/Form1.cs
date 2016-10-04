using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ListDirectory(tvMain, @Properties.Settings.Default.tvMainRootPath);
            tvMain.GetNodeAt(0, 0).Expand();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                try {
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.ToString());
                }
            foreach (var file in directoryInfo.GetFiles())
                try
                {
                    if (file.Extension == ".py")
                    {
                        directoryNode.Nodes.Add(new TreeNode(file.Name));
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.ToString());
                }
            
            return directoryNode;
        }

        private void btnRefreshTree_Click(object sender, EventArgs e)
        {
            refreshTvMain();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rutaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            refreshTvMain(fbd.SelectedPath);
        }

        private void refreshTvMain(string path)
        {
            Properties.Settings.Default.tvMainRootPath = path;
            Properties.Settings.Default.Save();
            ListDirectory(tvMain, @Properties.Settings.Default.tvMainRootPath);
            tvMain.GetNodeAt(0, 0).Expand();
        }

        private void refreshTvMain()
        {
            ListDirectory(tvMain, @Properties.Settings.Default.tvMainRootPath);
            tvMain.GetNodeAt(0, 0).Expand();
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            
        }
    }
}

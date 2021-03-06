﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmMain : Form
    {
        List<string> filesToAnalize = new List<string>();
        List<string> fileResults = new List<string>();

        public frmMain()
        {
            InitializeComponent();
            if (@Properties.Settings.Default.tvMainRootPath == "")
            {
                Properties.Settings.Default.tvMainRootPath = @"C:\Users\Public\Downloads";
                Properties.Settings.Default.Save();
            }
            refreshTvMain();
        }

        /// <summary>
        /// Agrega el primer elemento la raíz del treeview (y todos sus elementos)
        /// llamando a la función CreateDirectoryNode. Realiza las validaciones de acceso y
        /// existencia del directorio
        /// </summary>
        /// <param name="treeView">TreeView a llenar.</param>
        /// <param name="path">Directorio base.</param>
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            if (rootDirectoryInfo.Exists)
            {
                try
                {
                    treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show("El directorio \"" + @path + "\" o parte de este, no se ha podido acceder. Seleccione otro directorio.", "Directorio no encontrado");
                    changeTvMainRootPath();
                    this.Focus();
                }
            }
            else
            {
                MessageBox.Show("El directorio \"" + @path + "\" no se ha encontrado. Seleccione otro directorio.", "Directorio no encontrado");
                changeTvMainRootPath();
                this.Focus();
            }
        }

        /// <summary>
        /// Agrega al treeView todos los subdirectorios del directorio base.
        /// Únicamente agrega los archivos con extensión .py 
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns>TreeNode</returns>
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Extension == ".py")
                {
                    TreeNode node = new TreeNode(file.Name);
                    node.Tag = file.FullName;
                    directoryNode.Nodes.Add(node);
                }
            }
            return directoryNode;
        }

        /// <summary>
        /// Evento de selección de un checkbox. 
        /// </summary>
        private void HandleOnTreeViewAfterCheck(Object sender,TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }

        /// <summary>
        /// Selecciona o remueve la selección del checkbox de los hijos de un nodo
        /// </summary>
        /// <param name="node">Nodo.</param>
        /// <param name="isChecked">Seleccionar o quitar selección</param>
        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;
                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

        /// <summary>
        /// Evento del botón Actualizar
        /// </summary>
        private void btnRefreshTree_Click(object sender, EventArgs e)
        {
            refreshTvMain();
        }

        /// <summary>
        /// Evento del elemento salir del menú Archivo de la barra de menús principal.
        /// </summary>
        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = new frmSettings();
            s.Location = this.Location;
            s.StartPosition = FormStartPosition.Manual;
            s.FormClosing += delegate {  this.Enabled = true; };
            s.Show();
            this.Enabled = false;
            

        }

        /// <summary>
        /// Evento del elemento Salir del menú Archivo de la barra de menús principal.
        /// </summary>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento del elemento Ruta inicial del menú Archivo de la barra de menús principal.
        /// </summary>
        private void rutaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeTvMainRootPath();
        }

        /// <summary>
        /// Actualiza el árbol del TreeView principal
        /// </summary>
        /// <param name="path">Directorio raíz.</param>
        private void refreshTvMain(string path)
        {
            //En el solution explirer, en Properties//Settings.settings <- !double click! -
            //se pueden crear nuevas opciones a guardar. Muy útil para guardar las configuraciones -
            //de los indicadores.
            Properties.Settings.Default.tvMainRootPath = @path;  //El @ es para qie incluya los backslashes en el string
            Properties.Settings.Default.Save(); //al cambiar una configuración, es necesario guardarlas.
            refreshTvMain();
        }

        /// <summary>
        /// Actualiza el árbol del TreeView principal
        /// </summary>
        private void refreshTvMain()
        {
            ListDirectory(tvMain, @Properties.Settings.Default.tvMainRootPath);
            tvMain.GetNodeAt(0, 0).Expand();
        }

        /// <summary>
        /// Evento del botón Analizar
        /// </summary>
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            while (!File.Exists(@Properties.Settings.Default.astToXML)) //Mientras no encuentre el archivo, pedirá que lo busque en el fileDialog
            {
                MessageBox.Show("No se ha encontrado el archivo astToXML.py. Seleccione la ubicación de astToXML.py a continuación.", "Archivo no encontrado no encontrado");
                findAstToXML();
            }
            filesToAnalize.Clear();
            fileResults.Clear();
            GetCheckedNodes(tvMain.Nodes);
            foreach (string file in filesToAnalize) fileResults.Add(run_cmd(@Properties.Settings.Default.astToXML, file)); //agrega cada xml a la lista fileResults
            richTextBox1.Text = "Archivos py analizados: " + filesToAnalize.Count()+ "\n";
            foreach (string item in fileResults) richTextBox1.AppendText(item); //Solo para debug imprime toda la lista de resultados en el richTextBox1
        }

        /// <summary>
        /// Corre un script de python (pyFile) desde la consola de python.exe y retorna el resultado en un string.
        /// </summary>
        /// <param name="pyFile">El script de python a correr</param>
        /// <param name="args">Argumentos del script a correr</param>
        /// <returns>String</returns>
        private string run_cmd(string pyFile, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            while (!File.Exists(Properties.Settings.Default.python))
            {
                MessageBox.Show("No se ha encontrado el ejecutable python.exe. Seleccione la ubicación de python.exe a continuación.", "Archivo no encontrado");
                findPython();
            }
            start.FileName = Properties.Settings.Default.python; ;//cmd is full path to python.exe
            start.Arguments = "\""+@pyFile +"\" \""+ args+"\"";//args is path to .py file and any cmd line args
            ;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            string result = "";
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd(); ;
                }
            }
            return result;
        }

        /// <summary>
        /// Llena la lista filesToAnalize con los elementos del árbol seleccionado que tengan extensión .py
        /// </summary>
        /// <param name="nodes">Nodos a revisar.</param>
        public void GetCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode aNode in nodes)
            {
                if (aNode.Checked)
                    if (aNode.Tag != null)
                    {
                        if (aNode.Tag.ToString().EndsWith(".py"))
                        {
                            filesToAnalize.Add(@aNode.Tag.ToString());
                        }
                    }

                if (aNode.Nodes.Count != 0)
                    GetCheckedNodes(aNode.Nodes);
            }
        }

        /// <summary>
        /// Cambia el directorio raíz del TreeView Principal mediante un cuadro de diálogo de selección de folder.
        /// Si se presiona cancelar, el directorio no cambiará.
        /// </summary>
        private void changeTvMainRootPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
            {
                refreshTvMain(fbd.SelectedPath);
            }            
        }

        /// <summary>
        /// Abre un cuadro de diálogo para encontrar el ejecutable python.exe y lo guarda
        /// en las configuraciones de la aplicacion (Properties.Settings.Default.python)
        /// </summary>
        private void findPython()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "python*";
            openFileDialog1.Filter = "python.exe|*.exe";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            Properties.Settings.Default.python = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Abre un cuadro de diálogo para encontrar el archivo astToXML.py y lo guarda
        /// en las configuraciones de la aplicacion (Properties.Settings.Default.astToXML)
        /// </summary>
        private void findAstToXML()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "astToXML*";
            openFileDialog1.Filter = "astToXML.py|*.py";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            Properties.Settings.Default.astToXML= openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using FastColoredTextBoxNS;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        FastColoredTextBox CurrentTB
        {
            get
            {
                if (tabControl3.SelectedTab == null)
                    return null;
                return (tabControl3.SelectedTab.Controls[0] as FastColoredTextBox);
            }

            set
            {
                tabControl3.SelectedTab = new TabPage();
                value.Focus();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) => CreateTab(null);
        private void createToolStripMenuItem_Click(object sender, EventArgs e) => CreateTab(null);

        private void CreateTab(string fileName)
        {
            try
            {
                var tb = new FastColoredTextBox();
                tb.Font = new Font("Consolas", 9.75f);
                tb.Dock = DockStyle.Fill;
                tb.LeftPadding = 17;
                tb.Language = Language.Custom;

                var tab = new TabPage();
                tab.Text = fileName != null ? Path.GetFileName(fileName) : "Новый документ";
                tab.Controls.Add(tb);
                tab.Tag = fileName;
                
                if (fileName != null)
                    tb.OpenFile(fileName);
                
                tabControl3.Controls.Add(tab);
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            var control = sender as SplitContainer;
            //paint the three dots'
            Point[] points = new Point[3];
            var w = control.Width;
            var h = control.Height;
            var d = control.SplitterDistance;
            var sW = control.SplitterWidth;

            //calculate the position of the points'
            if (control.Orientation == Orientation.Horizontal)
            {
                points[0] = new Point((w / 2), d + (sW / 2));
                points[1] = new Point(points[0].X - 10, points[0].Y);
                points[2] = new Point(points[0].X + 10, points[0].Y);
            }
            else
            {
                points[0] = new Point(d + (sW / 2), (h / 2));
                points[1] = new Point(points[0].X, points[0].Y - 10);
                points[2] = new Point(points[0].X, points[0].Y + 10);
            }

            foreach (Point p in points)
            {
                p.Offset(-2, -2);
                e.Graphics.FillEllipse(SystemBrushes.ControlDark,
                    new Rectangle(p, new Size(3, 3)));

                p.Offset(1, 1);
                e.Graphics.FillEllipse(SystemBrushes.ControlLight,
                    new Rectangle(p, new Size(3, 3)));
            }
        }

        private void faTabStrip1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void faTabStrip1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesPath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string filePath in filesPath)
                CreateTab(filePath);
        }

        void open_btn_Click(object sender, EventArgs e) => openFile();
        void открытьToolStripMenuItem_Click(object sender, EventArgs e) => openFile();
        private void openFile()
        {
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CreateTab(ofdMain.FileName);
        }

        private bool Save(Control tab, SaveFileDialog dialog)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);

            if (tab.Tag == null)
            {
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Name = Path.GetFileName(dialog.FileName);
                tab.Tag = dialog.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab, sfdMain);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (tabControl3.SelectedTab != null)
                Save(tabControl3.SelectedTab, sfdMain);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl3.SelectedTab != null)
                Save(tabControl3.SelectedTab, sfdMain);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl3.SelectedTab != null)
                Save(tabControl3.SelectedTab, sfdSaveAs);
        }

        private void tsFiles_TabStripItemClosing(Control tab)
        {
            if (((FastColoredTextBox)tab.Controls[0]).IsChanged)
            {
                switch (MessageBox.Show("Хотите ли вы сохранить файл - " + tab.Text + " ?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Yes:
                        Save(tab, sfdMain);
                        break;
                    case DialogResult.No:
                        tabControl3.Controls.Remove(tabControl3.SelectedTab);
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        private void tmUpdateInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTB != null && tabControl3.Controls.Count > 0)
                {
                    var tb = CurrentTB;
                    save_btn.Enabled = saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = tb.IsChanged;

                    undo_btn.Enabled = undoToolStripMenuItem.Enabled = tb.UndoEnabled;
                    redo_btn.Enabled = redoToolStripMenuItem.Enabled = tb.RedoEnabled;

                    paste_btn.Enabled = pasteToolStripMenuItem.Enabled = true;
                    cut_btn.Enabled = cutToolStripMenuItem.Enabled =
                    copy_btn.Enabled = copyToolStripMenuItem.Enabled = !tb.Selection.IsEmpty;

                    selectAllToolStripMenuItem.Enabled = tb.CanSelect;
                    CloseCurrentTab.Enabled = true;
                }
                else
                {
                    save_btn.Enabled = saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = false;

                    undo_btn.Enabled = undoToolStripMenuItem.Enabled = false;
                    redo_btn.Enabled = redoToolStripMenuItem.Enabled = false;

                    cut_btn.Enabled = cutToolStripMenuItem.Enabled =
                    copy_btn.Enabled = copyToolStripMenuItem.Enabled = false;
                    paste_btn.Enabled = pasteToolStripMenuItem.Enabled = false;
                    selectAllToolStripMenuItem.Enabled = false;

                    CloseCurrentTab.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        void undo_btn_Click(object sender, EventArgs e)
        {
            
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        void redo_btn_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }
        void copy_btn_Click(object sender, EventArgs e) => CurrentTB.Copy();
        void copyToolStripMenuItem_Click(object sender, EventArgs e) => CurrentTB.Copy();
        void cutToolStripMenuItem_Click(object sender, EventArgs e) => CurrentTB.Cut();
        void cut_btn_Click(object sender, EventArgs e) => CurrentTB.Cut();
        void deleteToolStripMenuItem_Click(object sender, EventArgs e) => CurrentTB.SelectedText = "";

        void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();
        void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => CurrentTB.Selection.SelectAll();
        void paste_btn_Click(object sender, EventArgs e) => CurrentTB.Paste();
        void pasteToolStripMenuItem_Click(object sender, EventArgs e) => CurrentTB.Paste();
     
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            while(tabControl3.Controls.Count > 0)
            {
                Control tab = tabControl3.Controls[0];
                tsFiles_TabStripItemClosing(tab);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tsFiles_TabStripItemClosing(tabControl3.SelectedTab);
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "spr.html";
            string appPath = Assembly.GetEntryAssembly().Location;
            string filename = Path.Combine(Path.GetDirectoryName(appPath), path);
            Process.Start(filename);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram AboutProgram = new AboutProgram();
            AboutProgram.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            вызовСправкиToolStripMenuItem_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            оПрограммеToolStripMenuItem_Click(sender, e);
        }
    }
}

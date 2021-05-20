#region

using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FSLibrary;
using FSException;
using FSGraphics;

#endregion

namespace FSFormControls
{
    public class DBForm : Form
    {
        private Color m_GradientEndColor = Color.White;
        private LinearGradientMode m_GradientMode = LinearGradientMode.Horizontal;
        private Color m_GradientStartColor = Color.Blue;
        private Form m_mdiParent;
        private Global.AccessMode m_Mode = Global.AccessMode.ReadMode;
        private bool m_ShowMenu = true;
        //private bool m_ShowStatusBar = true;
        private bool m_ShowToolBar = true;

        public bool CanClose { get; set; } = true;

        //public DbConnection DBConnection { get; set; }

        public Form MDIMain
        {
            get { return m_mdiParent; }
            set
            {
                if (m_mdiParent.IsMdiContainer)
                {
                    m_mdiParent = value;
                    MdiParent = m_mdiParent;
                }
            }
        }

        public bool AlertOnSave { get; set; } = true;

        public bool AutoSave { get; set; }

        public double AutoSaveTime { get; set; } = 10 * 60 * 1000;

        public TabOrderManager.TabScheme TabOrder { get; set; } = TabOrderManager.TabScheme.AcrossFirst;


        public Color GradientStartColor
        {
            get { return m_GradientStartColor; }
            set
            {
                m_GradientStartColor = value;
                Invalidate();
            }
        }

        public Color GradientEndColor
        {
            get { return m_GradientEndColor; }
            set
            {
                m_GradientEndColor = value;
                Invalidate();
            }
        }

        public LinearGradientMode GradientMode
        {
            get { return m_GradientMode; }
            set
            {
                m_GradientMode = value;
                Invalidate();
            }
        }

        public bool Gradient { get; set; }

        public bool ShowContextMenu { get; set; } = true;


        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                ModeAllControls(Controls, value);
            }
        }


        public bool ShowToolBar
        {
            get { return m_ShowToolBar; }
            set
            {
                m_ShowToolBar = value;
                DbToolBar1.Visible = m_ShowToolBar;
            }
        }

        //public bool ShowStatusBar
        //{
        //    get { return m_ShowStatusBar; }
        //    set
        //    {
        //        m_ShowStatusBar = value;
        //        DbStatusBar1.Visible = m_ShowStatusBar;
        //    }
        //}

        public bool ShowMenu
        {
            get { return m_ShowMenu; }
            set
            {
                m_ShowMenu = value;

                ShowMenuBar(m_ShowMenu);
            }
        }

        public DBToolBarEx.tToolbar ToolbarType
        {
            get { return DbToolBar1.ToolBarType; }
            set { DbToolBar1.ToolBarType = value; }
        }

        public StatusBar.StatusBarPanelCollection StatusBarPanels { get; set; }

        //public long ProgressPosition
        //{
        //    get { return DbStatusBarProgressPanel1.ProgressPosition; }
        //    set { DbStatusBarProgressPanel1.ProgressPosition = value; }
        //}

        //public long ProgressStartPoint
        //{
        //    get { return DbStatusBarProgressPanel1.StartPoint; }
        //    set { DbStatusBarProgressPanel1.StartPoint = value; }
        //}

        //public long ProgressEndPoint
        //{
        //    get { return DbStatusBarProgressPanel1.EndPoint; }
        //    set { DbStatusBarProgressPanel1.EndPoint = value; }
        //}

        public bool AllowNavigate
        {
            get { return DbToolBar1.AllowNavigate; }
            set { DbToolBar1.AllowNavigate = value; }
        }

        public bool AllowSearch
        {
            get { return DbToolBar1.AllowSearch; }
            set { DbToolBar1.AllowSearch = value; }
        }

        public bool AllowCancel
        {
            get { return DbToolBar1.AllowCancel; }
            set { DbToolBar1.AllowCancel = value; }
        }

        public bool AllowSave
        {
            get { return DbToolBar1.AllowSave; }
            set { DbToolBar1.AllowSave = value; }
        }

        public bool AllowAddNew
        {
            get { return DbToolBar1.AllowAddNew; }
            set { DbToolBar1.AllowAddNew = value; }
        }

        public bool AllowPrint
        {
            get { return DbToolBar1.AllowPrint; }
            set { DbToolBar1.AllowPrint = value; }
        }

        public bool AllowFilter
        {
            get { return DbToolBar1.AllowFilter; }
            set { DbToolBar1.AllowFilter = value; }
        }

        public bool AllowRecord
        {
            get { return DbToolBar1.AllowRecord; }
            set { DbToolBar1.AllowRecord = value; }
        }

        public bool AllowEdit
        {
            get { return DbToolBar1.AllowEdit; }
            set { DbToolBar1.AllowEdit = value; }
        }

        public bool AllowDelete
        {
            get { return DbToolBar1.AllowDelete; }
            set { DbToolBar1.AllowDelete = value; }
        }

        public bool AllowList
        {
            get { return DbToolBar1.AllowList; }
            set { DbToolBar1.AllowList = value; }
        }

        public bool ShowNavigate
        {
            get { return DbToolBar1.ShowNavigateButton; }
            set { DbToolBar1.ShowNavigateButton = value; }
        }

        public bool ShowSearch
        {
            get { return DbToolBar1.ShowSearchButton; }
            set { DbToolBar1.ShowSearchButton = value; }
        }

        public bool ShowScrollBar
        {
            get { return DbToolBar1.ShowScrollBar; }
            set { DbToolBar1.ShowScrollBar = value; }
        }

        public bool ShowCancel
        {
            get { return DbToolBar1.ShowCancelButton; }
            set { DbToolBar1.ShowCancelButton = value; }
        }

        public bool ShowSave
        {
            get { return DbToolBar1.ShowSaveButton; }
            set { DbToolBar1.ShowSaveButton = value; }
        }

        public bool ShowAddNew
        {
            get { return DbToolBar1.ShowAddNewButton; }
            set { DbToolBar1.ShowAddNewButton = value; }
        }

        public bool ShowPrint
        {
            get { return DbToolBar1.ShowPrintButton; }
            set { DbToolBar1.ShowPrintButton = value; }
        }

        public bool ShowFilter
        {
            get { return DbToolBar1.ShowFilterButton; }
            set { DbToolBar1.ShowFilterButton = value; }
        }

        public bool ShowRecord
        {
            get { return DbToolBar1.ShowRecordButton; }
            set { DbToolBar1.ShowRecordButton = value; }
        }

        public bool ShowEdit
        {
            get { return DbToolBar1.ShowEditButton; }
            set { DbToolBar1.ShowEditButton = value; }
        }

        public bool ShowDelete
        {
            get { return DbToolBar1.ShowDeleteButton; }
            set { DbToolBar1.ShowDeleteButton = value; }
        }

        public bool ShowList
        {
            get { return DbToolBar1.ShowListButton; }
            set { DbToolBar1.ShowListButton = value; }
        }

        public bool ShowClose
        {
            get { return DbToolBar1.ShowCloseButton; }
            set { DbToolBar1.ShowCloseButton = value; }
        }

        public bool AutomaticConnect { get; set; } = true;

        //public DBStatusBar StatusBar
        //{
        //    get { return DbStatusBar1; }
        //    set { DbStatusBar1 = value; }
        //}

        //public void ProgressStep()
        //{
        //    DbStatusBarProgressPanel1.Step();
        //}

        //public void ProgressReset()
        //{
        //    DbStatusBarProgressPanel1.Reset();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (Gradient)
                GraphicsUtil.DrawGradient(this, m_GradientStartColor, m_GradientEndColor, m_GradientMode);
        }

        private void DBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = null;

            if (AutoSave) DataControl.Save();

            if (AlertOnSave == false) return;

            if (DataControl != null)
            {
                dt = DataControl.HasDataToSave();
                if (dt != null)
                {
                    var frmS = new frmShowTable();
                    frmS.DataTable = dt;
                    frmS.ShowDialog("Tiene datos sin guardar. �Est� seguro de querer salir?", "Salir");
                    if (frmS.button == frmShowTable.button_enum.no)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        //if (DBConnection != null)
                        //    if (DBConnection.State == ConnectionState.Open)
                        //        DBConnection.Close();
                        e.Cancel = false;
                    }
                }
                else
                {
                    e.Cancel = false;
                }
            }
            else
            {
                e.Cancel = false;
            }

            if (!CanClose)
                e.Cancel = true;
        }


        private void DBForm_Load(object sender, EventArgs e)
        {
            //si estamos en modo dise�o, no hacer nada
            if(this.Site != null && this.Site.DesignMode)
                return;

            Cursor.Current = Cursors.WaitCursor;

            //DbOfficeMenu1.Start(this);

            UpdateTabPageData(this);


            if (DataControl != null)
            {
                DataControl.ChangeRecord += m_DBControl_ChangeRecord;

                //if (!(DataControl.TypeDB == DBControl.DbType.XML || DataControl.TypeDB == DBControl.DbType.Data))
                //    if (DBConnection == null)
                //    {
                //        DBConnection = DataControl.DBConnection;
                //        if (DBConnection == null)
                //            if (AutomaticConnect)
                //                throw new ExceptionUtil(
                //                    "Imposible establecer conexi�n con la base de datos. Propiedad DBConnection, no establecida." +
                //                    "\r\n" + "Formulario: " + Name);
                //    }
            }


            if (AutomaticConnect)
            {
                ConnectDBControls(Controls);
                FillCombosAndGrids(Controls);
            }

            DbToolBar1.DataControl = DataControl;

            DbToolBar1.Initialize();


            if (ShowContextMenu == false) ContextMenuStrip = null;

            LinkUnBoundControls(this);


            if (CanClose == false) 
                ShowClose = false;


            //if (DBConnection != null)
            //    if (DBConnection.State == ConnectionState.Open)
            //        DBConnection.Close();

            //DbStatusBar1.Panels[0].Text = "Usuario: " + Global.UserName;

            if (DataControl != null) 
                UpdateScrollBar();

            tmrAutoSave.Enabled = AutoSave;

            if (AutoSaveTime > 0)
                tmrAutoSave.Interval = Convert.ToInt32(AutoSaveTime);
            else
                tmrAutoSave.Enabled = false;

            var tom = new TabOrderManager(this);
            tom.SetTabOrder(TabOrder);

            Cursor.Current = Cursors.Default;
        }


        private void ModeAllControls(Control.ControlCollection frm, Global.AccessMode mode)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ModeAllControls(ctr.Controls, mode);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).Mode = mode;
                }
        }

        public new void Refresh()
        {
            Reconnect(this);
        }


        private bool Reconnect(Form frm)
        {
            if (frm == null) return false;

            foreach (Control ctr in frm.Controls)
                if (FunctionsForms.IsContainer(ctr))
                {
                    Reconnect((Form) ctr);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).ReConnect();
                }

            return false;
        }


        public void InitializeScrollBar()
        {
            DbToolBar1.DataControl = DataControl;
            DbToolBar1.Initialize();
            DataControl.Go(0);
        }


        private void LinkUnBoundControls(Control frm)
        {
            try
            {
                foreach (Control ctr in frm.Controls)
                {
                    if (FunctionsForms.IsContainer(ctr))
                        LinkUnBoundControls(ctr);


                    if (ctr is DBCheckBox)
                    {
                        if (((DBCheckBox)ctr).DataControl == null)
                            ((DBCheckBox)ctr).UpdateCheckBox();
                    }

                    if (ctr is DBTextBox)
                    {
                        if (((DBTextBox)ctr).DataControl == null)
                            ((DBTextBox)ctr).UpdateText();
                    }

                    if (ctr is DBFindTextBox)
                    {
                        if (((DBFindTextBox)ctr).DataControl == null)
                            ((DBFindTextBox)ctr).UpdateText();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        //private void TrackMode(Control frm, bool mode)
        //{
        //    try
        //    {
        //        foreach (Control ctr in frm.Controls)
        //        {
        //            if (FunctionsForms.IsContainer(ctr)) TrackMode(ctr, mode);
        //            if (ctr is DBUserControl) ((DBUserControl) ctr).Track = mode;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExceptionUtil(ex);
        //    }
        //}


        public void Save()
        {
            DbToolBar1.Save();
        }


        private void MnuEditar(object sender, EventArgs e)
        {
            //TrackMode(this, true);

            ((ToolStripMenuItem)mnuContext.Items[11]).Checked = true;
            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = false;
        }


        private void MnuNormal(object sender, EventArgs e)
        {
            //TrackMode(this, false);

            ((ToolStripMenuItem)mnuContext.Items[11]).Checked = false;
            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = true;
        }


        private void MnuFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.ShowFilter();
        }


        private void MnuRefresh(object sender, EventArgs e)
        {
            Refresh();
        }


        private void MnuDelFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.DeleteFilter();
        }


        private void MnuFind(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.ShowFind();
        }


        private void MnuAcercade(object sender, EventArgs e)
        {
            try
            {
                var s = new frmAbout();
                s.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void MnuFindNext(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.FindNext();
        }


        private void SaveAsHTML(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.ShowDialog();
                var fic = SaveFileDialog1.FileName;
                if (fic == "") return;
                var tw = new StreamWriter(fic);

                var dbform2html = new Form2Html();
                tw.Write(dbform2html.GenerateHTML(this));
                tw.Close();
                tw = null;
                ProcessUtil.OpenDocument(fic);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Errores en la exportaci�n.", ex);
            }
        }


        private void PrintDocument(object sender, EventArgs e)
        {
            var fp = new DBFormPrint(this);
            fp.PrintPreview = false;
            fp.Print();
        }


        private void PrintPreview(object sender, EventArgs e)
        {
            var fp = new DBFormPrint(this);
            fp.PrintPreview = true;
            fp.Print();
        }


        private void ConnectDBControls(Control.ControlCollection frm)
        {
            if (frm == null) 
                return;


            PutDBConnection(frm);

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ConnectDBControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl == null)
                        {
                            if (!string.IsNullOrEmpty(((DBControl) ctr).Selection))
                            {
                                //if (DBConnection != null)
                                //{
                                //    ((DBControl) ctr).DBConnection = DBConnection;
                                //    if (((DBControl) ctr).AutoConnect) 
                                //        ((DBControl) ctr).Connect();
                                //}
                                if (((DBControl)ctr).AutoConnect)
                                    ((DBControl)ctr).Connect();
                            }
                            else
                            {
                                if (((DBControl) ctr).TypeDB == DBControl.DbType.XML ||
                                    ((DBControl) ctr).TypeDB == DBControl.DbType.Data)
                                    if (((DBControl) ctr).AutoConnect)
                                        ((DBControl) ctr).Connect();
                            }
                        }
                }
        }

        private void FillCombosAndGrids(Control.ControlCollection frm)
        {
            if (frm == null) return;


            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillCombosAndGrids(ctr.Controls);
                }
                else
                {
                    if (ctr is DBCombo)
                        ((DBCombo)ctr).Fill();

                    if (ctr is DBGrid)
                        ((DBGrid)ctr).Fill();
                }
        }

        private void PutDBConnection(Control.ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    PutDBConnection(ctr.Controls);
                }
                //else
                //{
                //    if (ctr is DBControl)
                //        if (((DBControl) ctr).DBConnection == null)
                //            ((DBControl) ctr).DBConnection = DBConnection;
                //}
        }


        private void mnuConfigurarPagina_Click(object sender, EventArgs e)
        {
            PageSetup.Setup();
        }


        private void mnuCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowMenuBar(bool show)
        {
            mnuForm.Visible = show;
            for (var f = 0; f <= mnuFormMain.MenuItems.Count - 1; f++) mnuFormMain.MenuItems[f].Visible = show;
        }

        private void mnuCalculadora_Click(object sender, EventArgs e)
        {
            var fCalc = new frmCalculator();
            fCalc.Show();
        }


        public void UpdateTabPageData(Control con)
        {
            if (con.HasChildren)
                foreach (Control subcon in con.Controls)
                    if (subcon is TabPage | subcon is TabControl)
                    {
                        if (subcon is TabPage) ((TabPage) subcon).Visible = true;
                        UpdateTabPageData(subcon);
                    }
        }


        private void m_DBControl_ChangeRecord()
        {
            UpdateScrollBar();
        }


        private void UpdateScrollBar()
        {
            //DbStatusBar1.Panels[2].Text = "SQL: " + DataControl.Selection;
            //DbStatusBar1.Panels[3].Text = "Tabla: " + DataControl.TableName;
            //DbStatusBar1.Panels[4].Text = DataControl.DBPosition + 1 + "/" + DataControl.RecordCount();
        }


        private void tmrAutoSave_Tick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //var tx = StatusBar.Panels[0].Text;
            //StatusBar.Panels[0].Text = "Guardando datos ...";
            DataControl.Save();
            //StatusBar.Panels[0].Text = "Datos guardados";
            //StatusBar.Panels[0].Text = tx;
            Cursor.Current = Cursors.Default;
        }


        private void mnuAcercaDe_Click(object sender, EventArgs e)
        {
            try
            {
                var s = new frmAbout();
                s.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        #region '" C�digo generado por el Dise�ador de Windows Forms "' 

        internal DBOfficeMenu DbOfficeMenu1;
        internal TabOrderSchemaProvider DbTabOrderSchemeProvider1;
        public DBToolBarEx DbToolBar1;
        internal MenuItem MenuItem3;
        internal MenuItem MenuItem7;
        internal SaveFileDialog SaveFileDialog1;
        private readonly IContainer components = null;
        internal MenuItem mnuAbout;
        internal MenuItem mnuCalc;
        internal MenuItem mnuClose;
        internal MenuItem mnuConfPag;
        internal ContextMenuStrip mnuContext;
        public MenuItem mnuForm;
        public MainMenu mnuFormMain;
        internal Timer tmrAutoSave;


        private DBControl m_DataControl;
        /// <summary>
        /// Asignaci�n del DBcontrol.
        /// Asignamos el parent del dbcontrol cuando se user dl dbcontrol sin asignar a un formulario.
        /// </summary>
        [Description("Control de datos para la gesti�n de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set
            {
                if (value != null && value.Parent is null)
                    value.Parent = this;
                m_DataControl = value;
            }
        }

        public DBForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            mnuContext.Items.Add("&Imprimir", null, PrintDocument);
            mnuContext.Items.Add("&Vista Preliminar", null, PrintPreview);
            mnuContext.Items.Add("&Guardar como HTML", null, SaveAsHTML);
            mnuContext.Items.Add("&Refrescar", null, MnuRefresh);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Filtro", null, MnuFilter);
            mnuContext.Items.Add("&Quitar filtro", null, MnuDelFilter);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Buscar", null, MnuFind);
            mnuContext.Items.Add("&Buscar siguiente", null, MnuFindNext);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Modo Editar", null, MnuEditar);
            mnuContext.Items.Add("Modo &Normal", null, MnuNormal);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Acerca de ...", null, MnuAcercade);

            ((ToolStripMenuItem)mnuContext.Items[11]).Checked = false;
            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = true;


            //Guardamos la referencia al formulario en una variable global para poder consultarlo despues.
            if (!Global.Forms.Exist(this))
                Global.Forms.Add(this);

            this.Load += DBForm_Load;
            this.FormClosing += DBForm_FormClosing;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);

            Global.Forms.Remove(this);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBForm));
            this.mnuFormMain = new System.Windows.Forms.MainMenu();
            this.mnuForm = new System.Windows.Forms.MenuItem();
            this.mnuConfPag = new System.Windows.Forms.MenuItem();
            this.mnuCalc = new System.Windows.Forms.MenuItem();
            this.MenuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.MenuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuClose = new System.Windows.Forms.MenuItem();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tmrAutoSave = new System.Windows.Forms.Timer();
            this.DbToolBar1 = new FSFormControls.DBToolBarEx();
            this.DbOfficeMenu1 = new FSFormControls.DBOfficeMenu();
            this.DbTabOrderSchemeProvider1 = new FSFormControls.TabOrderSchemaProvider();
            this.SuspendLayout();
            // 
            // mnuFormMain
            // 
            this.mnuFormMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuForm});
            // 
            // mnuForm
            // 
            this.mnuForm.Index = 0;
            this.mnuForm.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuConfPag,
            this.mnuCalc,
            this.MenuItem7,
            this.mnuAbout,
            this.MenuItem3,
            this.mnuClose});
            this.mnuForm.Text = "&Formulario";
            // 
            // mnuConfPag
            // 
            this.mnuConfPag.Index = 0;
            this.mnuConfPag.Text = "&Configuraci�n p�gina";
            this.mnuConfPag.Click += new System.EventHandler(this.mnuConfigurarPagina_Click);
            // 
            // mnuCalc
            // 
            this.mnuCalc.Index = 1;
            this.mnuCalc.Text = "Calculadora";
            this.mnuCalc.Click += new System.EventHandler(this.mnuCalculadora_Click);
            // 
            // MenuItem7
            // 
            this.MenuItem7.Index = 2;
            this.MenuItem7.Text = "-";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 3;
            this.mnuAbout.Text = "&Acerca de ...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAcercaDe_Click);
            // 
            // MenuItem3
            // 
            this.MenuItem3.Index = 4;
            this.MenuItem3.Text = "-";
            // 
            // mnuClose
            // 
            this.mnuClose.Index = 5;
            this.mnuClose.Text = "&Cerrar";
            this.mnuClose.Click += new System.EventHandler(this.mnuCerrar_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(61, 4);
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "Archivos HTML|*.htm*|Todos los archivos|*.*";
            // 
            // tmrAutoSave
            // 
            this.tmrAutoSave.Interval = 60000;
            this.tmrAutoSave.Tick += new System.EventHandler(this.tmrAutoSave_Tick);
            // 
            // DbToolBar1
            // 
            
            this.DbToolBar1.AllowAddNew = true;
            this.DbToolBar1.AllowCancel = true;
            this.DbToolBar1.AllowClose = true;
            this.DbToolBar1.AllowDelete = true;
            this.DbToolBar1.AllowEdit = true;
            this.DbToolBar1.AllowFilter = true;
            this.DbToolBar1.AllowList = true;
            this.DbToolBar1.AllowNavigate = true;
            this.DbToolBar1.AllowPrint = true;
            this.DbToolBar1.AllowRecord = true;
            this.DbToolBar1.AllowSave = true;
            this.DbToolBar1.AllowSearch = true;
            this.DbToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.DbToolBar1.Location = new System.Drawing.Point(0, 0);
            this.DbToolBar1.Name = "DbToolBar1";
            this.DbToolBar1.ShowAddNewButton = true;
            this.DbToolBar1.ShowCancelButton = true;
            this.DbToolBar1.ShowCloseButton = true;
            this.DbToolBar1.ShowDeleteButton = true;
            this.DbToolBar1.ShowEditButton = true;
            this.DbToolBar1.ShowFilterButton = true;
            this.DbToolBar1.ShowListButton = true;
            this.DbToolBar1.ShowNavigateButton = true;
            this.DbToolBar1.ShowPrintButton = true;
            this.DbToolBar1.ShowRecordButton = true;
            this.DbToolBar1.ShowSaveButton = true;
            this.DbToolBar1.ShowScrollBar = true;
            this.DbToolBar1.ShowSearchButton = true;
            this.DbToolBar1.Size = new System.Drawing.Size(1042, 50);
            this.DbToolBar1.TabIndex = 1;
            this.DbToolBar1.TabStop = false;
            this.DbToolBar1.ToolBarType = FSFormControls.DBToolBarEx.tToolbar.ToolbarXPBig;
            this.DbToolBar1.Value = 0;
            this.DbToolBar1.VisibleScroll = true;
            this.DbToolBar1.VisibleTotalRecord = false;
            // 
            // DbOfficeMenu1
            // 
            this.DbOfficeMenu1.ImageList = null;
            // 
            // DBForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1042, 354);
            this.ContextMenuStrip = this.mnuContext;
            this.Controls.Add(this.DbToolBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mnuFormMain;
            this.Name = "DBForm";
            this.Text = "DBForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
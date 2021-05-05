#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBImage.bmp")]
    [ToolboxItem(true)]
    public class DBImage
        : DBUserControlBase
    {
        private AccessMode m_Mode = AccessMode.ReadMode;

        public new BorderStyle BorderStyle
        {
            get { return PictureBox1.BorderStyle; }
            set { PictureBox1.BorderStyle = value; }
        }

        public PictureBoxSizeMode SizeMode
        {
            get { return PictureBox1.SizeMode; }
            set { PictureBox1.SizeMode = value; }
        }


        //[Description("DataBindings.")]
        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return PictureBox1.DataBindings; }
        //}


        public AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;

                switch (m_Mode)
                {
                    case AccessMode.ReadMode:
                        ContextMenu1.MenuItems[0].Enabled = false;
                        break;
                    case AccessMode.WriteMode:
                        ContextMenu1.MenuItems[0].Enabled = true;
                        break;
                }
            }
        }

        public void UpdateImage()
        {
            //if (PictureBox1.DataBindings.Count > 0) return;

            //Binding dbnPicture = new Binding("Image", DataControl.DataTable, DBField);

            if (DataControl != null)
                LoadImage(DataControl.GetFieldByte(DBField), DataControl.StoreInBase64Format);

            //dbnPicture.Format += dbnFormat;

            //try
            //{
            //    PictureBox1.DataBindings.Add(dbnPicture);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil(e);
            //}
        }


        private void LoadImage(byte[] image, bool isBase64)
        {
            if (isBase64)
                image = Convert.FromBase64String(Encoding.ASCII.GetString(image));

            if (image == null)
            {
                var b = new Bitmap(150, 150);
                PictureBox1.Image = b;
                Label1.Visible = true;
            }
            else
            {
                var ms = new MemoryStream();
                var offset = 0;
                ms.Write(image, offset, image.Length - offset);
                var bmp = new Bitmap(ms);
                ms.Close();

                PictureBox1.Image = bmp;
            }
        }


        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter =
                    "Todos los formatos gr�ficos|*.gif;*.jpg;*.ico;*.wmf|JPEG (*.jpg)|*.jpg|Mapa de bits (*.bmp)|*.bmp|GIF (*.gif)|*.gif|Metarchivo (*.wmf)|*.wmf|Icono (*.ico)|*.ico|Todos los archivos (*.*)|*.*";
                OpenFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(OpenFileDialog1.FileName))
                {
                    PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName);
                    Label1.Visible = false;
                }
            }
            catch (Exception)
            {
                throw new ExceptionUtil("Formato de imagen, no valido.");
            }
        }


        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Width = PictureBox1.Width;
            Height = PictureBox1.Height;
        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Mode == AccessMode.WriteMode)
                ContextMenu1.Show(this, e.Location);
        }


        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region '" C�digo generado por el Dise�ador de Windows Forms "' 

        internal ContextMenu ContextMenu1;
        internal Label Label1;
        internal MenuItem MenuItem1;
        internal OpenFileDialog OpenFileDialog1;
        internal PictureBox PictureBox1;
        internal ToolTip ToolTip1;
        private IContainer components;

        public DBImage()
        {
            InitializeComponent();

            MenuItem1.Click += MenuItem1_Click;
            PictureBox1.MouseDown += PictureBox1_MouseDown;
            PictureBox1.MouseUp += PictureBox1_MouseUp;
            PictureBox1.SizeChanged += PictureBox1_SizeChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            PictureBox1 = new PictureBox();
            ContextMenu1 = new ContextMenu();
            MenuItem1 = new MenuItem();
            OpenFileDialog1 = new OpenFileDialog();
            Label1 = new Label();
            ToolTip1 = new ToolTip(components);
            ((ISupportInitialize) PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // PictureBox1
            // 
            PictureBox1.BorderStyle = BorderStyle.FixedSingle;
            PictureBox1.ContextMenu = ContextMenu1;
            PictureBox1.Dock = DockStyle.Fill;
            PictureBox1.Location = new Point(0, 0);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(145, 141);
            PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // ContextMenu1
            // 
            ContextMenu1.MenuItems.AddRange(new[]
            {
                MenuItem1
            });
            // 
            // MenuItem1
            // 
            MenuItem1.Index = 0;
            MenuItem1.Text = "Seleccionar Imagen";
            // 
            // Label1
            // 
            Label1.BorderStyle = BorderStyle.Fixed3D;
            Label1.ContextMenu = ContextMenu1;
            Label1.Dock = DockStyle.Fill;
            Label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(145, 141);
            Label1.TabIndex = 1;
            Label1.Text = "Imagen no definida. Utilice bot�n derecho y \'Seleccionar Imagen\', en modo \'Edici�" +
                          "n\', para seleccionar la imagen deseada.";
            ToolTip1.SetToolTip(Label1,
                "Imagen no definida. Utilice bot�n derecho y \'Seleccionar Imagen\', en modo \'Edici�" +
                "n\', para seleccionar la imagen deseada.");
            Label1.Visible = false;
            // 
            // DBImage
            // 
            Controls.Add(Label1);
            Controls.Add(PictureBox1);
            Name = "DBImage";
            Size = new Size(145, 141);
            ((ISupportInitialize) PictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
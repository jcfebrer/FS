#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using StringConverter = System.ComponentModel.StringConverter;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBLopd : DBUserControl
    {
        //private string m_Table;

        public enum Action
        {
            Add,
            Modify,
            Delete,
            Show
        }

        public string User { get; set; }


        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara el identificador del usuario.")]
        public string LOPD_UserField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description(
            "Campo de la base de datos que almacenar? la operaci�n realizada (alta, baja, modificaci�n, consulta, etc.)"
        )]
        public string LOPD_OperationField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara la fecha de la operaci�n.")]
        public string LOPD_DateField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara la fecha de la operaci�n.")]
        public string LOPD_TimeField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara el recurso accedido.")]
        public string LOPD_RecurseField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenar? el c�digo de recurso accedido.")]
        public string LOPD_RecurseCodeField { get; set; }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignaci�n del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gesti�n de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        public void Save(string tableName, Action operation)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string registerCode = DataControl.GetField(db.PrimaryKeyName(tableName)).ToString();
            string ssql;

            ssql = "INSERT INTO " + DataControl.TableName + " (" + LOPD_DateField + "," + LOPD_TimeField + "," +
                   LOPD_OperationField + "," + LOPD_RecurseCodeField + "," + LOPD_RecurseField + "," + LOPD_UserField +
                   ") VALUES ('" + DateTimeUtil.ShortDate(System.DateTime.Now) + "','" +
                   DateTimeUtil.ShortDate(System.DateTime.Now) + "','" +
                   TextCode(operation) + "','" + registerCode + "','" + tableName + "','" + User + "')";

            try
            {
                db.ExecuteScalar(ssql);
            }
            catch
            {
                throw new ExceptionUtil("Error almacenando informaci�n de LOPD. SQL: " + ssql);
            }
        }


        private string TextCode(Action code)
        {
            switch (code)
            {
                case Action.Add:
                    return "A�adir";
                case Action.Show:
                    return "Cosultar";
                case Action.Delete:
                    return "Eliminar";
                case Action.Modify:
                    return "Modificar";
            }

            return string.Empty;
        }

        public class FieldList : StringConverter
        {
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(DBControl.Fields);
            }


            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }


            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
        }

        #region '" C�digo generado por el Dise�ador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label Label1;

        public DBLopd()
        {
            InitializeComponent();

            Visible = false;
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
            Label1 = new Label();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.BackColor = Color.FromArgb(192, 255, 192);
            Label1.BorderStyle = BorderStyle.FixedSingle;
            Label1.Dock = DockStyle.Fill;
            Label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(136, 48);
            Label1.TabIndex = 0;
            Label1.Text = "LOPD (Ley Org�nica de Protecci�n de Datos)";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DBLopd
            // 
            Controls.Add(Label1);
            Name = "DBLopd";
            Size = new Size(136, 48);
            ResumeLayout(false);
        }

        #endregion
    }
}
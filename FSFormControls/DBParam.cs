#region

using System.ComponentModel;

#endregion

namespace FSFormControls
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class DBParam : Component
    {
        public DBParam()
        {
        }

        public DBParam(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Nombre del par�metro
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Name { get; set; } = "";

        /// <summary>
        ///     Valor del par�metro
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value { get; set; }

    }
}
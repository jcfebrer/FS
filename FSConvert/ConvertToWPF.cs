﻿using FSTrace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace FSConvert
{
    public class ConvertToWPF
    {
        public static bool Convert_old(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new Exception($"Archivo no encontrado: {inputFile}");
            }

            string content = File.ReadAllText(inputFile);

            // 1. Eliminar Suspensión y Reanudación de Diseño
            content = Regex.Replace(content, @"this\.SuspendLayout\(\);[\s\S]*?this\.ResumeLayout\(false\);\s*this\.PerformLayout\(\);", "");

            // 2. Controles Comunes de Windows Forms
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Button\(\);", "<Button Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Label\(\);", "<TextBlock Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TextBox\(\);", "<TextBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.CheckBox\(\);", "<CheckBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.RadioButton\(\);", "<RadioButton Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ComboBox\(\);", "<ComboBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ListBox\(\);", "<ListBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.DataGridView\(\);", "<DataGrid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.PictureBox\(\);", "<Image Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.Panel\(\);", "<Grid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.GroupBox\(\);", "<GroupBox Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TabControl\(\);", "<TabControl Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TabPage\(\);", "<TabItem Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.FlowLayoutPanel\(\);", "<WrapPanel Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TableLayoutPanel\(\);", "<Grid Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.TrackBar\(\);", "<Slider Name=\"$1\" />");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+) = new System\.Windows\.Forms\.ProgressBar\(\);", "<ProgressBar Name=\"$1\" />");

            // 3. Propiedades de los Controles
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.BackColor = System\.Drawing\.Color\.(\w+);", "Background=\"{x:Static SystemColors.$2Brush}\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.ForeColor = System\.Drawing\.Color\.(\w+);", "Foreground=\"{x:Static SystemColors.$2Brush}\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Size = new System\.Drawing\.Size\((\d+), (\d+)\);", "Width=\"$2\" Height=\"$3\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Location = new System\.Drawing\.Point\((\d+), (\d+)\);", "Margin=\"$2,$3,0,0\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Text = ""([^""]+)"";", "Content=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Enabled = false;", "IsEnabled=\"False\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Visible = false;", "Visibility=\"Collapsed\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Anchor = System\.Windows\.Forms\.AnchorStyles\.(\w+);", "HorizontalAlignment=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Dock = System\.Windows\.Forms\.DockStyle\.(\w+);", "VerticalAlignment=\"$2\"");

            // 4. Eventos de los Controles
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.Click \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "Click=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.CheckedChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "Checked=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.TextChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "TextChanged=\"$2\"");
            content = Regex.Replace(content, @"this\.([a-zA-Z0-9_]+)\.SelectedIndexChanged \+= new System\.EventHandler\(this\.([a-zA-Z0-9_]+)\);", "SelectionChanged=\"$2\"");

            // 5. Declaración de Controles
            content = Regex.Replace(content, @"private System\.Windows\.Forms\.([a-zA-Z0-9_]+) ([a-zA-Z0-9_]+);", "");

            // Guardar el resultado
            File.WriteAllText(outputFile, content);

            Log.Trace($"Reemplazos completados. Archivo generado: {outputFile}");

            return true;
        }

        public static string Convert(string winFormsCode)
        {
            // Definir patrones y reemplazos para diferentes controles de Windows Forms.
            // Se usan literales de cadena (con comillas dobles delimitadoras) para el patrón
            // y en el reemplazo se utilizan comillas simples para delimitar los valores de atributos XAML.
            var controlPatterns = new List<(string Control, string Pattern, string Replacement)>()
            {
        (
            "Button",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.Button\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
              (?=.*this\.\k<name>\.Click\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandler>[a-zA-Z0-9_]+)\))?",
            "<fs:Button x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Width='${width}' Height='${height}' Click='${eventHandler}' />"
        ),
        (
            "DBButton",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBButton\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
              (?=.*this\.\k<name>\.Click\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandler>[a-zA-Z0-9_]+)\))?",
            "<fs:Button x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Width='${width}' Height='${height}' Click='${eventHandler}' />"
        ),
        (
            "Label",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.Label\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:Label x:Name='${name}' Content='${text}' Padding='0' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBLabel",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBLabel\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:Label x:Name='${name}' Content='${text}' Padding='0' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "TextBox",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.TextBox\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:TextBox x:Name='${name}' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBTextBox",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBTextBox\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:TextBox x:Name='${name}' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DatePicker",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.DatePicker\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:DatePicker x:Name='${name}' Padding='0' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBDate",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBDate\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:DatePicker x:Name='${name}' Padding='0' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "CheckBox",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.CheckBox\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
              (?=.*this\.\k<name>\.CheckedChanged\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandler>[a-zA-Z0-9_]+)\))?
              (?=.*this\.\k<name>\.Enter\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandlerEnter>[a-zA-Z0-9_]+)\))?",
            "<fs:CheckBox x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' CheckedChanged='${eventHandler}' Enter='${eventHandlerEnter}' />"
        ),
        (
            "DBCheckBox",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBCheckBox\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
              (?=.*this\.\k<name>\.CheckedChanged\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandler>[a-zA-Z0-9_]+)\))?
              (?=.*this\.\k<name>\.Enter\s*\+=\s*new\s*System\.EventHandler\(this\.(?<eventHandlerEnter>[a-zA-Z0-9_]+)\))?",
            "<fs:CheckBox x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' CheckedChanged='${eventHandler}' Enter='${eventHandlerEnter}' />"
        ),
        (
            "RadioButton",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.RadioButton\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:RadioButton x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBRadioButton",
            @"this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBRadioButton\(\);\s*
              (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
              (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
              (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?",
            "<fs:RadioButton x:Name='${name}' Content='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "ComboBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.ComboBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"")?
            ",
            "<fs:ComboBox x:Name='${name}' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBCombo",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBCombo\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"")?
            ",
            "<fs:ComboBox x:Name='${name}' Text='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "ListBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.ListBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:ListBox x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBListBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.ListBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:ListBox x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DataGridView",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.DataGridView\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:DataGrid x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n{{columns}}\n</fs:DataGrid>"
        ),
        (
            "DBGridView",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBGridView\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:DataGrid x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n{{columns}}\n</fs:DataGrid>"
        ),
        (
            "DBGrid",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBGrid\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:DataGrid x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n{{columns}}\n</fs:DataGrid>"
        ),
        (
            "PictureBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.PictureBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Image x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBPictureBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBPictureBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Image x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "Panel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.Panel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:StackPanel x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:StackPanel>"
        ),
        (
            "DBPanel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBPanel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:StackPanel x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:StackPanel>"
        ),
        (
            "GroupBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.GroupBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
            ",
            "<fs:GroupBox x:Name='${name}' Header='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:GroupBox>"
        ),
        (
            "DBGroupBox",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBGroupBox\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
            ",
            "<fs:GroupBox x:Name='${name}' Header='${text}' HorizontalAlignment='Left' VerticalAlignment='Top' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:GroupBox>"
        ),
        (
            "TabControl",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.TabControl\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:TabControl x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  {{children}}\n</fs:TabControl>"
        ),
        (
            "DBTabControl",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBTabControl\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:TabControl x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  {{children}}\n</fs:TabControl>"
        ),
        (
            "TabPage",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.TabPage\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
            ",
            "<fs:TabItem x:Name='${name}' Header='${text}'>\n  <Grid>{{children}}</Grid>\n</fs:TabItem>"
        ),
        (
            "DBTabPage",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBTabPage\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            (?=.*this\.\k<name>\.Text\s*=\s*""(?<text>[^""]+)"" )?
            ",
            "<fs:TabItem x:Name='${name}' Header='${text}'>\n  <Grid>{{children}}</Grid>\n</fs:TabItem>"
        ),
        (
            "FlowLayoutPanel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.FlowLayoutPanel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:StackPanel x:Name='${name}' Orientation='Horizontal' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:StackPanel>"
        ),
        (
            "DBFlowLayoutPanel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBFlowLayoutPanel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:StackPanel x:Name='${name}' Orientation='Horizontal' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <Grid>{{children}}</Grid>\n</fs:StackPanel>"
        ),
        (
            "TableLayoutPanel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.TableLayoutPanel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Grid x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <!-- Define rows and columns -->\n</fs:Grid>"
        ),
        (
            "DBTableLayoutPanel",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBTableLayoutPanel\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Grid x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}'>\n  <!-- Define rows and columns -->\n</fs:Grid>"
        ),
        (
            "TrackBar",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.TrackBar\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Slider x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBTrackBar",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBTrackBar\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:Slider x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "ProgressBar",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*System\.Windows\.Forms\.ProgressBar\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:ProgressBar x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        ),
        (
            "DBProgressBar",
            @"
            this\.(?<name>[a-zA-Z0-9_]+)\s*=\s*new\s*FSFormControls\.DBProgressBar\(\);
            (?=.*this\.\k<name>\.Location\s*=\s*new\s*System\.Drawing\.Point\((?<x>\d+),\s*(?<y>\d+)\))?
            (?=.*this\.\k<name>\.Size\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\))?
            ",
            "<fs:ProgressBar x:Name='${name}' HorizontalAlignment='Left' VerticalAlignment='Top' Margin='${x},${y},0,0' Width='${width}' Height='${height}' />"
        )
                // Puedes agregar más patrones para otros controles (ComboBox, ListBox, etc.) aquí.
            };

            // Extraer el título de la ventana.
            var titleMatch = Regex.Match(winFormsCode, @"this\.Text\s*=\s*""(?<title>[^""]+)"";");
            string windowTitle = titleMatch.Success ? titleMatch.Groups["title"].Value : "Título de la ventana";

            // Extraer las dimensiones de la ventana.
            var sizeMatch = Regex.Match(winFormsCode, @"this\.ClientSize\s*=\s*new\s*System\.Drawing\.Size\((?<width>\d+),\s*(?<height>\d+)\);");
            string windowWidth = sizeMatch.Success ? sizeMatch.Groups["width"].Value : "800";
            string windowHeight = sizeMatch.Success ? sizeMatch.Groups["height"].Value : "450";

            // Extraer columnas de Grid.
            var gridColumnsRegex = new Regex(
                @"this\.(?<gridName>[a-zA-Z0-9_]+)\.Columns\.Add\(new\s*System\.Windows\.Forms\.DataGridViewTextBoxColumn\(\)\s*\{\s*HeaderText\s*=\s*""(?<header>[^""]+)""\s*\}\);");
            var gridColumnsMapping = new Dictionary<string, List<string>>();

            foreach (Match match in gridColumnsRegex.Matches(winFormsCode))
            {
                string gridName = match.Groups["gridName"].Value;
                string headerText = match.Groups["header"].Value;

                if (!gridColumnsMapping.ContainsKey(gridName))
                {
                    gridColumnsMapping[gridName] = new List<string>();
                }

                gridColumnsMapping[gridName].Add($"<DataGridTextColumn Header='{headerText}' />");
            }

            // Paso 1: Convertir cada control individualmente.
            var controlXamlMapping = new Dictionary<string, (string Xaml, string ControlType)>();

            foreach (var (control, pattern, replacement) in controlPatterns)
            {
                var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
                var matches = regex.Matches(winFormsCode);
                foreach (Match match in matches)
                {
                    if (match.Groups["name"].Success)
                    {
                        string name = match.Groups["name"].Value;
                        string xaml = replacement;

                        //Procesamos las columnas del grid
                        if ((control == "Grid" || control == "DBGrid" || control == "DBGridView") && gridColumnsMapping.ContainsKey(name))
                        {
                            string columnsXaml = string.Join("\n", gridColumnsMapping[name]);
                            xaml = xaml.Replace("{{columns}}", columnsXaml);
                        }

                        foreach (var groupName in regex.GetGroupNames())
                        {
                            if (match.Groups[groupName].Success)
                            {
                                xaml = xaml.Replace($"${{{groupName}}}", match.Groups[groupName].Value);
                            }
                            else
                            {
                                xaml = xaml.Replace($"${{{groupName}}}", "");
                            }
                        }
                        controlXamlMapping[name] = (xaml, control); // Sobrescribir si ya existe.
                    }
                }
            }

            // Paso 2: Extraer la jerarquía de contenedores.
            var containerRegex = new Regex(
                @"this\.(?<container>[a-zA-Z0-9_]+)\.Controls\.Add\(\s*this\.(?<child>[a-zA-Z0-9_]+)\s*\);",
                RegexOptions.Multiline);
            var containerMapping = new Dictionary<string, List<string>>();
            foreach (Match m in containerRegex.Matches(winFormsCode))
            {
                string container = m.Groups["container"].Value;
                string child = m.Groups["child"].Value;
                if (!containerMapping.ContainsKey(container))
                    containerMapping[container] = new List<string>();
                containerMapping[container].Add(child);
            }

            // Paso 3: Determinar los controles de nivel superior, incluyendo los del formulario.
            var topLevelControls = new List<string>();

            foreach(var container in containerMapping)
            {
                if(container.Value.Count > 0)
                    topLevelControls.Add(container.Key);
            }

            // Controles añadidos directamente al formulario (this.Controls.Add).
            if (containerMapping.ContainsKey("this"))
            {
                topLevelControls.AddRange(containerMapping["this"]);
            }

            // Controles definidos pero que no son hijos de ningún contenedor.
            var allChildControls = new HashSet<string>(containerMapping.Values.SelectMany(c => c));
            foreach (var control in controlXamlMapping.Keys)
            {
                if (!allChildControls.Contains(control) && !topLevelControls.Contains(control))
                {
                    topLevelControls.Add(control);
                }
            }

            //topLevelControls.Clear();
            //topLevelControls.Add("tabServicio");

            // Construir el XAML final combinando los controles de nivel superior.
            var finalXamlBuilder = new StringBuilder();
            finalXamlBuilder.AppendLine($"<Window " +
                $"x:Class = \"FSAppLauncher.MVVM.View.SubView.Window1\"\n" +
                $"xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n" +
                $"xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\n" +
                $"xmlns:fs=\"clr-namespace:FSFormControls.UI;assembly=FSFormControls.UI\"\n" +
                $"Title=\"{windowTitle}\" Height=\"{windowHeight}\" Width=\"{windowWidth}\">");
            finalXamlBuilder.AppendLine("<Grid>");

            // Construir el XAML final combinando los controles de nivel superior.
            foreach (var topControl in topLevelControls)
            {
                finalXamlBuilder.AppendLine(BuildXamlForControl(controlXamlMapping, containerMapping, topControl));
            }

            finalXamlBuilder.AppendLine("</Grid>");
            finalXamlBuilder.AppendLine("</Window>");

            return finalXamlBuilder.ToString();
        }

        /// <summary>
        /// Función recursiva para construir el XAML final de un control, insertando a sus hijos.
        /// </summary>
        /// <param name="controlXamlMapping"></param>
        /// <param name="containerMapping"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        static string BuildXamlForControl(Dictionary<string, (string Xaml, string ControlType)> controlXamlMapping, Dictionary<string, List<string>> containerMapping, string controlName)
        {
            if (!controlXamlMapping.ContainsKey(controlName))
                return ""; // Si no se encontró, devuelve cadena vacía.

            var (xamlBase, controlType) = controlXamlMapping[controlName];
            string childrenXaml = "";
            if (containerMapping.ContainsKey(controlName))
            {
                foreach (string child in containerMapping[controlName])
                {
                    childrenXaml += BuildXamlForControl(controlXamlMapping, containerMapping, child) + "\n";
                }
            }

            if (xamlBase.Contains("{{children}}"))
            {
                return xamlBase.Replace("{{children}}", childrenXaml);
            }
            else
            {
                return xamlBase;
            }
        }
    }
}
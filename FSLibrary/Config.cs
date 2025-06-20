﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace FSLibrary
{
    /// <summary>
    /// Funciones para acceso al fichero de configuración App.config
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Variable con la sección por defecto.
        /// </summary>
        string APP_SETTINGS = "appSettings";

        private string _section;
        /// <summary>
        /// Sección a utilizar
        /// </summary>
        public string Section {
            get { return _section; }
            set {
                if (_section != value)
        {
                    _section = value;

                    // Forzamos la lectura de la nueva sección
                    _settings = null;
                }
            }
        }

        private object _settings;
        /// <summary>
        /// Configuración
        /// </summary>
        public object Settings
        {
            get
            {
                if (_settings == null)
                {
                    Configuration configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _settings = configManager.GetSection(Section);
            
                    if (_settings == null)
                        throw new Exception("Sección no encontrada. Sección: " + Section);
                }
                return _settings;
            }
        }
            
        /// <summary>
        /// Constructor con la sección a utilizar
        /// </summary>
        /// <param name="section"></param>
        public Config(string section)
        {
            Section = section;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Config()
        {
            Section = APP_SETTINGS;
        }

        /// <summary>
        /// Actualizar propiedad elimandola antes.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdatePropertyWithRemove(string key, string value)
        {
            if (!CheckSettings(key))
                return;

            if (Settings is FSSettingsSection)
            {
                ((FSSettingsSection)Settings).Settings.Remove(key);
                ((FSSettingsSection)Settings).Settings.Add(key, value);
            }
            else
        {
                ((AppSettingsSection)Settings).Settings.Remove(key);
                ((AppSettingsSection)Settings).Settings.Add(key, value);
            }

            //SaveConfigFile();
        }

        /// <summary>
        /// Leemos las propiedades de AppSettings
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public FSSettingsCollection ReadProperties()
        {
            FSSettingsCollection result = new FSSettingsCollection();

            if (!CheckSettings())
                return null;

            object appSettings;
            if (Settings is FSSettingsSection)
                appSettings = ((FSSettingsSection)Settings).Settings;
            else
                appSettings = ((AppSettingsSection)Settings).Settings;

            // Get the AppSettings section.
            //NameValueCollection appSettings = ConfigurationManager.AppSettings;

            if (Settings is FSSettingsSection)
            {
                if (((FSSettingsCollection)appSettings).Count == 0)
            {
                    throw new Exception(string.Format("[ReadProperties: {0}]", "FSSettings is empty. Use GetSection command first."));
            }

                return (FSSettingsCollection)appSettings;
            }
            else
            {
                if (((KeyValueConfigurationCollection)appSettings).Count == 0)
                {
                    throw new Exception(string.Format("[ReadProperties: {0}]", "AppSettings is empty. Use GetSection command first."));
                }
                foreach (KeyValueConfigurationElement keyElement in (KeyValueConfigurationCollection)appSettings)
            {
                    FSSettingsElement fsKeyElement = new FSSettingsElement();
                    fsKeyElement.Key = keyElement.Key;
                    fsKeyElement.Value = keyElement.Value;

                    result.Add(fsKeyElement);
                }
            }

            return result;
        }

        /// <summary>
        /// Añadimos una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddProperty(string key, string value)
        {
            InsertProperty(key, value);
        }

        /// <summary>
        /// Añadimos una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void InsertProperty(string key, string value)
        {
            if (!CheckSettings(key))
                return;

            if (Settings is FSSettingsSection)
            {
                ((FSSettingsSection)Settings).Settings.Add(key, value);
            }
            else
            {
                ((AppSettingsSection)Settings).Settings.Add(key, value);
            }
        }

        /// <summary>
        /// Borramos una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public void DeleteProperty(string key)
        {
            if (!CheckSettings(key))
                return;

            if (Settings is FSSettingsSection)
            {
                ((FSSettingsSection)Settings).Settings.Remove(key);
            }
            else
            {
                ((AppSettingsSection)Settings).Settings.Remove(key);
            }
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public string ValueProperty(string key)
        {
            if(!CheckSettings(key))
                return null;

            if (Settings is FSSettingsSection)
            {
                return ((FSSettingsSection)Settings).Settings[key].Value;
            }
            else
            {
                return ((AppSettingsSection)Settings).Settings[key].Value;
            }
        }

        /// <summary>
        /// Comprueba si la key existe en Settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckSettings(string key = null)
        {
            if (Settings == null)
                return false;

            if (Settings is FSSettingsSection)
            {
                if (((FSSettingsSection)Settings).Settings is null)
                    return false;

                if (key != null)
                {
                    if (((FSSettingsSection)Settings).Settings[key] is null)
                        return false;
                }
            }
            else
            {
                if (((AppSettingsSection)Settings).Settings is null)
                    return false;

                if (key != null)
                {
                    if (((AppSettingsSection)Settings).Settings[key] is null)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public string[] ValuePropertyArray(string key)
        {
            string value = ValueProperty(key);

            if (!String.IsNullOrEmpty(value))
                return value.Split(new char[] { '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
            else
                return new string[] { };
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public bool ValuePropertyBool(string key)
        {
            return Convert.ToBoolean(ValueProperty(key));
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public int ValuePropertyInt(string key)
        {
            return Convert.ToInt32(ValueProperty(key));
        }

        /// <summary>
        /// Recuperamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        public DateTime ValuePropertyDate(string key)
        {
            return Convert.ToDateTime(ValueProperty(key));
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key, string[] value)
        {
            SetProperty(key, String.Join("|", value));
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key, List<string> value)
        {
            SetProperty(key, String.Join("|", value.ToArray()));
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key, string value)
        {
            if (!CheckSettings(key))
                return;

            if (Settings is FSSettingsSection)
            {
                ((FSSettingsSection)Settings).Settings[key].Value = value;
            }
            else
            {
                ((AppSettingsSection)Settings).Settings[key].Value = value;
            }
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key, bool value)
        {
            SetProperty(key, value.ToString());
        }

        /// <summary>
        /// Guardamos el valor de una propiedad.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key, DateTime value)
        {
            SetProperty(key, value.ToString());
        }

        /// <summary>
        /// Guardamos la configuracíón en el fichero config.
        /// </summary>
        public void SaveConfigFile()
        {
            Configuration configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configManager.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(Section);
        }

        /// <summary>
        /// Devuelve las secciones definidas en /condfiguration/configSections.
        /// </summary>
        /// <returns></returns>
        public List<string> GetSections()
        {
            Configuration configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<string> sections = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(configManager.FilePath);

            XmlNode configSectionsNode = doc.SelectSingleNode("/configuration/configSections");

            if (configSectionsNode != null)
            {
                XmlNodeList sectionNodes = configSectionsNode.SelectNodes("section");

                foreach (XmlNode sectionNode in sectionNodes)
                {
                    //string sectionType = sectionNode.Attributes["type"].Value;
                    string sectionName = sectionNode.Attributes["name"].Value;
                    sections.Add(sectionName);
                }
            }

            return sections;
        }
    }
}
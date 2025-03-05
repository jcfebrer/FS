﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFormControls
{
    public class DBSummarie : CollectionBase
    {
        public enum SummarieType
        {
            Average,
            Count,
            Sum
        }

        private string m_Name;
        private SummarieType m_Type;
        private DBColumn m_dBColumn;

        public DBSummarie(string name, SummarieType type)
        {
            m_Name = name;
            m_Type = type;
        }

        public DBSummarie(string name, SummarieType type, DBColumn dBColumn)
        {
            m_Name = name;
            m_Type = type;
            m_dBColumn = dBColumn;
        }

        public string Name { get; set; }
        public SummarieType Type { get; set; }
        public string DisplayFormat { get; set; }
        public object SourceColumn { get; set; }
    }
}

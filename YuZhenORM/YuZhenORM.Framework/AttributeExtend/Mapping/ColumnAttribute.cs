﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuZhenORM.Framework.AttributeExtend.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
     public class ColumnAttribute:Attribute
    {
        public ColumnAttribute(string name)
        {
           this._Name= name;
        }

        private string _Name = null;

        public string GetColumnName()
        {
            return this._Name;
        }
    }
}

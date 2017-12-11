using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication1.Models.DataTypes
{
    public class 手機電話格式Attribute : DataTypeAttribute
    {
        public 手機電話格式Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string str = (string)value;

            if (Regex.IsMatch(str, @"^\d{4}-\d{6}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
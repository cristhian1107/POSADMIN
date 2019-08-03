using System;
using System.Xml;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace SoftwareFactory.Infrastructure.BusinessEntity
{
  public class BusinessEntityLoader<TEntity>
  {
    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Start class BusinessEntityLoader
    /// </summary>
    public BusinessEntityLoader()
    {
      EntityType = typeof(TEntity);
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Entity Type
    /// </summary>
    public Type EntityType { get; set; }

    #endregion

    #region [ METODOS ]

    /// <summary>
    /// Load Entity Reader to Class
    /// </summary>
    /// <param name="Reader"></param>
    /// <param name="Scheme"></param>
    public void LoadEntity(IDataReader Reader, object Scheme)
    {
      try
      {
        int l_fields = Reader.FieldCount - 1;
        for (int l_index = 0; l_index <= l_fields; l_index++)
        {
          if (!Reader.IsDBNull(l_index))
          {
            object l_value = Reader.GetValue(l_index);
            string l_field = Reader.GetName(l_index);
            PropertyInfo l_propertInfo = null;
            l_propertInfo = EntityType.GetProperty(l_field);
            if (!((l_propertInfo == null)))
            {
              Type l_typeField = l_propertInfo.PropertyType;
              if (l_propertInfo.CanWrite)
              { this.SetValueEntity(ref l_propertInfo, l_typeField.ToString(), l_value, ref Scheme); }
            }
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Load Entity DataRow to Class
    /// </summary>
    /// <param name="Row"></param>
    /// <param name="Scheme"></param>
    public void LoadEntity(DataRow Row, object Scheme)
    {
      try
      {
        int l_fields = Row.Table.Columns.Count - 1;
        for (int l_index = 0; l_index <= l_fields; l_index++)
        {
          if ((Row[l_index] != DBNull.Value))
          {
            object l_value = Row[l_index];
            string l_field = Row.Table.Columns[l_index].ColumnName;
            PropertyInfo l_propertInfo = null;
            l_propertInfo = EntityType.GetProperty(l_field);
            if (!((l_propertInfo == null)))
            {
              Type l_typeField = l_propertInfo.PropertyType;
              if (l_propertInfo.CanWrite)
              { SetValueEntity(ref l_propertInfo, l_typeField.ToString(), l_value, ref Scheme); }
            }
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Set Value Entity
    /// </summary>
    /// <param name="PropertInfo"></param>
    /// <param name="TypeField"></param>
    /// <param name="Value"></param>
    /// <param name="Scheme"></param>
    private void SetValueEntity(ref PropertyInfo PropertInfo, string TypeField, object Value, ref object Scheme)
    {
      if (Value != null)
      {
        switch (TypeField)
        {
          case "System.String":
            PropertInfo.SetValue(Scheme, Value.ToString(), null);
            break;
          case "System.Int16":
            Int16 l_value1;
            if (Int16.TryParse(Value.ToString(), out l_value1))
            { PropertInfo.SetValue(Scheme, l_value1, null); }
            break;
          case "System.Nullable`1[System.Int16]":
            Int16 l_value2;
            if (Int16.TryParse(Value.ToString(), out l_value2))
            { PropertInfo.SetValue(Scheme, l_value2, null); }
            break;
          case "System.Int32":
            Int32 l_value3;
            if (Int32.TryParse(Value.ToString(), out l_value3))
            { PropertInfo.SetValue(Scheme, l_value3, null); }
            break;
          case "System.Nullable`1[System.Int32]":
            Int32 l_value4;
            if (Int32.TryParse(Value.ToString(), out l_value4))
            { PropertInfo.SetValue(Scheme, l_value4, null); }
            break;
          case "System.Int64":
            Int64 l_value5;
            if (Int64.TryParse(Value.ToString(), out l_value5))
            { PropertInfo.SetValue(Scheme, l_value5, null); }
            break;
          case "System.Nullable`1[System.Int64]":
            Int64 l_value6;
            if (Int64.TryParse(Value.ToString(), out l_value6))
            { PropertInfo.SetValue(Scheme, l_value6, null); }
            break;
          case "System.Double":
            Double l_value7;
            if (Double.TryParse(Value.ToString(), out l_value7))
            { PropertInfo.SetValue(Scheme, l_value7, null); }
            break;
          case "System.Nullable`1[System.Double]":
            Double l_value8;
            if (Double.TryParse(Value.ToString(), out l_value8))
            { PropertInfo.SetValue(Scheme, l_value8, null); }
            break;
          case "System.Decimal":
            Decimal l_value9;
            if (Decimal.TryParse(Value.ToString(), out l_value9))
            { PropertInfo.SetValue(Scheme, l_value9, null); }
            break;
          case "System.Nullable`1[System.Decimal]":
            Decimal l_value10;
            if (Decimal.TryParse(Value.ToString(), out l_value10))
            { PropertInfo.SetValue(Scheme, l_value10, null); }
            break;
          case "System.DateTime":
            DateTime l_value11;
            if (DateTime.TryParse(Value.ToString(), out l_value11))
            { PropertInfo.SetValue(Scheme, l_value11, null); }
            break;
          case "System.Nullable`1[System.DateTime]":
            DateTime l_value12;
            if (DateTime.TryParse(Value.ToString(), out l_value12))
            { PropertInfo.SetValue(Scheme, l_value12, null); }
            break;
          case "System.Boolean":
            Boolean l_value13;
            if (Boolean.TryParse(Value.ToString(), out l_value13))
            { PropertInfo.SetValue(Scheme, l_value13, null); }
            break;
          case "System.Nullable`1[System.Boolean]":
            Boolean l_value14;
            if (Boolean.TryParse(Value.ToString(), out l_value14))
            { PropertInfo.SetValue(Scheme, l_value14, null); }
            break;
          case "System.Guid":
            Guid l_value15 = default(Guid);
            if ((Guid.TryParse(Value.ToString(), out l_value15)))
            { PropertInfo.SetValue(Scheme, l_value15, null); }
            break;
          case "System.Nullable`1[System.Guid]":
            Guid l_value16 = default(Guid);
            if ((Guid.TryParse(Value.ToString(), out l_value16)))
            { PropertInfo.SetValue(Scheme, l_value16, null); }
            break;
          case "System.Xml.XmlDocument":
            try
            {
              XmlDocument l_value17 = new XmlDocument();
              l_value17.LoadXml(Value.ToString());
              PropertInfo.SetValue(Scheme, l_value17, null);
            }
            catch (Exception)
            { }
            break;
          case "System.TimeSpan":
            TimeSpan l_value18 = default(TimeSpan);
            if ((TimeSpan.TryParse(Value.ToString(), out l_value18)))
            { PropertInfo.SetValue(Scheme, l_value18, null); }
            break;
          case "System.Nullable`1[System.TimeSpan]":
            TimeSpan l_value19;
            if ((TimeSpan.TryParse(Value.ToString(), out l_value19)))
            { PropertInfo.SetValue(Scheme, l_value19, null); }
            break;
          case "System.Byte[]":
            if (Value != null)
            { PropertInfo.SetValue(Scheme, (byte[])Value, null); }
            break;
        }
      }
    }

    #endregion
  }
}

using System;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace SoftwareFactory.Infrastructure.DataAccess
{
  public class MyDataAccessSQL : IDisposable
  {
    #region [ PROPIEDADES ]

    private SqlCommand DASqlCommand { get; set; }
    private SqlParameter DASqlParameter { get; set; }
    private SqlConnection DASqlConnection { get; set; }
    private SqlTransaction DASqlTransaction { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    /// <summary>
    /// Buil the object and start the connection
    /// </summary>
    /// <param name="ConnectionStrings"></param>
    public MyDataAccessSQL(string ConnectionStrings)
    {
      try
      {
        DASqlConnection = new SqlConnection(ConnectionStrings);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ TRANSACTIONS ]

    /// <summary>
    /// Begin Transaction
    /// </summary>
    public void DABeginTransaction()
    {
      try
      {
        DASqlConnection.Open();
        DASqlTransaction = DASqlConnection.BeginTransaction();
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Commit Transaction
    /// </summary>
    public void DACommitTransaction()
    {
      try
      {
        if (DASqlTransaction != null)
        {
          DASqlTransaction.Commit();
          DASqlConnection.Close();
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Rollback Transaction
    /// </summary>
    public void DARollbackTransaction()
    {
      try
      {
        if (DASqlTransaction != null)
        {
          DASqlTransaction.Rollback();
          DASqlConnection.Close();
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ PROCEDURE ]

    /// <summary>
    /// Assign Procedure
    /// </summary>
    /// <param name="Procedure"></param>
    public void DAAssignProcedure(String Procedure)
    {
      try
      {

        DASqlCommand = DASqlConnection.CreateCommand();
        DASqlCommand.CommandText = Procedure;
        DASqlCommand.CommandTimeout = DASqlConnection.ConnectionTimeout;
        DASqlCommand.CommandType = CommandType.StoredProcedure;
        if (DASqlTransaction != null)
        { DASqlCommand.Transaction = DASqlTransaction; }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ EXECUTE ]    

    /// <summary>
    /// Execute Reader
    /// </summary>
    /// <returns></returns>
    public IDataReader DAExecuteReader()
    {
      try
      {
        if (DASqlConnection.State == ConnectionState.Open)
        {
          return DASqlCommand.ExecuteReader();
        }
        else
        {
          if (DASqlTransaction != null)
          {
            return DASqlCommand.ExecuteReader();
          }
          else
          {
            DASqlConnection.Open();
            return DASqlCommand.ExecuteReader();
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Execute Non Query
    /// </summary>
    /// <returns></returns>
    public Int32 DAExecuteNonQuery()
    {
      try
      {
        if (DASqlConnection.State == ConnectionState.Open)
        {
          return DASqlCommand.ExecuteNonQuery();
        }
        else
        {
          if (DASqlTransaction != null)
          {
            return DASqlCommand.ExecuteNonQuery();
          }
          else
          {
            DASqlConnection.Open();
            var l_return = DASqlCommand.ExecuteNonQuery();
            DASqlConnection.Close();
            return l_return;
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Execute Data Set
    /// </summary>
    /// <returns></returns>
    public DataSet DAExecuteDataSet()
    {
      try
      {
        DataSet l_ds = new DataSet();
        using (SqlDataAdapter l_dataAdapter = new SqlDataAdapter(DASqlCommand))
        {
          if (DASqlConnection.State == ConnectionState.Open)
          {
            l_dataAdapter.Fill(l_ds);
          }
          else
          {
            if (DASqlTransaction != null)
            {
              l_dataAdapter.Fill(l_ds);
            }
            else
            {
              DASqlConnection.Open();
              l_dataAdapter.Fill(l_ds);
            }
          }
        }
        return l_ds;
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Execute Scalar
    /// </summary>
    /// <returns></returns>
    public Object DAExecuteScalar()
    {
      try
      {
        if (DASqlConnection.State == ConnectionState.Open)
        {
          return DASqlCommand.ExecuteScalar();
        }
        else
        {
          if (DASqlTransaction != null)
          {
            return DASqlCommand.ExecuteScalar();
          }
          else
          {
            DASqlConnection.Open();
            var l_return = DASqlCommand.ExecuteScalar();
            return l_return;
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ PARAMETERS ]

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    public void DAAddParameter(String ParameterName, Object Value)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        if (Value == null) { DASqlParameter.Value = System.DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = ParameterDirection.Input;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = ParameterDirection.Input;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    /// <param name="Direction"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType, ParameterDirection Direction)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = Direction;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    /// <param name="Size"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType, int Size)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        DASqlParameter.Size = Size;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = ParameterDirection.Input;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    /// <param name="Precision"></param>
    /// <param name="Scale"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType, byte Precision, byte Scale)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        DASqlParameter.Precision = Precision;
        DASqlParameter.Scale = Scale;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = ParameterDirection.Input;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    /// <param name="Size"></param>
    /// <param name="Direction"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType, int Size, ParameterDirection Direction)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        DASqlParameter.Size = Size;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = Direction;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Add Parameter
    /// </summary>
    /// <param name="ParameterName"></param>
    /// <param name="Value"></param>
    /// <param name="SqlDbType"></param>
    /// <param name="Precision"></param>
    /// <param name="Scale"></param>
    /// <param name="Direction"></param>
    public void DAAddParameter(String ParameterName, Object Value, SqlDbType SqlDbType, byte Precision, byte Scale, ParameterDirection Direction)
    {
      try
      {
        DASqlParameter = DASqlCommand.CreateParameter();
        DASqlParameter.ParameterName = ParameterName;
        DASqlParameter.SqlDbType = SqlDbType;
        DASqlParameter.Precision = Precision;
        DASqlParameter.Scale = Scale;
        if (Value == null) { DASqlParameter.Value = DBNull.Value; } else { DASqlParameter.Value = Value; }
        DASqlParameter.Direction = Direction;
        DASqlParameter.IsNullable = true;
        DASqlCommand.Parameters.Add(DASqlParameter);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ OUTPUT ]

    /// <summary>
    /// Recovery value of Parameter OutPut
    /// </summary>
    /// <param name="NameParameter"></param>
    /// <returns></returns>
    public String ParameterOutPut(String NameParameter)
    {
      try
      {
        return DASqlCommand.Parameters[NameParameter].Value.ToString();
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ IDISPOSABLE ]

    bool disposed = false;
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
      { return; }

      if (disposing)
      {
        if (DASqlTransaction != null)
        {
          DASqlTransaction.Dispose();
          DASqlTransaction = null;
        }
        if (DASqlCommand != null)
        {
          DASqlCommand.Connection.Close();
          DASqlCommand.Connection.Dispose();
          DASqlCommand.Connection = null;
          if (DASqlCommand.Transaction != null)
          {
            DASqlCommand.Transaction.Dispose();
            DASqlCommand.Transaction = null;
          }
          DASqlCommand.Dispose();
          DASqlCommand = null;
        }
        if (DASqlConnection != null)
        {
          DASqlConnection.Close();
          DASqlConnection.Dispose();
          DASqlConnection = null;
        }
        if (DASqlParameter != null)
        { DASqlParameter = null; }
      }
      disposed = true;
    }

    #endregion

  }
}

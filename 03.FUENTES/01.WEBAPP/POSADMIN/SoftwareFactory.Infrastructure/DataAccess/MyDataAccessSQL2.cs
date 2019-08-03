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

    private SqlDatabase DASqlDatabase { get; set; }
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
        DASqlDatabase = new SqlDatabase(ConnectionStrings);
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
        DASqlConnection = (SqlConnection)DASqlDatabase.CreateConnection();
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
          DASqlTransaction = null;
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
          DASqlTransaction = null;
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
        DASqlCommand = (SqlCommand)DASqlDatabase.GetStoredProcCommand(Procedure);
        DASqlConnection = (SqlConnection)DASqlDatabase.CreateConnection();
        DASqlCommand.CommandTimeout = DASqlConnection.ConnectionTimeout;
        DASqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ EXECUTE ]

    /// <summary>
    /// Execute Non Query
    /// </summary>
    /// <returns></returns>
    public Int32 DAExecuteNonQuery()
    {
      try
      {
        if (DASqlTransaction == null)
        { return DASqlDatabase.ExecuteNonQuery(DASqlCommand); }
        else
        { return DASqlDatabase.ExecuteNonQuery(DASqlCommand, DASqlTransaction); }
      }
      catch (Exception ex)
      { throw ex; }
    }

    /// <summary>
    /// Execute Reader
    /// </summary>
    /// <returns></returns>
    public IDataReader DAExecuteReader()
    {
      try
      {
        if (DASqlTransaction == null)
        { return DASqlDatabase.ExecuteReader(DASqlCommand); }
        else
        { return DASqlDatabase.ExecuteReader(DASqlCommand, DASqlTransaction); }
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
        if (DASqlTransaction == null)
        { return DASqlDatabase.ExecuteDataSet(DASqlCommand); }
        else
        { return DASqlDatabase.ExecuteDataSet(DASqlCommand, DASqlTransaction); }
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
        if (DASqlTransaction == null)
        { return DASqlDatabase.ExecuteScalar(DASqlCommand); }
        else
        { return DASqlDatabase.ExecuteScalar(DASqlCommand, DASqlTransaction); }
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
        DASqlDatabase = null;
        DASqlParameter = null;
        if (DASqlConnection != null)
        {
          DASqlConnection.Close();
          DASqlConnection.Dispose();
        }
        if (DASqlTransaction != null)
        { DASqlTransaction.Dispose(); }
        if (DASqlCommand != null)
        { DASqlCommand.Dispose(); }
      }
      disposed = true;
    }

    #endregion

  }
}

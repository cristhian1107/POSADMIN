using System;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SoftwareFactory.Infrastructure.BusinessEntity
{
  /// <summary>
  /// Instance Entity (Unchanged, Added, Modified, Deleted)
  /// </summary>
  [DataContract()]
  public enum InstanceEntity
  {
    [EnumMember()]
    Unchanged = 0,
    [EnumMember()]
    Added = 1,
    [EnumMember()]
    Modified = 2,
    [EnumMember()]
    Deleted = 3
  }

  /// <summary>
  /// Master Business Entity
  /// </summary>
  [DataContract]
  [Serializable()]
  public class MasterBusinessEntity : INotifyPropertyChanged, ICloneable
  {
    #region [ INSTANCE ENTITY ]

    private InstanceEntity m_instance;

    [DataMember]
    public InstanceEntity Instance
    {
      get { return m_instance; }
      set { m_instance = value; }
    }

    #endregion

    #region [ METODOS ]

    public object Clone()
    {
      MasterBusinessEntity l_new = (MasterBusinessEntity)this.MemberwiseClone();
      return l_new;
    }

    #endregion

    #region [ INotifyPropertyChanged ]

    /// <summary>
    /// Verify if any modification has been made to the class
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Verify if any modification has been made to the class
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged(String propertyName)
    {
      if (m_instance != InstanceEntity.Added && m_instance != InstanceEntity.Deleted)
      { m_instance = InstanceEntity.Modified; }

      PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

  }
}

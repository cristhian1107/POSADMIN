using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Hosting;
using Posadmin.ServiceContracts;
using Posadmin.ServiceProxy;


namespace Posadmin
{
  public class ProxyManager : IDisposable
  {
    #region [ VARIABLES ]

    private IServices m_client;

    #endregion

    #region [ CONSTRUCTORES ]

    public ProxyManager()
    { InitializeClient(); }

    #endregion

    #region [ PROPIEDADES ]

    public IServices Client
    {
      get
      {
        if (m_client == null) { InitializeClient(); }
        return m_client;
      }
      set { if (m_client == null) { m_client = value; } }
    }

    #endregion

    #region [ METODOS ]

    private void InitializeClient()
    {
      try
      {
        if (m_client == null)
        {
          var l_ContainerService = new UnityContainer();
          var l_BusinessPath = HostingEnvironment.MapPath("~/Config/PosadminBusinessLogic.config");
          var l_BusinessMap = new ExeConfigurationFileMap() { ExeConfigFilename = l_BusinessPath };
          var l_BusinessSection = ConfigurationManager.OpenMappedExeConfiguration(l_BusinessMap, ConfigurationUserLevel.None).GetSection("unity") as UnityConfigurationSection;
          l_ContainerService.LoadConfiguration(l_BusinessSection);
          l_ContainerService.RegisterType<IServices, ServicesProxy>(new ContainerControlledLifetimeManager());
          Client = l_ContainerService.Resolve<IServices>();
        }
      }
      catch (Exception ex)
      {
        String _messge = ex.Message;
        throw;
      }
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
      { }
      disposed = true;
    }

    #endregion
  }
}
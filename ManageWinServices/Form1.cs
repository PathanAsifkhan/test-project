using System;
using System.Windows.Forms;
using System.Management;
using System.Security;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using System.ServiceProcess;


namespace ManageWinServicese
{
    public partial class RdpServicesStatus : Form
    {
        private string _serverName;

        private List<string> _serviceList;
        public RdpServicesStatus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Name: GetServicesList
        /// Input parameters : None
        /// Purpose : This method adds all the services names which we want to be running on the given app server. 
        ///           It will return the List containing the all the service names.
        /// Date Created : 15/03/2017
        /// </summary>
        /// <returns>List of String </returns>
        private static List<string> GetServicesList()
        {
            var chkServicesList = new List<string>
            {
                "Asos.Commerce.Orders.Endpoint",
                "Asos.Commerce.Orders.Endpoint-HIGH",
                "Asos.Commerce.Orders.Endpoint-LOW",
                "Asos.Commerce.Orders.Endpoint-MEDIUM",
                "Asos.Fulfilment.Delivery.BuyersRemorse.Endpoint",
                "Asos.Fulfilment.Delivery.Scheduling.Endpoint",
                "Asos.Finance.Payments.Card.Endpoint"
            };


            return chkServicesList;
        }

     private void SetDataGridPropertise()
        {
            servicesStatusDetailsGrid.DataSource = GetStatustable();
            servicesStatusDetailsGrid.ReadOnly = true;
            servicesStatusDetailsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            servicesStatusDetailsGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Name: GetStatustable
        /// Input parameters : None
        /// Purpose : This method will create the table with Service Name & Status as columns to display in DataGridView
        /// Date Created : 15/03/2017
        /// </summary>
        /// <returns> DataTable</returns>
        private DataTable GetStatustable()
        {
            var statusDataTable = new DataTable();

            statusDataTable.Columns.Add("Service Name");
            statusDataTable.Columns.Add("Status");

            try
            {
                if (_serviceList.Any())
                {
                    foreach (var record in _serviceList)
                    {
                        statusDataTable.Rows.Add(record, CheckServiceStatus(record));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return statusDataTable;
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(_serverName)) return;
            ShowWaitCursor(true);

            try
            {
                  ServiceController[] servicesOnServer;

                servicesOnServer = ServiceController.GetServices(_serverName).Where(x => x.ServiceName.ToLower().Contains("ASOS".ToLower())).ToArray();

                if (servicesOnServer.Any())
                {
                    _serviceList.Clear();
                    foreach (var service in servicesOnServer)
                    {
                        _serviceList.Add(service.ServiceName);
                    }
                }
                
                SetDataGridPropertise();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                ShowWaitCursor(false);
            }

        }

        /// <summary>
        /// Name: CheckServiceStatus
        /// Input parameters : Name of the service whose status needs to be checked
        /// Purpose : This method will check and returns the status of the given service on the given app server
        /// Date Created : 15/03/2017
        /// </summary>
        /// <returns> String describing current service status</returns>
        private string CheckServiceStatus(string serviceName)
        {
            try
            {
                var serviceController = new ServiceController(serviceName, _serverName);
                return serviceController.Status.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        /// <summary>
        /// Name: RunService
        /// Input parameters : Name of the service which needs to be started
        /// Purpose : This method will check if the given service is running, if NOT RUNNING then will start it on the given app server
        /// Date Created : 15/03/2017
        /// </summary>
        /// <returns> Bool representing current service status</returns>
        private bool RunService(string serviceName)
        {
            var serviceController = new ServiceController(serviceName, _serverName);
            bool serviceStatus = false;
            if (serviceController.Status == ServiceControllerStatus.Running) return serviceStatus;
            try
            {
                serviceController.Start();
                serviceStatus = true;
            }
            catch (Exception ex)
            {
                WaitForServiceToPerformOperations(1000);
                try
                {
                    serviceController.Start();
                    serviceStatus= true;
                }
                catch (Exception secex)
                {
                    Console.WriteLine(secex);
                }
            }

            return serviceStatus;
        }

        /// <summary>
        /// Name: StartService
        /// Input parameters : Name of the service which needs to be started
        /// Purpose : This method will check if the given service is in running status, if NOT RUNNING then will start it on the given app server
        /// Date Created : 15/03/2017
        /// </summary>
        private void StartService(string serviceName)
        {
            var serviceController = new ServiceController(serviceName, _serverName);
            if (serviceController.Status == ServiceControllerStatus.Running) return;
            else
            {
                serviceController.Start();
                WaitForServiceToPerformOperations(3000);

                var retryCount = 0;
                while (retryCount < 15)
                {
                    if (serviceController.Status != ServiceControllerStatus.Running)
                        WaitForServiceToPerformOperations(1000);
                    retryCount += 1;
                }

                //if (serviceController.Status != ServiceControllerStatus.Running)
                //{
                //    //Add code to check the Error Log
                //}
            }
            
        }

        private void RestartService(string serviceName)
        {
            var serviceController = new ServiceController(serviceName, _serverName);
            if (serviceController.Status == ServiceControllerStatus.Running)
            {
                serviceController.Stop();
                WaitForServiceToPerformOperations(3000);
            }
            serviceController.Start();
        }

        private void WaitForServiceToPerformOperations(int timeInMilliseconds)
        {
            Thread.Sleep(timeInMilliseconds);
        }

        private string GetLastException( string provider)
        {

            string result = "";
            const string Query =
                ("<QueryList> <Query Id='0' Path='Application'> <Select Path='Application'>*[System[(Level=2)]]</Select></Query></QueryList>");

            EventLogReader eventLogReader = new EventLogReader(new EventLogQuery("Application", PathType.LogName, Query)
            {
                ReverseDirection = true,
                Session = new EventLogSession(_serverName)
            });
            while (true)
            {
                try
                {
                    EventRecord eventRecord = eventLogReader.ReadEvent();
                    if (eventRecord != null && eventRecord.Properties.Count > 0 &&
                        eventRecord.ProviderName == provider)
                    {
                        result = eventRecord.Properties[0].Value.ToString();
                        break;
                    }
                }
                catch (Exception exception)
                {
                    result = "Exception not logged on Server";
                    break;
                }
            }
            return result;
        }

        private void txtServer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _serverName = txtServer.Text;

            if (string.IsNullOrWhiteSpace(_serverName))
            {
                txtServerNameValidator.SetError(txtServer,
                    "Oops you forgot to tell which app server you want to connect");
            }
            else
            {
                txtServerNameValidator.Clear();
            }
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                if (servicesStatusDetailsGrid.Rows.Count <= 0 || string.IsNullOrWhiteSpace(servicesStatusDetailsGrid.CurrentCell.Value.ToString()))
                    txtServerNameValidator.SetError(servicesStatusDetailsGrid, "No Services record available");
                else
                {
                    txtServerNameValidator.Clear();
                    ShowWaitCursor(true);

                    var selectedCells = servicesStatusDetailsGrid.SelectedCells;

                    if (selectedCells.Count > 0)
                        foreach (DataGridViewCell cell in selectedCells)
                    {
                        StopService(servicesStatusDetailsGrid.Rows[cell.RowIndex].Cells[0].Value.ToString());
                    }

                    SetDataGridPropertise();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                ShowWaitCursor(false);
            }
        }

        /// <summary>
        /// Name: StopService
        /// Input parameters : Name of the service which needs to be stopped
        /// Purpose : This method will check if the given service is not running, if  RUNNING then will stop it on the given app server
        /// Date Created : 16/03/2017
        /// </summary>
        /// <returns> Bool representing current service status</returns>
        private void StopService(string serviceName)
        {
            var serviceController = new ServiceController(serviceName, _serverName);
            if (serviceController.Status == ServiceControllerStatus.Stopped) return;
            else
            {
                serviceController.Stop();
                WaitForServiceToPerformOperations(3000);

                var retryCount = 0;
                while (retryCount < 15)
                {
                    if (serviceController.Status != ServiceControllerStatus.Stopped)
                        WaitForServiceToPerformOperations(1000);
                    retryCount += 1;
                }

                //if (serviceController.Status != ServiceControllerStatus.Stopped)
                //{
                //    //Add code to check the Error Log
                //}
            }
        }


        private void btnStartService_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_serverName)) return;
            ShowWaitCursor(true);

            try
            {
                var selectedCells = servicesStatusDetailsGrid.SelectedCells;

                if(selectedCells.Count>0)
                foreach (DataGridViewCell cell in selectedCells)
                {
                    StartService(servicesStatusDetailsGrid.Rows[cell.RowIndex].Cells[0].Value.ToString());
                    WaitForServiceToPerformOperations(1000);
                }

                SetDataGridPropertise();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ShowWaitCursor(false);
            }
        }

        private void ShowWaitCursor(bool isWait)
        {
            if(isWait)
                Cursor = Cursors.WaitCursor;
            else
                Cursor = Cursors.Default;
        }

        private void RdpServicesStatus_Load(object sender, EventArgs e)
        {
            _serviceList = new List<string>();
            _serviceList = GetServicesList();
        }

        private void btnStartAll_Click(object sender, EventArgs e)
        {
            try
            {
                ShowWaitCursor(true);
                foreach (var service in _serviceList)
                    {
                        StartService(service);
                    }

                SetDataGridPropertise();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ShowWaitCursor(false);
            }
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            try
            {
                ShowWaitCursor(true);
                foreach (var service in _serviceList)
                {
                    StopService(service);
                }

                SetDataGridPropertise();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ShowWaitCursor(false);
            }
        }
    }
}

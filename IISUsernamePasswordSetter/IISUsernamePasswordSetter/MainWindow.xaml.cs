using System;
using System.Linq;
using System.Windows;
using Microsoft.Web.Administration;
using System.Diagnostics;
using System.Security.Principal;

namespace IISUsernamePasswordSetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string Password { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            if (!IsAdministrator())
            {
                btnIISReset.IsEnabled = false;

            }
            using (ServerManager serverManager = new ServerManager())
            {
                var pools = serverManager.ApplicationPools.ToList().FindAll(x => x.ProcessModel.IdentityType == ProcessModelIdentityType.SpecificUser);
                var webSites = serverManager.Sites.ToList().FindAll(x => x.Name.Contains("Default") == false);
                lblInfoGeneral.Content = string.Format("IIS üzerinde kullanıcınıza ait {0} adet pool {1} adet websitesi bulunmaktadır.", pools.Count, webSites.Count);
            }
        }

        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            string domain = @"kuveytturk\";
            if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Password))
            {
                using (ServerManager serverManager = new ServerManager())
                {
                    if (chckOnlyPool.IsChecked == false)
                    {
                        var webSites = serverManager.Sites.ToList().FindAll(x => x.Name.Contains("Default") == false);
                        foreach (var item in webSites)
                        {

                            foreach (var app in item.Applications)
                            {
                                var director = app.VirtualDirectories.First();
                                if (director != null)
                                {
                                    director.UserName = domain + txtUsername.Text;
                                    director.Password = txtPassword.Password;
                                }
                            }
                        }
                    }
                    var pools = serverManager.ApplicationPools.ToList().FindAll(x => x.ProcessModel.IdentityType == ProcessModelIdentityType.SpecificUser);
                    foreach (var item in pools)
                    {
                        item.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                        item.ProcessModel.UserName = domain + txtUsername.Text;
                        item.ProcessModel.Password = txtPassword.Password;
                    }
                    serverManager.CommitChanges();

                }
                var iisprocess = Process.Start("iisreset");
                iisprocess.WaitForExit();
                MessageBox.Show("IIS üzerinde güncellemeler yapıldı.");
            }
            else
            {
                MessageBox.Show("Kullanıcı adı şifre bilgilerini doldurunuz.");
            }

        }

        private void btnIISReset_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("iisreset");

        }
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
             .IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}

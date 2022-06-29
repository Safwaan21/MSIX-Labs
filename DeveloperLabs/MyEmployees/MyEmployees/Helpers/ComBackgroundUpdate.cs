using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using System.Runtime.InteropServices;
using Windows.Management.Deployment;
using Windows.ApplicationModel;
using Windows.Foundation;
using System.Windows.Forms;

namespace MyEmployees.Helpers
{
    [ComVisible(true)]
    [Guid("095D47F4-030E-4AFF-8963-9CB33D63F682")]
    // Implements the background updater task using the IBackgroundTask interface
    public sealed class ComBackgroundUpdate : IBackgroundTask
    {
        private volatile int cleanupTask = 0;
        private readonly string newVersion = "2.0.0.0";
        private readonly string inputPackageUri = "C:\\temp\\MyEmployees.Package_2.0.0.0_Test\\MyEmployees.Package_2.0.0.0_x64.msixbundle";
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Add the cancellation handler.
            //taskInstance.Canceled += OnCanceled;

            // Check for Update if and only if there has not been a cancellation
          //  if (cleanupTask == 0)
           // {
                // Check version data to see if a new update exists
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion packageVersion = packageId.Version;
            MessageBox.Show(inputPackageUri);
                var currentVersion = new Version(string.Format("{0}.{1}.{2}.{3}", packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision));
                var updVersion = new Version(newVersion);

                // If a new version exists, update the application
                //if (newVersion.CompareTo(currentVersion) > 0)
                //{
                    // Adds the newer package using the AddPackageAsync() method
                    PackageManager packageManager = new PackageManager();
                    IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = null;
                    Uri packageUri = new Uri(inputPackageUri);

                    try
                    {
                MessageBox.Show("update");
                        deploymentOperation = packageManager.AddPackageAsync(
                                packageUri,
                                null,
                                DeploymentOptions.ForceApplicationShutdown);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
               // }
            //}
        }

        public void OnCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason cancellationReason)
        {
            // Set the flag to indicate to the main thread that it should stop performing
            // work and exit.
            cleanupTask = 1;

            return;
        }
    }
}

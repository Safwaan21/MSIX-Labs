using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

// Required for BackgroundTaskBuilder APIs.
using Windows.ApplicationModel.Background;

// Required for COM registration API (RegisterTypeForComClients).
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MyEmployees.Helpers
{

    class BackgroundUpdateRegister
    {

        /// <summary>
        /// Register a background task with the specified taskEntryPoint, name, and trigger
        /// </summary>
        /// <param name="taskEntryPoint">Task entry point for the background task.</param>
        /// <param name="name">A name for the background task.</param>
        /// <param name="trigger">The trigger for the background task.</param>
        public IBackgroundTaskRegistration RegisterBackgroundTaskWithSystem(string taskEntrypoint, string taskName, IBackgroundTrigger trigger)
        {
            foreach (var regIterator in BackgroundTaskRegistration.AllTasks)
            {
                if (regIterator.Value.Name == taskName)
                {
                    return regIterator.Value;
                }
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

            builder.SetTrigger(trigger);
            builder.Name = taskName;

            // **** Want to use builder.SetTaskEntryPointClsid(entryPointClsid);

            BackgroundTaskRegistration registration;

            try
            {
                registration = builder.Register();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                registration = null;
            }

            return registration;
        }

    }
}

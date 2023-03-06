using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;

namespace Mauimgur.WinUI {
    /// <summary>
    /// Main Program.
    /// </summary>
    internal class Program {
        private const string AppKey = "3acf67b8-9c8d-473f-8b69-0a614328f449";

        [STAThread]
        private static int Main(string[] args) {
            WinRT.ComWrappersSupport.InitializeComWrappers();
            bool isRedirect = DecideRedirection();
            if (!isRedirect) {
                Microsoft.UI.Xaml.Application.Start((p) => {
                    var context = new DispatcherQueueSynchronizationContext(
                        DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(context);
                    new App();
                });
            }

            return 0;
        }

        private static bool DecideRedirection() {
            bool isRedirect = false;
            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            ExtendedActivationKind kind = args.Kind;
            AppInstance keyInstance = AppInstance.FindOrRegisterForKey(AppKey);

            if (keyInstance.IsCurrent) {
                keyInstance.Activated += OnActivated;
            }
            else {
                isRedirect = true;
                keyInstance.RedirectActivationToAsync(args).GetAwaiter().GetResult();
            }

            return isRedirect;
        }

        private static void OnActivated(object? sender, AppActivationArguments args) {
            ExtendedActivationKind kind = args.Kind;
        }
    }
}

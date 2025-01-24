using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace GluwaPro.UITest
{
    public class AppInitializer
    {
        private static bool bTestTablet = false; // Set to true to test against iPad Air instead of iPhone X
        private static bool bAppCenterBuild = false; // Set to true to test against App Center build rather than local build
        private static (string Android, string iOS) GetAppPackageNames()
        {
            var t = (Android: "com.gluwa.android", iOS: "com.gluwa.app");
            if (bAppCenterBuild)
            {
                t.Android = t.Android + ".dev";
                t.iOS = t.iOS + ".dev";
            }
            return t;
        }

        // Calls xcrun to get device ID based on boolean above
        public static string GetDeviceID()
        {
            string device = "iPhone 13";
            if (bTestTablet)
                device = "iPad Air";

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "xcrun",
                    Arguments = "simctl list devices '" + device + "' available",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();

            // Set device ID to first guid found in output of call
            // TODO - What happens if the command fails? Should add error handling
            string deviceId = Regex.Match(output, "[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}").Value;
            return deviceId;
        }

        /// <summary>
        /// Start app depends on platform
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="restartOnly"></param>
        /// <returns></returns>
        public static IApp StartApp(Platform platform, bool restartOnly = false)
        {
            (string Android, string iOS) packages = GetAppPackageNames();

            // Mac and Windows get the base directory differently :(
            string keystore = Path.Combine(System.AppContext.BaseDirectory, "debug.keystore");
            if (!File.Exists(keystore))
            {
                keystore = Path.Combine(Directory.GetCurrentDirectory(), "debug.keystore");
            }

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            string appConfig = Path.Combine(System.AppContext.BaseDirectory, "app.config");
            if (!File.Exists(appConfig))
            {
                appConfig = Path.Combine(Directory.GetCurrentDirectory(), "app.config");
            }
            configMap.ExeConfigFilename = appConfig;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            if (platform == Xamarin.UITest.Platform.Android && !TestEnvironment.IsTestCloud)
            {
                string androidAppMac = config.AppSettings.Settings["androidAppMac"].Value;
                string androidAppWindows = config.AppSettings.Settings["androidAppWindows"].Value;

                try // Try looking for installed app first
                {
                    if (restartOnly)
                    {
                        return ConfigureApp.Android.InstalledApp(packages.Android).KeyStore(keystore).EnableLocalScreenshots().StartApp(AppDataMode.DoNotClear);
                    }
                    else
                    {
                        return ConfigureApp.Android.InstalledApp(packages.Android).KeyStore(keystore).EnableLocalScreenshots().StartApp();
                    }
                }
                catch // If that fails try to install from APK file
                {
                    if (File.Exists(androidAppMac))
                    {
                        if (restartOnly)
                        {
                            return ConfigureApp.Android.ApkFile(androidAppMac).KeyStore(keystore).EnableLocalScreenshots().StartApp(AppDataMode.DoNotClear);
                        }
                        else
                        {
                            return ConfigureApp.Android.ApkFile(androidAppMac).KeyStore(keystore).EnableLocalScreenshots().StartApp();
                        }
                    }
                    else
                    {
                        if (restartOnly)
                        {
                            return ConfigureApp.Android.ApkFile(androidAppWindows).EnableLocalScreenshots().StartApp(AppDataMode.DoNotClear);
                        }
                        else
                        {
                            return ConfigureApp.Android.ApkFile(androidAppWindows).EnableLocalScreenshots().StartApp();
                        }
                    }
                }
            }
            else if (platform == Platform.iOS && !TestEnvironment.IsTestCloud)
            {
                string deviceId = GetDeviceID();
                string iosAppPath = config.AppSettings.Settings["iosApp"].Value;

                try // Try looking for installed app first
                {                    
                    if (restartOnly)
                    {
                        return ConfigureApp.iOS.InstalledApp(packages.iOS).DeviceIdentifier(deviceId).EnableLocalScreenshots().StartApp(AppDataMode.DoNotClear);

                    }
                    else
                    {
                        Environment.SetEnvironmentVariable("UITEST_FORCE_IOS_SIM_RESTART", "1");
                        return ConfigureApp.iOS.InstalledApp(packages.iOS).DeviceIdentifier(deviceId).EnableLocalScreenshots().StartApp();
                    }
                        
                }
                catch // If that fails try to install from APP bundle
                {                    
                    if (restartOnly)
                    {
                        return ConfigureApp.iOS.AppBundle(iosAppPath).DeviceIdentifier(deviceId).EnableLocalScreenshots().StartApp(AppDataMode.DoNotClear);
                    }

                    else
                    {
                        Environment.SetEnvironmentVariable("UITEST_FORCE_IOS_SIM_RESTART", "1");
                        return ConfigureApp.iOS.AppBundle(iosAppPath).DeviceIdentifier(deviceId).EnableLocalScreenshots().StartApp();
                    }
                }
            }
            else // Assuming is App Center
            {
                if (platform == Xamarin.UITest.Platform.Android) // Android
                {
                    if (restartOnly)
                    {
                        return ConfigureApp.Android.StartApp(AppDataMode.DoNotClear);
                    }
                    else
                    {
                        return ConfigureApp.Android.StartApp();
                    }

                }
                else // iOS
                {
                    if (restartOnly)
                    {
                        return ConfigureApp.iOS.StartApp(AppDataMode.DoNotClear);
                    }
                    else
                    {
                        return ConfigureApp.iOS.StartApp();
                    }                  
                }
            }
        }
    }
}
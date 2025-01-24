using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.TestLogging
{
    public class ScreenshotTools
    {
        public static bool IsTakePosScreenshot { get; set; }
        public static bool IsTakeErrorScreenshot { get; set; }
        public static void Screenshot(IApp app, string screenshotName = "", [CallerMemberName] string callerName = "")
        {
            string ssDirectory = "Screenshots";
            string screenshot;

            // Check whether name has been changed
            if (screenshotName == "")
            {
                screenshot = string.Join("_", callerName.Split(Path.GetInvalidFileNameChars()));
            }
            else
            {
                screenshot = string.Join("_", screenshotName.Split(Path.GetInvalidFileNameChars()));
            }

            FileInfo ss = app.Screenshot(screenshot);
            if (Environment.GetEnvironmentVariable("APP_CENTER_TEST") != "1")
            {
                try
                {
                    string path = Path.Combine(TestContext.CurrentContext.TestDirectory, ssDirectory);

                    // Create directory
                    Directory.CreateDirectory(path);
                    ss.CopyTo(Path.Combine(path, $"{screenshot}.png"), true);
                }
                catch (System.IO.DirectoryNotFoundException copyTo)
                {
                    // For incompatible names
                    throw new System.IO.DirectoryNotFoundException($"{screenshot} cannot be copied", copyTo);
                }
                catch
                {
                    TestContext.WriteLine("Can't copy screenshots using TestContext.CurrenyContext.TestDirectory. " +
                        "/n Trying with app.Screenshot directory");
                    try
                    {
                        string path = Path.Combine(ss.Directory.ToString(), ssDirectory);

                        // Create directory
                        Directory.CreateDirectory(path);
                        ss.CopyTo(Path.Combine(path, $"{screenshot}.png"), true);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed to copy screenshot. Might want to check screenshot's name", e);
                    }
                }
                // Clean up screenshot.png in the base directory (for local machine)
                ss.Delete();
            }
        }

        /// <summary>
        /// Use this method for screenshoting test steps
        /// </summary>
        /// <param name="app"></param>
        /// <param name="screenshotName"></param>
        public static void TakePosScreenshot(IApp app, string screenshotName = "No Description", params object[] args)
        {
            if (IsTakePosScreenshot)
            {
                Screenshot(app, string.Format(screenshotName, args));
            }
        }

        /// <summary>
        /// Use this method to specify the error in the screenshot name
        /// </summary>
        /// <param name="app"></param>
        /// <param name="errorMessage"></param>
        public static void TakeErrorScreenshot(IApp app, string errorMessage = "No Description", params object[] args)
        {
            StackTrace stackTrace = new StackTrace();

            string screenshotName = string.Format(errorMessage, args)
                + " ERROR from "
                + stackTrace.GetFrame(1).GetMethod().Name;

            if (IsTakeErrorScreenshot)
            {
                Screenshot(app, screenshotName);
            }
        }

        /// <summary>
        /// Use this method to assert and screenshot app if assert fail
        /// Wrapper method for Assert.AreEqual
        /// </summary>
        /// <param name="app"></param>
        /// <param name="expectedValue"></param>
        /// <param name="actualValue"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void AreEqualScreenshot(IApp app, object expectedValue, object actualValue, string message = "", params object[] args)
        {
            string screenshotName;
            if (message == "")
            {
                screenshotName = "No Description";
            }
            else
            {
                screenshotName = string.Format(message, args);
            }

            try
            {
                Assert.AreEqual(expectedValue, actualValue, screenshotName);
            }
            catch
            {
                screenshotName += string.Format(" Expected {0}, Actual {1}", expectedValue, actualValue);
                Screenshot(app, screenshotName);
                //Fail to stop the test
                Assert.Fail(message);
            }
        }

        /// <summary>
        /// Use this method to assert and screenshot app if assert fail
        /// Wrapper method for Assert.IsTrue
        /// </summary>
        /// <param name="app"></param>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void IsTrueScreenshot(IApp app, bool condition, string message = "", params object[] args)
        {
            string screenshotName;
            if (message == "")
            {
                screenshotName = "No Description";
            }
            else
            {
                screenshotName = string.Format(message, args);
            }

            try
            {
                Assert.IsTrue(condition, screenshotName);
            }
            catch
            {
                screenshotName += " Expected \"True\" value";
                Screenshot(app, screenshotName);
                //Fail to stop the test
                Assert.Fail(message);
            }
        }
    }
}

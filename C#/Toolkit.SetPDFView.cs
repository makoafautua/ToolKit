
using System;
using System.Text;

namespace ToolkitExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory;

            // Instantiate Object
            using (APToolkitNET.Toolkit toolkit = new APToolkitNET.Toolkit())
            {
                // Here you can place any code that will alter the output file
                // such as adding security, setting page dimensions, etc.

                // Create the new PDF file
                int result = toolkit.OpenOutputFile(FileName: $"{strPath}Toolkit.SetPDFView.pdf");
                if (result != 0)
                {
                    WriteResult($"Error opening output file: {result.ToString()}", toolkit);
                    return;
                }

                // Open the template PDF
                result = toolkit.OpenInputFile(InputFileName: $"{strPath}Toolkit.Input.pdf");
                if (result != 0)
                {
                    WriteResult($"Error opening input file: {result.ToString()}", toolkit);
                    return;
                }

                // Get the reference to the InitialViewInfo object
                APToolkitNET.InitialViewInfo viewInfo = toolkit.GetInitialViewInfo();

                // Options for viewer window
                viewInfo.CenterWindow = true;
                viewInfo.FullScreen = false;
                viewInfo.ResizeWindow = true;
                viewInfo.Show = APToolkitNET.IVShow.IVShow_DocumentTitle;

                // Show or hide UI elements of the viewer
                viewInfo.HideMenuBar = true;
                viewInfo.HideToolBars = true;
                viewInfo.HideWindowControls = true;
                viewInfo.NavigationTab = APToolkitNET.IVNavigationTab.IVNavigationTab_PageOnly;

                // Page settings
                viewInfo.Magnification = APToolkitNET.IVMagnification.IVMagnification_150;
                viewInfo.OpenToPage = 2;
                viewInfo.PageLayout = APToolkitNET.IVPageLayout.IVPageLayout_SinglePageContinuous;

                // Copy the template (with any changes) to the new file
                // Start page and end page, 0 = all pages
                result = toolkit.CopyForm(FirstPage: 0, LastPage: 0);
                if (result != 1)
                {
                    WriteResult($"Error copying file: {result.ToString()}", toolkit);
                    return;
                }

                // Close the new file to complete PDF creation
                toolkit.CloseOutputFile();
            }

            // Process Complete
            WriteResult("Success!");
        }

        public static void WriteResult(string result, APToolkitNET.Toolkit toolkit = null)
        {
            StringBuilder resultText = new StringBuilder();
            resultText.AppendLine(result);
            if (toolkit != null)
            {
                resultText.AppendLine($"ErrorCode: {toolkit.ExtendedErrorCode.ToString()}");
                resultText.AppendLine($"Location: {toolkit.ExtendedErrorLocation}");
                resultText.AppendLine($"Description: {toolkit.ExtendedErrorDescription}");
            }
            Console.WriteLine(resultText.ToString());
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

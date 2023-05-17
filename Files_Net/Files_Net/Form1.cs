using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Files_Net
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    webBrowser1.Url = new Uri(fbd.SelectedPath);
                    txtInput.Text = fbd.SelectedPath;
                }

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
            {
                webBrowser1.GoForward();
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            string dirName = txtInput.Text;

            // Клас Directory
            string[] files = Directory.GetFiles(dirName, "*.txt");

            // Клас DirectoryInfo
            var directory = new DirectoryInfo(dirName);
            FileInfo[] fileInfoArray = directory.GetFiles("*.txt");

            // Вивести значення масивів files та fileInfoArray у вікні повідомлення
            string filesMessage = "Files:\n" + string.Join("\n", files);
            string fileInfoMessage = "FileInfo Array:\n" + string.Join("\n", fileInfoArray.Select(fileInfo => fileInfo.FullName));

            MessageBox.Show(filesMessage + "\n\n" + fileInfoMessage, "Filtered Files");
        }

        private void filterByDateBtn_Click(object sender, EventArgs e)
        {
            string dirName = txtInput.Text;
            DateTime selectedDate = dateTimePicker.Value.Date;

            // Клас DirectoryInfo
            var directory = new DirectoryInfo(dirName);
            DirectoryInfo[] filteredDirectories = directory.GetDirectories()
                .Where(dir => dir.LastWriteTime.Date == selectedDate)
                .ToArray();

            // Вивести відфільтровані каталоги у вікні повідомлення
            string directoriesMessage = "Filtered Directories:\n" + string.Join("\n", filteredDirectories.Select(dir => dir.FullName));
            MessageBox.Show(directoriesMessage, "Filtered Directories");
        }

        private void btnViewDiskProperties_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            DriveInfo drive = new DriveInfo(Path.GetPathRoot(selectedPath));

            string diskProperties = $"Drive Name: {drive.Name}\n" +
                                    $"Drive Type: {drive.DriveType}\n" +
                                    $"Drive Format: {drive.DriveFormat}\n" +
                                    $"Total Size: {FormatBytes(drive.TotalSize)}\n" +
                                    $"Available Space: {FormatBytes(drive.AvailableFreeSpace)}\n" +
                                    $"Total Free Space: {FormatBytes(drive.TotalFreeSpace)}";

            MessageBox.Show(diskProperties, "Disk Properties");
        }

        private string FormatBytes(long bytes)
        {
            const int scale = 1024;
            string[] units = { "B", "KB", "MB", "GB", "TB" };

            int magnitude = (int)Math.Log(bytes, scale);
            double adjustedSize = (double)bytes / Math.Pow(scale, magnitude);

            return $"{adjustedSize:0.##} {units[magnitude]}";
        }

        private void btnViewDirectoryProperties_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            DirectoryInfo directory = new DirectoryInfo(selectedPath);

            string directoryProperties = $"Directory Name: {directory.Name}\n" +
                                         $"Parent Directory: {directory.Parent}\n" +
                                         $"Creation Time: {directory.CreationTime}\n" +
                                         $"Last Access Time: {directory.LastAccessTime}\n" +
                                         $"Last Write Time: {directory.LastWriteTime}\n" +
                                         $"Attributes: {directory.Attributes}";

            MessageBox.Show(directoryProperties, "Directory Properties");
        }

        private void btnViewFileProperties_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            FileInfo file = new FileInfo(selectedPath);

            string fileProperties = $"File Name: {file.Name}\n" +
                                    $"Directory: {file.Directory}\n" +
                                    $"Size: {FormatBytes(file.Length)}\n" +
                                    $"Creation Time: {file.CreationTime}\n" +
                                    $"Last Access Time: {file.LastAccessTime}\n" +
                                    $"Last Write Time: {file.LastWriteTime}\n" +
                                    $"Attributes: {file.Attributes}";

            MessageBox.Show(fileProperties, "File Properties");
        }

        private void btnViewImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = openFileDialog.FileName;
                    if (IsImageFile(selectedPath))
                    {
                        pictureBox.Image = Image.FromFile(selectedPath);
                    }
                    else
                    {
                        MessageBox.Show("Selected file is not an image.", "Invalid File");
                    }
                }
            }
        }

        private bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            return supportedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        private void btnViewSecurity_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи вибраний елемент є файлом
            if (File.Exists(selectedPath))
            {
                ShowFileSecurity(selectedPath);
            }
            // Перевірка, чи вибраний елемент є каталогом
            else if (Directory.Exists(selectedPath))
            {
                ShowDirectorySecurity(selectedPath);
            }
            else
            {
                MessageBox.Show("Selected path is not a valid file or directory.", "Invalid Path");
            }
        }

        private void ShowFileSecurity(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            FileSecurity fileSecurity = fileInfo.GetAccessControl();

            AuthorizationRuleCollection fileRules = fileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            StringBuilder sb = new StringBuilder();

            foreach (FileSystemAccessRule rule in fileRules)
            {
                sb.AppendLine("IdentityReference: " + rule.IdentityReference);
                sb.AppendLine("Access Control Type: " + rule.AccessControlType);
                sb.AppendLine("File System Rights: " + rule.FileSystemRights);
                sb.AppendLine();
            }

            MessageBox.Show(sb.ToString(), "File Security");
        }

        private void ShowDirectorySecurity(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

            AuthorizationRuleCollection directoryRules = directorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            StringBuilder sb = new StringBuilder();

            foreach (FileSystemAccessRule rule in directoryRules)
            {
                sb.AppendLine("IdentityReference: " + rule.IdentityReference);
                sb.AppendLine("Access Control Type: " + rule.AccessControlType);
                sb.AppendLine("File System Rights: " + rule.FileSystemRights);
                sb.AppendLine();
            }

            MessageBox.Show(sb.ToString(), "Directory Security");
        }

        private void btnViewTextFile_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи вибраний елемент є файлом
            if (File.Exists(selectedPath))
            {
                if (IsTextFile(selectedPath))
                {
                    string fileContent = File.ReadAllText(selectedPath);
                    ShowTextFileContent(fileContent);
                }
                else
                {
                    MessageBox.Show("Selected file is not a text file.", "Invalid File");
                }
            }
            else
            {
                MessageBox.Show("Selected path is not a valid file.", "Invalid Path");
            }
        }

        private bool IsTextFile(string filePath)
        {
            string extension = Path.GetExtension(filePath)?.TrimStart('.');
            string[] textExtensions = { "txt", "log", "csv" }; // Додайте інші розширення за потреби

            return textExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    

        private void ShowTextFileContent(string content)
        {
            // Відображення вмісту текстового файлу у новому вікні
            TextFileContentForm contentForm = new TextFileContentForm();
            contentForm.FileContent = content;
            contentForm.ShowDialog();
        }
    }
}
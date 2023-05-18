using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи шлях є дійсним каталогом
            if (Directory.Exists(selectedPath))
            {
                string newDirectoryName = "NewDirectory";
                string newDirectoryPath = Path.Combine(selectedPath, newDirectoryName);

                try
                {
                    // Створення нового каталогу
                    Directory.CreateDirectory(newDirectoryPath);
                    MessageBox.Show($"Directory '{newDirectoryName}' created successfully.", "Create Directory");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create directory: {ex.Message}", "Create Directory Error");
                }
            }
            else
            {
                MessageBox.Show("Selected path is not a valid directory.", "Invalid Path");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sourceFolderName = @"C:\Users\HP\Desktop\home\Verstka";
            string destFolderName = @"C:\Users\HP\Desktop\home\NewDirectory";

            var allFiles = Directory.GetFiles(sourceFolderName, sourceFolderName);
            foreach (var file in allFiles)
            {
                var destFileName = destFolderName + Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file);
                File.Move(file, destFileName);
            }

            // Вивести MessageBox з повідомленням після переміщення файлів
            MessageBox.Show("Файли були успішно переміщені.");

            // Додаткові дії після переміщення файлів
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory if it doesn't exist
            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            string newDirectoryPath = @"C:\Users\HP\Desktop\home\NewDirectory";

            // Копіювання каталогу
            CopyDirectory(selectedPath, newDirectoryPath, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи шлях є дійсним каталогом
            if (Directory.Exists(selectedPath))
            {
                try
                {
                    // Видалення каталогу
                    Directory.Delete(selectedPath, true);
                    MessageBox.Show("Directory deleted successfully.", "Delete Directory");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete directory: {ex.Message}", "Delete Directory Error");
                }
            }
            else
            {
                MessageBox.Show("Selected path is not a valid directory.", "Invalid Path");
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            string newFilePath = Path.Combine(selectedPath, "NewFile.txt");

            try
            {
                // Створення порожнього файлу
                File.Create(newFilePath).Close();
                MessageBox.Show($"File '{newFilePath}' created successfully.", "Create File");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create file: {ex.Message}", "Create File Error");
            }
        }

        private void btnMoveFile_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            string sourceFilePath = Path.Combine(selectedPath, "NewFile.txt");
            string destFilePath = Path.Combine(selectedPath, "Приклад.txt");

            try
            {
                // Перенесення файлу
                File.Move(sourceFilePath, destFilePath);
                MessageBox.Show($"File moved to '{destFilePath}' successfully.", "Move File");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to move file: {ex.Message}", "Move File Error");
            }
        }

        private void btnCopyFile_Click_1(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            string sourceFilePath = Path.Combine(selectedPath, "SourceFile.txt");
            string destFilePath = Path.Combine(selectedPath, "CopyFile.txt");

            try
            {
                // Копіювання файлу
                File.Copy(sourceFilePath, destFilePath);
                MessageBox.Show($"File copied to '{destFilePath}' successfully.", "Copy File");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to copy file: {ex.Message}", "Copy File Error");
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;
            string filePath = Path.Combine(selectedPath, "FileToDelete.txt");

            try
            {
                // Видалення файлу
                File.Delete(filePath);
                MessageBox.Show($"File '{filePath}' deleted successfully.", "Delete File");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete file: {ex.Message}", "Delete File Error");
            }
        }

        private void btnEditAttributes_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи шлях є дійсним файлом або каталогом
            if (File.Exists(selectedPath) || Directory.Exists(selectedPath))
            {
                // Отримання поточних атрибутів
                FileAttributes attributes;
                if (File.Exists(selectedPath))
                {
                    attributes = File.GetAttributes(selectedPath);
                }
                else
                {
                    attributes = File.GetAttributes(selectedPath);
                }

                // Показати атрибути у MessageBox
                string message = $"Attributes of {selectedPath}:\n";
                message += $"Read-Only: {(attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly}\n";
                message += $"Hidden: {(attributes & FileAttributes.Hidden) == FileAttributes.Hidden}\n";
                message += $"Archive: {(attributes & FileAttributes.Archive) == FileAttributes.Archive}\n";
                // Додайте інші атрибути за потреби

                MessageBox.Show(message, "File Attributes");
            }
            else
            {
                MessageBox.Show("Selected path is not a valid file or directory.", "Invalid Path");
            }
        }

        private void EditDirAttributes_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи шлях є дійсним каталогом
            if (Directory.Exists(selectedPath))
            {
                // Отримання поточних атрибутів
                var directoryInfo = new DirectoryInfo(selectedPath);
                var currentAttributes = directoryInfo.Attributes;

                // Підготовка повідомлення про атрибути
                string message = $"Attributes of {selectedPath}:\n";
                message += $"Read-Only: {(currentAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly}\n";
                message += $"Hidden: {(currentAttributes & FileAttributes.Hidden) == FileAttributes.Hidden}\n";
                message += $"Archive: {(currentAttributes & FileAttributes.Archive) == FileAttributes.Archive}\n";
                // Додайте інші атрибути за потреби

                MessageBox.Show(message, "Directory Attributes");
            }
            else
            {
                MessageBox.Show("Selected path is not a valid directory.", "Invalid Path");
            }
        }

        private void EditTextFile_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи вибраний елемент є файлом
            if (File.Exists(selectedPath))
            {
                if (TextFile(selectedPath))
                {
                    // Відкриття текстового файлу у редакторі
                    Process.Start("notepad.exe", selectedPath);
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

        private bool TextFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string[] textExtensions = { ".txt", ".log", ".csv" }; // Додайте інші розширення за потреби

            return textExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи вибраний елемент є файлом або каталогом
            if (File.Exists(selectedPath))
            {
                // Архівація окремого файлу
                string zipPath = Path.ChangeExtension(selectedPath, "zip");
                ZipFile.CreateFromDirectory(Path.GetDirectoryName(selectedPath), zipPath);

                MessageBox.Show($"File compressed successfully. Archive path: {zipPath}", "Compression");
            }
            else if (Directory.Exists(selectedPath))
            {
                // Архівація каталогу
                string zipPath = selectedPath + ".zip";
                ZipFile.CreateFromDirectory(selectedPath, zipPath);

                MessageBox.Show($"Directory compressed successfully. Archive path: {zipPath}", "Compression");
            }
            else
            {
                MessageBox.Show("Selected path is not a valid file or directory.", "Invalid Path");
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            string selectedPath = txtInput.Text;

            // Перевірка, чи вибраний елемент є файлом ZIP архіву
            if (File.Exists(selectedPath) && Path.GetExtension(selectedPath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                string extractPath = Path.Combine(Path.GetDirectoryName(selectedPath), Path.GetFileNameWithoutExtension(selectedPath));

                // Розпакування файлів
                ZipFile.ExtractToDirectory(selectedPath, extractPath);

                MessageBox.Show($"Files extracted successfully. Destination path: {extractPath}", "Extraction");
            }
            else
            {
                MessageBox.Show("Selected path is not a valid ZIP archive.", "Invalid Path");
            }
        }
    }
}

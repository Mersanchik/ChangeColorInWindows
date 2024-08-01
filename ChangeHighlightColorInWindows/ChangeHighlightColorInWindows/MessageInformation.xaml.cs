using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace ChangeHighlightColorInWindows
{
    /// <summary>
    /// Логика взаимодействия для MessageInformation.xaml
    /// </summary>
    public partial class MessageInformation : Window
    {
        public MessageInformation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="filePath"></param>
        private void OpenFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Файл не найден.");
            }
        }


        private void btOpenFile_Click(object sender, RoutedEventArgs e)//открытие файла
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Information.txt");
            OpenFile(filePath);

        }

        private void btOpenWindow_Click(object sender, RoutedEventArgs e)//открыть приложение
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btWrapProgs_Click(object sender, RoutedEventArgs e)//свернуть окно
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btCloseProgs_Click(object sender, RoutedEventArgs e)//закрыть окно 
        {
            this.Close();
        }
    }
}

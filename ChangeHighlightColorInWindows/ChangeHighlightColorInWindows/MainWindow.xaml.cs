using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Reflection;

namespace ChangeHighlightColorInWindows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        byte r, g, b;
        string numberColorForHilight = "";
        string numberColorForHotTrackingColor = "";
        string numberStandartWindowsHilight = "0 120 215";
        string numberStandartWindowsHotTrackingColor = "0 102 204";

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)//для свободного перетаскивания формы
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Изменение реестра
        /// </summary>
        /// <param name="numberHilight"></param>
        /// <param name="numberHotTrackingColor"></param>
        public void EditRegedit(string numberHilight, string numberHotTrackingColor)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors", true);
            if (key == null)
            {
                return;
            }
            if (numberColorForHilight != null)
            {
                key.DeleteValue("Hilight");
                key.SetValue("Hilight", numberHilight);
            }
            if (numberColorForHotTrackingColor != null)
            {
                key.DeleteValue("HotTrackingColor");
                key.SetValue("HotTrackingColor", numberHotTrackingColor);
            }
            key.Close();
        }

        /// <summary>
        /// Получение цвета от ColorDialog
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <param name="finalNumberColor"></param>
        public string SetColorOnColorDialog(Border border, byte r, byte g, byte b, string finalNumberColor)
        {
            ColorDialog colorDialog = new ColorDialog();
            var dres = colorDialog.ShowDialog();

            if (dres != System.Windows.Forms.DialogResult.Cancel)
            {
                border.Background = new SolidColorBrush(
                Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)
                );
                r = colorDialog.Color.R;
                g = colorDialog.Color.G;
                b = colorDialog.Color.B;
                finalNumberColor = r + " " + g + " " + b;
            }
            return finalNumberColor;
        }

        private void btColorForHilight_Click(object sender, RoutedEventArgs e)//применение цвета к border, цвет для выделения текста
        {
            numberColorForHilight = SetColorOnColorDialog(borderColorSystem, r, g, b, numberColorForHilight);
        }

        private void btColorForHotTrackingColor_Click(object sender, RoutedEventArgs e)//применение цвета к border, цвет при наведении курсора
        {
            SetColorOnColorDialog(borderColorSystem2, r, g, b, numberColorForHotTrackingColor);
        }

        private void btWrapProgs_Click(object sender, RoutedEventArgs e)//свернуть приложение 
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btCloseProgs_Click(object sender, RoutedEventArgs e)//закрыть приложение
        {
            this.Close();
        }

        private void btReturnColor_Click(object sender, RoutedEventArgs e)//вернуть всё обратно
        {
           EditRegedit(numberStandartWindowsHilight, numberStandartWindowsHotTrackingColor);
        }  

        private void btOpenInfoWindow_Click(object sender, RoutedEventArgs e)//открыть ркно информации
        {
            MessageInformation messageInformation = new MessageInformation();
            messageInformation.Show();
            this.Close();
        }

        private void btApplyColor_Click(object sender, RoutedEventArgs e)//изменение значения реестра
        {
            if(numberColorForHilight != null || numberColorForHotTrackingColor != null)
            {
                EditRegedit(numberColorForHilight, numberColorForHotTrackingColor);
            }
            else
            {
                System.Windows.MessageBox.Show("Цвета не выбраны для изменения!");
            }
        }
    }
}

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FontSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string fontName = selectedItem.Content.ToString();
                TextBox1.FontFamily = new FontFamily(fontName);
                TextBox2.FontFamily = new FontFamily(fontName);
            }
        }

        private void FontSizeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                if (double.TryParse(selectedItem.Content.ToString(), out double fontSize))
                {
                    TextBox1.FontSize = fontSize;
                    TextBox2.FontSize = fontSize;
                }
            }
        }

        private void TextColorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextColorSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string colorName = selectedItem.Content.ToString();
                Color color = (Color)ColorConverter.ConvertFromString(colorName);
                TextBox1.Foreground = new SolidColorBrush(color);
                TextBox2.Foreground = new SolidColorBrush(color);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCloseButton();
        }

        private void UpdateCloseButton()
        {
            CloseButton.IsEnabled = string.IsNullOrWhiteSpace(TextBox1.Text) && string.IsNullOrWhiteSpace(TextBox2.Text);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            UpdateCloseButton();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                TextBox1.Text = fileContent;
            }
        }
    }
}
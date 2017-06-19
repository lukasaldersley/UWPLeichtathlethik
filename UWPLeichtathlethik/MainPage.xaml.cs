using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace UWPLeichtathlethik
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            JahrgangsstufenComboBox.SelectionChanged += JgstGeschlechtPruefen;
        }

        private void JgstGeschlechtPruefen(object sender, SelectionChangedEventArgs e)
        {
            if (JahrgangsstufenComboBox.SelectedIndex > 0 && JahrgangsstufenComboBox.SelectedIndex < 7)
            {
                GeschlechtComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                GeschlechtComboBox.Visibility = Visibility.Collapsed;
            }
        }

        private async void WeiterButton_Click(object sender, RoutedEventArgs e)
        {
            Speicherinterface.SaveToDocuments("README.txt","GENERAL",await Speicherinterface.Download("https://raw.githubusercontent.com/lukasaldersley/UWPLeichtathlethik/master/README.md"));
            Debug.WriteLine("DONE WRITING PIEHDJGFASIOHIFGISDFISUFDGIUHZSDFIGAISFUAGSFGZ");
        }
    }
}

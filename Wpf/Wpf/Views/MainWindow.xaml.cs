using Microsoft.Win32;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Windows;
using Wpf.Models;
using Wpf.ViewModels;

namespace Wpf;

public partial class MainWindow : IViewFor<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = (MainViewModel)DataContext;

        this.WhenActivated(disposables =>
        {
            disposables(ViewModel.OpenFileDialog.RegisterHandler(interaction =>
            {
                var window = new OpenFileDialog();
                window.Filter = @"Json files (*.json)|*.json";

                if (window.ShowDialog() == false) return;
                
                var points = JsonConvert.DeserializeObject<Point2D[]>(File.ReadAllText(window.FileName));

                if (points == null)
                {
                    MessageBox.Show("Bad deserialization");
                    return;
                }

                interaction.SetOutput(points);
            }));

            disposables(ViewModel.SaveFileDialog.RegisterHandler(interaction =>
            {
                var window = new SaveFileDialog();
                window.Filter = @"Json files (*.json)|*.json";

                if (window.ShowDialog() == false) return;

                interaction.SetOutput(window.FileName);
            }));
        });
    }

    public MainViewModel? ViewModel { get;set; }

    object? IViewFor.ViewModel 
    { 
        get => ViewModel; 
        set => throw new NotImplementedException(); 
    }
}
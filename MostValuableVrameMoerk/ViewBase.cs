using System;
using System.Collections.Generic;
using System.Linq;
using System.Xaml;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Input;
using System.Windows.Controls;
using System.Globalization;

namespace MostValuableVrameMoerk
{
    public class ViewBase<T> : Window where T : new()
    {
        T ViewModel { get; set; }
        bool isLoaded = false;
        public ViewBase()
        {
            ViewModel = new T();
            DataContext = ViewModel;
            InitializeComponent();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (isLoaded)
            {
                return;
            }
            isLoaded = true;
            System.Windows.Application.LoadComponent(
                this, 
                new System.Uri($"/{typeof(T).Namespace};component/{this}.xaml", 
                System.UriKind.Relative));
        }

        public ViewBase(T viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }
    }

    public class AsyncButton : Button
    {
        public string ClickAsync { get; set; }

        protected override void OnClick()
        {
            var win = Window.GetWindow(this);
            win.DataContext.GetType()
                .GetMethods()
                .First(x => x.Name == ClickAsync)
                .Invoke(
                    win.DataContext, 
                    System.Reflection.BindingFlags.Default, 
                    null, 
                    null, 
                    CultureInfo.CurrentCulture);
        }
    }
}

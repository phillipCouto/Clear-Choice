﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C5EC6C7A9F3CB7CAA259AF5EFA993E49"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Stemstudios.UIControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClearChoice {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition LeftColumnField;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopStackPanel;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MinimizeIcon;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MaxWindowIcon;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CloseIcon;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel TabControlContainer;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LeftContentGrid;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition CommonTasksContentDef;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border CommonTasksExpanderBorder;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CommonTasksContent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LeftColumnField = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 2:
            this.TopStackPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 40 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.TopStackPanel_MouseMove);
            
            #line default
            #line hidden
            
            #line 40 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TopStackPanel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.MinimizeIcon = ((System.Windows.Controls.Image)(target));
            
            #line 48 "..\..\MainWindow.xaml"
            this.MinimizeIcon.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseEnter);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.MinimizeIcon.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseLeave);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.MinimizeIcon.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MinimizeIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MaxWindowIcon = ((System.Windows.Controls.Image)(target));
            
            #line 49 "..\..\MainWindow.xaml"
            this.MaxWindowIcon.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MaxWindowIcon_MouseDown);
            
            #line default
            #line hidden
            
            #line 49 "..\..\MainWindow.xaml"
            this.MaxWindowIcon.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseEnter);
            
            #line default
            #line hidden
            
            #line 49 "..\..\MainWindow.xaml"
            this.MaxWindowIcon.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CloseIcon = ((System.Windows.Controls.Image)(target));
            
            #line 50 "..\..\MainWindow.xaml"
            this.CloseIcon.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CloseIcon_MouseDown);
            
            #line default
            #line hidden
            
            #line 50 "..\..\MainWindow.xaml"
            this.CloseIcon.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseEnter);
            
            #line default
            #line hidden
            
            #line 50 "..\..\MainWindow.xaml"
            this.CloseIcon.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CloseIcon_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TabControlContainer = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 9:
            
            #line 58 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            
            #line 58 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Grid_MouseEnter);
            
            #line default
            #line hidden
            
            #line 58 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Grid_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 10:
            this.LeftContentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.CommonTasksContentDef = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 12:
            this.CommonTasksExpanderBorder = ((System.Windows.Controls.Border)(target));
            
            #line 73 "..\..\MainWindow.xaml"
            this.CommonTasksExpanderBorder.MouseEnter += new System.Windows.Input.MouseEventHandler(this.UpComingExpanderBorder_MouseEnter);
            
            #line default
            #line hidden
            
            #line 73 "..\..\MainWindow.xaml"
            this.CommonTasksExpanderBorder.MouseLeave += new System.Windows.Input.MouseEventHandler(this.UpComingExpanderBorder_MouseLeave);
            
            #line default
            #line hidden
            
            #line 73 "..\..\MainWindow.xaml"
            this.CommonTasksExpanderBorder.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CommonTasksExpanderBorder_MouseDown);
            
            #line default
            #line hidden
            return;
            case 13:
            this.CommonTasksContent = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


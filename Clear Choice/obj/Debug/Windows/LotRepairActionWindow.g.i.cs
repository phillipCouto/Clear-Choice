﻿#pragma checksum "..\..\..\Windows\LotRepairActionWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ABF688276B9AC0FD9A143710642ED8A5"
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


namespace Clear_Choice.Windows {
    
    
    /// <summary>
    /// LotRepairActionWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class LotRepairActionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProblem;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDescription;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SDatePicker dpActionDate;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTime;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAction;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdSaveEdit;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\LotRepairActionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/windows/lotrepairactionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\LotRepairActionWindow.xaml"
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
            this.txtProblem = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.txtProblem.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtDescription = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.txtDescription.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dpActionDate = ((Stemstudios.UIControls.SDatePicker)(target));
            
            #line 26 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.dpActionDate.SelectedDateChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dpActionDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtTime = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.txtTime.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtAction = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.txtAction.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cmdSaveEdit = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.cmdSaveEdit.Click += new System.Windows.RoutedEventHandler(this.cmdSaveEdit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cmdCancel = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Windows\LotRepairActionWindow.xaml"
            this.cmdCancel.Click += new System.Windows.RoutedEventHandler(this.cmdCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


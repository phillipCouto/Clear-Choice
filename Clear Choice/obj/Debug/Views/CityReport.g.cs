﻿#pragma checksum "..\..\..\Views\CityReport.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DC84B25257DF311836EB1A94C1B4B25F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
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


namespace Clear_Choice.Views {
    
    
    /// <summary>
    /// CityReport
    /// </summary>
    public partial class CityReport : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\Views\CityReport.xaml"
        internal Stemstudios.UIControls.SComboBox cmboCities;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\CityReport.xaml"
        internal Microsoft.Windows.Controls.DataGrid dgLots;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Views\CityReport.xaml"
        internal System.Windows.Controls.Button button1;
        
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
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/views/cityreport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CityReport.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Views\CityReport.xaml"
            ((Clear_Choice.Views.CityReport)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UserControl_IsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmboCities = ((Stemstudios.UIControls.SComboBox)(target));
            
            #line 37 "..\..\..\Views\CityReport.xaml"
            this.cmboCities.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmboCities_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgLots = ((Microsoft.Windows.Controls.DataGrid)(target));
            
            #line 47 "..\..\..\Views\CityReport.xaml"
            this.dgLots.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.dgLots_MouseDown);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\Views\CityReport.xaml"
            this.dgLots.AutoGeneratedColumns += new System.EventHandler(this.dgLots_AutoGeneratedColumns);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button1 = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\Views\CityReport.xaml"
            this.button1.Click += new System.Windows.RoutedEventHandler(this.button1_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

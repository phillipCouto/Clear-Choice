﻿#pragma checksum "..\..\..\Views\ClientView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4410313B9DFAD92ADFA10F559B7D5354"
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


namespace ClearChoice.Views {
    
    
    /// <summary>
    /// ClientView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class ClientView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox grpNames;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SComboBox cmbTypeOfClient;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SPhoneField txtContactNumber;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SPhoneField txtFaxNumber;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdSaveEdit;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdCancel;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox grpAddressInfo;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtStreet;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCity;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPostalCode;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox grpPriceInfo;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox amtRoughIn;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox amtService;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox amtFinal;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Views\ClientView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid siteLotsView;
        
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
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/views/clientview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ClientView.xaml"
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
            this.grpNames = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\Views\ClientView.xaml"
            this.txtName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbTypeOfClient = ((Stemstudios.UIControls.SComboBox)(target));
            
            #line 55 "..\..\..\Views\ClientView.xaml"
            this.cmbTypeOfClient.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.combo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtContactNumber = ((Stemstudios.UIControls.SPhoneField)(target));
            
            #line 62 "..\..\..\Views\ClientView.xaml"
            this.txtContactNumber.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtFaxNumber = ((Stemstudios.UIControls.SPhoneField)(target));
            
            #line 64 "..\..\..\Views\ClientView.xaml"
            this.txtFaxNumber.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 66 "..\..\..\Views\ClientView.xaml"
            this.txtEmail.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cmdSaveEdit = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Views\ClientView.xaml"
            this.cmdSaveEdit.Click += new System.Windows.RoutedEventHandler(this.cmdSaveEdit_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cmdCancel = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\Views\ClientView.xaml"
            this.cmdCancel.Click += new System.Windows.RoutedEventHandler(this.cmdCancel_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.grpAddressInfo = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 10:
            this.txtStreet = ((System.Windows.Controls.TextBox)(target));
            
            #line 89 "..\..\..\Views\ClientView.xaml"
            this.txtStreet.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.txtCity = ((System.Windows.Controls.TextBox)(target));
            
            #line 91 "..\..\..\Views\ClientView.xaml"
            this.txtCity.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.txtPostalCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 93 "..\..\..\Views\ClientView.xaml"
            this.txtPostalCode.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.grpPriceInfo = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 14:
            this.amtRoughIn = ((Stemstudios.UIControls.SNumberBox)(target));
            
            #line 113 "..\..\..\Views\ClientView.xaml"
            this.amtRoughIn.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.amtService = ((Stemstudios.UIControls.SNumberBox)(target));
            
            #line 115 "..\..\..\Views\ClientView.xaml"
            this.amtService.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.amtFinal = ((Stemstudios.UIControls.SNumberBox)(target));
            
            #line 117 "..\..\..\Views\ClientView.xaml"
            this.amtFinal.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldValueChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            this.siteLotsView = ((System.Windows.Controls.DataGrid)(target));
            
            #line 124 "..\..\..\Views\ClientView.xaml"
            this.siteLotsView.AutoGeneratedColumns += new System.EventHandler(this.siteLotsView_AutoGeneratedColumns);
            
            #line default
            #line hidden
            
            #line 129 "..\..\..\Views\ClientView.xaml"
            this.siteLotsView.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.siteLotsView_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


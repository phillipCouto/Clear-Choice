﻿#pragma checksum "..\..\IconButton.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "46D630F049C0B38D0C4EC1A666B03B02"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Stemstudios.UIControls {
    
    
    /// <summary>
    /// IconButton
    /// </summary>
    public partial class IconButton : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\IconButton.xaml"
        internal System.Windows.Controls.Grid ObjectContainer;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\IconButton.xaml"
        internal System.Windows.Controls.Border Border;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBGFirst;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBGSecond;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBGThird;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBDFirst;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBDSecond;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\IconButton.xaml"
        internal System.Windows.Media.GradientStop HoverBDThird;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\IconButton.xaml"
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\IconButton.xaml"
        internal System.Windows.Controls.Image IconImage;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\IconButton.xaml"
        internal System.Windows.Controls.TextBlock ButtonText;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\IconButton.xaml"
        internal System.Windows.Media.SolidColorBrush ButtonTextColor;
        
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
            System.Uri resourceLocater = new System.Uri("/UIControls;component/iconbutton.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\IconButton.xaml"
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
            
            #line 7 "..\..\IconButton.xaml"
            ((Stemstudios.UIControls.IconButton)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ContentPanel_MouseEnter);
            
            #line default
            #line hidden
            
            #line 7 "..\..\IconButton.xaml"
            ((Stemstudios.UIControls.IconButton)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ContentPanel_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ObjectContainer = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Border = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.HoverBGFirst = ((System.Windows.Media.GradientStop)(target));
            return;
            case 5:
            this.HoverBGSecond = ((System.Windows.Media.GradientStop)(target));
            return;
            case 6:
            this.HoverBGThird = ((System.Windows.Media.GradientStop)(target));
            return;
            case 7:
            this.HoverBDFirst = ((System.Windows.Media.GradientStop)(target));
            return;
            case 8:
            this.HoverBDSecond = ((System.Windows.Media.GradientStop)(target));
            return;
            case 9:
            this.HoverBDThird = ((System.Windows.Media.GradientStop)(target));
            return;
            case 10:
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.IconImage = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.ButtonText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.ButtonTextColor = ((System.Windows.Media.SolidColorBrush)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

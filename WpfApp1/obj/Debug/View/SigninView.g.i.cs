﻿#pragma checksum "..\..\..\View\SigninView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "972DED71CD1F0E1F90CF2176D924974A29D580FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using System.Windows.Shell;
using WpfApp1;


namespace WpfApp1.View {
    
    
    /// <summary>
    /// SigninView
    /// </summary>
    public partial class SigninView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxUsername;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelInfoUsername;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBoxPasswordCheck;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelInfoPasswordCheck;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSignin;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\View\SigninView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/view/signinview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\SigninView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextBoxUsername = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\View\SigninView.xaml"
            this.TextBoxUsername.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxUsername_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LabelInfoUsername = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.PasswordBoxPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.PasswordBoxPasswordCheck = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 36 "..\..\..\View\SigninView.xaml"
            this.PasswordBoxPasswordCheck.LostFocus += new System.Windows.RoutedEventHandler(this.PasswordBoxPasswordCheck_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LabelInfoPasswordCheck = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ButtonSignin = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\View\SigninView.xaml"
            this.ButtonSignin.Click += new System.Windows.RoutedEventHandler(this.ButtonSignin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\View\SigninView.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\View\SigninView.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

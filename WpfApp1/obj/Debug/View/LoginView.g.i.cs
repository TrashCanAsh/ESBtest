﻿#pragma checksum "..\..\..\View\LoginView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "30F03E2CAEFEF8C7812A1593F92AB89CE37B942B"
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
    /// LoginView
    /// </summary>
    public partial class LoginView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 74 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonMini;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxUsername;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLogin;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\View\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSignin;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/view/loginview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\LoginView.xaml"
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
            
            #line 72 "..\..\..\View\LoginView.xaml"
            ((System.Windows.Controls.Label)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Label_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonMini = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\View\LoginView.xaml"
            this.ButtonMini.Click += new System.Windows.RoutedEventHandler(this.ButtonMini_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\View\LoginView.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBoxUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PasswordBoxPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 87 "..\..\..\View\LoginView.xaml"
            this.PasswordBoxPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordBoxPassword_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonLogin = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\View\LoginView.xaml"
            this.ButtonLogin.Click += new System.Windows.RoutedEventHandler(this.ButtonLogin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonSignin = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\..\View\LoginView.xaml"
            this.ButtonSignin.Click += new System.Windows.RoutedEventHandler(this.ButtonSignin_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


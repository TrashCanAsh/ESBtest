   M S B A M L   `   `   `  ????B  >ESBtest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ESBtest.ViewESBtest.View  5   	   /clr-namespace:ESBtest.CommonESBtest.Common  5   	       ESBtest.View.UpdateView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   &localclr-namespace:ESBtest.View  5   	   )commonclr-namespace:ESBtest.Common  5   	   -   5        $??UpdateWindow??5	        ? Title$  修改界面??5
   	   $	??450??6   $	??250??6#    ? 
ResizeMode$ NoResize=?6/    ? WindowStartupLocation$ CenterScreen=?5   	    ? WindowStyle$
 None=?6/    ? AllowsTransparency .    MaterialDesignBody#???   5   	   $??Medium"?5   	   $??12(?5   	   ??5   #   Z? .??5      ?? $??5q?6   ???#FAFAFA J? Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$
 Blue??6      ShadowDepth$ 0Q?6,      
BlurRadius$ 5Q?6<   	   Opacity$		 0.2Q?6K   
   	Direction$
 0Q?6Y   ._?5   
   ?  ? RowDefinitions 5      ?? 5      $??20 ?6    ?? 5      .+?5      ?? -   5        $	??Top=?5      $??20??6,   68   ?? 5       ?? Orientation$ 
Horizontal=?6   $??Center=?62   $??Right=?6M   $??0??6i   .+?5      ?? -   5         ??Name$ 
ButtonMini??5        MinimizeWindowButtonStyle#??[ 6-   ??6`   ?? *MinWindowCommand+??6?   ??  ?? ElementName$ UpdateWindow???? 5      -   5        $ ButtonClose??5        CloseWindowButtonStyle#??[ 6.   ??6^   ?? *CloseWindowCommand+??6?   ?? $ UpdateWindow??5      ?? 5      $??1??6   .@?5       ?? $??修改样品信息??6   .??5!      ?  5"      ?? 5#      $
??auto ?6,   ?? 5$      $
??auto ?6,   ?? 5%      $
??auto ?6,   ?? 5&      $
??auto ?6,   ?? 5'      $
??auto ?6,   ?? 5(      $
??auto ?6,   ?? 5)      $
??auto ?6,   ?? 5*      $
??auto ?6,   .+?5,      ?? $??0??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?5-      ?? $??样品编号??6$   ?? 5.      -   5         ??Name$ TextBoxSampleID??5.   &   $	??125??6g    ?? 
IsReadOnly . ??6?   ?? *SampleUpdated.SampleID+6s   5/      ?? 50      $??1??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?51      ?? $??样品名称??6$   ?? 52      -   5        $ TextBoxSampleName??52   &   $	??125??6k   ??6A   ?? *SampleUpdated.SampleName+6k   53      ?? 54      $??2??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?55      ?? $??样品种类??6$   ?? 56      -   5         ??Name$ ComboBoxCategoryUpdate??56   '   $	??125??6G    ?? SelectedIndex 6S   ?? *SampleUpdated.CategoryIndex+ ?? Mode$ TwoWay=???57   1   ?? *ComboBoxCategory+ ?? UpdateSourceTrigger$ PropertyChanged=?$ TwoWay=?58      ?? 59      $??3??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?5:      ?? $??采样时间??6$   (  "System.Windows.Controls.DatePicker  5;      -   5          Name$ DatePikerStartDateUpdate??5;   )   $	??125??6?      SelectedDate 6K   ?? * SampleUpdated.SamplingDateTime+$ PropertyChanged=?$ TwoWay=?6?   5<      ?? 5=      $??4??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?5>      ?? $??采样地点??6$   ?? 5?      $	??x: ??6$   ?? 5@      -	   5        $ TextBoxSampleLocX??5@   &   $??40??6A   .   (ESBtest.Common.TextBoxAttachedProperties   MyIsOnlyNumber . ??6L   ?? *SampleUpdated.Longitude+ ?? StringFormat$ f2??$ TwoWay=?6?   ?? 5A      $	??y: ??6$   ?? 5B      -
   5        $ TextBoxSampleLocY??5B   &   $??40??6A    . ??6L   ?? *SampleUpdated.Latitude+$ f2??$ TwoWay=?6?   5C      ?? 5D      $??5??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?5E      ?? $??样品状态??6$   ?? 5F      -   5        $ ComboBoxStateUpdate??5F   '   $	??125??6D    6P   ?? *SampleUpdated.State+$ TwoWay=???5G   1   ?? *ComboBoxState+$ PropertyChanged=?$ TwoWay=?5H      ?? 5I      $??6??6%   $ 
Horizontal=?62   $??0 10 0 0q?6K   .+?5J      ?? $??备注??6$   ?? 5K      -   5        $ TextBoxSampleComment??5K   &   $	??170??6D   $	??100??6P    ! MaterialDesignOutlinedTextBox#??[ 6]   $??24 0 0 0q?5L   M    ?? TextWrapping$
 Wrap=?6_    ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?6?   ??6&   ?? *SampleUpdated.Comment+6?   5M      ?? 5N      -   5         ??Name$ AdminController??5N   %   $??7??6>   $ 
Horizontal=?6K   $??0 10 0 0q?6d   .+?5O      ?? $??确认修改??6%   $??90??64   $??20 0 0 0q?6?     MaterialDesignPaperButton#??[ 6Q   ??5P   %   ?? *UpdateSampleCommand+??6M   ?? $ UpdateWindow???? 5Q      $??取消??6%   $??90??62   $??20 0 0 0q?6=   #??[ 6O   ??5R   %   ?? *CancelCommand+??6G   ?? $ UpdateWindow??5S      5T      5U      ?? 5W      $??	二维码??6   .??5X      ?  5Y      ?? 5Z      ?? 5[      $
??auto ?6,   .+?5]      ?? -   5         ??Name$ QRcodeImage??5]       $??0??65   $	??200??6B   $	??200??6O   ??6[   ?? *QRcode+$ PropertyChanged=??? 5^      $??1??6%   $ 
Horizontal=?62   .+?5_      ?? $??生成二维码??6%   #??[ 65   ??5`   %   ?? *GenerateQRcodeCommand+??6O   ?? $ UpdateWindow??5a      5b      5c      5d      5e      5g      5h      
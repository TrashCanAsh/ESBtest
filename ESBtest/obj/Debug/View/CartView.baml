   M S B A M L   `   `   `  ????B  >ESBtest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ESBtest.ViewESBtest.View  5   	       ESBtest.View.CartView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   &localclr-namespace:ESBtest.View  5   	   -   5        $??
CartWindow??5        ? Title$  
CartWindow??5	   	   $	??450??6   $	??500??6)   $	??300??65   $	??500??6E    ? WindowStartupLocation$ CenterScreen=?5
   	    ? WindowStyle$
 None=?6/    ? AllowsTransparency .  ? 
ResizeMode$ CanResizeWithGrip=?5   9      MaterialDesignBody#???   5   	   $??Medium"?5   	   $??12(?5   	   ??5   #   Z? .??5      ?? $??5q?6   ???#F0F0F0 J? Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Green??6      ShadowDepth$ 0Q?6-      
BlurRadius$ 5Q?6=   	   Opacity$		 0.3Q?6L   
   	Direction$
 0Q?6Z   ._?5   
   ?  ? RowDefinitions 5      ?? 5      $
??auto ?6    ?? 5      ?? 5      $??50 ?6    .+?5      ? $??0??6    ? ColumnDefinitions 5      ?? 5      ?? 5      $??30 ?6'   ?? 5      $??30 ?6'   ?? 5       $??30 ?6'   .+?5"      ?? -   5        $	??Top=?5"      $??20??60   $??0??6X   ?? 5#      -   5         ??Name$ 
ButtonMini??5#        MinimizeWindowButtonStyle#??[ 6-   $??1??5$   r   ??6   ?? *MinWindowCommand+??6>   ??  ?? ElementName$ 
CartWindow??6r   ?? 5%      -   5        $ 	ButtonMax??5%        MaximizeWindowButtonStyle1#??[ 6,   $??2??5&   r   ??6   ?? *MaxWindowCommand+??6>   ?? $ 
CartWindow??6r   ?? 5'      -   5        $ ButtonClose??5'        CloseWindowButtonStyle#??[ 6.   $??3??5(   t   ??6   ?? *CloseWindowCommand+??6@   ?? $ 
CartWindow??6t   5)      &   System.Windows.Controls.DataGrid  5+      -   5        $??CartDataGrid??5+      $??1??6+   $??14(?6?      AutoGenerateColumns .     CanUserAddRows .  ??68   ?? *SampleModelList+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ TwoWay=?   RowStyle 5-      ?? 5.       ?? 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .?5/      ??  ?? Property ? ?? ?? Value
 ?????67    ?? Triggers 50      P? 51       P? Property ?@J?IsMouseOver P? Value . . ?52   "   ??  ? ??
 ?????6?   53      P? 54       ?@ 
IsSelected . . ?55   "   ??  ? ??
 ?????6?   56      58         	CellStyle 5:      ?? 5;      *  $System.Windows.Controls.DataGridCell"  6    5<      P? 5=       ?@ 
IsSelected . . ?5>   "   ??  ? ??
 ???? 6?   ?? 5?   "    ? ??
 ?   ?6?   5@      5B         Columns 5D      4  .System.Windows.Controls.DataGridCheckBoxColumn  5E      1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   ?? *
IsSelected+$ TwoWay=?$ PropertyChanged=?,  &System.Windows.Controls.DataGridColumn   HeaderTemplate 5F      ?? 5G      .R?5H   "   ?? $??Center=?6+   $??Center=?6H   ??6c   ??  ?? Path$! DataContext.CheckAllCommand? ?? RelativeSource ??   ?? AncestorType$  DataGridL???5I   /   ?? $ 	IsChecked? ?? *Self+5J      !   ElementStyle! 5L      ?? 5M      " ??6$   "   	CellStyle" 5O      ?? 5P      .?5Q   "   ??  ? ??$ Center=?6P   ?? 5R   "    ? ??$ Center=?6R   5S      5U      0  *System.Windows.Controls.DataGridTextColumn  5V      #   Header$# 编号??6)   $   
IsReadOnly$ .  65   ?? *
SampleID+6R     5W      $# 名称??6)   $ .  65   ?? *
SampleName+6T     5X      $# 种类??6)   $ .  65   ?? *
Category+6R     5Y      $# 采样时间??6)   $ .  67   ?? *SamplingDate+6X     5Z      $# 采样地点??6)   $ .  67   ?? *SamplingLocation+6\   5\      ?? 5]      $??2??6   % ?? Orientation$% 
Horizontal=?6&   $??100 5q?6?   .+?5^      ?? $??移除??6   $??90??6&   $??0 0 20 0q?61     MaterialDesignPaperButton#??[ 6C   ??5_      ?? *DeleteCartCommand+??6?   ?? $ 
CartWindow???? 5`      $??申请领用??6   $??90??6(   #??[ 63   ??5a      ?? *SampleRequestCommand+??6B   ?? $ 
CartWindow??5b      5c      5d      5e      
   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice#   Clear_Choice.Views.CityReport   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5      Atoolkit-http://schemas.microsoft.com/wpf/2008/toolkit     5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $	��150 �6#   �� 5       � RowDefinitions 5      �� 5       $
��Auto �6    �� 5!      .+�5#      �� $��0��6   $��0��6(   $
��Left=�65   $��20,5,5,5q�6P   $	��Top=�6b   $��Select a city:��6z   $��20(�6�   $��Normal&�6�   
�������6�   &    Stemstudios.UIControls.SComboBox  5$      -   5        $��
cmboCities��5$   #   $��1��65   $��5q�6E   6P   &   System.Windows.Controls.DataGrid  5%      -   5        $��dgLots��5%      $��1��6%   $��3��62   $��5,16,5,5q�5&         CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5*         RowHeaderWidth$ 17Q�5+         	RowHeight$ 20Q�63      SelectionMode$ Extended=�5,      $   HorizontalScrollBarVisibility$ Hidden=�68   $��12(�5-      50      51      52      53      
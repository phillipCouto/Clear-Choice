   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice,   &Clear_Choice.Views.LotLabourCostReport   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5       � RowDefinitions 5       �� 5!      $
��Auto �6    �� 5"      $
��Auto �6    �� 5#      �� 5$      $
��Auto �6    .+�5&      �� $��0��6   $��0��6(   $��5��65   $
��Left=�6I   $��20,5,5,5q�6d   $	��Top=�6v   $��Total Labour Report��6�   $��20(�6�   $��Normal&�6�   
�������6�   �� 5'      $��0��6   $��1��6(   $��20,5,5,5q�65   $��Center=�6G   $��Right=�6b   $��From:��6~   $��14(�6�   $��Normal&�6�   
�������6�   (   "Stemstudios.UIControls.SDatePicker  5(      -   5        $��dpFrom��5(   %   $��1��63   $��1��6C   $��5q�6P   $	��100��6�   �� 5)      $��2��6   $��1��6(   $��20,5,5,5q�65   $��Center=�6G   $��Right=�6b   $	��To:��6~   $��14(�6�   $��Normal&�6�   
�������6�     5*      -   5        $
��dpTo��5*   %   $��3��61   $��1��6A   $��5q�6N   $	��100��6�   &   System.Windows.Controls.DataGrid  5+      -   5        $��dgLabourHours��5+      $��2��6,   $��5��69   $��5,16,5,5q�5,         CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 50         RowHeaderWidth$ 17Q�51         	RowHeight$ 20Q�63      SelectionMode$ Single=�52      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�53      56      �� 57       �� Orientation$ 
Horizontal=�6   $��3��62   $��5��6?   $��Right=�6S   .+�58      �� $��Average Hours��6   $��5q�61   $��14(�6<   ���	#FFFFFFFF6J   �� 59      -   5        $��txtAvgHours��59      $��5q�6-   ���	#00FFFFFF���	#FFFFFFFF �� 
IsReadOnly . $��0��6x   �� 5:      $��Average Cost��6   $��5q�60   $��14(�6;   ���	#FFFFFFFF6I   '   !Stemstudios.UIControls.SNumberBox  5;      -   5        $��
amtAvgCost��5;   (   $��5q�6:      
IsCurrency .    
IsReadOnly .    Amount$ 0��6i   �� 5<      $��Total Hours��6   $��5q�6/   $��14(�6:   ���	#FFFFFFFF6H   �� 5=      -   5        $��txtTotalHours��5=      $��5q�6/   ���	#00FFFFFF���	#FFFFFFFF . $��0��6z   �� 5>      $��
Total Cost��6   $��5q�6.   $��14(�69   ���	#FFFFFFFF6G     5?      -   5        $��amtTotalCost��5?   (   $��5q�6<    .  . $ 0��6k   5@      5A      5B      5C      
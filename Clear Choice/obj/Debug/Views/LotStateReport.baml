   M S B A M L   `   `   `  ����  Clear Choice'    !Clear_Choice.Views.LotStateReport   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $	��150 �6#   �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       .+�5"      �� $��0��6   $��0��6(   $��3��65   $
��Left=�6I   $��20,5,5,5q�6d   $��Center=�6v   $��Lots by Status:��6�   $��20(�6�   $��Normal&�6�   
�������6�   �� 5#      $��0��6   $��1��6(   $
��Left=�65   $��20,5,5,5q�6P   $��Center=�6b   $��
Completed:��6}   $��14(�6�   $��Normal&�6�   
�������6�   �� 5$      -   5        $��
cmboStatus��5$      $��1��6)   $��1��69   $��5q�6F   $��14(�6Q    �� SelectedIndex$ 0��6�   .@�5%      �� .��6   No6#   �� 5&      .��6   Yes6$   5'      &   System.Windows.Controls.DataGrid  5(      -   5        $��dgLotsCompleted��5(      $��0��5)   #   $��2��5*   #   $��3��5+      $��5q�5,      	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 50         RowHeaderWidth$ 17Q�51         	RowHeight$ 20Q�63      SelectionMode$ Single=�52      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�53      55      56      57      58      
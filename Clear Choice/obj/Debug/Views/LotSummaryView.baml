   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice'   !Clear_Choice.Views.LotSummaryView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      .+�5       �� -   5        $��
grpSummary��5       $��16(�6)   $��0��67   $��0��6G   ���#FFFFFF$��5q�6i   ��5!      �� 5"      $��Lot Summary��6    
���������6F   ��  �� Path$ Width�	 �� ElementName$	 
grpSummary��
 �� Mode$
 Default=�.��5$      �  5%      �� 5&      $
��Auto �6+   �� 5'      $	��150 �6+    5)      �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $��20 �6(   �� 5-      $
��Auto �6(   �� 5.      $
��Auto �6(   �� 5/      $��20 �6(   �� 50      $
��Auto �6(   .+�52      �� $��0��6    $��0��60   $��Services Billed:��6=   $��Right=�6U   $��Center=�6q   $��5q�6�   '   !Stemstudios.UIControls.SNumberBox  53      -   5        $��amtServices��53   ,   $��1��6?   $��0��6O      
IsReadOnly . $��5q�6n   �� 54      $��0��6    $��1��60   $��Extras Billed:��6=   $��Right=�6S   $��Center=�6o   $��5q�6�     55      -   5        $��	amtExtras��55   ,   $��1��6=   $��1��6M    . $��5q�6l   �� 56      $��0��6    $��3��60   $��Labour Cost:��6=   $��Right=�6Q   $��Center=�6m   $��5q�6�     57      -   5        $��	amtLabour��57   ,   $��1��6=   $��3��6M    . $��5q�6l   �� 58      $��0��6    $��4��60   $��Average Material Cost:��6=   $��Right=�6[   $��Center=�6w   $��5q�6�     59      -   5        $��amtMaterials��59   ,   $��1��6@   $��4��6P    . $��5q�6o   �� 5:      $��0��6    $��6��60   $��Profit:��6=   $��Right=�6L   $��Center=�6h   $��5q�6�     5;      -   5        $��	amtProfit��5;   ,   $��1��6=   $��6��6M    . $��5q�6l   5<      5=      �� 5>      -   5        $��
grpRepairs��5>      $��14(�6)   $��0��67   $��1��6G   ���#FFFFFF$��5q�6i   ��5?      �� 5@      $��Outstanding Repairs��6    
���������6N   �� $ Width�$	 
grpRepairs��$
 Default=�.��5B      &   System.Windows.Controls.DataGrid  -   5        $��	dgRepairs��5B      $��0��5C   #   $��1��5D   #   $��5q�5E         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5I         RowHeaderWidth$ 17Q�5J         	RowHeight$ 20Q�63      SelectionMode$ Single=�5K      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�5L      5M      5N      5O      5P      5Q      
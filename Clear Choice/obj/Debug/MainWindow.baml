   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5   	    Clear Choice   ClearChoice.MainWindow   5   	   ?my8clr-namespace:Stemstudios.UIControls;assembly=UIControls  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$!  Clear Choice Business Suite��5   	   $	��800��6-   $
��1024��6:    � AllowsTransparency .  � WindowStyle$
 None=�6a    � 
ResizeMode$ CanResizeWithGrip=�6t    � Icon$ /Resources/logo2.ico��6�   ���	#00000000 � 	Resources 5         HomeIcon  
windowIcon  maximizeIcon g� 	BeginTime  SideBarAnimationShrink0   *Stemstudios.UIControls.GridLengthAnimation �� 
TargetName �� TargetProperty	   From	
   To g� Duration  SideBarAnimationExpand�� 5   
   %�  &	        &	 3     &	 l     &	 �     &	 7    �� 5      $��/Resources/Icons/Home.png��6%   �� 5	      $%��/Resources/Icons/windowIcon.png��6'   �� 5
      $'��!/Resources/Icons/maximizeIcon.png��6)   �� 5      $ 0X�6   .��5        $ LeftColumnField��6)   $ Width�6Q   $		 220 �6t   $
 0 �6   $ 0:0:0.1D�6�   5      �� 5      $ 0X�6   .��5        $ LeftColumnField��6)   $ Width�6Q   $	 0 �6t   $	
 220 �6}   $ 0:0:0.1D�6�   5      5      .��5      �� $��1q�6    �� CornerRadius$ 5��6!   ���	#FF000000 �� Padding$ 5q�6J   ��5   
   �� 5       �� ImageSource$ /Resources/Background.png��6    j� Stretch$ UniformToFill=�6A   ._�5      �  � RowDefinitions 5   
   �� 5       $��36 �6   6*   �� 5!      6    � ColumnDefinitions 5#   
   �� 5$      -   5         ��Name$ LeftColumnField��5$   +   $	��220 �6   6F   �� 5%      $��15 �6   6,   �� 5&      6!   .+�5(   
   ]� $��0��6   $��0��6$   $��3��61   .+�5)      �� $��36��6    �� Orientation$ Vertical=�6%   .+�5*      � -   5        $��TopStackPanel��5*   #   $��35��6   $��Stretch=�69    5+      �� 5,      $	��200 �6+   69   �� 5-      6-   �� 5.      $	��230 �6+   69   .+�50      �� -   5        $��36��50   !   $ 
Horizontal=�6-   $��0��6F   .+�51      �� $��Clear Choice��6$   $��20(�68   
�������$��Italic$�6Y   $��20,2,5,2q�6l   52      �� 53      $��1��6$   ���	#8897c3dc$��5,5,5,5q�6?   6R   54      �� 55      $��36��6!   $ 
Horizontal=�6-   $��2��6F   .+�56      �� -   5        $��	SearchBox��56   "   $��
Search Box��63   $	��150��6E   $��20��6Q   $��Center=�6]   $y�Hidden=�6x   �� 57      $��1��6$   ���	#8897c3dc$��5,5,5,5q�6?   6R   �� 58      -   5        $��MinimizeIcon��58       $'��!/Resources/Icons/minimizeIcon.png��64   $��20��6_   $��20��6k   6�   �� 59      -   5        $��MaxWindowIcon��59       $'��!/Resources/Icons/maximizeIcon.png��65   $��20��6`   $��20��6l   6�   �� 5:      -   5        $��	CloseIcon��5:       $$��/Resources/Icons/closeIcon.png��61   $��20��6Y   $��20��6e   6�   5;      5<      �� 5=      $��1��6   ���	#8897c3dc6:   5>      5?      ]� 5@   
   -	   5        $��TabControlContainer��5@   o   $��2��6   $��1��6$   ��. $��0,0,10,5q�6F   ���	#000000005A      � 5B   
   -
   5        $��1��5B      $��1��6    � ToolTip$- 'Click to Expand and Shrink Left Column.��6�   .+�5C   
   �� $��5,0,5,0q�6   ��5D      
� 5E      .��5F      �  � Color$ 	#99FFFFFF��6#    � Offset$ 0Q�65   � 5G      $ 	#00AAAAAA��6#   $	 0.7Q�65   5H      5J      5K      � 5L   
   -   5        $��LeftContentGrid��5L   >   $��0��6   $��1��6   $��5,5,0,5q�6,    5M      �� 5N      $
��25px �6    �� 5O      -   5         �Name$ CommonTasksContentDef��5O       6?   .+�5Q      �� -   5        $��CommonTasksExpanderBorder��5Q      $��0��66   $ 3,3,0,0��6C   ���	#99555555$��1,1,1,1q�6r   ��5R      �� 5S       �� 
StartPoint$ 0.4,0&�6*    �� EndPoint$ 0.4,1&�6=   .��5T      � $ 	#EE999999��6'   $ 0Q�69   � 5U      $ 	#EE222222��6'   $	 0.4Q�69   � 5V      $ 	#EE222222��6'   $	 0.6Q�69   � 5W      $ 	#EE333333��6'   $ 1Q�69   5X      ._�5Z      �� $��Center=�6   $��Center=�67   ���#FFFFFF	Actions5\      5]      � 5^      -   5        $��CommonTasksContent��5^       $��1��6   $
��Left=�6:   $	��Top=�6U   5_      5`      5a      5b      5c      
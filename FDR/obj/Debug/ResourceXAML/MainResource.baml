   M S B A M L   `   `   `  ����>  :FDR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FDR.ViewModelFDR.ViewModel  5      3clr-namespace:FDR.ResourceXAMLFDR.ResourceXAML  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OMaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null!    FDR.ViewModel.MainViewModel 
  MainVM$   FDR.ViewModel.SubjectViewModel 	 SubVM'   !FDR.ViewModel.AttendanceViewModel  AttendanceVM#  FDR.ViewModel.CameraViewModel  CameraVM#   FDR.ViewModel.PersonViewModel  PersonVM  ListViewItemStyle1  �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorLevel �� AncestorType �� 
TargetType%  MaterialDesignThemes.Wpf.Ripple J� SnapsToDevicePixels   Feedback	 �� Property
 �� 	Converter=  7MaterialDesignThemes.Wpf.Converters.BrushRoundConverter   	HighValue   LowValue  ScrollBarTrackThumb �Name ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName � RowDefinitions ]�Name ]� Thumb n�Name ]� IncreaseRepeatButton ��Name J� Opacity ]� DecreaseRepeatButton P� 
SourceName  ButtonSelectBrush  	DarkBrush �� Angle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      )	viewmodelclr-namespace:FDR.ViewModel  5      (localclr-namespace:FDR.ResourceXAML  5      %�  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 �    '�� F       5      6     5	      6!     5
      6$     5      6      5      6    �� 5      "  ��6'   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��$ 0q�6,   �� 5   
    � �� 67   �� *HorizontalContentAlignment+ �� *FindAncestor+$ 1��" ���� 5   
    � �� 65   �� *VerticalContentAlignment+ �� *FindAncestor+$ 1��" ���� 5   
    � ��$ 0q�6$   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� #��z��6   #��z��6I   
������ #}�z}�5   :   #��z��6h   ._�5      � .+�5        #��z��64   #��z��6h   |�.  #��z��5    J   #��z��6�   # z 5!   8   #��z��6t    5"   "   �� 5#   &   	 � ��
 5$   *     5%   .   
 �����
 ���� 6c   5'   '   5)      5*      5+      5-      5/      50      �� 52      "  n�53   	   .�54   
   ��  � �� 55      �� 56      " n�6"   .R�57      � $
 Grid��6   .+�58      �� $��Stretch=�6$   $��Stretch=�59   !   $
��Auto��5:   !   $
��Auto��5;   !   
������ 5<   !   �� 5=      $ CornerScrollBarRectangle��6!   $ 5��5>      $��Stretch=�5?      $��Stretch=�5@      $
��Auto��5A      $
��Auto��5B      $��0,1,0,1q�5C      #��z��5D      5E       5F      P� 5G       �@�Tag$ 
Horizontal��5H   !   . �5I      �� $ CornerScrollBarRectangle��6%    � ��$
 Auto��5K   !   �� 5L      $ CornerScrollBarRectangle��6%    � ��$ 6��5N   !   5O      5Q      5S      5T      �� 5V      "  ��6   .�5W   
   ��  �@��IsFlicksEnabled .  5X      �� 5Y   
    � �� �#ADABAB5Z      �� 5[   
    � ��
 ���� 5\      �� 5]   
    � ��$ 7��5^      �� 5_   
    � �� 5`      �� 5a      " ��6"   .R�5b      � $ GridRoot��6   $��7��5c      #��z��5d       5e      �� 5f      $��0.00001* �6,   .+�5h      ]� $ 
PART_Track��6    $��0��5i      ��. |�.   5l      n� 5m   "   $ Thumb��6(   #��z��5n   %   #���  5o   %    5q      �� 5r   "   $ PageUp��6/   $��ScrollBar.PageDownCommand��5s   -   $ 0Q�5t   -   |�.  5u   -    5w      �� 5x   "   $ PageDown��6/   $��ScrollBar.PageUpCommand��5y   -   $ 0Q�5z   -   |�.  5{   -   5}      5~       5      P� 5�      $ Thumb��6"    �@J�IsMouseOver . . �5�      �� # �  6%   $ Thumb��5�   !    � ��5�   !   5�      P� 5�      $ Thumb��6"    �@n�
IsDragging . . �5�      �� # �  6%   $ Thumb��5�   !    � ��5�   !   5�      P� 5�       � {� .  . �5�      �� $ Thumb��6%    � y�$ 	Collapsed=�5�   !   5�      P� 5�       �@��Orientation$ 
Horizontal=�5�   !   . �5�      �� $ GridRoot��6%    �@�LayoutTransform 5�   "   �� 5�   &   $	 -90Q�66   5�      �� 5�      $ 
PART_Track��6%    �@�LayoutTransform 5�   "   �� 5�   &   $	 -90Q�66   5�      �� 5�       � ��$
 Auto��5�   !   �� 5�       � ��$ 8��5�   !   �� 5�      $ Thumb��6%    �@�Tag$ 
Horizontal��5�   !   �� 5�      $ PageDown��6%    � ��$ ScrollBar.PageLeftCommand��5�   !   �� 5�      $ PageUp��6%    � ��$  ScrollBar.PageRightCommand��5�   !   5�      5�      5�      5�      5�      
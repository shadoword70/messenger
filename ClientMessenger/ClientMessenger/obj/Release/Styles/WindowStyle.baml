   M S B A M L   `   `   `  ���� J  FClientMessenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ClientMessenger.CommandsClientMessenger.Commands  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466   CaptionButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� ClearTypeHint&    System.Windows.Media.TextOptions    TextRenderingMode    TextFormattingMode	 �� Triggers
 P� Property P� Value �� 
TargetName  MinimizeButtonStyle �� BasedOn  MaximizeButtonStyle  RestoreButtonStyle  CloseButtonStyle  CustomWindow'  !System.Windows.Shell.WindowChrome   CaptionHeight ��Name �� CornerRadius � RowDefinitions �� Orientation   IsHitTestVisibleInChrome � ToolTip#  System.Windows.SystemCommands   MinimizeWindowCommand ��Name   RestoreWindowCommand   MaximizeWindowCommand   CloseWindowCommand  CustomButton �� Triggers :� RoutedEvent �� TargetProperty �� From	 �� To g� Duration  g� AutoReverse  CustomIconButton  CustomLabel 	 CustomTextBlock 
 CustomTextBox-  'System.Windows.Input.KeyboardNavigation! �� Padding" �� Color'  !System.Windows.VisualStateManager#   VisualStateGroups%  System.Windows.VisualStateGroup$   States   System.Windows.VisualState% ��Name  CustomPasswordBox  CustomWriteMessageTextBox  CustomListBox& K� IsItemsHost  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor  ControlLightColor  ControlMediumColor  ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor   PressedBorderLightColor ! PressedBorderDarkColor " DisabledBorderLightColor # DisabledBorderDarkColor $ DefaultBorderBrushDarkColor % HeaderTopColor "& DatagridCurrentCellBorderColor ' SliderTrackDarkColor ( NavButtonFrameColor ) MenuPopupBrush' �� EndPoint( �� 
StartPoint) � Color* � Offset $*  ProgressBarIndicatorAnimatedFill + ComboBoxToggleButton"  FontAwesome.WPF.ImageAwesome+   Icon , CustomComboBoxItem, J� SnapsToDevicePixels-   
Storyboard. �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame/ �� KeyTime0 �� Value - CustomComboBoxSettings1 R� KeyTime	  System.Windows.Visibility2 	  Visible3 R� Value4 	  Hidden5 d�Name6 �� 	ClickMode7 d� 	IsChecked8 �� Mode9 �� RelativeSource: �Name; �� IsDropDownOpen< � AllowsTransparency= � ActualHeight> � VerticalOffset? � ActualWidth@ � HorizontalOffsetA �� MaxDropDownHeightB   DirectionalNavigationC P� 
SourceName . ScrollBarPageButton / ScrollBarThumb 0 VerticalScrollBarD ]�NameE ]� Thumb 1 HorizontalScrollBar 2 CustomScrollViewerF � ColumnDefinitionsG �� VerticalOffsetH � ValueI �� ScrollableHeightJ � MaximumK �� ViewportHeightL �� ViewportSize*M �� #ComputedVerticalScrollBarVisibilityN �� OrientationO �� HorizontalOffsetP �� ScrollableWidthQ �� ViewportWidth,R �� %ComputedHorizontalScrollBarVisibility 3 CustomMessageListBox 4 CustomListBoxItemS ]� 
ConditionsT �� PropertyU �� Value�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     Bi:http://schemas.microsoft.com/expression/2010/interactivity  3commands&clr-namespace:ClientMessenger.Commands  -fa$http://schemas.fontawesome.io/icons/  %�(  &	        &	 V    7   &	 �    7   &	     7   &	 a    7   &	 �    7  7  7  7  &	 �    &	 �	    7  &	 q
    &		 �
    &	
 �
    &	 �    &	 �    7 
 &	 �    &	     &	     &	 1    &	 D    &	 W    &	 j    &	 }    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 F    &	 Y    &	 l    &	      &	! �    &	" �    &	# �    &	$ �    &	% �    &	& �    &	' �    &	(     &	) "    &	* �    &	+     &	, �    &	- g    7 , 7  7 + &	. �    &	/ �    &	0 S    7 / &	1 �    7 / '�� �     7 1 7 0 &	2 �!    &	3 T#    7  7 2 &	4 $    �� "  ��.���  � �� �� " ��.R�� $ 
LayoutRoot��
������ $��44��$��30��.+��� $	 txt��#��z��$��Segoe MDL2 Assets)�$��10(�
�������$��Center=�$��Center=�$
 Auto=�$ Aliased=�$ Display=�	 P� 
 �@J�IsMouseOver . . ��� $ 
LayoutRoot�� � ��
 �\\���� $	 txt�� � ��
 �   ��� "  ��8   .���  � ��$	 ����  � ��
 �������  � ��
 ������� "  ��8   .���  � ��$	 ����  � ��
 �������  � ��
 ������� "  ��8   .���  � ��$	 ����  � ��
 �������  � ��
 ������� "  ��8   .���  � ��$	 ����  � ��
 �������  � ��
 ������� "  �.���  �@ WindowChrome   $ 34Q���  � �� �� " �.R��� $ MainWindowBorder��
���   �$	��0.1q�$ 10��._�� ���#282e33 �� $
��Auto ��� $��* �.+�� $��0��$��30��
���   �.+��� $ 
Horizontal=�$
��Left=�$��Center=�.+��� $��../Images/logo.png��$��18��$��5 0 0 0q�$
��Left=�$��Center=��� $��	Messenger��$��Arial)�$��5 0 0 0q�$��Center=�
��������� $ 
Horizontal=�$��Right=�.+��� 8��   . $ Minimize��#��Z �� $ RestoreButton��$y�	Collapsed=�8�� #��Z  . $ Restore���� $ MaximizeButton��$y�Visible=�8�� #��Z  . $ Maximize���� 8��  . $ Close��#��Z � $��1��.+��� $��Content��$��Stretch=�$��Stretch=�	 P� 
 �@�WindowState$ 	Maximized=�. ��� $ RestoreButton�� � y�$ Visible=��� $ MaximizeButton�� � y�$ 	Collapsed=��� $ MainWindowBorder�� � ��$ 8q�P� 
 �@�WindowState$ 	Minimized=�. ��� $ RestoreButton�� � y�$ 	Collapsed=��� $ MaximizeButton�� � y�$ Visible=��� $ MainWindowBorder�� � ��$ 0q�P� 
 �@�WindowState$ Normal=�. ��� $ RestoreButton�� � y�$ 	Collapsed=��� $ MaximizeButton�� � y�$ Visible=��� $ MainWindowBorder�� � ��$ 0q��� "  ��.���  � ��
 �������  � ��
 ���� ��  � ��$ 30����  � �� �� " ��.R��� #��z�����#242a2e$��1q�._��� $��Center=�$��Center=� P� 
 � {� .  . ���  � ��
 ���� ��  � ��
 �����P� 
 �@J�IsMouseOver . . ���  � �� �#353c43:� $ Click��[��� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ TransparentX�$ #353c43X�$ 0:0:0.1D�  . �� "  ��8   .���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $ Border��#��z��$ 2��$��1q�._��� $��2q���. �� "  ��.���  � ��$ 30����  � ��
 ������� "  ��.���  � ��$ 30����  � ��
 ������� "  ��.���  � ��$ 30����  � ��$	 0.4q���  � �� �#242a2e��  � �� �#3d444b��  � ��
 �������  �@��TextWrapping$
 Wrap=���  � ��$ Center=���  � ��$ 5 0 0 0q���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop . ��  � �� �� " ��.R��� $��Border��$ 2��$! 2q�$��1q����� $" #3d444b������ $" #3d444b��#   CommonStates��.$   
Normal��  Disabled��  ReadOnly��  	MouseOver��._��� $% PART_ContentHost��$��0q��� "  G�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$ Verdana)���  �@G�PasswordChar$ *����  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop . ��  � �� �� " G�.R��� $��Border��$ 2��$! 2q�$��1q����� $" #3d444b������ $" #3d444b��#   CommonStates��.$   
Normal��  Disabled��  	MouseOver��._��� $% PART_ContentHost��$��0q��� "  ��8   .���  � �� �� " ��.R��� $��Border��$ 2��$! 2q�$��1q����� $" #282e33������ $" #282e33��#   CommonStates��.$   
Normal��  Disabled��  ReadOnly��  	MouseOver��._��� $% PART_ContentHost��$��0q��� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��$	 120����  � ��$ 95����  � �� �� " ��.R��� $��Border��$��1q�$ 2������ $" #282e33������ $" #242a2e��._��� $��0q�|�.  .���� $��0q�& . 	 P� 
 � {� .  . ��� $ Border�� � �� �� $" #18191d���� $ Border�� � �� �� $" #18191d��P� 
 �@��
IsGrouping . . ���  � �� .  ��	#FFE8EDF9����	#FFC5CBF9����	#FF7381F9����	#FFE8EDF9����	#FFC5CBF9����	#FF888888����	#FFC5CBF9����	#FFDDDDDD����	White����	#FF7381F9����	#FF211AA9����	#FF3843C4����	#FF211AA9����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124����	#FFCCCCCC����	#FF888888����	#FF444444����	#FF888888����	#FF444444����	#FFAAAAAA����	#FF888888����	Black����	#FFC5CBF9����	Black����	#FFC5CBF9����	#FF3843C4���� $' 0.5,1&�$( 0.5,0&�.��� #) �  $* 0Q�� #) �  $	* 0.5Q�� #) �  $* 1Q��� $	( 0,0&�$	' 1,0&���� � $) 	#000000FF��$* 0Q�� $) 	#600000FF��$	* 0.4Q�� $) 	#600000FF��$	* 0.6Q�� $) 	#000000FF��$* 1Q��� " d�.R��� .+�  $+ Tasks=�$��40��$��40������ "    P� 
 �@J�IsMouseOver . . ���  �@ 
Foreground
 �����P� 
 �@J�IsMouseOver .  . ���  �@ 
Foreground �#808080�� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$ 30����  � �� �� " ��.R��� $ Border��$! 2q�, . 
������ #   CommonStates��.$   
Normal��  	MouseOver��.- �� .���� $. Border��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $/ 0��$0 #313b43��._��� �� "  ��.���  � ��$ Right=���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��8   ��  � ��$ 40����  � ��$ 40����  � �� �� " ��.R�� #   CommonStates��.$   
Normal��  	MouseOver��  Disabled��.- �� .���� $. PART_EditableTextBox��$6 0(TextElement.Foreground).(SolidColorBrush.Color)�.g�  $/ 0��80    
EditStates��.$   Editable��.- �� .��T� $ (UIElement.Visibility)�$. PART_EditableTextBox��..�i� $1 0��#3 Z2 T� $ (UIElement.Visibility)�$. ContentSite��..�i� $1 0��#3 Z4   
Uneditable��.+�d� $5 ToggleButton��8�� |�.  $6 Press=�7 �� *IsDropDownOpen+$8 TwoWay=�9 �� *TemplatedParent+� $: Popup��$
��Left=�#��z; < . |�.  $��Slide=�#> z= #@ z? .��� $ DropDown��, . #��z? #��zA .+��� $ DropDownBorder��$	��0.6q����� $" #18191d������ $" #282e33���� $��	0,10,0,10q�, . .���� & . $B 	Contained=�	 P� 
 �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P� 
 �@��
IsGrouping . . ���  � �� .  P� $C Popup��
 �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��CornerRadius$ 4���� $ DropDownBorder�� � ��$ 0,2,0,0q��� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " n�.R��� #��z��#��z��$ 6��$��5q��� " ��.R�� .+��� $��3��$ 2��
������ ]� $D 
PART_Track����. E n� 8��  $��1,0,1,0q����� $" Transparent������ $" #5e5e5f���� " ��.R�� .+��� $��3��$ 2��
������ ]� $D 
PART_Track����.  E n� 8��  $��0,1,0,1q����� $" Transparent������ $" #5e5e5f���� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P� 
 �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P� 
 �@��Orientation$ Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��8  �� "  ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� F �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $��1��$��1��#��z���� $��PART_VerticalScrollBar��$��0��$��1��#��z��#��z��#H zG #J zI #L zK #y�zM �� $��PART_HorizontalScrollBar��$N 
Horizontal=�$��1��$��0��#��z��#��z��#H zO #J zP #L zQ #y�zR �� "  ��8   .���  � �� �� " ��.R��� $��Border��$��1q�$ 2������ $" #18191d������ $" #242a2e��._��� $��0q�|�.  8�� .���� $��0q�& . �� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $ Border��$! 7q�, . $	��0.1q����� $" Transparent������ $" #18191d��._��� 	 ]� S �� T �@��
IsSelectedU . �� T �@J�IsMouseOverU .  /��� $ Border�� � �� �#a33c32]� S �� T �@��
IsSelectedU . �� T �@J�IsMouseOverU . /��� $ Border�� � �� �#a33c32]� S �� T �@��
IsSelectedU .  �� T �@J�IsMouseOverU . /��� $ Border�� � �� �#353c43
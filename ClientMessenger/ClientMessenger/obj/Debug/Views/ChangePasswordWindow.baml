   M S B A M L   `   `   `  ����J  FClientMessenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ClientMessenger.ViewsClientMessenger.Views  5   	   G(clr-namespace:ClientMessenger.ViewModelsClientMessenger.ViewModels  5	   	   G(clr-namespace:ClientMessenger.ConvertersClientMessenger.Converters  5
   	   0    *ClientMessenger.Views.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ClientMessenger.Views     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   9
viewModels(clr-namespace:ClientMessenger.ViewModels  5	   	   9
converters(clr-namespace:ClientMessenger.Converters  5
   	     � Title$  ChangePasswordWindow��5   	   $	��400��5   	   $	��300��6    � WindowStartupLocation$ CenterScreen=�6"      CustomWindow#��[  5   	    � DataContext 5      8   2ClientMessenger.ViewModels.ChangePasswordViewModel  5   
    � 	Resources 5      2   ,ClientMessenger.Converters.PasswordConverter  PasswordConverter��%   &	         5      6#   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        #  System.Windows.SystemCommands   CloseWindowCommand   Command# Z 5      6w     5   
   -   5           MaximizeWindowCommand# Z 5      6z     5   
   -   5           MinimizeWindowCommand# Z 5      6z     5   
   -   5        	   RestoreWindowCommand# Z	 5      6y   .��5      � $��Stretch=�6   $��Center=�6)   
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   .+�5&   
   �� $��0��6   $��Старый пароль��6     CustomLabel#��[ 65   G� 5'   
   -   5         G�Name$ OldPassword��5'      $��0 0 0 10q�6+   $��1��6=     CustomPasswordBox#��[ 6J   �� 5(   
   $��2��6   $��Новый пароль��6   #��[ 64   G� 5)   
   -   5        $ NewPassword��5)      $��0 0 0 10q�6+   $��3��6=   #��[ 6J   �� 5*   
   $��4��6   $,��&Повтор нового пароля��6   #��[ 6<   G� 5+   
   -   5        $ RepeatNewPassword��5+      $��0 0 0 10q�61   $��5��6C   #��[ 6P   �� 5,   
   $��6��6   $��Изменить��6     CustomButton#��[ 6T   ��61   �� *ChangePassword+��5-      `� 5.       `� 	Converter# [ 6   .1�5/      ��  �� ElementName$ OldPassword��6   6:   �� 50      $ NewPassword��6   6:   �� 51      $ RepeatNewPassword��6   6@   52      54      55      56      
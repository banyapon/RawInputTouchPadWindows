   M S B A M L   `   `   `  ����e  aMicrosoft.Web.WebView2.Wpf, Version=1.0.2210.55, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf  5   	    RawInput.Touchpad"   RawInput.Touchpad.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      \wv2Lclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf     5   	   -   5          �Name$  
WindowRoot��5       � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��6   
������  � Title$# RawInput Touchpad for Windows��5       � WindowState$ 	Maximized=�5   	   $
��1000��5	   	   $	��800��6    � 
ResizeMode$ 	CanResize=�5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��Center=�6!   $
��10,0q�5      �� 5      $ �Precision touchpad exists:��6	    �� 5      $
��Bold"�6	   �5      ��  �� ElementName$ 
WindowRoot�� �� Path$ TouchpadExists�5      �� 5   
   -   5        $��0��5      $��Right=�5      $��0q�5      $	��8,4q�6   $��Copy to clipboard��5      5      ]� 5   
   .+�5       �� -   5        	 ��Name$	 touchpadCanvas��5       $��1��6-   
������ 6:   5!      �� 5#   
   $��1��6   $��10,10,10,10q�5$      $
��Wrap=�5%      
 J� RenderTransformOrigin$
 0.5,0.5&�5&   G   ��6   �� $ 
WindowRoot��$ TouchpadContacts�z�5'      V� 5(      .~�5)      ��  �� ScaleY$ 1Q�6%   �� 5*      �� 5+      T� 5,      5-      5/      50      51      
   M S B A M L   `   `   `  ����  	topologia'    !br.corp.bonus630.topologia.Docker   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ���	#FFF4F4F4$	��225��5   7   .��5      � $��0,0,0,0q�6   .+�5   
   �� $
��Left=�6   $��0,0,0,2q�61   $	��Top=�6B   $	��225��6Z     �� Orientation$  Vertical=�6i   .+�5      �� -   5        $��lba_caveName��5   '   $��Sem Nome��6   $
��Left=�6;   $	��Top=�6V   $��14(�6n   $
��Bold"�6|   �� 5      � 5      $��28��6   $��0,4,0,4q�6   .+�5      �� -   5         ��Name$ btn_Draw��5   V   $��Planta Baixa��6   $
��Left=�60   $��99��6K   {�.  $��	145,0,0,0q�6�   $��24��6�   �� 5      $��Orientar��6   $��6,-6,0,6q�6+   $��52��6=   $��10(�6H   $
��Left=�6V   �� 5      $��	pela Base��6   $��6,5,0,0q�6,   $��52��6=   $��10(�6H   $
��Left=�6V   �� 5      -   5        $��
cb_refbase��5   -   $��63,0,0,0q�6   $ �� IsSynchronizedWithCurrentItem$ FalseY�6?    �� IsDropDownOpen .  {�.  $
��Left=�6�   $��77��6�   $��24��6�   5      � 5      $��30��6   $��0,0,0,4q�6   .+�5      �� -   5        $ btn_ShowEditor��5   �   $��Editor de Dados��6   $
��Left=�63   $��24��6N   $��Center=�6Z   $��91��6u   {�.  $��4,3,0,3q�6�   �� 5      $��	Escala 1:��6   $��98,2,0,2q�6,   $��Center=�6>   $
��Left=�6Y   $��54��6t   �� 5       -   5        $��	txt_scale��5    W    �� TextWrapping$
 Wrap=�6   $	��100��6.   $��35��69   $��	151,6,0,6q�6D   $��Center=�6h   $
��Left=�6�   �� 5!      -   5        $��btn_drawScale��5!   �   $��Escala��6   $
��Left=�6*   $��	191,3,0,3q�6E   $��Center=�6X   $��53��6s   $��24��6~   6�   5"      � 5$      $��30��6   .+�5%      �� -   5         ��Name$ cb_firstbase_check��5%      $��4,1,0,1q�67   {�.  $��61��6Z   $
��Left=�6�   $��24��6�   �� 5&      -   5        $ cb_lastbase_check��5&      $��70,1,0,1q�66   {�.  $��66��6Z   $
��Left=�6�   $��24��6�   �� 5'      $��Tamanho:��6   $��	133,0,0,0q�6+   $
��Left=�6>   $��61��6Y   6f   �� 5(      -	   5        $��lba_totalsize��5(   &   $	��0 m��6   $��	185,0,0,0q�6;   $
��Left=�6N   $��58��6i   6v   5)      � 5*      $��30��6   .+�5+      �� $��Orientar��6   $��6,-6,0,6q�6+   $��57��6=   $��10(�6H   $
��Left=�6V   �� 5,      $��	pela Base��6   $��6,5,0,0q�6,   $��57��6=   $��10(�6H   $
��Left=�6V   �� 5-      -
   5        $ cb_long_base��5-      $��
63,0,107,0q�61   $��24��6E   �� 5.      -   5        $��btn_drawlong��5.   7   $��Perfil Longitudinal��6   $��	142,0,0,0q�6K   $��24��6z   5/      � 50      $��30��6   .+�52      �� $��Área:��6   $��2,0,0,0q�6(   $
��Left=�69   $	��223��6T   6b   �� 53      -   5        $��lba_area��53   &   $	��0 m��6   $��85,0,0,0q�66   $
��Left=�6H   $��95��6c   6q   �� 54      -   5        $��btn_area��54   ,   $��Calcular��6   $��	185,4,0,2q�6<   $��24��6O   6[   56      � 57      $��30��6   .+�58      �� $��Volume:��6   $��2,0,0,0q�6*   $
��Left=�6;   $	��223��6V   6d   �� 59      -   5        $��lba_vol��59   &   $	��0 m��6   $��85,0,0,0q�65   $
��Left=�6G   $��95��6b   6o   �� 5;      -   5        $��btn_vol��5;   ,   $��Calcular��6   $��	185,4,0,2q�6;   $��24��6N   6Z   5<      � 5=      $��30��6   .+�5>      �� $��Continuidade:��6   $��2,0,0,0q�60   $
��Left=�6A   $	��223��6\   6j   �� 5?      -   5        $��lba_cont��5?   &   $	��0 m��6   $��85,0,0,0q�66   $
��Left=�6H   $��95��6c   6p   �� 5A      -   5        $��btn_cont��5A   ,   $��Calcular��6   $��	185,4,0,2q�6<   $��24��6O   6[   5B      9� 5C      $��Camadas��6   $
��Left=�6(   $��0,16,0,0q�6C   $	��Top=�6U   $	��225��6m    9� 
IsExpanded . .��5D      �  � RowDefinitions 5E      �� 5F      $
��auto �6(   �� 5G      $��20 �6(   .+�5I      �� $	��320��6#   $	��225��63   $��0��6B   .��5J      �� -   5        $��
box_layers��5J   �   $
��Left=�6%   $��0,16,0,0q�6A   $	��Top=�6S   $	��225��6k   $  Vertical=�6z   5L      5M      � 5N      $��18��6   $��0,2,0,0q�6'   $��1��68   .+�5O      �� -   5        $��btn_new_layer��5O   �   $��18��6!   $��18��6-   $��	0,0,112,0q�69   $	��Top=�6L   $��Right=�6d    � ToolTip$ Criar Nova Camada��5R   -   5S      �� 5T      -   5        $��btn_delete_layer��5T      $��18��6!   $��18��6-   $��0,0,80,0q�69   $	��Top=�6K   $��Right=�6c   $ Deletar Camada��5W   -   5X      5Y      5Z      5[      9� 5]      $��Desenhar Perfils��6   $
��Left=�61   $��0,16,0,0q�6L   $	��Top=�6^   $	��225��6v   .��5^      �� $	��320��6   $	��225��6/   .��5_      �� -   5        $��box_perfils��5_   �   $
��Left=�6!   $��0,16,0,0q�6=   $	��Top=�6O   $	��225��6g   $  Vertical=�6v   5`      5a      5b      9� 5c      $��Informações da Base��6   $
��Left=�64   $��0,16,0,0q�6O   $	��Top=�6a   $	��225��6y   .��5d      �� $	��320��6   $	��225��6/   .��5e      �� $
��Left=�6!   $��0,16,0,0q�6=   $	��Top=�6O   $	��225��6g   $  Vertical=�6v   .+�5f      � .+�5g      �� $��Nome da Base:��6$   �� 5h      -   5        $��lba_baseName��5h   $   $��	114,0,0,0q�68   5i      � 5k      .+�5l      �� $��Azymuti:��6$   �� 5m      -   5        $��lba_azymuti��5m   $   $��	114,0,0,0q�67   5n      � 5o      .+�5p      �� $��Inclinação:��6$   �� 5q      -   5        $��lba_inclination��5q   $   $��	114,0,0,0q�6;   5r      5t      5u      5v      � 5w      -   5        $��grid_socialMedia��5w      $	��225��6+   $��72��6:   .+�5x      �� $��Visite e compartilhe:��6   $��25��68   $	��Top=�6D   �� 5y      -   5        $��facebook��5y   Q   $��30��6   $��150,30,0,12q�6<   	 � Cursor$
	 Hand��6a   $
��Left=�6o   $��30��6�   $ Facebook��5|   %   5}      �� 5~      -   5        $��youtube��5~   Q   $��30��6   $��114,30,0,12q�6<   $
	 Hand��6`   $
��Left=�6n   $��30��6�   $ Youtube��5�   %   5�      �� 5�      -   5        $��twitter��5�   P   $��30��6   $��
78,30,0,12q�6<   $
	 Hand��6_   $
��Left=�6m   $��30��6�   $ Twitter��5�   %   5�      �� 5�      -   5        $��linkedin��5�   P   $��30��6   $��
42,30,0,12q�6<   $
	 Hand��6`   $
��Left=�6n   $��30��6�   $ Linkedin��5�   %   5�      �� 5�      -   5        $��
googleplus��5�   O   $��30��6   $��	6,30,0,12q�6<   $
	 Hand��6a   $
��Left=�6o   $��30��6�   $ Google+��5�   %   5�      �� 5�      -   5        $��bonus630��5�   Q   $��30��6   $��186,30,0,12q�6<   $
	 Hand��6a   $
��Left=�6o   $��30��6�   $ Bonus630.tk��5�   %   5�      5�      5�      5�      5�      
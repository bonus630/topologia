   M S B A M L   `   `   `  ����D  @topologia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:br.corp.bonus630.topologiabr.corp.bonus630.topologia  5   	   %    br.corp.bonus630.topologia.Data   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   :local(clr-namespace:br.corp.bonus630.topologia     5   	     � Title$
  Data��5      $	��480��6   $	��900��6    � 
ResizeMode$ NoResize=�6+    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$	 New��5
      6'     5   
   -   5        $
 Save��5      6(     5   
   -   5        $
 Open��5      6(     5   
   -   5        $ SaveAs��5      6*     5   
   -   5        .   (br.corp.bonus630.topologia.ConfigCommand   
ConfigOpen# Z 5      6v     5   
   -   5        $ Close��5      6)    J� InputBindings 5      �� 5   
   
 �� Key$ S��6    �� 	Modifiers$ Controlf�6    �� Command$
 Save��61   �� 5   
   $ O��6   $ Controlf�6   $
 Open��61   �� 5   
   $ N��6   $ Controlf�6   $	 New��61   �� 5   
   $ S��6   $ Control+Shiftf�6   $ SaveAs��67   �� 5   
   $ U��6   $ Controlf�6   # Z 61   �� 5   
   $ W��6   $ Controlf�6   $ Close��61   .��5      � -   5        	 �Name$		 box��5      $��0,0,0,-2q�6   .+�5"   
   o� $��24��6   $	��Top=�6   ��5#      �� 5$      
 �� EndPoint$	
 0,1&�6&    �� 
StartPoint$	 0,0&�65   .��5%      �  � Color$ 	#FFF6F6F6��6#    � Offset$
 0.25Q�65   � 5&      $ 	#FFEAE8E8��6#   $ 0.101Q�65   � 5'      $ 	#FFDCD9D9��6#   $ 0.931Q�65   � 5(      $ White��6#   $ 1Q�61   5)      .@�5+      m� $��Caverna��6   $��24��6(   $��83��64   .@�5,      m� -   5         m�Name$ btn_novo��5,      $
��Novo��6-    m� Command$ ApplicationCommands.New��6;    m� InputGestureText$ Ctrl+N��6^   m� 5-      -	   5        $ btn_load��5-      $��Carregar��6-   $ ApplicationCommands.Open��6?   $ Ctrl+O��6b   m� 5.      -
   5        $ btn_save��5.      $��Salvar��6-   $ ApplicationCommands.Save��6=   $ Ctrl+S��6a   50      m� 51      -   5        $ 
btn_saveas��51      $��Salvar Como��6/   $  ApplicationCommands.SaveAs��6D   $ Ctrl+Shift+S��6j   53      m� 54      $��Bases��6   $��83��6&   $��24��61   .@�55      m� -   5        $ menuItem_add��55      $��	Adicionar��61   6D   m� 56      -   5        $ menuItem_edit��56      $��Editar��62   6B   m� 57      -   5        $ menuItem_remove��57      $��Remover��64   6E   m� 58      -   5        $ menuItem_clear��58      $��Limpar��63   6C   59      m� 5:      $��Opções��6   $��83��6'   $��24��62   .@�5;      m� -   5        $ menuItem_config��5;      $��Configurações��64   # Z 6K   $ Ctrl+U��6�   m� 5<      -   5        $ menuItem_close��5<      $��Fechar��63   $ ApplicationCommands.Close��6C   $ Ctrl+W��6h   5>      5?      �� 5A   
   $
��Left=�6   $��0,24,0,0q�60   $	��428��6C   $	��Top=�6P   $	��894��6h   .+�5B      �� $��Dados da Caverna��6   ���	#00000000���	#FF780404$��14(�6]   �� 5D       �� Orientation$ 
Horizontal=�6   $��40��62   .+�5E      �� $��Nome:��6   $��Center=�6(   �� 5F      -   5        $��txt_caveName��5F      $��26��6.   $�� ��6:   $��Center=�6B   $	��0,7q�6]   $	��270��6j   �� 5G      $��Data:��6   $��Center=�6(   (  "System.Windows.Controls.DatePicker  5H      -   5        $��datepicker_dateTopo��5H      $��26��68   $��Center=�6D   6_   5I      �� 5J      6   �� 5K      $��Dados das Bases��6   ���	#00000000���	#FF780404$��14(�6\   �� 5L      $ 
Horizontal=�6   $��40��62   .+�5N      �� -   5         ��Name$ 
cb_entrace��5N   c   $��Entrada��6   $
��Left=�6-   $��Center=�6H    d� 	IsChecked$
 TrueY�6w   $��16��6�   $��66��6�   6�   � 5O      -   5        $��baseAnt��5O      $y�	Collapsed=�6&   $	��140��6=   $��26��6I   .+�5P      �� $��Da Base��6   $
��Left=�6.   $��Center=�6I   $��	0,0,-21,0q�6d   �� 5Q      -   5         ��Name$ 
cb_baseAnt��5Q   `   $
��Left=�6   $��Center=�6:   $��58��6U   $��
79,0,-79,0q�6t   5R      �� 5S      $��	Para Base��6   $
��Left=�6,   $��Center=�6G   $��26��6b   $��64��6n   $	��0,7q�6y   �� 5T      -   5         ��Name$ txt_baseName��5T   {   $
��Left=�6   $��22��65    �� TextWrapping$
 Wrap=�6A   $��Center=�6U   $��61��6p   6�   5V      �� 5W      -   5        $��stackPanel_teste��5W   2   $ 
Horizontal=�6   $��40��6J   5Y      � 5Z      -   5        $��box_comments��5Z      $y�	Collapsed=�6'   $��80��6>   .+�5[      �� $��Comentários:��6   �� 5\      -   5        $��txt_comments��5\      $	��Top=�6/    �� 	IsEnabled . $��80��6c   $��	80,0,20,0q�6p   5]      � 5^      $��31��6   $��	17,0,10,0q�6   .+�5_      �� -   5         ��Name$ btn_add��5_      $��	Adicionar��6*   $
��Left=�6>   $��75��6Y   $��12,4,0,5q�6d   {�.  6�   �� 5`      -   5        $ btn_edit��5`      $��Editar��6+   $
��Left=�6<   $��75��6W   $��	494,4,0,5q�6b   {�.  6�   �� 5a      -   5        $ 
btn_remove��5a      $��Remover��6-   $
��Left=�6?   $��75��6Z   $��	574,4,0,5q�6e   {�.  6�   �� 5b      -   5        $ 	btn_clear��5b      $��Limpar��6,   $
��Left=�6=   $��75��6X   $��92,4,0,5q�6c   6u   �� 5c      -   5         ��Name$ lba_baseTopoName��5c      $�� ��62   $
��Left=�6=   $��	654,2,0,0q�6X   $	��Top=�6k   5e      �� 5f      $	��194��6   $��0,0,10,0q�6+   .��5g      &   System.Windows.Controls.DataGrid  -    5          Name$ datagrid_base��5g   p      AutoGenerateColumns .  $	��Top=�6X      
IsReadOnly . $	��865��6�   $
��Left=�6  ��6   �� *
ListBase+   Columns 5h      4  .System.Windows.Controls.DataGridCheckBoxColumn  5i      ,  &System.Windows.Controls.DataGridColumn   Header$ Entrada��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *	IsStart+  �� Mode$  OneWay=�6m   0  *System.Windows.Controls.DataGridTextColumn  5j      $ Da Base��6-    6>   �� *RefBaseName+$  OneWay=�6n     5k      $ 	Para Base��6-    6@   �� *
BaseName+$  OneWay=�6l     5l      $
 Cima��6-    6;   �� *	TopSide+$  OneWay=�6f     5m      $ Baixo��6-    6<   �� *
BottomSide+$  OneWay=�6j     5n      $ Esquerda��6-    6?   �� *
LeftSide+$  OneWay=�6k     5o      $ Direita��6-    6>   �� *	RightSide+$  OneWay=�6k     5p      $ Azymuti��6-    6>   �� *	Azymuti+$  OneWay=�6i     5q      $ Inclinação��6-    6A   �� *Incrination+$  OneWay=�6p     5r      $ 
Distância��6-    6@   �� *
Distance+$  OneWay=�6l     5s      $ Comentários��6-    6B   �� *	Coments+$  OneWay=�6m   5u      5v      5w      5y      5z      
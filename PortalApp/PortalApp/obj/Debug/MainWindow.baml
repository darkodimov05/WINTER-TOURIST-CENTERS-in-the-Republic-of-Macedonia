   M S B A M L   `   `   `  ����  	PortalApp    PortalApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Z VEsri.ArcGISRuntime, Version=10.2.7.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad866esri+http://schemas.esri.com/arcgis/runtime/2013  5   	     � Title$  
MainWindow��5   	   $	��650��6   $	��950��6)   .��5	      � .+�5
   
   )  #Esri.ArcGISRuntime.Controls.MapView  -   5          Name$ 	MyMapView��5
      �� 5   
   
�������$	��300��5   	   $��Right=�5   	   $��10q�5   	   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��5q�5      .+�5      �� $��Search:��6   �� 5      -   5         ��Name$ SearchTextBox��5      $	��100��5      $
��10,0q�5      $
��Left=�5      $�� ��5      |�. 6   �� 5      -   5         ��Name$ ItemTypeComboBox��5       �� SelectedValuePath$ Content��5      $	��120��5      .@�5      �� $��Web map��6#    �� 
IsSelected . 65   �� 5      $��Feature service��6#   �� 5      $��Basemap��6#   5      5      �� 5       -   5         ��Name$ SearchButton��5       $��Search ArcGIS.com��5!      $��20q�5"       �� 	IsDefault . 5#   ,   �� 5$      -   5        	 ��Name$	 ResultListBox��5$      $
��20,0q�5%      $	��200��5&      $	��200��5'      ��5*      �� 5+      .R�5,      �� $ Vertical=�6%   .+�5-      �� ��6(   �� *Title+�� 5.      $��9(�6?   ��6(   �� *Owner+6?   5/      50      53      �� 54      $ 
Horizontal=�6   $��Center=�62   $��0,15,0,0q�6O   .+�55      �� -   5        $ PrevPageButton��55      $��Previous Page��61   $
��10,0q�6f   6v   �� 56      -   5        
 ��Name$
 CurrentPageNumber��56      $��Page 1��67   $��Center=�6E   $
��10,0q�6b   6r   �� 57      -   5        $ NextPageButton��57      $��	Next Page��61   $
��10,0q�6b   6r   58      �� 59      -	   5        $
 SnippetTextBlock��59      $�� ��62   $��50��6:   $��10q�6F   $
��Wrap=�6R   �� 5:      -
   5         ��Name$ ThumbnailImage��5:      $��UniformToFill=�6,   $	��200��6D   $	��133��6P   �� 5;      -   5        $ ShowMapButton��5;      $��Show Map��5<   	   $��5q�5=   	   $	��150��5>   	   {�.  5@   
   5B      5D      5E      
< ? p h p  
 s e s s i o n _ s t a r t ( ) ;  
  
 	 	 	  
 	 	  
 	 	 	 $ a n w e n d e r _ n a m e = $ _ S E S S I O N [ ' a n w e n d e r _ n a m e ' ] ;  
 	 	 	 $ i d _ a n w e n d e r = $ _ S E S S I O N [ ' i d _ a n w e n d e r ' ] ;  
 	 	 	 $ t e s t   =   $ _ G E T [ ' l i k e ' ] ;  
 	 	 	  
 	 	 	 	 / / E i n e   D B - V e r b i n d u n g   w i r d   h e r g e s t e l l t  
 	 	 	 	 $ m y s q l i   =   n e w   m y s q l i ( $ h o s t ,   $ u s e r ,   $ p w d ,   $ d b ) ;  
  
 	 	 	 	 i f   ( $ m y s q l i - > c o n n e c t _ e r r n o )  
 	 	 	 	 {  
 	 	 	 	 	 e c h o   " A n m e l d u n g   f e h l g e s c h l a g e n :   " .   $ m y s q l i - > c o n n e c t _ e r r n o ;  
 	 	 	 	 	 e x i t ( 0 ) ;  
 	 	 	 	 }  
 	 	 	 	 / / D e r   Z e i c h e n s a t z   z u r   V e r s t a e n d i g u n g   m i t   d e r   D B   w i r d   f e s t g e l e g t  
 	 	 	 	 $ m y s q l i - > s e t _ c h a r s e t ( " u t f 8 " ) ;  
 	 	 	 	  
 	 	 	 	 $ s e l e c t _ a n w e i s u n g   =   " S E L E C T   C O U N T ( * )   A S   a n z a h l  
 	 	 	 	 	 	 	 	 	   F R O M   t b l _ a n w e n d e r _ r e z e p t e _ l i k e  
 	 	 	 	 	 	 	 	 	   W H E R E   i d _ a n w e n d e r   =   $ i d _ a n w e n d e r  
 	 	 	 	 	 	 	 	 	   A N D       r e z e p t e _ i d     =   $ t e s t " ;    
 	 	 	 	 	 	 	 	 	    
 	 	 	 	 	 	 	 	 	   $ e r g e b n i s m e n g e   =   $ m y s q l i - > q u e r y ( $ s e l e c t _ a n w e i s u n g ) ;  
 	 	 	 	 	 	 	 	 	   $ d a t e n s a t z   =   $ e r g e b n i s m e n g e - > f e t c h _ a s s o c ( ) ;  
 	 	 	 	 	 	 	 	 	   i f ( $ d a t e n s a t z [ ' a n z a h l ' ]   = =   0 )  
 	 	 	 	 	 	 	 	 	   {  
 	 	 	 	 	 	 	 	 	 	   $ i n s e r t _ a n w e i s u n g   =   " I N S E R T   I N T O   t b l _ a n w e n d e r _ r e z e p t e _ l i k e  
 	 	 	 	 	 	 	 	 	 ( r e z e p t e _ i d ,   i d _ a n w e n d e r ,   d a t u m )  
 	 	 	 	 	 	 	 	 	 V A L U E S   (   ' " . $ t e s t . " ' ,   ' " . $ i d _ a n w e n d e r . " ' ,   C U R D A T E ( )   )   " ;  
  
 	 	 	 	 	 	 	 	 	 / / D i e   S E L E C T - A n w e i s u n g   h o l t   s i c h   v o n   d e r   D B   d i e   I n f o r m a t i o n e n   i n   d i e   E r g e b n i s m e n g e  
 	 	 	 	 	 	 	 	 	 $ m y s q l i - > q u e r y ( $ i n s e r t _ a n w e i s u n g ) ;  
 	 	 	 	 	 	 	 	 	   }  
 	 	 	 	 	 	 	 	 	    
 	 	 	 	  
 	 	 	 	  
 	 	 	 	  
 	 	 	 	 h e a d e r   ( ' l o c a t i o n :   r e z e p t e _ a u s w a h l _ a n f r a g e . p h p ' ) ;  
 	 	 	 	  
 	 ? > 
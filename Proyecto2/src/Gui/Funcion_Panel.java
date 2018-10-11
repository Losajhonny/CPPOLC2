/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui;

import Gui.Texto.JTextLineNumber;
import Gui.Texto.TextArea;
import java.awt.BorderLayout;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.ScrollPaneConstants;
import javax.swing.event.CaretListener;

/**
 *
 * @author Jhonatan López
 */
public class Funcion_Panel {
    
    /**
     * Retorna un panel con un area de texto y al area de texto
     * se le coloca su direccion donde fue creado, ademas se le 
     * asigna un CaretListener
     * @param path
     * @param cl
     * @return 
     */
    public static Panel nuevo_Tab_Editor(String path, CaretListener cl){
        JPanel panTxt = new JPanel(new BorderLayout());
        TextArea txt = new TextArea();
        txt.setPathRelativa(path);
        txt.addCaretListener(cl);
        JTextLineNumber tln = new JTextLineNumber(txt);
        txt.setLineWrap(true);
        
        panTxt.add(tln, BorderLayout.WEST);
        panTxt.add(txt, BorderLayout.CENTER);
        
        JScrollPane jsp = new JScrollPane(panTxt);
        jsp.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED);
        jsp.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);
        
        Panel pan = new Panel(txt);
        pan.add(jsp, BorderLayout.CENTER);
        return pan;
    }

    /**
     * Retorna un panel con un area de texto para la consola
     * donde se imprimira las operaciones
     * @return 
     */
    public static Panel nuevo_Consola(){
        JPanel panTxt = new JPanel(new BorderLayout());
        TextArea txt = new TextArea();
        txt.setLineWrap(true);
        
        panTxt.add(txt, BorderLayout.CENTER);
        
        JScrollPane jsp = new JScrollPane(panTxt);
        jsp.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED);
        jsp.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);
        
        Panel pan = new Panel(txt);
        pan.add(jsp, BorderLayout.CENTER);
        return pan;
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui.Texto;

import javax.swing.JTextArea;
import javax.swing.event.CaretEvent;
import javax.swing.text.BadLocationException;

/**
 *
 * @author Jhonatan López
 */
public class TextArea extends JTextArea {
    private String pathRelativa;
    
    public TextArea(){
        super();
    }
    
    /**
     * Retorna una posicion actualizada de la fila y columna de TextArea
     * @param e
     * @param area
     * @return 
     */
    public static String actualizarCaret(CaretEvent e, TextArea area){
        TextArea tarea = area != null ? area : (TextArea) e.getSource();
        int linea = 1, col = 1;
        try{
            int posicion = tarea.getCaretPosition();
            linea = tarea.getLineOfOffset(posicion);
            col = posicion - tarea.getLineStartOffset(linea);
            linea += 1;
        }catch(BadLocationException ex){ }
        return ("Linea: " + linea + " Columna: " + col);
    }

    /**
     * @return the pathRelativa
     */
    public String getPathRelativa() {
        return pathRelativa;
    }

    /**
     * @param pathRelativa the pathRelativa to set
     */
    public void setPathRelativa(String pathRelativa) {
        this.pathRelativa = pathRelativa;
    }
}

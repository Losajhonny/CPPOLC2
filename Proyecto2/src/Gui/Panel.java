/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui;

import Gui.Texto.TextArea;
import java.awt.BorderLayout;
import javax.swing.JPanel;
import javax.swing.JTable;

/**
 * Esta clase guarda informacion de un TextArea, JTable
 * para su facil acceso
 * @author Jhonatan López
 */
public class Panel extends JPanel{
    private TextArea texto;
    private JTable tabla;
    
    Panel(TextArea texto){
        super(new BorderLayout());
        this.texto = texto;
    }
    
    Panel(JTable tabla){
        super(new BorderLayout());
        this.tabla = tabla;
    }

    /**
     * @return the texto
     */
    public TextArea getTexto() {
        return texto;
    }

    /**
     * @param texto the texto to set
     */
    public void setTexto(TextArea texto) {
        this.texto = texto;
    }

    /**
     * @return the tabla
     */
    public JTable getTabla() {
        return tabla;
    }

    /**
     * @param tabla the tabla to set
     */
    public void setTabla(JTable tabla) {
        this.tabla = tabla;
    }
}

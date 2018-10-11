/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui.TabPane;

import Gui.Funcion_Panel;
import Gui.Panel;
import javax.swing.Icon;
import javax.swing.JTabbedPane;
import javax.swing.event.CaretListener;

/**
 *
 * @author Jhonatan López
 */
public class Funcion_TabPane {
    
    /**
     * Realiza la accion de seleccionar un Tab con su nombre en un
     * JTabbedPane determinado
     * @param jtp
     * @param nombre 
     */
    public static void setSelectTab(JTabbedPane jtp, String nombre){
        for(int i = 0; i < jtp.getTabCount(); i++){
            if(jtp.getTitleAt(i).equals(nombre)){
                jtp.setSelectedIndex(i);
                break;
            }
        }
    }

    public static Panel getSelectTab(JTabbedPane jtp){
        return (Panel)jtp.getSelectedComponent();
    }
    
    /**
     * Crea un Tab para compilar en @jtp, se le asigna un nombre, una direccion
     * y un caretListener al tab
     * @param jtp
     * @param img
     * @param nombre
     * @param path
     * @param cl 
     */
    public static void crearTabCompiler(JTabbedPane jtp, Icon img, String nombre, String path, CaretListener cl){
        if(nombre == null){
            String n = "Nuevo Tab" + (jtp.getTabCount() + 1);
            jtp.addTab(n, img, Funcion_Panel.nuevo_Tab_Editor(path, cl), n);
        }else{
            jtp.addTab(nombre, img, Funcion_Panel.nuevo_Tab_Editor(path, cl), nombre);
        }
        //Seleccionar el Tab creado
        setSelectTab(jtp, nombre);
    }
}

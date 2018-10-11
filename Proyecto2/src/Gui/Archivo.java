/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import javax.swing.ImageIcon;
import javax.swing.JOptionPane;

/**
 *
 * @author Jhonatan López
 */
public class Archivo {
    
    /**
     * Metodo que guarda el texto en su pathRelativa
     * @param txt 
     */
    public static void guardarTexto(TextArea txt){
        File f = new File(txt.getPathRelativa());
        //este archivo siempre existe
        FileWriter fw;
        BufferedWriter bw;
        try{
            fw = new FileWriter(f, false);
            bw = new BufferedWriter(fw);
            bw.write(txt.getText());
            bw.close();
            fw.close();
        }catch(IOException ex){}
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Analizadores;

import static Analizadores.dasm.CompilarDasm.MoverArchivo;

/**
 *
 * @author Jhonatan López
 */
public class CompilarDs {
    
    public static void main(String[] args) {
        analizadorDracoScript();
    }
    
    /**
     * Crea y mueve el analizador DracoScript a su carpeta
     */
    public static void analizadorDracoScript(){
        String archLexico = "dracoLexico.flex";
        String archSintactico = "dracoSintactico.cup";

        String[] alexico = {archLexico};
        String[] asintactico = {"-parser", "DsSintactico", archSintactico};
        
        jflex.Main.main(alexico);
        System.out.println("Genero lexico");
        try {
            java_cup.Main.main(asintactico);
            System.out.println("Genero sintactico");
        } catch (Exception ex) {
            System.out.println("******************** ERROR *************************\nNo se genero el AnalizadorSintactico");
        }

        /*********** Movemos los archivos generados *************/
        boolean mvAL = MoverArchivo("DsLexico.java", "DracoScript");
        boolean mvAS = MoverArchivo("DsSintactico.java", "DracoScript");
        boolean mvSym = MoverArchivo("sym.java", "DracoScript");
        if(mvAL && mvAS && mvSym){
            System.out.println("Se movieron todos los archivos");
        }
    }
}

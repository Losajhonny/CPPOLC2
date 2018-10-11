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
public class CompilarDplus {
    
    public static void main(String[] args) {
        analizadorDplus();
    }
    
    /**
     * Crea y mueve el analizador Dplus a su carpeta
     */
    public static void analizadorDplus(){
        String archLexico = "dplusLexico.flex";
        String archSintactico = "dplusSintactico.cup";

        String[] alexico = {archLexico};
        String[] asintactico = {"-parser", "Sintactico", archSintactico};
        
        jflex.Main.main(alexico);
        System.out.println("Genero lexico");
        try {
            java_cup.Main.main(asintactico);
            System.out.println("Genero sintactico");
        } catch (Exception ex) {
            System.out.println("******************** ERROR *************************\nNo se genero el AnalizadorSintactico");
        }

        /*********** Movemos los archivos generados *************/
        boolean mvAL = MoverArchivo("Lexico.java", "Dplus");
        boolean mvAS = MoverArchivo("Sintactico.java", "Dplus");
        boolean mvSym = MoverArchivo("sym.java", "Dplus");
        if(mvAL && mvAS && mvSym){
            System.out.println("Se movieron todos los archivos");
        }
    }
}

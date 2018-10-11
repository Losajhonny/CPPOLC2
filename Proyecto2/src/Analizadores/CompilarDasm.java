/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Analizadores.dasm;

import java.io.File;
import java.nio.file.Path;
import java.nio.file.Paths;

/**
 *
 * @author Jhonatan López
 */
public class CompilarDasm {
    
    public static void main(String[] args) {
        analizadorDasm();
    }
    
    /**
     * Crea y mueve el analizador Dasm a su carpeta
     */
    public static void analizadorDasm(){
        String archLexico = "dasmLexico.flex";
        String archSintactico = "dasmSintactico.cup";

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
        boolean mvAL = MoverArchivo("Lexico.java", "Dasm");
        boolean mvAS = MoverArchivo("Sintactico.java", "Dasm");
        boolean mvSym = MoverArchivo("sym.java", "Dasm");
        if(mvAL && mvAS && mvSym){
            System.out.println("Se movieron todos los archivos");
        }
    }
    
    /**
     * Mueve los archivos a sus respectivas carpetas
     * @param archNombre
     * @param carpeta
     * @return 
     */
    public static boolean MoverArchivo(String archNombre, String carpeta) {
        boolean efectuado = false;
        File arch = new File(archNombre);
        if (arch.exists()) {
            Path currentRelativePath = Paths.get("");
            String nuevoDir =   currentRelativePath.toAbsolutePath().toString() + File.separator 
                                + "src" + File.separator
                                + "Analizadores" + File.separator
                                + carpeta + File.separator
                                + arch.getName();
            File archViejo = new File(nuevoDir);
            archViejo.delete();
            if (arch.renameTo(new File(nuevoDir))) {
                System.out.println("Se movio " + archNombre);
                efectuado = true;
            } else {
                System.out.println("No se movio " + archNombre);
            }

        } else {
            System.out.println("El archivo no se puede mover: " + archNombre);
        }
        return efectuado;
    }
}

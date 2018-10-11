/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Gui;

import java.io.File;
import javax.swing.JTree;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreePath;

/**
 *
 * @author Jhonatan López
 */
public class Arbol {
    static final String[] extension = {".d", ".ds", ".dasm"};
    
    /**
     * Actualiza el arbol donde se mira los directorios y archivos
     * @param arbol
     */
    public static void nuevo_arbol(JTree arbol){
        //abrir la carpeta root
        File padre = new File("root");
        //carpeta root para ingresarle los hijos
        DefaultMutableTreeNode root = new DefaultMutableTreeNode(padre.getName());
        //creando un modelo para el arbol
        DefaultTreeModel modelo = new DefaultTreeModel(root);        
        //modificando modelo
        arbol.setModel(modelo);
        //creando nodos hijos al padre
        getNodoArbol(padre, root, modelo);
        //expander la carpeta raiz
        arbol.expandRow(0);
        //seleccionar root
        arbol.setSelectionRow(0);
        //modificando si es visible la carpeta root
        //arbol.setRootVisible(false);
        //volver a pintar el arbol
        arbol.repaint();
    }
    
    /**
     * Crea los nodos del arbol, solo muestra los archivos con el vector
     * extension y carpetas
     * @param padre
     * @param npadre
     * @param modelo 
     */
    public static void getNodoArbol(File padre, DefaultMutableTreeNode npadre, DefaultTreeModel modelo){
        File[] hijo = padre.listFiles();
        //buscar archivos hijos
        if(hijo != null){
            int posNodo = 0;
            for(int i = 0; i < hijo.length; i++){
                DefaultMutableTreeNode h = new DefaultMutableTreeNode(hijo[i].getName());
                //modelo.insertNodeInto(h, npadre, i);
                if(hijo[i].isDirectory()){
                    modelo.insertNodeInto(h, npadre, posNodo);
                    posNodo++;
                    //si el archivo es directorio entonces recorrerlo tambien
                    getNodoArbol(hijo[i], h, modelo);
                }else if (hijo[i].getName().endsWith(extension[0]) || 
                        hijo[i].getName().endsWith(extension[1]) ||
                        hijo[i].getName().endsWith(extension[2])){
                    modelo.insertNodeInto(h, npadre, posNodo);
                    posNodo++;
                }
            }
        }
    }
    
    /**
     * Actualiza la ruta relativa del archivo o carpeta que se selecciona
     * @param tp 
     * @return  
     */
    public static String actualizarSeleccionTree(TreePath tp){
        String pathRelativa = "";
        try{
            Object [] nodos = tp.getPath();
            if(nodos.length > 0){
                for (int i = 0; i < nodos.length; i++){
                    Object nodo = nodos[i];
                    if(i != (nodos.length - 1)){
                        pathRelativa += nodo.toString() + "/";
                    }else{
                        pathRelativa += nodo.toString();
                    }
                }
            }else{
                pathRelativa = "root";
            }
        }catch(Exception ex){ }
        return pathRelativa;
    }
}

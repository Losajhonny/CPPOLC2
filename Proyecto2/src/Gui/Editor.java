package Gui;

import Gui.TabPane.Funcion_TabPane;
import Gui.Texto.TextArea;
import java.awt.Color;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.swing.Icon;
import javax.swing.ImageIcon;
import javax.swing.JOptionPane;
import javax.swing.event.CaretEvent;
import javax.swing.event.CaretListener;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import javax.swing.text.BadLocationException;
import javax.swing.text.DefaultHighlighter;
import javax.swing.text.Highlighter;
import javax.swing.tree.TreePath;

/**
 *
 * @author Jhonatan López
 */
public class Editor extends javax.swing.JFrame implements CaretListener {
    private String pathRelativa = "root";
    
    /**
     * Creates new form Editor
     */
    public Editor() {
        initComponents();
        initPersonal();
    }
    
    /**
     * Inicializando las funcionalidades del editor
     */
    private void initPersonal(){
        //Evento para actualizar la linea y columna del editor de texto
        tabpane_editor.addChangeListener(new ChangeListener() {
            @Override
            public void stateChanged(ChangeEvent e) {
                Panel pan = Funcion_TabPane.getSelectTab(tabpane_editor);
                status.setText(TextArea.actualizarCaret(null, pan.getTexto()));
            }
        });
        //Evento cuando se hace doble clic en el arbol
        arbol.addMouseListener(new MouseListener() {
            @Override
            public void mouseClicked(MouseEvent e) { dobleClick_Arbol(e); }
            @Override
            public void mousePressed(MouseEvent e) { }
            @Override
            public void mouseReleased(MouseEvent e) { }
            @Override
            public void mouseEntered(MouseEvent e) { }
            @Override
            public void mouseExited(MouseEvent e) { }
        });
        //Crea un tab vacio en DracoCompiler
        //crearTabDracoCompiler(null, null);
        //Crea tab de opciones
        tabpane_opciones.addTab("Dasm", Funcion_Panel.nuevo_Tab_Editor(null, this));
        tabpane_opciones.addTab("Consola", Funcion_Panel.nuevo_Consola());
        //actualizar arbol
        Arbol.nuevo_arbol(arbol);
    }
    
    /**
     * Retorna la imagen con el nombre
     * @param nombre
     * @return 
     */
    private ImageIcon getImg(String nombre){
        return new ImageIcon(getClass().getResource("/Resources/" + nombre));
    }
        
    /**
     * Crea un Tab para DracoCompiler con el nombre espeficado y se le asigna
     * una ruta relativa al texto y selecciona el Tab creado
     * @param nombre
     * @param path 
     */
    private void crearTabDracoCompiler(String nombre, String ruta){
        Icon img = getImg("edit.png");
        Funcion_TabPane.crearTabCompiler(tabpane_editor, img, nombre, ruta, this);
    }
    
    /**
     * Crea un nuevo archivo en una carpeta seleccionada del arbol
     * @param isArchivo 
     */
    private void nuevo_archivo(boolean isArchivo){
        //abrir el pathRelativa el archivo siempre existe por que se selecciona del arbol
        File f = new File(pathRelativa);
        if(f.isDirectory()){
            //si el path es un directorio entonces se puede crear el archivo
            String mssg;
            String title;
            if(isArchivo){
                mssg = "El archivo sera de extension (\'" + Arbol.extension[0] + "\')"
                    + "\nIngrese el nombre del archivo";
                title = "Nuevo Tab";
            }else{
                mssg = "Ingrese el nombre de la carpeta";
                title = "Nueva Carpeta";
            }
            String msg = (String)JOptionPane.showInputDialog(this, mssg, title, JOptionPane.INFORMATION_MESSAGE, getImg("escribir.png"), null, null);
            if(msg != null){
                if(!msg.equals("")){
                    if(isArchivo){ msg += Arbol.extension[0]; }
                    String rutaRelativa = pathRelativa + "/" + msg;
                    f = new File(rutaRelativa);
                    //verifica si existe el archivo
                    if(!f.exists()){
                        try {
                            if(isArchivo){
                                f.createNewFile();
                                crearTabDracoCompiler(msg, rutaRelativa);
                            }else{
                                f.mkdir();
                            }
                            Arbol.nuevo_arbol(arbol);
                        } catch (IOException ex) { JOptionPane.showMessageDialog(this, "Ocurrio un error al crear el archivo", "Error", JOptionPane.ERROR_MESSAGE); }
                    }else{ JOptionPane.showMessageDialog(this, "El archivo ya existe", "Informacion", JOptionPane.INFORMATION_MESSAGE, getImg("info.png")); }
                }else{
                    if(isArchivo){
                        JOptionPane.showMessageDialog(this, "Campo obligatorio. No se creo el archivo", "Informacion", JOptionPane.INFORMATION_MESSAGE, getImg("info.png"));
                    }else{
                        JOptionPane.showMessageDialog(this, "Campo obligatorio. No se creo la carpeta", "Informacion", JOptionPane.INFORMATION_MESSAGE, getImg("info.png"));
                    }
                }
            }
        }else{ JOptionPane.showMessageDialog(this, "Seleccione una carpeta", "Informacion", JOptionPane.INFORMATION_MESSAGE, getImg("info.png")); }
    }
    
    /**
     * Abre el archivo que se selecciono del arbol
     * @param e 
     */
    private void dobleClick_Arbol(MouseEvent e){
        //obtener la direccion del nodo seleccionado
        TreePath tp = arbol.getPathForLocation(e.getX(), e.getY());
        if(tp != null){
            //actualizar el pathRelativa
            pathRelativa = Arbol.actualizarSeleccionTree(tp);
            if(e.getClickCount() == 2 && e.getButton() == MouseEvent.BUTTON1){
                //cuando se realiza doble click
                //Se busca el archivo
                File f = new File(pathRelativa);
                if(!f.isDirectory()){
                    //variables para lectura de un archivo
                    FileReader fr;
                    BufferedReader br;
                    //si es con la extension [0]
                    if(f.getName().endsWith(Arbol.extension[0])){
                        boolean ban = false;
                        int i = 0;
                        //busca si ya existe el Tab
                        while(i < tabpane_editor.getTabCount()){
                            if(tabpane_editor.getTitleAt(i).equals(f.getName())){
                                ban = true;
                                break;
                            }
                            i++;
                        }
                        if(!ban){
                            //cuando el tab no existe crear uno
                            crearTabDracoCompiler(f.getName(), pathRelativa);
                            tabpane_editor.setSelectedIndex(i);
                            //leer el archivo y colocarle el texto al tab
                            try {
                                fr = new FileReader(f);
                                br = new BufferedReader(fr);

                                String linea;
                                String texto = "";
                                while((linea = br.readLine()) != null){
                                    texto += linea + "\n";
                                }
                                Panel pan = (Panel)tabpane_editor.getSelectedComponent();
                                pan.getTexto().setText(texto);
                                br.close();
                                fr.close();
                            } catch (IOException ex) {
                                JOptionPane.showMessageDialog(this, "Error al leer el archivo", "Error", JOptionPane.ERROR_MESSAGE);
                            }
                        }else{
                            tabpane_editor.setSelectedIndex(i);
                        }
                    }else{
                        tabpane_opciones.setSelectedIndex(0);
                        try {
                            fr = new FileReader(f);
                            br = new BufferedReader(fr);

                            String linea;
                            String texto = "";
                            while((linea = br.readLine()) != null){
                                texto += linea + "\n";
                            }
                            Panel pan = (Panel)tabpane_opciones.getSelectedComponent();
                            pan.getTexto().setText(texto);
                        } catch (IOException ex) {
                            JOptionPane.showMessageDialog(this, "Error al leer el archivo", "Error", JOptionPane.ERROR_MESSAGE);
                        }
                    }
                }
            }
        }
        System.out.println(pathRelativa);
    }
    
    /**
     * Realiza la funcion de Salir del sistema
     */
    private void salir(){
        //Icon img = new ImageIcon(getClass().getResource("/Resources/cerrar.png"));
        //int salir = JOptionPane.showConfirmDialog(this, 
        //        "¿Desea salir de la aplicacion?", "Informacion",
        //        JOptionPane.OK_CANCEL_OPTION, 
        //        JOptionPane.QUESTION_MESSAGE, 
        //        img);
        //if(salir == JOptionPane.YES_OPTION){
            cerrarTodo();
            System.exit(0);
        //}
    }
    
    /**
     * Busca una palabra o letra en tabpane_editor y lo resalta con color amarillo
     * en caso contrario deja el texto a la normalidad
     */
    private void buscar(){
        Panel pan = (Panel)tabpane_editor.getSelectedComponent();
        
        DefaultHighlighter.DefaultHighlightPainter highlightPainter = new DefaultHighlighter.DefaultHighlightPainter(Color.YELLOW);
        Highlighter h = pan.getTexto().getHighlighter();
        h.removeAllHighlights();
        
        if(txt_buscar.getText().length() > 0){
            Pattern p = Pattern.compile(txt_buscar.getText());
            Matcher m = p.matcher(pan.getTexto().getText());
            while(m.find()){
                try{
                    h.addHighlight(m.start(), m.end(), highlightPainter);
                }catch(BadLocationException ex){ }
            }
        }
    }

    private void guardar(){
        if(tabpane_editor.getTabCount() > 0){
            Archivo.guardarTexto(Funcion_TabPane.getSelectTab(tabpane_editor).getTexto());
        }
    }
    
    private void cerrarTodo(){
        /*Panel pan;
        for(int i = 0; i < tabpane_editor.getTabCount(); i++){
            tabpane_editor.setSelectedIndex(i);
            pan = (Panel)tabpane_editor.getSelectedComponent();
            cerrar(pan);
            pan.getTexto().repaint();
            pan.repaint();
        }
        tabpane_editor.repaint();*/
    }
    
    private void cerrar(Panel pan){
        if(tabpane_editor.getTabCount() > 0){
            Archivo.guardarTexto(pan.getTexto());
            try{
                tabpane_editor.removeTabAt(tabpane_editor.getSelectedIndex());
            }catch(Exception ex){}
            pan.getTexto().repaint();
            pan.repaint();
        }
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        pm_arbol = new javax.swing.JPopupMenu();
        mi_actualizar = new javax.swing.JMenuItem();
        jsp_arbol = new javax.swing.JScrollPane();
        arbol = new javax.swing.JTree();
        pan_draco_compiler = new javax.swing.JPanel();
        btn_buscar = new javax.swing.JButton();
        txt_buscar = new javax.swing.JTextField();
        pan_tab = new javax.swing.JPanel();
        tabpane_editor = new javax.swing.JTabbedPane();
        status = new javax.swing.JLabel();
        btn_compilar = new javax.swing.JButton();
        btn_generar = new javax.swing.JButton();
        pan_dj_dasm = new javax.swing.JPanel();
        tabpane_opciones = new javax.swing.JTabbedPane();
        menubar = new javax.swing.JMenuBar();
        m_archivo = new javax.swing.JMenu();
        mi_nuevo_archivo = new javax.swing.JMenuItem();
        mi_nueva_carpeta = new javax.swing.JMenuItem();
        mi_guardar = new javax.swing.JMenuItem();
        mi_cerrar = new javax.swing.JMenuItem();
        mi_salir = new javax.swing.JMenuItem();
        m_depurador = new javax.swing.JMenu();
        mi_iniciar = new javax.swing.JMenuItem();
        mi_sig_paso = new javax.swing.JMenuItem();
        mi_terminar = new javax.swing.JMenuItem();

        mi_actualizar.setText("Actualizar");
        mi_actualizar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_actualizarActionPerformed(evt);
            }
        });
        pm_arbol.add(mi_actualizar);

        setDefaultCloseOperation(javax.swing.WindowConstants.DO_NOTHING_ON_CLOSE);
        setTitle("Draco Ensamblado Web - Editor Texto");
        setPreferredSize(new java.awt.Dimension(1240, 720));
        setResizable(false);
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowClosing(java.awt.event.WindowEvent evt) {
                formWindowClosing(evt);
            }
        });

        jsp_arbol.setBorder(javax.swing.BorderFactory.createTitledBorder(javax.swing.BorderFactory.createEtchedBorder(), "Carpetas y archivos"));

        arbol.setModel(null);
        arbol.setComponentPopupMenu(pm_arbol);
        jsp_arbol.setViewportView(arbol);

        pan_draco_compiler.setBorder(javax.swing.BorderFactory.createTitledBorder(javax.swing.BorderFactory.createEtchedBorder(), "DracoCompiler"));

        btn_buscar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/buscar.png"))); // NOI18N
        btn_buscar.setText("Buscar");
        btn_buscar.setPreferredSize(new java.awt.Dimension(59, 25));
        btn_buscar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btn_buscarActionPerformed(evt);
            }
        });

        txt_buscar.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        txt_buscar.setToolTipText("Buscar Texto");
        txt_buscar.setPreferredSize(new java.awt.Dimension(59, 25));

        pan_tab.setBorder(javax.swing.BorderFactory.createEtchedBorder());

        status.setPreferredSize(new java.awt.Dimension(59, 25));

        btn_compilar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/compilar.png"))); // NOI18N
        btn_compilar.setText("Compilar");

        btn_generar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/imagen.png"))); // NOI18N
        btn_generar.setText("Generar Imagen");
        btn_generar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btn_generarActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout pan_tabLayout = new javax.swing.GroupLayout(pan_tab);
        pan_tab.setLayout(pan_tabLayout);
        pan_tabLayout.setHorizontalGroup(
            pan_tabLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pan_tabLayout.createSequentialGroup()
                .addComponent(status, javax.swing.GroupLayout.PREFERRED_SIZE, 160, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(42, 42, 42)
                .addComponent(btn_generar, javax.swing.GroupLayout.PREFERRED_SIZE, 150, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(btn_compilar, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
            .addComponent(tabpane_editor)
        );
        pan_tabLayout.setVerticalGroup(
            pan_tabLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pan_tabLayout.createSequentialGroup()
                .addComponent(tabpane_editor, javax.swing.GroupLayout.PREFERRED_SIZE, 280, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(pan_tabLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(status, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(pan_tabLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(btn_compilar, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(btn_generar, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE))))
        );

        javax.swing.GroupLayout pan_draco_compilerLayout = new javax.swing.GroupLayout(pan_draco_compiler);
        pan_draco_compiler.setLayout(pan_draco_compilerLayout);
        pan_draco_compilerLayout.setHorizontalGroup(
            pan_draco_compilerLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pan_draco_compilerLayout.createSequentialGroup()
                .addGap(0, 751, Short.MAX_VALUE)
                .addComponent(txt_buscar, javax.swing.GroupLayout.PREFERRED_SIZE, 160, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(btn_buscar, javax.swing.GroupLayout.PREFERRED_SIZE, 100, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pan_draco_compilerLayout.createSequentialGroup()
                .addComponent(pan_tab, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addContainerGap())
        );
        pan_draco_compilerLayout.setVerticalGroup(
            pan_draco_compilerLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pan_draco_compilerLayout.createSequentialGroup()
                .addGroup(pan_draco_compilerLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btn_buscar, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(txt_buscar, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(pan_tab, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        pan_dj_dasm.setBorder(javax.swing.BorderFactory.createTitledBorder(javax.swing.BorderFactory.createEtchedBorder(), "Opciones"));
        pan_dj_dasm.setPreferredSize(new java.awt.Dimension(17, 250));

        javax.swing.GroupLayout pan_dj_dasmLayout = new javax.swing.GroupLayout(pan_dj_dasm);
        pan_dj_dasm.setLayout(pan_dj_dasmLayout);
        pan_dj_dasmLayout.setHorizontalGroup(
            pan_dj_dasmLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(tabpane_opciones)
        );
        pan_dj_dasmLayout.setVerticalGroup(
            pan_dj_dasmLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(tabpane_opciones, javax.swing.GroupLayout.DEFAULT_SIZE, 228, Short.MAX_VALUE)
        );

        m_archivo.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/carpeta.png"))); // NOI18N
        m_archivo.setText("Archivo");

        mi_nuevo_archivo.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/nuevo_archivo.png"))); // NOI18N
        mi_nuevo_archivo.setText("Nuevo Archivo");
        mi_nuevo_archivo.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_nuevo_archivoActionPerformed(evt);
            }
        });
        m_archivo.add(mi_nuevo_archivo);

        mi_nueva_carpeta.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/nuevo_carpeta.png"))); // NOI18N
        mi_nueva_carpeta.setText("Nueva Carpeta");
        mi_nueva_carpeta.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_nueva_carpetaActionPerformed(evt);
            }
        });
        m_archivo.add(mi_nueva_carpeta);

        mi_guardar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/guardar_archivo.png"))); // NOI18N
        mi_guardar.setText("Guardar");
        mi_guardar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_guardarActionPerformed(evt);
            }
        });
        m_archivo.add(mi_guardar);

        mi_cerrar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/cerrar_archivo.png"))); // NOI18N
        mi_cerrar.setText("Cerrar");
        mi_cerrar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_cerrarActionPerformed(evt);
            }
        });
        m_archivo.add(mi_cerrar);

        mi_salir.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/salir.png"))); // NOI18N
        mi_salir.setText("Salir");
        mi_salir.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mi_salirActionPerformed(evt);
            }
        });
        m_archivo.add(mi_salir);

        menubar.add(m_archivo);

        m_depurador.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/verificar.png"))); // NOI18N
        m_depurador.setText("Depurador");

        mi_iniciar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/iniciar.png"))); // NOI18N
        mi_iniciar.setText("Iniciar");
        m_depurador.add(mi_iniciar);

        mi_sig_paso.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/siguiente.png"))); // NOI18N
        mi_sig_paso.setText("Siguiente paso");
        m_depurador.add(mi_sig_paso);

        mi_terminar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Resources/detener.png"))); // NOI18N
        mi_terminar.setText("Terminar todo");
        m_depurador.add(mi_terminar);

        menubar.add(m_depurador);

        setJMenuBar(menubar);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jsp_arbol, javax.swing.GroupLayout.PREFERRED_SIZE, 205, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(pan_draco_compiler, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
            .addComponent(pan_dj_dasm, javax.swing.GroupLayout.DEFAULT_SIZE, 1240, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(pan_draco_compiler, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jsp_arbol))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(pan_dj_dasm, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void mi_nuevo_archivoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_nuevo_archivoActionPerformed
        nuevo_archivo(true);
    }//GEN-LAST:event_mi_nuevo_archivoActionPerformed

    private void mi_salirActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_salirActionPerformed
        salir();
    }//GEN-LAST:event_mi_salirActionPerformed

    private void formWindowClosing(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowClosing
        salir();
    }//GEN-LAST:event_formWindowClosing

    private void btn_buscarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btn_buscarActionPerformed
        buscar();
    }//GEN-LAST:event_btn_buscarActionPerformed

    private void mi_nueva_carpetaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_nueva_carpetaActionPerformed
        nuevo_archivo(false);
    }//GEN-LAST:event_mi_nueva_carpetaActionPerformed

    private void mi_actualizarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_actualizarActionPerformed
        Arbol.nuevo_arbol(arbol);
    }//GEN-LAST:event_mi_actualizarActionPerformed

    private void mi_guardarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_guardarActionPerformed
        guardar();
    }//GEN-LAST:event_mi_guardarActionPerformed

    private void mi_cerrarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mi_cerrarActionPerformed
        Panel pan = Funcion_TabPane.getSelectTab(tabpane_editor);
        cerrar(pan);
    }//GEN-LAST:event_mi_cerrarActionPerformed

    private void btn_generarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btn_generarActionPerformed
        
    }//GEN-LAST:event_btn_generarActionPerformed

    @Override
    public void caretUpdate(CaretEvent e) {
        status.setText(TextArea.actualizarCaret(e, null));
    }
    
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        /*
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(Editor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(Editor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(Editor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(Editor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>
        */
        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Editor().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JTree arbol;
    private javax.swing.JButton btn_buscar;
    private javax.swing.JButton btn_compilar;
    private javax.swing.JButton btn_generar;
    private javax.swing.JScrollPane jsp_arbol;
    private javax.swing.JMenu m_archivo;
    private javax.swing.JMenu m_depurador;
    private javax.swing.JMenuBar menubar;
    private javax.swing.JMenuItem mi_actualizar;
    private javax.swing.JMenuItem mi_cerrar;
    private javax.swing.JMenuItem mi_guardar;
    private javax.swing.JMenuItem mi_iniciar;
    private javax.swing.JMenuItem mi_nueva_carpeta;
    private javax.swing.JMenuItem mi_nuevo_archivo;
    private javax.swing.JMenuItem mi_salir;
    private javax.swing.JMenuItem mi_sig_paso;
    private javax.swing.JMenuItem mi_terminar;
    private javax.swing.JPanel pan_dj_dasm;
    private javax.swing.JPanel pan_draco_compiler;
    private javax.swing.JPanel pan_tab;
    private javax.swing.JPopupMenu pm_arbol;
    private javax.swing.JLabel status;
    private javax.swing.JTabbedPane tabpane_editor;
    private javax.swing.JTabbedPane tabpane_opciones;
    private javax.swing.JTextField txt_buscar;
    // End of variables declaration//GEN-END:variables
}

﻿using SuMueble.DataAccess;
using SuMueble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace SuMueble
{
    public partial class FormularioInventarios : Form
    {
        private int IDglobal = 0;


        public FormularioInventarios(int ID = 0)
        {
            InitializeComponent();
            using (var db = new SuMuebleDBContext())
            {
                cmb_Categoria.DataSource = db.Categorias.ToList();

            }
            cmb_Categoria.DisplayMember = "Categoria";
            cmb_Categoria.ValueMember = "ID";

            if (ID != 0)
            {
                cargarDatos(ID);
                txt_Existencia.Enabled = false;
                txt_Precio.Enabled = false;
            }
            IDglobal = ID;
        }

        private void cargarDatos(int ID)
        {

            Producto p = new Producto();
            using (var db = new SuMuebleDBContext())
            {
                p = db.Productos.Find(ID);
            }
            txt_Existencia.Value = p.Cantidad;
            txt_Nombre.Text = p.Nombre;
            txt_Precio.Value = p.Precio;
            cmb_Categoria.SelectedValue = p.CategoriaId;
            txt_Codigo.ReadOnly = true;

        }

      

        private void FormularioInventarios_Load(object sender, EventArgs e)
        {

        }

        private void btn_Hecho_Click(object sender, EventArgs e)
        {

            //VALIDACIONES TEXTBOX VACIOS
            //       cod01
            var prodCod = txt_Codigo.Text.Trim();
            if ( prodCod == "" || prodCod.Contains(" ") )
            {
                MessageBox.Show("Codigo articulo es invalido", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Codigo.Focus();
            }
            else if (txt_Nombre.Text.Trim() == "")
            {
                MessageBox.Show("Nombre articulo esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Nombre.Focus();
            }
            else if (txt_Precio.Value == 0)
            {
                MessageBox.Show("Precio articulo esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Precio.Focus();
            }
            else if (cmb_Categoria.Text == "Todos")
            {
                MessageBox.Show("No se ha seleccionado ninguna categoria", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_Categoria.Focus();
            }
            else if (txt_Existencia.Value == 0)
            {
                MessageBox.Show("Existencia articulo esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Producto p = new Producto()
                {
                    ID = IDglobal,
                    Codigo = txt_Codigo.Text.Trim(),
                    Existencias = (int)txt_Existencia.Value,
                    Producto = txt_Nombre.Text.Trim(),
                    PrecioUnitario = (float)txt_Precio.Value,
                    IDCategoria = cmb_Categoria.SelectedValue.GetHashCode(),
                    ISV = (float)txt_impuesto.Value
                };
                productoControlador.SaveProductos(p);
                MessageBox.Show("Guardado con exito", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

      


      

       

        
    }
}

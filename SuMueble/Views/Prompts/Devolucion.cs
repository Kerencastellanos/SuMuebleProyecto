﻿using SuMueble.Controller;
using SuMueble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuMueble.Views
{

    public partial class Devolucion : Form
    {
        DevolucionControlador devolucionControlador = new DevolucionControlador();
        DetalleVentaController dvControllador = new DetalleVentaController();
        List<SQLViewVentas> detalles_;
        public Devolucion(Guid IDVenta_)
        {
            InitializeComponent();
            
            cargarDatos(IDVenta_);
        }
        

        private void cargarDatos(Guid IDVenta)
        {
            detalles_ = dvControllador.GetDetalles(IDVenta);
            cb_productos.DataSource = detalles_;
            cb_productos.DisplayMember = "Producto";
            cb_productos.ValueMember = "IDProducto";

            //
            txt_CodigoFactura.Text = detalles_[0].CodigoFactura.ToString();
        }

        private void btn_hecho_Click(object sender, EventArgs e)
        {
            if (txt_Motivo.Text == "")
            {
                MessageBox.Show("Motivo esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Motivo.Focus();
            }
            else if (txt_Cantidad.Value == 0)
            {
                MessageBox.Show("Cantidad esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Cantidad.Focus();
            }
            else if (txt_Observacion.Text == "")
            {
                MessageBox.Show("Observaciones esta vacio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Observacion.Focus();
            }
            else
            {
                Models.Devolucion devolucion = new Models.Devolucion()
                {
                    CodigoFactura = detalles_[0].CodigoFactura,
                    Cantidad = (int)txt_Cantidad.Value,
                    IDProducto = cb_productos.SelectedValue.GetHashCode(),
                    Motivo = txt_Motivo.Text,
                    Observaciones = txt_Observacion.Text

                };
                var res = devolucionControlador.InsertarDevolucion(devolucion);
                if (res > 0)
                    MessageBox.Show("Devolucion Guardada Corrrecta Mente", "Devoluciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("La Devolucion NO se Guardo Corrrecta Mente", "Devoluciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Close();
            }
            
        }

    

        private void cb_productos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txt_Cantidad.Value = detalles_.Find(x => x.IDProducto == cb_productos.SelectedValue.GetHashCode()).Cantidad;

        }


        

    }
}

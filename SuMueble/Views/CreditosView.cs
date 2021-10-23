﻿using SuMueble.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SuMueble.Views
{
    public partial class CreditosView : UserControl
    {
        VentaController ventaController = new VentaController();
        DataTable ListaVentas;
        public CreditosView()
        {
            InitializeComponent();
            GetData();


        }
        private void GetData()
        {
            ListaVentas = ventaController.GetCreditosPendientes();
            CargarDataGrid(ListaVentas);

        }
        private void CargarDataGrid(DataTable lista)
        {

            dgv_ventasCredito.DataSource = lista;
        }


        private string GetCell(int cell)
        {
            if (dgv_ventasCredito.Rows.Count > 0)
            {
                int index = dgv_ventasCredito.CurrentRow.Index;
                return dgv_ventasCredito.Rows[index].Cells[cell].Value.ToString();

            }
            else return "0";
        }
            
        private void btn_pagarcuota_Click(object sender, EventArgs e)
        {
            string cod_factura = GetCell(0);
            if (cod_factura != "0")
            {
                PagarCuota pagarCuota = new PagarCuota(cod_factura);
                pagarCuota.ShowDialog();
                GetData();

            }
            else
            {
                MessageBox.Show("No hay ningúna venta seleccionada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            var filtrados = ListaVentas.AsEnumerable().Where(x =>
            {
                int cf = 0;
                int.TryParse(txtbuscar.Text, out cf);
                string cl = txtbuscar.Text.ToLower();
                return x.Field<int>("CodigoFactura") == cf || x.Field<string>("Cliente").ToLower().StartsWith(cl);
            });

            CargarDataGrid(filtrados.AsDataView().ToTable());
        }

        private void txtbuscar_Leave(object sender, EventArgs e)
        {
            txtbuscar.Text = txtbuscar.Text.Trim();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnoRegV2
{
    /// <summary>
    /// Lógica de interacción para VentanaProductos.xaml
    /// </summary>
    public partial class VentanaProductos : Window
    {
        public VentanaProductos()
        {
            InitializeComponent();
            btnProductos.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnProductos.IsEnabled = true;
        }
    }
}

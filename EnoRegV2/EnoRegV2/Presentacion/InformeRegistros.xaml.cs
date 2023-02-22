using EnoregV2.Dominio;
using Microsoft.Win32;
using System;
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

namespace EnoregV2.Presentacion
{
    /// <summary>
    /// Lógica de interacción para InformeRegistros.xaml
    /// </summary>
    public partial class InformeRegistros : Window
    {
        ProductoDAO productoDAO;

        /// <summary>
        /// Constructor de la ventana InformeRegistros <see cref="InformeRegistros"/> class.
        /// </summary>
        /// <param name="productoDAO"></param>
        public InformeRegistros(ProductoDAO productoDAO)
        {
            this.productoDAO = productoDAO;
            InitializeComponent();
            reportViewer.Owner = this;
        }

        /// <summary>
        /// Evento Loaded de la ventana, el cual carga el informe 
        /// segun el filtro que tengamos seleccionado(Entradas/Salidas o Entradas o Salidas)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (productoDAO.RegistroFiltros.Equals("Entradas/Salidas"))
            {
                CrystalReport5_EntradaSalida crystalReport5 = new CrystalReport5_EntradaSalida();
                crystalReport5.SetParameterValue("param_FechaEntrada", productoDAO.ParamFechaEntrada);
                crystalReport5.SetParameterValue("param_LoteProducto", productoDAO.Param);
                crystalReport5.SetParameterValue("param_FechaSalida", productoDAO.ParamFechaSalida);
                reportViewer.ViewerCore.ReportSource = crystalReport5;

            }
            else if (productoDAO.RegistroFiltros.Equals("Entradas"))
            {
                CrystalReport3_Entrada cargarReport3 = new CrystalReport3_Entrada();
                cargarReport3.SetParameterValue("param_FechaEntrada", productoDAO.ParamFechaEntrada);
                cargarReport3.SetParameterValue("param_LoteProducto", productoDAO.Param);
                reportViewer.ViewerCore.ReportSource = cargarReport3;
            }
            else
            {
                CrystalReport4_Salida crystalReport4 = new CrystalReport4_Salida();
                crystalReport4.SetParameterValue("param_Destino", productoDAO.ParamDestino);
                crystalReport4.SetParameterValue("param_LoteProducto", productoDAO.Param);
                crystalReport4.SetParameterValue("param_Fecha", productoDAO.ParamFechaSalida);
                reportViewer.ViewerCore.ReportSource = crystalReport4;
            }
        }
    }
}

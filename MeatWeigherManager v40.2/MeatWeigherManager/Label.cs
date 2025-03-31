using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using PrintEtiZpl2_Win;
using System.Windows.Forms;
using ConfigApp;
namespace MeatWeigherManager
{
    class CLabel
    {
        /*****************************************************************************************
        * Metodo:      PrintProduct
        * Descripcion: Imprime la Etiqueta del producto
        *              
        * Retorna:     
        *****************************************************************************************/
        public static bool PrintProduct(CPesada datPesaje,bool fieldWeightEnable = true, bool fieldUnitsEnable = true, int countDecimalsWeight = 1)
        {
            bool printOk = false;
            try
            {
                //format valor peso
                string formatPeso = String.Format("{{0,7:N{0}}}", countDecimalsWeight);

                PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                listaVariables.Add(datPesaje.Producto.CodSenasa);
                listaVariables.Add(datPesaje.Producto.TextAuxEtiL1);
                listaVariables.Add(datPesaje.Producto.TextAuxEtiL2);
                listaVariables.Add(datPesaje.Producto.NombreEtiL1);
                listaVariables.Add(datPesaje.Producto.NombreEtiL2);
                listaVariables.Add(datPesaje.Producto.NombreEtiL3);
                listaVariables.Add(datPesaje.Producto.NombreEtiL4);
                listaVariables.Add(datPesaje.Producto.NombreEtiL5);
                listaVariables.Add(datPesaje.Producto.NombreEtiL6);
                listaVariables.Add(String.Format("{0:D2}{1:D2}{2:D4}", datPesaje.FechaHora.Day, datPesaje.FechaHora.Month, datPesaje.FechaHora.Year));
                listaVariables.Add(datPesaje.FechaVencimiento.ToString("dd/MM/yyyy"));

                if (datPesaje.PesoNeto != 0.0f && fieldWeightEnable)
                {
                    listaVariables.Add("NETO");
                    listaVariables.Add(String.Format(formatPeso, datPesaje.PesoNeto));
                    listaVariables.Add("kg");
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                }
                string codBar = String.Format("MM,A{0,7:D7}", datPesaje.Id);
                listaVariables.Add(codBar); //QR

                if (CConfigApp.m_imprimirLineasTextoSuperiorInferiorDeEtiqueta)
                {
                    listaVariables.Add(CConfigApp.m_lineaDeTextoSuperiorDeEtiqueta);
                    listaVariables.Add(CConfigApp.m_lineaDeTextoInferiorDeEtiqueta);
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                }
                if (datPesaje.Unidades != 0.0f && fieldUnitsEnable)
                {
                    listaVariables.Add(datPesaje.Unidades.ToString());
                }
                else
                {
                    listaVariables.Add("");
                }
                listaVariables.Add(datPesaje.Id.ToString());

                /*
                 * Se imprime el codigo de barras ean13 si la propiedad Alias del producto 
                 * no esta vacia , no es null y representa un valor numerico.
                 */

                if (!String.IsNullOrEmpty(datPesaje.Producto.ProductoSAC.Alias) && !datPesaje.Producto.ProductoSAC.Alias.Any(c=> c < '0' || c > '9'))
                {
                    string pesoFormat3enteros3Decimales = datPesaje.PesoNeto.ToString("000.000").Replace(",", "");
                    string aliasProductoSac = datPesaje.Producto.ProductoSAC.Alias.PadLeft(5,'0');
                    string strEAN13 = "2" + aliasProductoSac + pesoFormat3enteros3Decimales;
                    listaVariables.Add(strEAN13);
                }
                else
                {
                    listaVariables.Add("");
                }
                listaVariables.Add(datPesaje.FechaHora.ToString("dd/MM/yyyy"));

                if (datPesaje.Unidades != 0.0f && fieldUnitsEnable)
                {
                    listaVariables.Add("UNIDADES:");
                }
                else
                {
                    listaVariables.Add("");
                }

                if(datPesaje.Producto.EsTropa && datPesaje.Tropa != null && datPesaje.Tropa.Numero != 0)
                {
                    listaVariables.Add("TROPA:");
                    listaVariables.Add(datPesaje.Tropa.Numero.ToString());
                    listaVariables.Add("TIPIF:");
                    listaVariables.Add(datPesaje.Tropa.Tipificacion?.Nombre ?? "");
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                }

                if (datPesaje.PesoNeto != 0.0f && fieldWeightEnable)
                {
                    listaVariables.Add("BRUTO");
                    listaVariables.Add(String.Format(formatPeso, datPesaje.PesoNeto + datPesaje.PesoTara));
                    listaVariables.Add("kg");
                    listaVariables.Add("TARA");
                    listaVariables.Add(String.Format(formatPeso, datPesaje.PesoTara));
                    listaVariables.Add("kg");
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                    listaVariables.Add("");
                }

                printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora, 
                    datPesaje.Producto.Etiqueta.Id != 0? datPesaje.Producto.Etiqueta.Nombre: ConfigApp.CConfigApp.m_nombreFormatoEtiquetaProducto, 
                                                    listaVariables,CConfigApp.m_encodingNameOutputPrinter, (short)CConfigApp.m_cantidadEtiquetasPorPesada);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Generando la Etiqueta:" + e.Message, "Error Imprimiendo Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return printOk;
        }
        /*****************************************************************************************
        * Metodo:      PrintCombo
        * Descripcion: Imprime la Etiqueta de un combo
        *              
        * Retorna:     
        *****************************************************************************************/
        public static bool PrintCombo(CContenedor datContenedor)
        {
            bool printOk = false;
            try
            {
                PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                listaVariables.Add(datContenedor.Producto.CodSenasa);
                listaVariables.Add(datContenedor.Producto.TextAuxEtiL1);
                listaVariables.Add(datContenedor.Producto.TextAuxEtiL2);
                listaVariables.Add(datContenedor.Producto.NombreEtiL1);
                listaVariables.Add(datContenedor.Producto.NombreEtiL2);
                listaVariables.Add(datContenedor.Producto.NombreEtiL3);
                listaVariables.Add(datContenedor.Producto.NombreEtiL4);
                listaVariables.Add(datContenedor.Producto.NombreEtiL5);
                listaVariables.Add(datContenedor.Producto.NombreEtiL6);
                listaVariables.Add(String.Format("{0:D2}{1:D2}{2:D4}", datContenedor.m_fechaHoraCreacion.Day, datContenedor.m_fechaHoraCreacion.Month, datContenedor.m_fechaHoraCreacion.Year));
                listaVariables.Add(datContenedor.m_fechaVencimiento.ToString("dd/MM/yyyy"));
                listaVariables.Add("");  //leyenda Peso Neto
                listaVariables.Add("");  //valor peso neto
                listaVariables.Add("");  //leyenda unidad del peso.
                string codBar = String.Format("MM,A{0,7:D7}", datContenedor.Id);
                listaVariables.Add("A" + codBar + "A"); //QR


                if (CConfigApp.m_imprimirLineasTextoSuperiorInferiorDeEtiqueta)
                {
                    listaVariables.Add(CConfigApp.m_lineaDeTextoSuperiorDeEtiqueta);
                    listaVariables.Add(CConfigApp.m_lineaDeTextoInferiorDeEtiqueta);
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                }
                listaVariables.Add(datContenedor.m_undsContenidas.ToString());
                listaVariables.Add(datContenedor.Id.ToString());
                /*
                 * Se imprime el codigo de barras ean13 si la propiedad Alias del producto 
                 * no esta vacia , no es null y representa un valor numerico.
                 */  
                if (!String.IsNullOrEmpty(datContenedor.Producto.ProductoSAC.Alias) && !datContenedor.Producto.ProductoSAC.Alias.Any(c => c < '0' || c > '9'))
                {
                    string pesoFormat3enteros3Decimales = datContenedor.PesoNeto.ToString("000.000").Replace(",", "");
                    string aliasProductoSac = datContenedor.Producto.ProductoSAC.Alias.PadLeft(5, '0');
                    string strEAN13 = "2" + aliasProductoSac + pesoFormat3enteros3Decimales;
                    listaVariables.Add(strEAN13);
                }
                else
                {
                    listaVariables.Add("");
                }
                listaVariables.Add(datContenedor.m_fechaHoraCreacion.ToString("dd/MM/yyyy"));

                printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora,
                                        datContenedor.Producto.Etiqueta.Id != 0 ? datContenedor.Producto.Etiqueta.Nombre : ConfigApp.CConfigApp.m_nombreFormatoEtiquetaProducto,
                                        listaVariables, CConfigApp.m_encodingNameOutputPrinter, (short)CConfigApp.m_cantidadEtiquetasPorPesada);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Generando la Etiqueta:" + e.Message, "Error Imprimiendo Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return printOk;
        }

        /*****************************************************************************************
        * Metodo:      PrintConteiner
        * Descripcion: Imprime la Etiqueta del contenedor
        *              
        * Retorna:     
        *****************************************************************************************/
        public static bool PrintContainer(CPesada datPesaje,short cantidadEtiquetas = 1)
        {
            bool printOk = false;
            try
            {
                PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                listaVariables.Add(datPesaje.Producto.NombreEtiL2);
                listaVariables.Add(datPesaje.Producto.CodSenasa);
                listaVariables.Add(String.Format("{0:D2}{1:D2}{2:D4}", datPesaje.FechaHora.Day, datPesaje.FechaHora.Month, datPesaje.FechaHora.Year));
                printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora, ConfigApp.CConfigApp.m_nombreFormatoEtiquetaContenedor, listaVariables, CConfigApp.m_encodingNameOutputPrinter, cantidadEtiquetas);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Generando la Etiqueta:" + e.Message, "Error Imprimiendo Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return printOk;
        }

        /*****************************************************************************************
        * Metodo:      PrintCaja
        * Descripcion: Imprime la Etiqueta de una Caja
        *              
        * Retorna:     
        *****************************************************************************************/
        public static bool PrintCaja(CContenedor datCaja, int countDecimalsWeight = 2)
        {
            bool printOk = false;
            try
            {
                //format valor peso
                string formatPeso = String.Format("{{0,7:N{0}}}", countDecimalsWeight);

                PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                listaVariables.Add(datCaja.Producto.CodSenasa);
                listaVariables.Add(datCaja.Producto.TextAuxEtiL1);
                listaVariables.Add(datCaja.Producto.TextAuxEtiL2);
                listaVariables.Add(datCaja.Producto.NombreEtiL1);
                listaVariables.Add(datCaja.Producto.NombreEtiL2);
                listaVariables.Add(datCaja.Producto.NombreEtiL3);
                listaVariables.Add(datCaja.Producto.NombreEtiL4);
                listaVariables.Add(datCaja.Producto.NombreEtiL5);
                listaVariables.Add(datCaja.Producto.NombreEtiL6);
                listaVariables.Add(String.Format("{0:D2}{1:D2}{2:D4}", datCaja.m_fechaHoraCreacion.Day, datCaja.m_fechaHoraCreacion.Month, datCaja.m_fechaHoraCreacion.Year));
                listaVariables.Add(datCaja.m_fechaVencimiento.ToString("dd/MM/yyyy"));
                listaVariables.Add(String.Format(formatPeso, datCaja.PesoNeto+ datCaja.PesoTara));
                listaVariables.Add(String.Format(formatPeso, datCaja.PesoTara));
                listaVariables.Add(String.Format(formatPeso, datCaja.PesoNeto));
                string codBar = String.Format("MM,A{0,7:D7}", datCaja.Id);
                listaVariables.Add("A"+codBar+"A"); //QR
                if (CConfigApp.m_imprimirLineasTextoSuperiorInferiorDeEtiqueta)
                {
                    listaVariables.Add(CConfigApp.m_lineaDeTextoSuperiorDeEtiqueta);
                    listaVariables.Add(CConfigApp.m_lineaDeTextoInferiorDeEtiqueta);
                }
                else
                {
                    listaVariables.Add("");
                    listaVariables.Add("");
                }
                listaVariables.Add(datCaja.m_undsContenidas.ToString());
                listaVariables.Add(datCaja.Id.ToString());

                /*
                 * Se imprime el codigo de barras ean13 si la propiedad Alias del producto 
                 * no esta vacia , no es null y representa un valor numerico.
                 */
                if (!String.IsNullOrEmpty(datCaja.Producto.ProductoSAC.Alias) && !datCaja.Producto.ProductoSAC.Alias.Any(c => c < '0' || c > '9'))
                {
                    string pesoFormat3enteros3Decimales = datCaja.PesoNeto.ToString("000.000").Replace(",", "");
                    string aliasProductoSac = datCaja.Producto.ProductoSAC.Alias.PadLeft(5, '0');
                    string strEAN13 = "2" + aliasProductoSac + pesoFormat3enteros3Decimales;
                    listaVariables.Add(strEAN13);
                }

                printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora,
                     datCaja.Producto.Etiqueta.Id != 0 ? datCaja.Producto.Etiqueta.Nombre : ConfigApp.CConfigApp.m_nombreFormatoEtiquetaCaja,
                    listaVariables, CConfigApp.m_encodingNameOutputPrinter, (short)CConfigApp.m_cantidadEtiquetasPorPesada);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Generando la Etiqueta:" + e.Message, "Error Imprimiendo Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return printOk;
        }

        /*****************************************************************************************
        * Metodo:      PrintPedido
        * Descripcion: Imprime las Etiquetas de logistica para un Pedido.
        *              Se indica el total de bultos a imprimir en la etiqueta.
        *              Se indica si se debe imprimir la secuencia , es decir una etiqueta por bulto
        *              con la leyenda 1 de X.
        *              Se indica la cantidad de duplicados de etiquetas a imprimir.
        * Retorna:     
        *****************************************************************************************/
        public static bool PrintPedido(CPEgreso datPedido, int totalBultos, bool conSecuencia=false,short duplicados=0)
        {
            bool printOk = false;
            int desdeBulto=1;
            duplicados++;

            try
            {
                if (conSecuencia)
                {
                    while (desdeBulto <= totalBultos)
                    {
                        PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                        listaVariables.Add("Fecha:");
                        listaVariables.Add(datPedido.FechaEgreso.ToString("dd-MM-yyyy"));
                        listaVariables.Add("Pedido:");
                        listaVariables.Add(datPedido.ComprobantePedidoSAC);
                        listaVariables.Add("Cliente:");
                        listaVariables.Add(datPedido.Cliente.Nombre);
                        listaVariables.Add("Bulto:");
                        listaVariables.Add(desdeBulto.ToString());
                        listaVariables.Add("de:");
                        listaVariables.Add(totalBultos.ToString());
                        printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora, ConfigApp.CConfigApp.m_nombreFormatoEtiquetaPedido, listaVariables, CConfigApp.m_encodingNameOutputPrinter, duplicados);
                        desdeBulto++;
                        System.Threading.Thread.Sleep(200);
                    }
                }
                else
                {
                    PrintEtiZpl2_Win.CListVariables listaVariables = new PrintEtiZpl2_Win.CListVariables();
                    listaVariables.Add("Fecha:");
                    listaVariables.Add(datPedido.FechaEgreso.ToString("dd-MM-yyyy"));
                    listaVariables.Add("Pedido:");
                    listaVariables.Add(datPedido.ComprobantePedidoSAC);
                    listaVariables.Add("Cliente:");
                    listaVariables.Add(datPedido.Cliente.Nombre);
                    listaVariables.Add("Total Bultos");
                    listaVariables.Add(totalBultos.ToString());
                    listaVariables.Add("");
                    listaVariables.Add("");
                    printOk = CPrintEtiZpl2_Win.Print(ConfigApp.CConfigApp.m_nombreImpresora, ConfigApp.CConfigApp.m_nombreFormatoEtiquetaPedido, listaVariables, CConfigApp.m_encodingNameOutputPrinter, duplicados);
                    desdeBulto++;
                    System.Threading.Thread.Sleep(200);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Generando la Etiqueta:" + e.Message, "Error Imprimiendo Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return printOk;
        }

    }
}

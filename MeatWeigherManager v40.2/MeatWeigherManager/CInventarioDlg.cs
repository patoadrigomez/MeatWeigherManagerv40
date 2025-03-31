using System;
using System.Data;
using System.Windows.Forms;
using Db;
using ExcelReport;
using StatusListProgressBar;
using PdfStampa;
using FormatColumn;
using StatusProgressBar;
using System.Threading.Tasks;
using IMCR.WaitCursor;

namespace MeatWeigherManager
{
    public partial class CInventarioDlg : Form
    {

        public CInventarioDlg()
        {
            InitializeComponent();
        }

        private void CInventarioDlg_Load(object sender, EventArgs e)
        {
            dateTimePicker_Inventario.Value = DateTime.Now;
        }

        private async void button_GenerarReporte_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                await Task.Run(() =>
                {
                    GenerarReporteSeleccionado();
                });
            }
        }


        private void GenerarReporteSeleccionado()
        {
            if (radioButton_TipoBultosEnStockSinExistenciaFisica.Checked)
            {
                GenerarReportBultosEnStockSinExistenciaFisica();
            }
            else if (radioButton_TipoBultosSinStockConExistenciaFisica.Checked)
            {
                GenerarReportBultosSinStockConExistenciaFisica();
            }
            else if (radioButton_TipoPiezasSinRegistro.Checked)
            {
                GenerarReportBultosSinRegistro();
            }
            else if (radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.Checked)
            {
                GenerarReportPiezasFueraDeContenedorEnStock();
            }
            else if (radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.Checked)
            {
                GenerarReportPiezasFueraDeContenedorSinStock();
            }
            else if (radioButton_TipoBultosSinExistenciaDePedidosAbiertos.Checked)
            {
                GenerarReportBultosEnPedidosAbiertosSinExistenciaFisica();
            }
            else if (radioButton_tipoBultosEnStockConProximidadVencimiento.Checked)
            {
                GenerarReportExistenciaEnStockDetalladoFullEnProximidadVencimiento();
            }
        }

        private bool GenerarReportBultosSinRegistro()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                //PIEZA,DESTINO
                Cursor.Current = Cursors.WaitCursor;
                ds = CDb.GetConsultaReporteInventario_BultosSinRegistro(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format("CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());

                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_PiezasSinRegistro",
                        NameFilePDF = "REPORTE DE INVENTARIO - BULTOS SIN REGISTRO",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = String.Format("Total de Piezas sin registro en el sistema : {0} ", ds.Tables[0].Rows.Count),
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS SIN REGISTRO"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REPORTE DE INVENTARIO - BULTOS SIN REGISTRO",
                        ReportDescription = infoFiltroFecha,
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS SIN REGISTRO"
                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }

        private bool GenerarReportPiezasFueraDeContenedorEnStock()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                //PIEZA_NRO,PIEZA_LOTE,PIEZA_PESO,PIEZA_PRODUCTO,CONTENEDOR_TIPO,CONTENEDOR_NRO,CONTENEDOR_LOTE,CONTENEDOR_PRODUCTO
                Cursor.Current = Cursors.WaitCursor;
                ds = CDb.GetConsultaReporteInventario_PiezasFueraDeContenedorEnStock(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format(" CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());
                string infoLine2 = String.Format("Total de Piezas con stock , con Existencia Fisisca pero fuera de su contenedor : {0} ", ds.Tables[0].Rows.Count);
                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_PiezasSinContenedor",
                        NameFilePDF = "REP-INV - PIEZAS FUERA DE CONTENEDOR EN STOCK",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = infoLine2,
                        TitleDescription = "REP-INV - PIEZAS FUERA DE CONTENEDOR EN STOCK"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REP-INV - PIEZAS FUERA DE CONTENEDOR EN STOCK",
                        ReportDescription = infoFiltroFecha +" -- "+infoLine2,
                        TitleDescription = "REP-INV - PIEZAS FUERA DE CONTENEDOR EN STOCK"
                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }
        private bool GenerarReportPiezasFueraDeContenedorSinStock()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                Cursor.Current = Cursors.WaitCursor;
                //PIEZA_NRO,PIEZA_LOTE,PIEZA_PESO,PIEZA_PRODUCTO,CONTENEDOR_TIPO,CONTENEDOR_NRO,CONTENEDOR_LOTE,CONTENEDOR_PRODUCTO
                ds = CDb.GetConsultaReporteInventario_PiezasFueraDeContenedorSinStock(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format(" CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());
                string infoLine2 = String.Format("Total de Piezas sin stock , con Existencia Fisisca pero fuera de su contenedor : {0} ", ds.Tables[0].Rows.Count);
                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_PiezasSinContenedor",
                        NameFilePDF = "REP-INV - PIEZAS FUERA DE CONTENEDOR SIN STOCK",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = infoLine2,
                        TitleDescription = "REP-INV - PIEZAS FUERA DE CONTENEDOR SIN STOCK"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REP-INV - PIEZAS FUERA DE CONTENEDOR SIN STOCK",
                        ReportDescription = infoFiltroFecha + " -- " + infoLine2,
                        TitleDescription = "REP-INV - PIEZAS FUERA DE CONTENEDOR SIN STOCK"
                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }
        private bool GenerarReportBultosSinStockConExistenciaFisica()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                Cursor.Current = Cursors.WaitCursor;
                //PIEZA,LOTE,PESO_NETO,PRODUCTO
                ds = CDb.GetConsultaReporteInventario_BultosSinStockConExistenciaFisica(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format(" CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());

                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_PiezasSinStockConExistenciaFisica",
                        NameFilePDF = "REP-INV - BULTOS SIN STOCK PERO EXISTENTES",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = String.Format("Total de Piezas Egresadas que se encuentran en el stock fisico : {0} ", ds.Tables[0].Rows.Count),
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS SIN STOCK PERO EXISTENTES"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    TablesColumnsFormatView tcfv = new TablesColumnsFormatView()
                    {
                        new ColumnsFormatView()
                        {
                            new ColumnFormatView()
                            {
                                Name = "PESO_NETO",
                                FormatView= FormatViewType.FLOAT_ROUND2
                            }
                        }
                    };
                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REP-INV - BULTOS SIN STOCK PERO EXISTENTES",
                        ReportDescription = infoFiltroFecha,
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS SIN STOCK PERO EXISTENTES",
                        TablesColumnsFormatView=tcfv
                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }


        private bool GenerarReportBultosEnStockSinExistenciaFisica()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                Cursor.Current = Cursors.WaitCursor;
                //PIEZA,LOTE,PRODUCTO
                ds = CDb.GetConsultaReporteInventario_BultosConStockSinExistenciaFisica(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format(" CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());

                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_PiezasConStockSinExistenciaFisica",
                        NameFilePDF = "REP-INV - BULTOS CON STOCK SIN EXISTENCIA",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = String.Format("Total de Piezas sin existencia en el stock fisico : {0} ", ds.Tables[0].Rows.Count),
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS CON STOCK SIN EXISTENCIA"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    TablesColumnsFormatView tcfv = new TablesColumnsFormatView()
                    {
                        new ColumnsFormatView()
                        {
                            new ColumnFormatView()
                            {
                                Name = "PESO_NETO",
                                FormatView= FormatViewType.FLOAT_ROUND2
                            }
                        }
                    };

                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REP-INV - BULTOS CON STOCK SIN EXISTENCIA",
                        ReportDescription = infoFiltroFecha,
                        TitleDescription = "REPORTE DE INVENTARIO - BULTOS CON STOCK SIN EXISTENCIA",
                        TablesColumnsFormatView = tcfv
                        
                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }


        private bool GenerarReportBultosEnPedidosAbiertosSinExistenciaFisica()
        {
            bool generado = false;
            DateTime fInventario = dateTimePicker_Inventario.Value;
            DataSet ds;

            if (CDb.ExisteInventario(fInventario))
            {
                Cursor.Current = Cursors.WaitCursor;
                //TIPO,NRO,PESO_NETO,LOTE,PRODUCTO,PEDIDO,COMPROBANTE
                ds = CDb.GetConsultaReporteInventario_BultosEnPedidosAbiertosSinExistenciaFisica(fInventario);
                Cursor.Current = Cursors.Default;

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                string infoFiltroFecha = String.Format(" CONSULTA GENERADA EL: {0} BASADA EN FECHA DE INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fInventario.ToShortDateString());

                if (radioButton_porPDF.Checked)
                {
                    CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileFormatXML = "FormatoRepInv_BultosSinExistenciaDePedidosAbiertos",
                        NameFilePDF = "REP-INV - BULTOS SIN EXISTENCIA DE PEDIDOS ABIERTOS",
                        ReportDescriptionLine1 = infoFiltroFecha,
                        ReportDescriptionLine2 = String.Format("Total de Piezas sin existencia : {0} ", ds.Tables[0].Rows.Count),
                        TitleDescription = "REPORTE INVENTARIO - BULTOS SIN EXISTENCIA DE PEDIDOS ABIERTOS"
                    };
                    generado = pdf.Create();
                }
                else
                {
                    TablesColumnsFormatView tcfv = new TablesColumnsFormatView()
                    {
                        new ColumnsFormatView()
                        {
                            new ColumnFormatView()
                            {
                                Name = "PESO_NETO",
                                FormatView= FormatViewType.FLOAT_ROUND2
                            }
                        }
                    };

                    CExcelReport rexcell = new CExcelReport()
                    {
                        PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                        DataSource = ds.Tables[0],
                        CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                        NameFileExcell = "REP-INV - BULTOS SIN EXISTENCIA DE PEDIDOS ABIERTOS",
                        ReportDescription = infoFiltroFecha,
                        TitleDescription = "REPORTE INVENTARIO - BULTOS SIN EXISTENCIA DE PEDIDOS ABIERTOS",
                        TablesColumnsFormatView = tcfv

                    };
                    generado = rexcell.Create();
                }
            }
            else
            {
                MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return generado;
        }


        private bool GenerarReportExistenciaEnStockDetalladoFullEnProximidadVencimiento()
        {
            bool generado = false;
            DataTable dt;

            //TIPO,NRO,LOTE,UBICACION,PRODUCTO,UNIDADES,NETO,VENCIMIENTO
            dt = CDb.GetConsultaReporte_ExistenciaEnStockDetalleFullEnProximidadVencimineto(0,DateTime.Now,0,0);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("La Consulta no genero resultados.", "Generando Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string infoDescripcionLinea1 = String.Format("FECHA REPORTE: {0}",DateTime.Now.ToShortDateString());

            if (radioButton_porPDF.Checked)
            {
                CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                {
                    PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                    FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                    DataSource = dt,
                    CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                    NameFileFormatXML = "FormatoRepExistenciaStockDetallePorVencimiento",
                    NameFilePDF = "EXISTENCIA EN STOCK FULL DETALLADO EN PROXIMIDAD DE VENCIMIENTO",
                    ReportDescriptionLine1 = infoDescripcionLinea1,
                    TitleDescription = "EXISTENCIA EN STOCK FULL DETALLADO EN PROXIMIDAD DE VENCIMIENTO"
                };
                generado = pdf.Create();
            }
            else
            {
                TablesColumnsFormatView tcfv = new TablesColumnsFormatView()
                {
                    new ColumnsFormatView()
                    {
                        new ColumnFormatView()
                        {
                            Name = "NETO",
                            FormatView= FormatViewType.FLOAT_ROUND2
                        },
                        new ColumnFormatView()
                        {
                            Name = "VENCIMIENTO",
                            FormatView= FormatViewType.DATETIME_ONLYDATE
                        }

                    }
                };

                CExcelReport rexcell = new CExcelReport()
                {
                    PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                    DataSource = dt,
                    CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                    NameFileExcell = "EXISTENCIA EN STOCK FULL DETALLADO EN PROXIMIDAD DE VENCIMIENTO",
                    ReportDescription = infoDescripcionLinea1,
                    TitleDescription = "EXISTENCIA EN STOCK FULL DETALLADO EN PROXIMIDAD DE VENCIMIENTO",
                    TablesColumnsFormatView = tcfv
                };
                generado = rexcell.Create();
            }
            return generado;
        }


        /// <summary>
        /// Realiza el ajuste de stock en funcion a los datos generados en un inventario
        /// </summary>
        private void AjusteStockPorInventario()
        {
            DateTime fInventario = dateTimePicker_Inventario.Value;
            CResultInventario infoInventario = new CResultInventario();

            bool error = false;

            if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
            {
                if (CDb.ExisteInventario(fInventario))
                {
                    try
                    {
                        CStatusProgressBar.ShowStatusProgressBar("Obteniendo informacion del Inventario...");

                        //obtener toda la informacion de estados de stock con respecto al inventario
                        CDb.GetInfoInventario(fInventario, ref infoInventario);
                        
                        CStatusProgressBar.CloseForm();

                        string msg = String.Format(
                                    "Este proceso realizará un ajuste de stock en función al inventario realizado con la colección " +
                                    "de piezas del stock fisico de planta. Aquellas piezas que se encuentren en el stock logico y " +
                                    "no tengan existencia fisica serán incluidas en un despacho para efectuar el egreso logico de " +
                                    "las mismas. Las piezas que no tengan stock logico por haber sido egresada y existan fisicamente" +
                                    "en stock , se quitarán de la registración de egresos quedando asi nuevamente en stock.\r\r" +
                                    " Se han detectado:\r" +
                                    "  Total Piezas en Stock             : {0}\r" +
                                    "  Total Piezas en Stock VERIFICADAS : {1}\r" +
                                    "  Total Cajas en Stock              : {2}\r" +
                                    "  Total Cajas en Stock VERIFICADAS  : {3}\r" +
                                    "  Total Combos en Stock             : {4}\r" +
                                    "  Total Combos en Stock VERIFICADOS : {5}\r" +
                                    "  Total Piezas en Stock sin Existencia: {6}\r" +
                                    "  Total Piezas sin Stock con Existencia: {7}\r" +
                                    "  Total Cajas en Stock sin Existencia: {8}\r" +
                                    "  Total Cajas sin Stock con Existencia: {9}\r" +
                                    "  Total Combos en Stock sin Existencia: {10}\r" +
                                    "  Total Combos sin Stock con Existencia: {11}\r" +
                                    "  Total Piezas fuera de Contenedores en Stock : {12}\r" +
                                    "  Total Piezas fuera de Contenedores sin Stock: {13}\r" +
                                    "  Total Piezas sin Registración: {14}\r" +
                                    "  Total Bultos en Pedidos Abiertos que no Existen: {15}\r" +
                                    "  Acepta continuar con el proceso ? ", infoInventario.Stock.Piezas, 
                                                                        infoInventario.StockVerificado.Piezas,
                                                                        infoInventario.Stock.Cajas,
                                                                        infoInventario.StockVerificado.Cajas,
                                                                        infoInventario.Stock.Combos,
                                                                        infoInventario.StockVerificado.Combos,
                                                                        infoInventario.BultosConSTLsinSTF.Piezas,
                                                                        infoInventario.BultosSinSTLconSTF.Piezas,
                                                                        infoInventario.BultosConSTLsinSTF.Cajas,
                                                                        infoInventario.BultosSinSTLconSTF.Cajas,
                                                                        infoInventario.BultosConSTLsinSTF.Combos,
                                                                        infoInventario.BultosSinSTLconSTF.Combos,
                                                                        infoInventario.PiezasFueraContenedorEnStock,
                                                                        infoInventario.PiezasFueraContenedorSinStock,
                                                                        infoInventario.BultosSinRegistracion,
                                                                        infoInventario.BultosEnPedidosAbiertosNoExisten);
                        if (MessageBox.Show(msg, "Ajuste de Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CStatusListProgressBar.ShowStatusListProgressBar("Iniciando Ajuste de Stock....");
                            CStatusListProgressBar.SetTextActivityAction("Realizando ajuste de bultos con stock no existentes..");
                            if (CDb.EjecutarAjusteInventario_BultosConStockSinExistenciaFisica(fInventario, ref infoInventario))
                            {
                                CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} piezas como Egresadas", infoInventario.BultosConSTLsinSTFAjustados.Piezas));
                                CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} cajas como Egresadas", infoInventario.BultosConSTLsinSTFAjustados.Cajas));
                                CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} combos como Egresados", infoInventario.BultosConSTLsinSTFAjustados.Combos));

                                CStatusListProgressBar.SetTextActivityAction("Realizando ajuste de bultos sin stock pero existentes..");
                                if (CDb.EjecutarAjusteInventario_BultosSinStockConExistenciaFisica(fInventario, ref infoInventario))
                                {
                                    CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} piezas Reingresandolas a Stock", infoInventario.BultosSinSTLconSTFAjustados.Piezas));
                                    CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} cajas Reingresandolas a Stock", infoInventario.BultosSinSTLconSTFAjustados.Cajas));
                                    CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} combos Reingresandolos a Stock", infoInventario.BultosSinSTLconSTFAjustados.Combos));

                                    CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} piezas por no estar en el contenedor", infoInventario.PiezasFueraContenedorEnStockAjustadas));

                                    CStatusListProgressBar.SetTextActivityAction("Realizando ajuste por piezas fuera de contenedores sin stock ..");
                                    if (CDb.EjecutarAjusteInventario_PiezasFueraDeContenedorSinStock(fInventario, ref infoInventario))
                                    {
                                        CStatusListProgressBar.SetTextActivityAction(String.Format("Se ajustaron {0} piezas por no estar en el contenedor", infoInventario.PiezasFueraContenedorSinStockAjustadas));
                                            
                                        CStatusListProgressBar.SetTextActivityAction("Realizando actualización de ubicaciones ..");
                                        if (CDb.EjecutarAjusteInventario_ActualizarUbicaciones(fInventario, ref infoInventario))
                                        {
                                            CStatusListProgressBar.SetTextActivityAction(String.Format("Se Actualizaron {0} ubicaciones de bultos ", infoInventario.BultosActualizadosEnHubicacion));
                                            CDb.GetTotalBultosEnStock(ref infoInventario);
                                            CStatusListProgressBar.SetTextActivityAction(String.Format("Stock luego del Ajuste Piezas= {0} Cajas= {1} Combos= {2}. ", infoInventario.StockDespuesAjuste.Piezas,
                                                                                                                                                                        infoInventario.StockDespuesAjuste.Cajas,
                                                                                                                                                                        infoInventario.StockDespuesAjuste.Combos));
                                            CStatusListProgressBar.SetTextActivityAction("Fin del Proceso de Ajuste.");
                                            CStatusListProgressBar.SetTextActivityAction("Generando el Reporte...");
                                                
                                            GenerarReporteAjusteStock(fInventario, infoInventario);

                                            CStatusListProgressBar.SetTextActivityAction("Registrando resultados del Inventario..");
                                            CDb.RegisterResultInventario(fInventario, infoInventario);
                                            CStatusListProgressBar.SetTextActivityAction("El proceso de Ajuste Finalizó con Exito.");
                                            CStatusListProgressBar.FillProgressBar();
                                        }
                                    }
                                    else
                                    {
                                        CStatusListProgressBar.SetTextActivityAction("(Error) Realizando ajuste de piezas sin stock ,existentes pero sin contenedor..");
                                        error = true;
                                    }
                                }
                                else
                                {
                                    CStatusListProgressBar.SetTextActivityAction("(Error) Realizando ajuste de bultos sin stock pero existentes..");
                                    error = true;
                                }
                            }
                            else
                            {
                                CStatusListProgressBar.SetTextActivityAction("(Error) Realizando ajuste de bultos con stock no existentes..");
                                error = true;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch(CDbException dbex)
                    {
                        CStatusProgressBar.CloseForm();
                        MessageBox.Show("Error de Base de Datos realizando el proceso de ajuste de inventario. Verifique integridad y conexion con la base de datos !!!\rDetalle: "+ dbex.Message, "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El inventario seleccionado no existe !!!", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    error = true;
                }
            }
            else
            {
                MessageBox.Show("Operacion no permitida para un Usuario no Supervisor", "Control de Acceso a usuarios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

        }
        private async void button_AjustarStock_Click(object sender, EventArgs e)
        {
            using(new WaitCursor())
            {
                await Task.Run(() =>
                {
                    AjusteStockPorInventario();
                });
            }
        }


        private bool GenerarReportDetalleInventario(DateTime fechaInventario, int TotalPiezasEnStock, int TotalPiezasEnStockVerificadas, int PiezasConStockSinExistencia, int PiezasSinStockConExistencia, int PiezasSinRegistracion)
        {
            bool generado = false;

            string infoFiltroFecha = String.Format(" FECHA DE GENERACIÓN DEL REPORTE : {0} BASADO EN EL INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fechaInventario.ToShortDateString());

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("DET_INVENTARIO");
            dt.Columns.Add(new DataColumn("Resultado", typeof(string)));
            dt.Columns.Add(new DataColumn("Unidades", typeof(int)));

            DataRow dr = dt.NewRow();
            dr["Resultado"] = "Total Piezas en Stock";
            dr["Unidades"] = TotalPiezasEnStock;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Resultado"] = "Total Piezas en Stock Verificadas";
            dr["Unidades"] = TotalPiezasEnStockVerificadas;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Resultado"] = "Piezas en Stock sin Existencia";
            dr["Unidades"] = PiezasConStockSinExistencia;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Resultado"] = "Piezas sin Stock con Existencia";
            dr["Unidades"] = PiezasSinStockConExistencia;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Resultado"] = "Piezas sin Registración";
            dr["Unidades"] = PiezasSinRegistracion;
            dt.Rows.Add(dr);

            ds.Tables.Add(dt);

            CPdfStampaGridReport pdf = new CPdfStampaGridReport()
            {
                PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                DataSource = ds.Tables[0],
                NameFileFormatXML = "FormatoRepDetalleInventario",
                NameFilePDF = "INFORME DE INVENTARIO",
                ReportDescriptionLine1 = infoFiltroFecha,
                TitleDescription = "INFORME DE INVENTARIO"
            };
            generado = pdf.Create();
            return generado;
        }

        /// <summary>
        /// Genera un reporte PDF con la informacion resultante del inventario y los valores de stock luego del ajuste. 
        /// </summary>
        /// <returns></returns>
        private bool GenerarReporteAjusteStock(DateTime fechaInventario, CResultInventario infoInventario)
        {
            bool generado = false;

            string infoFiltroFecha = String.Format(" FECHA DE GENERACIÓN DEL REPORTE : {0} BASADO EN EL INVENTARIO: {1} ", DateTime.Now.ToShortDateString(), fechaInventario.ToShortDateString());

            DataSet ds = new DataSet();
            DataTable dt_detInventario = new DataTable("DET_INVENTARIO");
            dt_detInventario.Columns.Add(new DataColumn("Resultado", typeof(string)));
            dt_detInventario.Columns.Add(new DataColumn("Unidades", typeof(int)));

            DataRow dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Piezas en Stock";
            dr["Unidades"] = infoInventario.Stock.Piezas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Piezas en Stock Verificadas";
            dr["Unidades"] = infoInventario.StockVerificado.Piezas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Cajas en Stock";
            dr["Unidades"] = infoInventario.Stock.Cajas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Cajas en Stock Verificadas";
            dr["Unidades"] = infoInventario.StockVerificado.Cajas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Combos en Stock";
            dr["Unidades"] = infoInventario.Stock.Combos;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Total Combos en Stock Verificadas";
            dr["Unidades"] = infoInventario.StockVerificado.Combos;
            dt_detInventario.Rows.Add(dr);
            
            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Bultos sin Registración";
            dr["Unidades"] = infoInventario.BultosSinRegistracion;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Bultos en Pedidos Abiertos que no existen.";
            dr["Unidades"] = infoInventario.BultosEnPedidosAbiertosNoExisten;
            dt_detInventario.Rows.Add(dr);

            ds.Tables.Add(dt_detInventario);

            DataTable dt_resultAjuste = new DataTable("RESULT_AJUSTE");
            dt_resultAjuste.Columns.Add(new DataColumn("Resultado", typeof(string)));
            dt_resultAjuste.Columns.Add(new DataColumn("Unidades", typeof(int)));

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Piezas en Stock sin Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosConSTLsinSTFAjustados.Piezas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Cajas en Stock sin Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosConSTLsinSTFAjustados.Cajas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Combos en Stock sin Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosConSTLsinSTFAjustados.Combos;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Piezas sin Stock con Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosSinSTLconSTFAjustados.Piezas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Cajas sin Stock con Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosSinSTLconSTFAjustados.Cajas;
            dt_detInventario.Rows.Add(dr);

            dr = dt_detInventario.NewRow();
            dr["Resultado"] = "Combos sin Stock con Existencia (AJUSTADAS)";
            dr["Unidades"] = infoInventario.BultosSinSTLconSTFAjustados.Combos;
            dt_detInventario.Rows.Add(dr);


            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Piezas fuera de Contenedores en Stock (AJUSTADAS)";
            dr["Unidades"] = infoInventario.PiezasFueraContenedorEnStockAjustadas;
            dt_resultAjuste.Rows.Add(dr);

            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Piezas fuera de Contenedores sin Stock (AJUSTADAS)";
            dr["Unidades"] = infoInventario.PiezasFueraContenedorSinStockAjustadas;
            dt_resultAjuste.Rows.Add(dr);

            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Bultos Actualizados en su Ubicación";
            dr["Unidades"] = infoInventario.BultosActualizadosEnHubicacion;
            dt_resultAjuste.Rows.Add(dr);

            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Piezas en Stock luego del Ajuste";
            dr["Unidades"] = infoInventario.StockDespuesAjuste.Piezas;
            dt_resultAjuste.Rows.Add(dr);

            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Cajas en Stock luego del Ajuste";
            dr["Unidades"] = infoInventario.StockDespuesAjuste.Cajas;
            dt_resultAjuste.Rows.Add(dr);

            dr = dt_resultAjuste.NewRow();
            dr["Resultado"] = "Combos en Stock luego del Ajuste";
            dr["Unidades"] = infoInventario.StockDespuesAjuste.Combos;
            dt_resultAjuste.Rows.Add(dr);

            ds.Tables.Add(dt_resultAjuste);

            CPdfStampaGridsReport pdf = new CPdfStampaGridsReport()
            {
                PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                DataSource = ds,
                NameFileFormatXML = "FormatoRepResultadoAjusteInventario",
                NameFilePDF = "INFORME DE AJUSTE DE INVENTARIO",
                ReportDescriptionLine1 = infoFiltroFecha,
                TitleDescription = "INFORME DE AJUSTE DE INVENTARIO"
            };
            generado = pdf.Create();
            return generado;
        }

    }


}

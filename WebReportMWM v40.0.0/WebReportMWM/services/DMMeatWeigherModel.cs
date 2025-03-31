namespace WebReportMWM.services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using WebReportMWM.Models;
    using WebReportMWM.Models.Entitys;
    public partial class DMMeatWeigherModel : DbContext
    {
        public DMMeatWeigherModel()
            : base("name = DMMeatWeigherModel")
        {
        }
        public virtual DbSet<Operadores> operadores { get; set; }

        public virtual DbSet<TipoProducto> TiposProducto { get; set; }

        public virtual DbSet<Producto> Productos { get; set; }

        public virtual DbSet<Sector> Sectores { get; set; }

        public virtual DbSet<TipoBulto> TiposBulto { get; set; }

        public virtual DbSet<TipoContenedor> TiposContenedor { get; set; }

        public virtual DbSet<DbLog> dbLog { get; set; }

        public virtual DbSet<Pesada> Pesadas { get; set; }

        public virtual DbSet<ProductoInsumo> ProductoInsumos { get; set; }

        public virtual DbSet<Caja> Cajas { get; set; }

        public virtual DbSet<Combo> Combos { get; set; }

        public virtual DbSet<Contenedor> Contenedores { get; set; }

        public virtual DbSet<MovInsumo> MovInsumos { get; set; }

        public virtual DbSet<Destino> Destinos { get; set; }

        public virtual DbSet<Inventario> Inventario { get; set; }

        public virtual DbSet<DLP> DLP { get; set; }

        public virtual DbSet<OrdenIngreso> OrdenIngreso { get; set; }

        public virtual DbSet<Etiqueta> Etiquetas { get; set; }

        public virtual DbSet<Tipificacion> Tipificaciones { get; set; }
    }
}
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Razor.Parser.SyntaxTree;

namespace WebReportMWM.HelperMethodsRepository
{
    public static class HtmlExtensions
    {

        /// <summary>
        /// Genera codigo HTML de un input checkbox con capacidad de edicion.
        /// El id y clase es nombrado con el prefijo "label-" para el caso estatico y "edited-" 
        /// para el caso editable.
        /// 
        /// En el caso de usarlo en el parametro format de un web grid se requiere envolver con
        /// un span como se muestra a continuacion:
        /// ,format: @<span>   @Html.WGEditCheckBox("EsInsumo", (bool?)item.EsInsumo)</span>)
        /// </summary>
        /// <param name="html">contexto html</param>
        /// <param name="name">nombre a asignar a la clase e id de los controles. 
        /// Se le agrega prefijo "label-" para el input statico y "edited-" para el editable.</param>
        /// <param name="value">valor inicial</param>
        /// <returns></returns>
        public static MvcHtmlString WGEditCheckBox(this HtmlHelper html, string name, bool? value)
        {
            return WGEditCheckBox(html, name, Convert.ToBoolean(value));
        }

        /// <summary>
        /// Genera codigo HTML de un input checkbox con capacidad de edicion.
        /// El id y clase es nombrado con el prefijo "label-" para el caso estatico y "edited-" 
        /// para el caso editable.
        /// 
        /// En el caso de usarlo en el parametro format de un web grid se requiere envolver con
        /// un span como se muestra a continuacion:
        /// ,format: @<span>   @Html.WGEditCheckBox("EsInsumo", (bool?)item.EsInsumo)</span>)
        /// </summary>
        /// <param name="html">contexto html</param>
        /// <param name="name">nombre a asignar a la clase e id de los controles. 
        /// Se le agrega prefijo "label-" para el input statico y "edited-" para el editable.</param>
        /// <param name="value">valor inicial</param>
        /// <returns></returns>
        public static MvcHtmlString WGEditCheckBox(this HtmlHelper html,string name,bool value)
        {
            TagBuilder inputLabel = new TagBuilder("input");
            inputLabel.Attributes.Add("class", "label-" + name);
            inputLabel.Attributes.Add("type", "checkbox");
            inputLabel.Attributes.Add("id", "label-" + name);
            inputLabel.Attributes.Add("value", value.ToString().ToLower());
            inputLabel.MergeAttribute("disabled","disabled");
            if(value)
                inputLabel.MergeAttribute("checked","checked");

            TagBuilder inputEdit = new TagBuilder("input");
            inputEdit.Attributes.Add("class", "edited-" + name);
            inputEdit.Attributes.Add("type", "checkbox");
            inputEdit.Attributes.Add("id", "edited-" + name);
            inputEdit.Attributes.Add("style", "display:none");
            if (value)
                inputEdit.MergeAttribute("checked", "checked");

            StringBuilder sb = new StringBuilder();
            sb.Append(inputLabel.ToString());
            sb.Append(inputEdit.ToString());

            return MvcHtmlString.Create(sb.ToString());
        }
        /// <summary>
        /// Genera codigo HTML de una lista de opciones (dropDownList) con capacidad de edicion.
        /// El id y clase es nombrado con el prefijo "label-" para el caso estatico y "edited-" 
        /// para el caso editable.
        /// 
        /// En el caso de usarlo en el parametro format de un web grid se requiere envolver con
        /// un span como se muestra a continuacion:
        /// ,format: @<span>   @Html.WGEditDropDownList("EsInsumo", (bool?)item.EsInsumo)</span>)
        /// </summary>
        /// <param name="html">contexto html</param>
        /// <param name="name">nombre a asignar a la clase e id de los controles. 
        /// Se le agrega prefijo "label-" para el input statico y "edited-" para el editable.</param>
        /// <param name="items">lista de opciones </param>
        /// <param name="initValueSelected">valor seleccionado inicialmente </param>
        /// <param name="initTextSelected">texto seleccionado inicialmente </param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString WGEditDropDownList(this HtmlHelper html, string name,IEnumerable items ,object initValueSelected,object initTextSelected)
        {
            TagBuilder spanLabel = new TagBuilder("span");
            spanLabel.Attributes.Add("class", "label-" + name);
            spanLabel.Attributes.Add("id", "label-" + name);
            spanLabel.Attributes.Add("value", initValueSelected?.ToString() ?? "");
            spanLabel.SetInnerText(initTextSelected?.ToString() ?? "");

            var dbEdit  = SelectExtensions.DropDownList(html,"edited-" + name, new SelectList(items, "Value", "Text", initValueSelected??""),
            new { @class = "edited-" + name, id = "edited-" + name, style = "display:none" });
            
            StringBuilder sb = new StringBuilder();
            sb.Append(spanLabel.ToString());
            sb.Append(dbEdit.ToHtmlString());

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Genera codigo HTML de una TextArea input con capacidad de edicion.
        /// El id y clase es nombrado con el prefijo "label-" para el caso estatico y "edited-" 
        /// para el caso editable.
        /// 
        /// En el caso de usarlo en el parametro format de un web grid se requiere envolver con
        /// un span como se muestra a continuacion:
        /// ,format: @<span>   @Html.WGEditTextArea(.....) <\span>
        /// </summary>
        /// <param name="html">contexto</param>
        /// <param name="name">nombre a asignar a la clase y id</param>
        /// <param name="value">texto a cargar inicialmente</param>
        /// <param name="rows">tamaño en filas</param>
        /// <param name="cols">tamaño en columnas</param>
        /// <param name="maxlength">largo maximo de texto admitido</param>
        /// <returns></returns>
        public static MvcHtmlString WGEditTextArea(this HtmlHelper html, string name,string value,int rows,int cols,int maxlength)
        {
            TagBuilder spanLabel = new TagBuilder("span");
            spanLabel.Attributes.Add("class", "label-" + name);
            spanLabel.Attributes.Add("id", "label-" + name);
            spanLabel.SetInnerText(value);

            var textEdit = TextAreaExtensions.TextArea(html, "edited-" + name, value,
                new { @class = "edited-" + name, id = "edited-" + name,
                    rows=rows,
                    cols= cols,
                    maxlength=maxlength,
                    style = "display:none" });

            StringBuilder sb = new StringBuilder();
            sb.Append(spanLabel.ToString());
            sb.Append(textEdit.ToHtmlString());

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Genera codigo HTML de una TextBox input con capacidad de edicion.
        /// Permite definir si la edicion es texto o numero, declarar el evento onkeypress.
        /// El id y clase es nombrado con el prefijo "label-" para el caso estatico y "edited-" 
        /// para el caso editable.
        /// 
        /// En el caso de usarlo en el parametro format de un web grid se requiere envolver con
        /// un span como se muestra a continuacion:
        /// ,format: @<span>   @Html.WGEditTextBox(.....) <\span>
        /// </summary>
        /// <param name="html">contexto</param>
        /// <param name="name">nombre a asignar a los tags</param>
        /// <param name="value">valor inicial de carga del control</param>
        /// <param name="isNumber">indica si la edicion es un numero</param>
        /// <param name="maxLength">maximo largo a editar</param>
        /// <param name="maxWidth">maximo ancho del control en px</param>
        /// <param name="minValue">minimo valor numerico que admite editar</param>
        /// <param name="onkeypressEvent">string que define la funcion que llama en un evento on key press Ejemplo: "return isDecimalNumberKey(event)"</param>
        /// <returns></returns>
        public static MvcHtmlString WGEditTextBox(this HtmlHelper html, string name, string value,bool isNumber ,int maxLength, int maxWidth,int minValue=1, string onkeypressEvent="")
        {
            TagBuilder spanLabel = new TagBuilder("span");
            spanLabel.Attributes.Add("class", "label-" + name);
            spanLabel.Attributes.Add("id", "label-" + name);
            spanLabel.SetInnerText(value);

            TagBuilder inputEdit = new TagBuilder("input");
            inputEdit.Attributes.Add("class", "edited-" + name);
            inputEdit.Attributes.Add("id", "edited-" + name);
            inputEdit.Attributes.Add("maxlength", maxLength.ToString());
            inputEdit.Attributes.Add("type", isNumber ? "number" : "text");
            if (isNumber)
                inputEdit.Attributes.Add("min", minValue.ToString());
            if(onkeypressEvent!= "")
                inputEdit.Attributes.Add("onkeypress", onkeypressEvent);
            inputEdit.Attributes.Add("style", "display:none;max-width:"+maxWidth+"px;");
            inputEdit.Attributes.Add("value", value);

            StringBuilder sb = new StringBuilder();
            sb.Append(spanLabel.ToString());
            sb.Append(inputEdit.ToString());

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
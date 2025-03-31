using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTPDF
{
    public class ColumnReportPDFCollection : IList<ColumnReportPDF>
    {
        private Dictionary<string, ColumnReportPDF> _dictionary;
        

        public ColumnReportPDFCollection()
        {
            _dictionary = new Dictionary<string, ColumnReportPDF>();
        }

        public ColumnReportPDF this[string name]
        {
            get
            {
                return _dictionary[name];
            }
        }

        public ColumnReportPDF this[int index] 
        {
            get
            {
                return _dictionary.ElementAt(index).Value;
            }
            set
            {
                var obj = _dictionary.ElementAt(index).Value;
                obj = value;
            }
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(ColumnReportPDF item)
        {
            _dictionary.Add(item.Name,item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(ColumnReportPDF item)
        {
            return _dictionary.ContainsKey(item.Name);
        }

        public bool Contains(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(ColumnReportPDF[] array, int arrayIndex)
        {
            foreach(ColumnReportPDF item in _dictionary.Values)
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<ColumnReportPDF> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        public int IndexOf(ColumnReportPDF item)
        {
            return _dictionary.Keys.ToList().IndexOf(item.Name);
        }

        public void Insert(int index, ColumnReportPDF item)
        {
        }

        public bool Remove(ColumnReportPDF item)
        {
            return _dictionary.Remove(item.Name);
        }

        public void RemoveAt(int index)
        {
            _dictionary.Remove(_dictionary.ElementAt(index).Key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

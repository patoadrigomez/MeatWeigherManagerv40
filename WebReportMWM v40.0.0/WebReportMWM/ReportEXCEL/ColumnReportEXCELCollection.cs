using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTEXCEL
{
    public class ColumnReportEXCELCollection : IList<ColumnReportEXCEL>
    {
        private Dictionary<string, ColumnReportEXCEL> _dictionary;
        

        public ColumnReportEXCELCollection()
        {
            _dictionary = new Dictionary<string, ColumnReportEXCEL>();
        }

        public ColumnReportEXCEL this[string name]
        {
            get
            {
                return _dictionary[name];
            }
        }

        public ColumnReportEXCEL this[int index] 
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

        public void Add(ColumnReportEXCEL item)
        {
            _dictionary.Add(item.Name,item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(ColumnReportEXCEL item)
        {
            return _dictionary.ContainsKey(item.Name);
        }

        public bool Contains(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(ColumnReportEXCEL[] array, int arrayIndex)
        {
            foreach(ColumnReportEXCEL item in _dictionary.Values)
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<ColumnReportEXCEL> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        public int IndexOf(ColumnReportEXCEL item)
        {
            return _dictionary.Keys.ToList().IndexOf(item.Name);
        }

        public void Insert(int index, ColumnReportEXCEL item)
        {
        }

        public bool Remove(ColumnReportEXCEL item)
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

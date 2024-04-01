using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Tables
{
    public class Table<TRow, TColumn, TValue>
    {
        private readonly Dictionary<TRow, Dictionary<TColumn, TValue>> _data = new Dictionary<TRow, Dictionary<TColumn, TValue>>();
        public OpenIndexer Open => new OpenIndexer(this);
        public ExistedIndexer Existed => new ExistedIndexer(this);

        public void AddRow(TRow row)
        {
            if (!_data.ContainsKey(row))
                _data[row] = new Dictionary<TColumn, TValue>();
        }

        public void AddColumn(TColumn column)
        {
            foreach (var rowData in _data)
            {
                if (!_data[rowData.Key].ContainsKey(column))
                {
                    _data[rowData.Key][column] = default(TValue);
                }
            }
        }

        public TValue this[TRow row, TColumn column]
        {
            get
            {
                if (_data.TryGetValue(row, out var columnData) && columnData.TryGetValue(column, out var value))
                    return value;
                return default(TValue);
            }
            set
            {
                AddRow(row);
                if (!_data[row].ContainsKey(column))
                {
                    AddColumn(column);
                }
                _data[row][column] = value;
            }
        }

        public IEnumerable<TRow> Rows => _data.Keys;
        public IEnumerable<TColumn> Columns => _data.Count > 0 ? _data.Values.First().Keys : Enumerable.Empty<TColumn>();

        public class OpenIndexer
        {
            private readonly Table<TRow, TColumn, TValue> _table;

            public OpenIndexer(Table<TRow, TColumn, TValue> table)
            {
                _table = table;
            }

            public TValue this[TRow row, TColumn column]
            {
                get => _table[row, column];
                set => _table[row, column] = value;
            }
        }

        public class ExistedIndexer
        {
            private readonly Table<TRow, TColumn, TValue> _table;

            public ExistedIndexer(Table<TRow, TColumn, TValue> table)
            {
                _table = table;
            }

            public TValue this[TRow row, TColumn column]
            {
                get
                {
                    if (!_table._data.ContainsKey(row) || !_table._data[row].ContainsKey(column))
                        throw new ArgumentException();
                    return _table[row, column];
                }
                set
                {
                    if (!_table._data.ContainsKey(row) || !_table._data[row].ContainsKey(column))
                        throw new ArgumentException();
                    _table[row, column] = value;
                }
            }
        }
    }
}
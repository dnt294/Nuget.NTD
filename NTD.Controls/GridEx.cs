using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace NTD.Controls
{
    public class GridEx : Grid
    {
        /// <summary>
        /// Rows Auto
        /// </summary>
        public string RowAuto
        {
            get { return (string)GetValue(RowAutoProperty); }
            set { SetValue(RowAutoProperty, value); }
        }
        public static readonly DependencyProperty RowAutoProperty = DependencyProperty.Register(
            "RowAuto", typeof(string), typeof(GridEx), new PropertyMetadata(null,new PropertyChangedCallback(OnRowAutoChanged)));

        private static void OnRowAutoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as GridEx;
            if (d == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            grid.OnRowAutoChanged(e);
        }
        private void OnRowAutoChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RowDefinitions.Clear();
            string format = e.NewValue as string;
            if (string.IsNullOrEmpty(format)) throw new ArgumentException("Row Auto wrong format");
            this.RowDefinitions.Clear();
            //
            string[] rows = format.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows)
            {
                if (row.ToLower().Equals("auto"))
                {
                    this.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                }
                else if (row.Equals("*"))
                {
                    this.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    });
                }
                else if (row.EndsWith("*"))
                {
                    double length;
                    if (double.TryParse(row.Substring(0, row.Length - 1), out length))
                    {
                        this.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(length, GridUnitType.Star)
                        });
                    }
                }
                else
                {
                    this.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(double.Parse(row))
                    });
                }
            }
        }

        /// <summary>
        /// Columns Auto
        /// </summary>
        public string ColumnAuto
        {
            get { return (string)GetValue(ColumnAutoProperty); }
            set { SetValue(ColumnAutoProperty, value); }
        }
        public static readonly DependencyProperty ColumnAutoProperty = DependencyProperty.Register(
            "ColumnAuto", typeof(string), typeof(GridEx), new PropertyMetadata(null,new PropertyChangedCallback(OnColumnAutoChanged)));
        private static void OnColumnAutoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as GridEx;
            if (d == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            grid.OnColumnAutoChanged(e);
        }
        private void OnColumnAutoChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ColumnDefinitions.Clear();
            string format = e.NewValue as string;
            if (string.IsNullOrEmpty(format)) throw new ArgumentException("Column Auto wrong format");
            this.ColumnDefinitions.Clear();
            //
            string[] columns = format.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var column in columns)
            {
                if (column.ToLower().Equals("auto"))
                {
                    this.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                }
                else if (column.Equals("*"))
                {
                    this.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
                }
                else if (column.EndsWith("*"))
                {
                    double length;
                    if (double.TryParse(column.Substring(0, column.Length - 1), out length))
                    {
                        this.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(length, GridUnitType.Star)
                        });
                    }
                }
                else
                {
                    this.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(double.Parse(column))
                    });
                }
            }
        }
    }
}

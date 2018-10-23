using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokemonStats_WPF.Helper {
    public static class Utility {
        /// <summary>
		/// Checks result of a row.
		/// </summary>
		/// <returns><c>true</c>, if row result was checked, <c>false</c> otherwise.</returns>
		/// <param name="reader"><c>IDataReader</c> object.</param>
		/// <param name="columnName">Column name.</param>
		public static bool Check(IDataReader reader, string columnName) {
            if (!DBNull.Value.Equals(reader[columnName])) {
                return true;
            } else {
                return false;
            }
        }

        public static T CheckValue<T>(this IDataReader reader, string columnName) where T : struct {
            if (DBNull.Value.Equals(reader[columnName])) {
                return default(T);
            }
            return (T)reader[columnName];
        }

        public static T CheckObject<T>(this IDataReader reader, string columnName) where T : class {
            if (DBNull.Value.Equals(reader[columnName])) {
                return default(T);
            }
            return reader[columnName] as T;
        }

        public static bool TryGetAs<T>(this IDataReader reader, out T value, string columnName) where T : class {
            if (DBNull.Value.Equals(reader[columnName])) {
                value = default(T);
                return false;
            } else {
                value = (T)reader[columnName];
                return true;
            }
        }

        public static double MetersToFeet(double meters){
            return meters / 0.3048;
        }

        public static double FeetToInches(double feet) {
            return feet * 12;
        }

        public static double KgToLb(double kg) {
            return kg / 0.45359237;
        }

        public static ImageSource ByteToImage(byte[] imageData) {
            BitmapImage biImg = new BitmapImage();
            biImg.BeginInit();
            biImg.StreamSource = new MemoryStream(imageData);
            biImg.EndInit();
            return biImg as ImageSource;
        }
    }
}
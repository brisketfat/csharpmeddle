using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.IO.Compression;


namespace ConsoleApp1
{
    public static class StringExtensions
    {
        //private static readonly ILogger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //I feel like this logging here is unnecessary overhead... I'll leave it commented here in case we need it prior to launch
        public static bool IsNullOrEmpty(this string theString)
        {
            if (theString != null)
            {
                theString = theString.Trim();
            }
            return String.IsNullOrEmpty(theString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ToNotNullString(this object o,string replacement)
        {
            if (o == null) return replacement;
            if (o.ToString().IsNullOrEmpty()) return replacement;
            return o.ToString().Trim();
        }
        public static int ToInt(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            if (!str.IsNumeric()) return 0;
            var retVal = Regex.Replace(str, "[^.0-9]", "");
            retVal = retVal.Replace(" ", "").Trim();
            if (retVal.Contains("."))
            {
                var val = retVal.Split(Convert.ToChar("."));
                retVal = val[0];
            }
            if (retVal.Length < 1 || retVal.Length >= 10) return 0;
            int result;
            var retInt = int.TryParse(retVal, out result) ? (int?)result : 0;
            return (int)retInt;
        }

        public static int ToInt(this decimal d)
        {
            return decimal.ToInt32(d);
        }

        public static DateTime? NullableDateTryParse(string text)
        {
            DateTime date;
            if (DateTime.TryParse(text, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        public static decimal ToDec(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            if (!str.IsNumeric()) return 0;
            var retVal = Regex.Replace(str, "[^.0-9]", "");
            retVal = retVal.Replace(" ", "").Trim();
            if (str.Length < 1 || str.Length >= 15) return 0;
            decimal result;
            var retDec = decimal.TryParse(retVal, out result) ? (int?)result : 0;
            return (decimal)retDec;
        }
        public static string ToMoneyString(this decimal dec)
        {
            if (dec < 1 && dec > 0)
            {
                return dec.ToString().Replace("0.", "") + "¢";
            }
            if (dec >= 1000000)
            {
                var tmp = dec / 1000000;
                return "$" + Math.Round(tmp, 3).ToString("G29") + " Million";
            }
            //if (dec > 1000)
            //{
            //	var tmp = dec / 1000;
            //	return "$" + Math.Round(tmp, 0) + " Thousand";
            //}

            return "$" + String.Format("{0:n0}", dec);
        }

        public static string ToMoneyDecString(this decimal dec)
        {
            if (dec < 1 && dec > 0)
            {
                return dec.ToString().Replace("0.", "") + "¢";
            }
            if (dec >= 1000000)
            {
                var tmp = dec / 1000000;
                return "$" + Math.Round(tmp, 3).ToString("G29") + " Million USD";
            }
            //if (dec > 1000)
            //{
            //	var tmp = dec / 1000;
            //	return "$" + Math.Round(tmp, 0) + " Thousand";
            //}

            return "$" + String.Format("{0:n0}", dec);
        }

        public static string ToMoneyString(this string str)
        {
            var x = str.ToDec();
            return x.ToMoneyString();
        }

        public static string ConvertQuotes(this string str)
        {
            return str.Replace("'", "''");
        }

        public static string ToCleanPhone(this string str)
        {
            if (str.Length < 10) return "";
            var x = new String(str.Where(Char.IsNumber).ToArray());
            x = x.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            if (x.Length < 10) return "";
            return x.Substring(0, 10);
        }

        public static string ToFormattedPhone(this string phoneStr)
        {
            phoneStr = new string(phoneStr.Where(c => char.IsDigit(c)).ToArray());  // remove non-numerical characters
            var p = phoneStr.ToArray();
            var phoneNumber = p[0].ToString() + p[1].ToString() + p[2].ToString() + "." +
                                p[3].ToString() + p[4].ToString() + p[5].ToString() + "." +
                                p[6].ToString() + p[7].ToString() + p[8].ToString() + p[9].ToString();
            return phoneNumber;
        }


        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;

            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUrl(this string url)
        {
            if (url.IsNullOrEmpty()) return false;
            System.Uri uriResult;
            bool validUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            if (uriResult != null)
            {
                validUrl = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }

            return validUrl;
        }

        public static string ToCapitalized(this string value)
        {
            var array = value.ToCharArray();
            if (array.Length >= 1 && char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ') continue;
                if (!char.IsLower(array[i])) continue;
                if (char.IsNumber(array[i])) continue;
                array[i] = char.ToUpper(array[i]);
            }
            return new string(array);
        }



        public static int ToNumeric(this string str)
        {
            if (!str.IsNullOrEmpty())
            {
                var digitsOnly = new Regex(@"[^\d.]");
                var ret = digitsOnly.Replace(str, "");
                return ret.ToInt();
            }
            return 0;
        }

        public static string Clean(this string str)
        {
            if (str.IsNullOrEmpty()) return "";
            str = Regex.Replace(str, @"<br />", System.Environment.NewLine).Trim();
            str = Regex.Replace(str, @"<br>", System.Environment.NewLine).Trim();
            str = Regex.Replace(str, @"<br/>", System.Environment.NewLine).Trim();
            str = Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();
            str = Regex.Replace(str, @"[\""]", "", RegexOptions.None);
            str = Regex.Replace(str, @"\s{2,}", " ");
            str = str.Replace("\"", "\\\"");
            str = Regex.Replace(str, "[^A-Za-z0-9 _.,!?()+%';:/&\\-\\*@$]*", "");
            str = str.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');


            return str;
        }

        public static string ToTitleCase(this string str)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            var ret = str;
            try
            {
                ret = textInfo.ToTitleCase(str.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }

        private static string ToCamelCase(this string phrase)
        {
            var result = "";
            if (phrase == null || phrase.Length < 2)
                return phrase;
            var words = phrase.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1)
            {
                result = words[0].ToLower();
                for (var i = 1; i < words.Length; i++)
                {
                    result +=
                        words[i].Substring(0, 1).ToUpper() +
                        words[i].Substring(1);
                }
            }
            else
            {
                result = phrase.Substring(0, 1).ToLower() + phrase.Substring(1, phrase.Length - 1);
            }

            return result;
        }

        public static string ToShortDate(this string str)
        {
            var ret = "";
            if (str.IsNullOrEmpty()) return ret;

            try
            {
                ret = DateTime.Parse(str).ToString("MM/dd/yyyy");
                return ret;
            }
            catch
            {
                ret = str.Substring(0, 10);
                return ret;
            }
        }

        public static string ToCentsString(this string str)
        {
            return str.IsNullOrEmpty() ? "" : str.Replace("000", "¢");
        }


        public static string ToYesNo(this int val)
        {
            var b = val.ToBool();
            return b.GetValueOrDefault().ToYesNo();
        }
        public static string ToYesNo(this string val)
        {
            if (val.IsNumeric()) return Convert.ToInt32(val).ToYesNo();
            if (val.IsBoolean()) return Convert.ToBoolean(val).ToYesNo();
            return null;
        }
        public static string ToYesNo(this bool? val)
        {
            return val.GetValueOrDefault() ? "yes" : "no";
        }
        public static string ToYesNo(this bool val)
        {
            return val ? "yes" : "no";
        }

        public static bool IsBoolean(this string val)
        {
            bool flag;
            return Boolean.TryParse(val, out flag);
        }

        public static bool IsNumeric(this string val)
        {
            try
            {
                if (val.IsNullOrEmpty()) return false;
                decimal n;
                return decimal.TryParse(val, out n);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool? ToBool(this int num)
        {
            return num != 0 && num > 0;
        }

        public static bool? ToBool(this string val)
        {
            if (val.IsNullOrEmpty()) return null;
            if (val.ToUpper() == "TRUE" || val.ToUpper() == "YES" || val.ToUpper() == "1") return true;
            return false;
        }


        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        //public static string Join(this IEnumerable<object> array, List<KeyValuePair<int, bool>> seperator)
        //{
        //    return array == null ? "" : String.Join(seperator, array.ToArray());
        //}

        public static string Join(this object[] array, string seperator)
        {
            return array == null ? "" : String.Join(seperator, array);
        }

        public static string ValidatePath(this string p)
        {
            return ValidatePath(p, false);
        }

        public static string ValidatePath(this string p, bool archive)
        {
            try
            {
                string folder;
                if (p.Substring(p.Length - 4, 4).Contains("."))
                {
                    folder = p.Substring(0, p.LastIndexOf(@"\", StringComparison.Ordinal));
                    if (File.Exists(p) && archive)
                    {
                        var arcFolder = folder.Substring(0, folder.LastIndexOf(@"\", StringComparison.Ordinal)) +
                                        @"\Archive";
                        var f = new FileInfo(p);
                        var newFi = f.Name.Replace(".", DateTime.Now.ToString("-MM.dd.yyyy-HH.mm.ss."));
                        newFi = @arcFolder + @"\" + newFi;
                        File.Move(p, newFi);
                    }
                }
                else
                {
                    folder = p;
                }

                var isExists = Directory.Exists(folder);
                if (!isExists)
                {
                    Directory.CreateDirectory(folder);
                }

                if (p.EndsWith(@"\"))
                {
                    p = folder.Substring(0, p.Length - 1);
                }
                return p;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return p;
        }

        public static String FileSystemSize(this long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength - 3) + "..."
                   );
        }

        public static string ToSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = str.Replace("--", "-");
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static bool FileExists(this string f)
        {
            return File.Exists(f);
        }

        

        public static string Truncate(this string value, int maxChars)
        {
            if (value.IsNullOrEmpty()) return "";
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + " ..";
        }

      
      
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static string ToXml(this Hashtable table)
        {
            var keys = table.Keys;
            XDocument xdoc = new XDocument(new XElement("AsyncRequest"));
            foreach (var key in keys)
            {
                string elName = key.ToString();
                var elVal = table[key.ToString()] ?? DBNull.Value;
                var el = new XElement(elName.Replace("@",""), elVal);
                xdoc.Element("AsyncRequest").Add(el);
            }
            return xdoc.ToString();
        }

        public static string ToXml(this DataTable table, int metaIndex = 0)
        {
            XDocument xdoc = new XDocument(
                new XElement(table.TableName,
                    from column in table.Columns.Cast<DataColumn>()
                    where column != table.Columns[metaIndex]
                    select new XElement(column.ColumnName,
                        from row in table.AsEnumerable()
                        select new XElement(row.Field<string>(metaIndex), row[column])
                        )
                    )
                );

            return xdoc.ToString();
        }

    }
    }

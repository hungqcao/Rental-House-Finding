using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RentalHouseFinding.RHF.Common
{
    public class StringUtil
    {
        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };
        //Viết hoa các chữ cái đầu
        public static string ToTitleCase(string s)
        {
            s = s.ToLower();
            char[] charArr = s.ToCharArray();
            charArr[0] = Char.ToUpper(charArr[0]);
            foreach (Match m in Regex.Matches(s, @"(\s\S)"))
            {
                charArr[m.Index + 1] = m.Value.ToUpper().Trim()[0];
            }
            return new String(charArr);
        }
        //Xóa dấu tiếng Việt
        public static string RemoveSign4VietnameseString(string str)
        {


            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
    }
}
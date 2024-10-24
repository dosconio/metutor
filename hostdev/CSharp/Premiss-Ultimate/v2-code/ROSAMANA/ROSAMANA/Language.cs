using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROSAMANA
{
    public static class Language
    {
        public static byte mode;
        //Language()
            //mode00 EN(USA)
            //mode01 JP(Kira)
        //{
            //this.mode = p_mode;
        //}
        public static string Lang(string str)
            //From EN(USA)
        {
            
            if (mode == 0) return str;
            if (mode == 1)
            {
                str = str.Substring(0, 1) == "&" ? str.Substring(1) : str;
                switch (str.ToLower())
                {

                    case "program":
                        return "&P 程式";
                    case "setting":
                        return "&S 設定";
                    case "expack":
                        return "&P パッケージ";
                    case "view":
                        return "&V 表示";
                    case "rose":
                        return "&R 薔薇";
                    case "load":
                        return "読み込み";
                    case "new":
                    case "add":
                        return "ナヨ";
                    case "refresh":
                        return "リフレッシュ";
                    case "save":
                        return "保存する";
                    case "cosave":
                        return "名前を付けて保存";
                    case "close":
                        return "すんー";
                    case "exit":
                        return "終了";
                    case "list":
                        return "リスト";
                    case "codeview":
                        return "コードー表示";
                    case "bindfolder":
                        return "縛るフォルダ";
                    case "addelm":
                        return "なヨ素";
                    case "property":
                        return "プロパティ";
                    case "menu":
                        return "メニュー";
                    case "partition":
                        return "パーティション";
                    case "name":
                        return "名称";
                    case "author":
                        return "作者";
                    case "createdatetime":
                        return "出産時";
                    case "lastdatetime":
                        return "最終編集時刻";
                    case "source":
                        return "源/を素";
                    default:
                        return null;
                }
            }


            return null;
        }
    }
}

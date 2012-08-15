using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.CheckHelper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using CBB.MongoDB;

namespace Moooyo.BiZ.FilterWord
{
    //邱志明
    //2012-02-21
    //过滤字符处理器
    public class FilterWordController
    {
        private static CBB.CheckHelper.FilterWord _fw_delete;
        private static CBB.CheckHelper.FilterWord _fw_shift;
        private static CBB.CheckHelper.FilterWord _fw_trial;
        private static CBB.CheckHelper.FilterWord _temp_del;

        static List<string> list = new List<string>() { "！","·"," ", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "@", "[", "]", "^", "_", "`", "{", "|", "}", "~", "", "￠", "￡", "¤", "￥", "|", "§", "¨", "-", "ˉ", "°", "±", "′", "μ", "·", "o", "à", "á", "è", "é", "ê", "ì", "í", "D", "ò", "ó", "×", "ù", "ú", "ü", "Y", "T", "à", "á", "a", "è", "é", "ê", "ì", "í", "e", "ò", "ó", "÷", "ù", "ú", "ü", "y", "t", "ā", "ā", "ē", "ē", "ě", "ě", "ī", "ī", "ń", "ň", "ō", "ō", "ū", "ū", "∥", "ǎ", "ǎ", "ǐ", "ǐ", "ǒ", "ǒ", "ǔ", "ǔ", "ǖ", "ǖ", "ǘ", "ǘ", "ǚ", "ǚ", "ǜ", "ǜ", "ɑ", "ɡ", "ˇ", "ˉ", "ˊ", "ˋ", "˙", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π",  "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", "Ё", "Б", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л", "П", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", "а", "б", "в", "г", "д", "е", "ж", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", "ё", "‐", "–", "—", "―", "‖", "'", "'", "‥", "…", "‰", "′", "″", "‵", "※", "￣", "€", "℃", "℅", "℉", "№", "℡", "Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ", "Ⅵ", "Ⅶ", "Ⅷ", "Ⅸ", "Ⅹ", "Ⅺ", "Ⅻ", "ⅰ", "ⅱ", "ⅲ", "ⅳ", "ⅴ", "ⅵ", "ⅶ", "ⅷ", "ⅸ", "ⅹ", "←", "↑", "→", "↓", "↖", "↗", "↘", "↙", "∈", "∏", "∑", "∕", "°", "√", "∝", "∞", "∟", "∠", "∣", "∥", "∧", "∨", "∩", "∪", "∫", "∮", "∴", "∵", "∶", "∷", "～", "∽", "≈", "≌", "≒", "≠", "≡", "≤", "≥", "≦", "≧", "≮", "≯", "⊕", "⊙", "⊥", "⊿", "⌒", "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩", "⑴", "⑵", "⑶", "⑷", "⑸", "⑹", "⑺", "⑻", "⑼", "⑽", "⑾", "⑿", "⒀", "⒁", "⒂", "⒃", "⒄", "⒅", "⒆", "⒇", "⒈", "⒉", "⒊", "⒋", "⒌", "⒍", "⒎", "⒏", "⒐", "⒑", "⒒", "⒓", "⒔", "⒕", "⒖", "⒗", "⒘", "⒙", "⒚", "⒛", "─", "━", "│", "┃", "┄", "┅", "┆", "┇", "┈", "┉", "┊", "┋", "┌", "┍", "┎", "┏", "┐", "┑", "┒", "┓", "└", "┕", "┖", "┗", "┘", "┙", "┚", "┛", "├", "┝", "┞", "┟", "┠", "┡", "┢", "┣", "┤", "┥", "┦", "┧", "┨", "┩", "┪", "┫", "┬", "┭", "┮", "┯", "┰", "┱", "┲", "┳", "┴", "┵", "┶", "┷", "┸", "┹", "┺", "┻", "┼", "┽", "┾", "┿", "╀", "╁", "╂", "╃", "╄", "╅", "╆", "╇", "╈", "╉", "╊", "╋", "═", "║", "╒", "╓", "╔", "╕", "╖", "╗", "╘", "╙", "╚", "╛", "╜", "╝", "╞", "╟", "╠", "╡", "╢", "╣", "╤", "╥", "╦", "╧", "╨", "╩", "╪", "╫", "╬", "╭", "╮", "╯", "╰", "╱", "╲", "╳", "▁", "▂", "▃", "▄", "▅", "▆", "▇", "█", "▉", "▊", "▋", "▌", "▍", "▎", "▏", "▓", "▔", "▕", "■", "□", "▲", "△", "▼", "▽", "◆", "◇", "○", "◎", "●", "◢", "◣", "◤", "◥", "★", "☆", "☉", "♀", "♂", "、", "。", "〃", "々", "〆", "〇", "〈", "〉", "《", "》", "「", "」", "『", "』", "【", "】", "〒", "〓", "〔", "〕", "〖", "〗", "〝", "〞", "〡", "〢", "〣", "〤", "〥", "〦", "〧", "〨", "〩", "ぁ", "あ", "ぃ", "い", "ぅ", "う", "ぇ", "え", "ぉ", "お", "か", "が", "き", "ぎ", "く", "ぐ", "け", "げ", "こ", "ご", "さ", "ざ", "し", "じ", "す", "ず", "せ", "ぜ", "そ", "ぞ", "た", "だ", "ち", "ぢ", "っ", "つ", "づ", "て", "で", "と", "ど", "な", "に", "ぬ", "ね", "の", "は", "ば", "ぱ", "ひ", "び", "ぴ", "ふ", "ぶ", "ぷ", "へ", "べ", "ぺ", "ほ", "ぼ", "ぽ", "ま", "み", "む", "め", "も", "ゃ", "や", "ゅ", "ゆ", "ょ", "よ", "ら", "り", "る", "れ", "ろ", "ゎ", "わ", "ゐ", "ゑ", "を", "ん", "゛", "゜", "ゝ", "ゞ", "ァ", "ア", "ィ", "イ", "ゥ", "ウ", "ェ", "エ", "ォ", "オ", "カ", "ガ", "キ", "ギ", "ク", "グ", "ケ", "ゲ", "コ", "ゴ", "サ", "ザ", "シ", "ジ", "ス", "ズ", "セ", "ゼ", "ソ", "ゾ", "タ", "ダ", "チ", "ヂ", "ッ", "ツ", "ヅ", "テ", "デ", "ト", "ド", "ナ", "ニ", "ヌ", "ネ", "ノ", "ハ", "バ", "パ", "ヒ", "ビ", "ピ", "フ", "ブ", "プ", "ヘ", "ベ", "ペ", "ホ", "ボ", "ポ", "マ", "ミ", "ム", "メ", "モ", "ャ", "ヤ", "ュ", "ユ", "ョ", "ヨ", "ラ", "リ", "ル", "レ", "ロ", "ヮ", "ワ", "ヰ", "ヱ", "ヲ", "ン", "ヴ", "ヵ", "ヶ", "ー", "ヽ", "ヾ", "ㄅ", "ㄆ", "ㄇ", "ㄈ", "ㄉ", "ㄊ", "ㄋ", "ㄌ", "ㄍ", "ㄎ", "ㄏ", "ㄐ", "ㄑ", "ㄒ", "ㄓ", "ㄔ", "ㄕ", "ㄖ", "ㄗ", "ㄘ", "ㄙ", "ㄚ", "ㄛ", "ㄜ", "ㄝ", "ㄞ", "ㄟ", "ㄠ", "ㄡ", "ㄢ", "ㄣ", "ㄤ", "ㄥ", "ㄦ", "ㄧ", "ㄨ", "ㄩ", "㈠", "㈡", "㈢", "㈣", "㈤", "㈥", "㈦", "㈧", "㈨", "㈩", "㈱", "㊣", "㎎", "㎏", "㎜", "㎝", "㎞", "㎡", "㏄", "㏎", "㏑", "㏒", "㏕", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "︰", "︱", "︳", "︴", "︵", "︶", "︷", "︸", "︹", "︺", "︻", "︼", "︽", "︾", "︿", "﹀", "﹁", "﹂", "﹃", "﹄", "﹉", "﹊", "﹋", "﹌", "﹍", "﹎", "﹏", "﹐", "﹑", "﹒", "﹔", "﹕", "﹖", "﹗", "﹙", "﹚", "﹛", "﹜", "﹝", "﹞", "﹟", "﹠", "﹡", "﹢", "﹣", "﹤", "﹥", "﹦", "﹨", "﹩", "﹪", "﹫", "！", "＂", "＃", "＄", "％", "＆", "＇", "（", "）", "＊", "＋", "，", "－", "．", "／", "：", "；", "＜", "＝", "＞", "？", "＠", "［", "＼", "］", "＾", "＿", "｀", ", ", "｛", "｜", "｝", "～", "￠", "￡", "￢", "￣", "￤", "￥" };

       
        /// <summary>
        /// 创建过滤字符缓存
        /// </summary>
        public static void GetNewFilterWordController() 
        {
            
            if (_fw_delete==null||_fw_shift == null||_fw_trial == null)
            {
                FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                _fw_delete = new CBB.CheckHelper.FilterWord(fwo.GetWord(CBB.CheckHelper.FilterWord.word_type.delete));
                _fw_shift = new CBB.CheckHelper.FilterWord(fwo.GetWord(CBB.CheckHelper.FilterWord.word_type.shift));
                _fw_trial = new CBB.CheckHelper.FilterWord(fwo.GetWord(CBB.CheckHelper.FilterWord.word_type.trial));
                _temp_del = new CBB.CheckHelper.FilterWord(list);
                
            }
        }

        /// <summary>
        /// 按类型过滤关键字
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="wt">过滤类型枚举</param>
        /// <returns>被过滤的关键字集合</returns>
        public List<string> FilterText(ref string text, CBB.CheckHelper.FilterWord.word_type wt) 
        {
            GetNewFilterWordController();

            _temp_del.SearchWord(ref text, CBB.CheckHelper.FilterWord.word_type.delete);

            if (wt == CBB.CheckHelper.FilterWord.word_type.delete)
            {
                return _fw_delete.SearchWord(ref text, wt);
            }
            else if (wt == CBB.CheckHelper.FilterWord.word_type.shift)
            {
                return _fw_shift.SearchWord(ref text, wt);
            }
            else {
                return _fw_trial.SearchWord(ref text, wt);
            }
        }

        /// <summary>
        /// 过滤关键字(删除>替换>审核)
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="tablename">表名</param>
        /// <param name="id">自动生成的id值</param>
        /// <param name="colname">列名</param>
        /// <returns></returns>
        public string FilterTextTrial(string text,string tablename, string id, string colname) 
        {
            if (text.Trim().Length <= 0)
                return "";
            GetNewFilterWordController();
           
            string _text = text;
            _temp_del.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.delete);
            List<string> result = new List<string>();
            _fw_delete.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.delete);
            _fw_shift.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.shift);
            result = _fw_trial.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.trial);
            if (result.Count > 0)
            {
                BiZ.FilterText.FilterTextModel ftm = new FilterText.FilterTextModel();
                ftm.Colid = id;
                ftm.Colname = colname;
                ftm.Jion_time = DateTime.Now;
                ftm.Tablename = tablename;
                ftm.Verify_status = Convert.ToInt32(BiZ.FilterText.VerifyStatus.waitaudit);
                ftm.Verify_text = _text;
                new Moooyo.BiZ.FilterText.FilterTextOperation().AddFilterText(ftm);
            }
            if (!text.Equals(_text)) 
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection(tablename);
                QueryComplete qcl = Query.EQ("_id", ObjectId.Parse(id));
                mc.Update(qcl, Update.Set(colname, _text));
            }
            return _text;            
        }

        /// <summary>
        /// 过滤关键字(删除>审核)
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="tablename">表名</param>
        /// <param name="id">自动生成的id值</param>
        /// <param name="colname">列名</param>
        /// <returns></returns>
        public string FilterText(string text, string tablename, string id, string colname, string memberid)
        {
            if (text.Trim().Length <= 0)
                return "";
            GetNewFilterWordController();
            string _text = text;
            _temp_del.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.delete);
            List<string> result_delete = new List<string>();
            List<string> result_shift = new List<string>();
            List<string> result_trial = new List<string>();
            result_delete = _fw_delete.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.delete);
            string outtext = _text;  //保存删除后的文本
            result_shift = _fw_shift.SearchWord(ref _text, CBB.CheckHelper.FilterWord.word_type.shift);

            result_trial = _fw_trial.SearchWord(ref outtext, CBB.CheckHelper.FilterWord.word_type.trial);
            if ((result_trial != null && result_trial.Count > 0) || (null != result_shift && result_shift.Count > 0))
            {
                BiZ.FilterText.FilterTextModel ftm = new FilterText.FilterTextModel();
                ftm.MemberID = memberid;
                ftm.Colid = id;
                ftm.Colname = colname;
                ftm.Jion_time = DateTime.Now;
                ftm.Tablename = tablename;
                ftm.Verify_status = Convert.ToInt32(BiZ.FilterText.VerifyStatus.waitaudit);
                ftm.Verify_text = text;
                new Moooyo.BiZ.FilterText.FilterTextOperation().AddFilterText(ftm);
            }
            if ((result_trial != null && result_trial.Count > 0) || (null != result_shift && result_shift.Count > 0) || (result_delete != null && result_delete.Count > 0))
            {
                string content = "";
                if (null != result_shift && result_shift.Count > 0)
                {
                    content = "内容审核中...";
                }
                else
                {
                    content = outtext;
                }
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection(tablename);
                QueryComplete qcl = Query.EQ("_id", ObjectId.Parse(id));
                mc.Update(qcl, Update.Set(colname, content));
            }
            return _text;
        }
        /// <summary>
        /// 手动添加待审内容
        /// </summary>
        /// <param name="text">原始文本（或者顾虑删除内容后的）</param>
        /// <param name="tablename">表名</param>
        /// <param name="id">原表ID</param>
        /// <param name="colname">原表列名</param>
        /// <param name="isupdate">添加前是否修改原表内容</param>
        /// <param name="trial_text">添加前替换原表内容的内容</param>
        public void AddFilterText(string text, string tablename, string id, string colname, bool isupdate, string trial_text) 
        {
            // 添加待审内容
            BiZ.FilterText.FilterTextModel ftm = new FilterText.FilterTextModel();
            ftm.Colid = id;
            ftm.Colname = colname;
            ftm.Jion_time = DateTime.Now;
            ftm.Tablename = tablename;
            ftm.Verify_status = Convert.ToInt32(BiZ.FilterText.VerifyStatus.waitaudit);
            ftm.Verify_text = text;
            new Moooyo.BiZ.FilterText.FilterTextOperation().AddFilterText(ftm);

            //修改原始内容
            if (isupdate)
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection(tablename);
                QueryComplete qcl = Query.EQ("_id", ObjectId.Parse(id));
                mc.Update(qcl, Update.Set(colname, trial_text));
            }
        }

        /// <summary>
        /// 过滤（删除）的关键字 修改原来的内容
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <param name="colname"></param>
        /// <param name="trial_text"></param>
        public void UpdateFilterText(string tablename, string id, string colname,  string trial_text)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection mc = md.GetCollection(tablename);
            QueryComplete qcl = Query.EQ("_id", ObjectId.Parse(id));
            mc.Update(qcl, Update.Set(colname, trial_text));
           
        }
    }
}

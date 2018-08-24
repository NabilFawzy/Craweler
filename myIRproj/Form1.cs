using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.Data.SqlClient;
namespace myIRproj
{
    public partial class Form1 : Form
    {
        /*public bool IsEnglish(string inputstring)
        {
            Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
            MatchCollection matches = regex.Matches(inputstring);

            if (matches.Count.Equals(inputstring.Length))
                return true;
            else
                return false;
        }*/

        private bool SendEnglishLanguageHeaders(HttpWebRequest request)
        {
            request.Headers.Add("Accept-Language", "en-EN");
            return true;
        }
        public HtmlAgilityPack.HtmlDocument load(string url)
        {

            HtmlWeb web = new HtmlWeb();
            web.PreRequest += SendEnglishLanguageHeaders;
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

            try {
                document = web.Load(url);
                if (web.StatusCode != HttpStatusCode.OK)
                    return null;
                return document;
            }
            catch (Exception e) { return null; }
           
        }
        public List<string> getLinks(HtmlAgilityPack.HtmlDocument document, string main_link)
        {
            List<string> links = new List<string>();
            HtmlNodeCollection n = document.DocumentNode.SelectNodes("//a[@href]");
            if (n == null) return links;
            foreach (HtmlNode link in n)
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (hrefValue != "/" && hrefValue != "#")
                {
                    if (hrefValue.Contains("http"))
                        links.Add(hrefValue);
                    else
                        links.Add(main_link + hrefValue);
                }
            }
            return links;
        }
        public List<string> new_list(List<string> all, List<string> part)
        {
            for (int i = 0; i < part.Count; i++)
            {
                if (!all.Contains(part[i]))
                    all.Add(part[i]);
            }
            return all;
        }
       
        public string cleanBody(string body) {

            // parse Document
            // parse document depend on optimize text which may has different languages
            body = Regex.Replace(body, @"á+", "a");
            body = Regex.Replace(body, @"Á+", "a");
            body = Regex.Replace(body, @"à+", "a");
            body = Regex.Replace(body, @"À+", "a");
            body = Regex.Replace(body, @"â+", "a");
            body = Regex.Replace(body, @"Â+", "a");
            body = Regex.Replace(body, @"ä+", "a");
            body = Regex.Replace(body, @"Ä+", "a");
            body = Regex.Replace(body, @"å+", "a");
            body = Regex.Replace(body, @"Å+", "a");
            body = Regex.Replace(body, @"ã+", "a");
            body = Regex.Replace(body, @"Ã+", "a");
            body = Regex.Replace(body, @"æ+", "a");
            body = Regex.Replace(body, @"Æ+", "a");


            body = Regex.Replace(body, @"ç+", "c");
            body = Regex.Replace(body, @"Ç+", "c");

            body = Regex.Replace(body, @"é+", "e");
            body = Regex.Replace(body, @"É+", "e");
            body = Regex.Replace(body, @"è,+", "e");
            body = Regex.Replace(body, @"È+", "e");
            body = Regex.Replace(body, @"ë+", "e");
            body = Regex.Replace(body, @"Ë+", "e");
            body = Regex.Replace(body, @"ê+", "e");
            body = Regex.Replace(body, @"Ê+", "e");

            body = Regex.Replace(body, @"í+", "i");
            body = Regex.Replace(body, @"Í+", "i");
            body = Regex.Replace(body, @"ì+", "i");
            body = Regex.Replace(body, @"Ì+", "i");
            body = Regex.Replace(body, @"ï+", "i");
            body = Regex.Replace(body, @"Ï+", "i");
            body = Regex.Replace(body, @"î+", "i");
            body = Regex.Replace(body, @"Î+", "i");

            body = Regex.Replace(body, @"ó+", "o");
            body = Regex.Replace(body, @"Ó+", "o");
            body = Regex.Replace(body, @"ò+", "o");
            body = Regex.Replace(body, @"Ò+", "o");
            body = Regex.Replace(body, @"ö+", "o");
            body = Regex.Replace(body, @"Ö+", "o");
            body = Regex.Replace(body, @"õ+", "o");
            body = Regex.Replace(body, @"Õ+", "o");
            body = Regex.Replace(body, @"ô+", "o");
            body = Regex.Replace(body, @"Ô+", "o");

            body = Regex.Replace(body, @"ú+", "u");
            body = Regex.Replace(body, @"Ú+", "u");
            body = Regex.Replace(body, @"ü+", "u");
            body = Regex.Replace(body, @"Ü+", "u");
            body = Regex.Replace(body, @"ù+", "u");
            body = Regex.Replace(body, @"Ù+", "u");
            body = Regex.Replace(body, @"û+", "u");
            body = Regex.Replace(body, @"Û+", "u");
            body = Regex.Replace(body, @"ǹ+", "n");
            body = Regex.Replace(body, @"Ǹ+", "n");
            body = Regex.Replace(body, @"ń+", "n");
            body = Regex.Replace(body, @"Ń+", "n");
            body = Regex.Replace(body, @"й+", "n");
            body = Regex.Replace(body, @"и+", "n");
            body = Regex.Replace(body, @"ñ+", "n");

            body = Regex.Replace(body, @"š+", "s");
            body = Regex.Replace(body, @"s+", "s");

            body = Regex.Replace(body, @"ý+", "y");
            body = Regex.Replace(body, @"Ý+", "y");
            body = Regex.Replace(body, @"ÿ+", "y");
            body = Regex.Replace(body, @"Ÿ+", "y");

            body = Regex.Replace(body, @"ž+", "z");

            body = Regex.Replace(body, @"\.+", "");


            // token
            body = Regex.Replace(body, @"'s+", " is ");
            body = Regex.Replace(body, @"'re+", " are ");
            body = Regex.Replace(body, @"'ll+", " will ");




            //linguistics module
            body = Regex.Replace(body, @"\&nbsp;+", " ");
            body = Regex.Replace(body, @"\&amp;+", " ");
            body = Regex.Replace(body, @"\&quot;+", " ");
            body = Regex.Replace(body, @"\&lt;+", " ");
            body = Regex.Replace(body, @"\&gt;+", " ");
            body = Regex.Replace(body, @"\&euro;+", " ");
            body = Regex.Replace(body, @"\&copy;+", " ");
            body = Regex.Replace(body, @"\&reg;+", " ");
            body = Regex.Replace(body, @"\&permil;+", " ");
            body = Regex.Replace(body, @"\&Dagger;+", " ");
            body = Regex.Replace(body, @"\&lsaquo;+", " ");
            body = Regex.Replace(body, @"\&rsaquo;+", " ");
            body = Regex.Replace(body, @"\&bdquo;+", " ");
            body = Regex.Replace(body, @"\&rdquo;+", " ");
            body = Regex.Replace(body, @"\&ldquo;+", " ");
            body = Regex.Replace(body, @"\&sbquo;+", " ");
            body = Regex.Replace(body, @"\&rsquo;+", " ");
            body = Regex.Replace(body, @"\&lsquo;+", " ");
            body = Regex.Replace(body, @"\&mdash;+", " ");
            body = Regex.Replace(body, @"\&ndash;+", " ");
            body = Regex.Replace(body, @"\&rlm;+", " ");
            body = Regex.Replace(body, @"\&lrm;+", " ");
            body = Regex.Replace(body, @"\&zwj;+", " ");
            body = Regex.Replace(body, @"\&thinsp;+", " ");
            body = Regex.Replace(body, @"\&emsp;+", " ");
            body = Regex.Replace(body, @"\&ensp;+", " ");
            body = Regex.Replace(body, @"\&tilde;+", " ");
            body = Regex.Replace(body, @"\&circ;+", " ");
            body = Regex.Replace(body, @"\&Yuml;+", " ");
            body = Regex.Replace(body, @"\&scaron;+", " ");
            body = Regex.Replace(body, @"\&Scaron;+", " ");
            body = Regex.Replace(body, "[^A-Za-z]", "  ");
            body = Regex.Replace(body, @"\s+", " ");
            return body;
        }
        public string parseText(HtmlAgilityPack.HtmlDocument document)
        {
            try {
                document.DocumentNode.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style")
                    .ToList()
                    .ForEach(n => n.Remove());

                document.DocumentNode.Descendants()
                                     .Where(n => n.Name == "body")
                                     .FirstOrDefault();

                HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes(".//text()"); // get only text
                string body = "";
                foreach (HtmlNode node in htmlNodes)
                {
                    body += node.InnerText + " ";
                }

                //all letters accents

                body = cleanBody(body);
                return body;
            }
            catch (Exception e) { return null; }

            /*body = Regex.Replace(body, @"\$+", " ");
            body = Regex.Replace(body, @"\%+", " ");
            body = Regex.Replace(body, @"\[]+", " ");
            body = Regex.Replace(body, @"\{}+", " ");
            body = Regex.Replace(body, @"\''+", " ");
            body = Regex.Replace(body, @"\""+", " ");
            body = Regex.Replace(body, @"\,+", " ");
            body = Regex.Replace(body, @"\:+", " ");
            body = Regex.Replace(body, @"\/+", " ");
            body = Regex.Replace(body, @"\\+", " ");
            body = Regex.Replace(body, @"\.+", " ");
            body = Regex.Replace(body, @"\!+", " ");
            body = Regex.Replace(body, @"\?+", " ");*/


        }




        public void crawler(Object seed)
        {
            List<string> all_links = new List<string>();
            all_links.Add((string)seed); //seed   
            List<string> all_documents = new List<string>();
            AllPage page;
            IRdbEntities db = new IRdbEntities();
            for (int i = 0; i < 5000 && i < all_links.Count; i++)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = load(all_links[i]);
                if (doc != null) 
                {
                    string myBody = parseText(doc);
                    if (myBody != null) { //no body
                    all_documents.Add(myBody);
                    List<string> links = getLinks(doc, all_links[i]);
                    all_links = new_list(all_links, links);
                    page = new AllPage();
                    page.linkUrl = all_links[i];
                    page.mycontent = myBody;
                    db.AllPages.Add(page);
                    db.SaveChanges();
                        if (i == all_links.Count && links.Count == 0)//last Page in Crawler
                        {
                            break;
                        }
                    }

                }
               

            }
            db.SaveChanges();
         


        }
        public Form1()
        {
            InitializeComponent();
        }
     

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(crawler));
                thread.Start("http://" + textBox1.Text);
               

            }
            if (textBox2.Text != "")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(crawler));
                thread.Start("http://" + textBox2.Text);
              
            }
            if (textBox3.Text != "")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(crawler));
                thread.Start("http://" + textBox3.Text);
    
            }
         

        }
        private void allPagesBindingNavigatorSaveItem_Click(object sender, EventArgs e) { this.Validate();}



        public List<AllPage> AllDocuments;
       
     
        List<string> stopWords =new List<string> {"am","is","are","was","were","will","would","shall","should",
                                                    //Determiners stop word
                                                    "a","an","your","such","more","enough","who","whose","some",
                                                   "many","either","which","several","less","each","what",
                                                   "our",  "least","both","those","any","this","no","its",
                                                   "all","these","neither","his","their","my","her", "other",
                                                   "the","much","half","little","that","every", "few","fewer","fewest",
                                                   // coordinating conjunction stop word
                                                    "and","nor","but","or","yet","for","so",
                                                    //Prepositions  stop word
                                                    "aboard","about","above","absent","law","across","cross","archaic",
                                                   "after","against","gainst","again","gain","along","alongside",
                                                   "amid",  "amidst","mid","midst","among","amongst","mong","mong",
                                                   "mongst","apropos","apud","around","round","as","at", "atop",
                                                   "ontop","bar","before","afore","tofore","behind", "ahind","below","ablow",
                                                    "beneath",  "neath","beside","besides","between","atween","beyond","ayond",
                                                   "but","by","chez","his","their","my","her", "circa","in",
                                                   "come","dehors","despite","spite","down","during", "except","from","inside",
                                                    "into","less","like","minus","near","nearer","nearest","anear",
                                                   "notwithstanding","of","off","on","onto","opposite","out",
                                                   "outen",  "outside","over","pace","past","per","plus","post",
                                                   "pre","pro","re","sans","save","sauf","short", "since",
                                                   "sithence","than","through","thru","throughout","thruout", "till","to","toward",
                                                    "towards",  "under","underneath","unlike","until","til","til","unto",
                                                   "up","upon","upside","versus","via","vs","vice", " vis",
                                                   "with","within","without","worth"};
        
      
        List<Token> invertedIndex = new List<Token>();

        public List<KeyValuePair<string, int>> TokensForSoundex = new List<KeyValuePair<string, int>>();
        public Dictionary<string, List<string>> BgramIndex = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> SoundexIndex = new Dictionary<string, List<string>>();


        public void soundex(string word)
        {
            string result = "", temp = "", temp2 = "";
            char first_letter = word[0];
            char[] voul = new char[] { 'a', 'e', 'i', 'o', 'u', 'y', 'h', 'w' };
            Dictionary<char, char> constants = new Dictionary<char, char>();
            constants.Add('b', '1'); constants.Add('f', '1'); constants.Add('p', '1'); constants.Add('v', '1');
            constants.Add('c', '2'); constants.Add('g', '2'); constants.Add('j', '2'); constants.Add('k', '2');
            constants.Add('q', '2'); constants.Add('s', '2'); constants.Add('x', '2'); constants.Add('z', '2');
            constants.Add('d', '3'); constants.Add('t', '3');
            constants.Add('l', '4');
            constants.Add('m', '5'); constants.Add('n', '5');
            constants.Add('r', '6');
            // assign with zeros and numbers
            for (int i = 1; i < word.Length; i++)
            {
                if (voul.Contains(char.ToLower(word[i]))) { temp += "0"; }
                if (constants.ContainsKey(char.ToLower(word[i]))) { temp += constants[char.ToLower(word[i])]; }
            }
            //remove repetition
            int count = 0;
            while (count < temp.Length)
            {
                if (temp[count] == '0') { temp2 += temp[count]; count++; continue; }
                if (count + 1 >= temp.Length) { temp2 += temp[count]; break; }
                if (temp[count] != temp[count + 1]) temp2 += temp[count];
                count++;
            }
            //remove zeros 
            for (int i = 0; i < temp2.Length; i++)
            {
                if (temp2[i] != '0')
                    result += temp2[i];
            }
            // apend 0's if it have fewer than 4 chars 
            while (result.Length < 3)
                result += '0';
            string sound= first_letter + result.Substring(0, 3);
            try
            {
                SoundexIndex.Add(sound, new List<string>() { word });
               
            }
            catch(Exception e) { SoundexIndex[sound].Add(word); }
        }
        public void B_gramIndex(string token)
        {
            string gram = "";
            for(int i = 0; i < token.Length; i++)
            {
                if (i == 0)
                {
                    gram += "$";
                    gram += token[i];
                }
                else if (i == token.Length-1)
                {
                    gram += token[i];
                    gram += "$";
                }
                else
                {
                    gram += token[i];
                    gram += token[i+1];
                }
                if (!BgramIndex.ContainsKey(gram))
                {
                    BgramIndex.Add(gram, new List<string>() { token });
                }
                else
                {
                    BgramIndex[gram].Add(token);
                }
           
                gram = "";
            }

        }

        public List<int> getPositions(string Token,int DocumentIndex)
        {
            List<string> AllWords = AllDocuments[DocumentIndex].mycontent.ToLower().Split(' ').ToList();
            List<int> Positions = new List<int>();

            for(int i = 0; i < AllWords.Count; i++)
            {
                if(Token== AllWords[i])
                {
                    Positions.Add(i);
                }
            }
            return Positions;
            //FrequencyTokenizedWordswithPositiions.Add(new KeyValuePair<int, List<int>>(Positions.Count,Positions));
   
        }
        public void wordswithDocID(List<string> mTokens,int index)
        {
           
            Token thetoken;
            List<int> mPositions=new List<int>();
           PorterStemming porterStemming = new PorterStemming(); ;
            mTokens = mTokens.Distinct().ToList();
            foreach (var token in mTokens)
            {
                TokensForSoundex.Add(new KeyValuePair<string, int>(token, index));
                thetoken = new Token();
                string stemword = porterStemming.stem(token);
                thetoken.mToken = stemword;
                thetoken.docID = index;
                mPositions = getPositions(token, index);
                thetoken.docPositions = mPositions;
                thetoken.Freq = mPositions.Count;
                invertedIndex.Add(thetoken);
                B_gramIndex(token);
                soundex(token);
            }
      
        }
        public List<string> TokenizationAndLinguisticsModule(string body)
        {
            List<string> TokenizWords= new List<string>();
        string newbody="";
            //issues on  tokenization '
            newbody = Regex.Replace(body, @"'s+", " is ");
            newbody = Regex.Replace(newbody, @"'re+", " are ");
            newbody = Regex.Replace(newbody, @"'ll+", " will ");

            // for word-secword will be (word  secwod)
            body = Regex.Replace(body, @"-", " ");
            body = body.ToLower();
            TokenizWords = body.Split(' ').ToList();
            for(int i=0;i<TokenizWords.Count;i++)
            {
                if (TokenizWords[i].Length <= 1)
                {
                    TokenizWords.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < TokenizWords.Count; i++)
            {
                if (stopWords.Contains(TokenizWords[i]))
                {
                    TokenizWords.RemoveAt(i);
                    i--;
                }
            }
            
            return TokenizWords;
        }
        public void startIndexing()
        {
          
            for (int i = 0; i < 1500; i++)
            {
                List<string> Tokens= TokenizationAndLinguisticsModule(AllDocuments[i].mycontent);
                wordswithDocID(Tokens, i);
               // TokenizedWords.Add(new KeyValuePair<string, int>());
            }

            
        }
        public void fillInvertedIndex()
        {
            IRdbEntities db = new IRdbEntities();
            InvertedIndex inver;
   
            invertedIndex = invertedIndex.OrderBy(x => x.docID).ThenBy(x => x.mToken).ToList();
            foreach (var inv in invertedIndex)
            {
                try {
                    string combindedpos = string.Join(",", inv.docPositions.ToArray());
                    inver = new InvertedIndex();
                    inver.docID = inv.docID;
                    inver.frequency = inv.Freq;
                    inver.term = inv.mToken;
                    inver.positions = combindedpos;
                    db.InvertedIndexes.Add(inver);
                    db.SaveChanges();
                }
                catch (Exception e) { };
                

            }
        }
        public void fillBgramIndex()
        {
            IRdbEntities db = new IRdbEntities();
            kGramIndex kG;
            
            var l = BgramIndex.OrderBy(key => key.Key);
            var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
           
            foreach (var gram in dic)
            {
                string combindedGrams = string.Join(",", gram.Value.ToArray());
                kG = new kGramIndex();
                kG.k_gram = gram.Key;
                kG.terms = combindedGrams;
                db.kGramIndexes.Add(kG);
                db.SaveChanges();

            }

        }
        public void fillSoundexIndex()
        {
            IRdbEntities db = new IRdbEntities();

            SoundexIndex soun;
            var l = SoundexIndex.OrderBy(key => key.Key);
            var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            foreach (var sound in dic)
            {
                string combindedSoundes = string.Join(",", sound.Value.ToArray());
                soun = new SoundexIndex();
                soun.soundex = sound.Key;
                soun.items = combindedSoundes;
                db.SoundexIndexes.Add(soun);
                db.SaveChanges();

            }
        }

        void DistictItemsSoundex()
        {
            IRdbEntities db = new IRdbEntities();
            List<SoundexIndex> sounds = db.SoundexIndexes.ToList();

            foreach (var sound in sounds)
            {
                DistinctSoundexIndex s = new DistinctSoundexIndex();
                s.soundex = sound.soundex;
                String[] words = sound.items.Split(',').Distinct().ToArray();
                Array.Sort(words);
                s.items = string.Join(",", words);
                db.DistinctSoundexIndexes.Add(s);
                db.SaveChanges();

            }
            db.SaveChanges();
        }
        void AllDistictItemskGram()
        {
            IRdbEntities db = new IRdbEntities();
            List<kGramIndex> kGrams = db.kGramIndexes.ToList();

            foreach (var kgrm in kGrams)
            {
               DistinctkGramsIndex  k = new DistinctkGramsIndex();
                k.k_gram = kgrm.k_gram;
                String[] words = kgrm.terms.Split(',').Distinct().ToArray();
                Array.Sort(words);
                k.terms = string.Join(",", words);
                db.DistinctkGramsIndexes.Add(k);
                db.SaveChanges();

            }
            db.SaveChanges();
        }

        public void Indexing()
        {


            IRdbEntities db = new IRdbEntities();
         

             AllDocuments = db.AllPages.ToList();

             startIndexing();

             Thread thread20 = new Thread(fillInvertedIndex);
             thread20.Start();
             Thread thread21 = new Thread(fillBgramIndex);
             thread21.Start();
             Thread thread22 = new Thread(fillSoundexIndex);
            thread21.Start();
            thread22.Start();
            thread21.Join();
            thread22.Join();
            DistictItemsSoundex();
            AllDistictItemskGram();



        }

        private void button2_Click(object sender, EventArgs e) {   Indexing(); }
    }
}

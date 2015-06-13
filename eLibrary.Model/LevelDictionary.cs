using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eLibrary.Model
{
    public class LevelDictionary
    {

        public static SelectList LevelTypeListWithoutChoose
        {
            get { return new SelectList(LevelTypeDicWithoutChooses, "Value", "Key"); }
        }

        private static readonly IDictionary<string, string> LevelTypeDicWithoutChooses = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"A","A"},
        {"B","B"} ,
         {"C","C"},
         {"D","D"},
         {"E","E"},
         {"F","F"},
         {"G","G"},
         {"H","H"},
         {"I","I"},
         {"J","J"},
         {"K","K"},
         {"L","L"},
         {"M","M"},
         {"O","O"},
         {"P","P"},
         {"Q","Q"},
         {"R","R"},
         {"S","S"},
         {"T","T"},
         {"U","U"},
         {"V","V"},
         {"W","W"},
         {"X","X"},
         {"Y","Y"},
         {"Z","Z"},
         
             
       };

        public static SelectList LevelTypeList
        {
            get { return new SelectList(LevelTypeDic, "Value", "Key"); }
        }

        private static readonly IDictionary<string, string> LevelTypeDic = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"A","A"},
        {"B","B"} ,
         {"C","C"},
         {"D","D"},
         {"E","E"},
         {"F","F"},
         {"G","G"},
         {"H","H"},
         {"I","I"},
         {"J","J"},
         {"K","K"},
         {"L","L"},
         {"M","M"},
         {"O","O"},
         {"P","P"},
         {"Q","Q"},
         {"R","R"},
         {"S","S"},
         {"T","T"},
         {"U","U"},
         {"V","V"},
         {"W","W"},
         {"X","X"},
         {"Y","Y"},
         {"Z","Z"},
         
             
       };

        public static SelectList ItemDicoList
        {
            get { return new SelectList(ItemDicTtype, "Value", "Key"); }
        }



        private static readonly IDictionary<string, string> ItemDicTtype = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"Article","Article"},
         {"Journal","Journal"},
         {"TextBook","TextBook"},
           {"Novel","Novel"},
         {"Paper","Paper"},
            {"News Paper","News Paper"},
        {"DVD","DVD"},
         {"VCD","VCD"},
          {"CD","CD"},
            {"Cassette","Cassette"},
            {"Other","Other"},
      
       };

        public static SelectList StaffStudentList2
        {
            get { return new SelectList(StudentStaff2, "Value", "Key"); }
        }



        private static readonly IDictionary<string, string> StudentStaff2 = new Dictionary<string, string> 
       { {"Staff","Staff"},
                {"NonTeaching-Staff","NonTeaching-Staff"},
         {"Student","Student"},
       
      
       };


        public static SelectList StaffStudentList
        {
            get { return new SelectList(StudentStaff, "Value", "Key"); }
        }



        private static readonly IDictionary<string, string> StudentStaff = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"Staff","Staff"},
         {"NonTeaching-Staff","NonTeaching-Staff"},
         {"Student","Student"},
       
      
       };

        public static SelectList LevelDicoListWithoutChoose
        {
            get { return new SelectList(LevelDicWithoutChoose, "Value", "Key"); }
        }



        private static readonly IDictionary<string, string> LevelDicWithoutChoose = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"JSS1","JSS1"},
         {"JSS2","JSS2"},
         {"JSS3","JSS3"},
         {"SS1","SS1"},
        {"SS2","SS2"},
         {"SS3","SS3"},
      
       };


        public static SelectList LevelDicoList
        {
            get { return new SelectList(LevelDic, "Value", "Key"); }
        }



        private static readonly IDictionary<string, string> LevelDic = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"JSS1","JSS1"},
         {"JSS2","JSS2"},
         {"JSS3","JSS3"},
         {"SS1","SS1"},
        {"SS2","SS2"},
         {"SS3","SS3"},
      
       };

        public static SelectList LevelDicoListWithStaff
        {
            get { return new SelectList(LevelDicStaff, "Value", "Key"); }
        }



        public static readonly IDictionary<string, string> LevelDicStaff = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"Staff","Staff"},
        {"NonTeaching-Staff","NonTeaching-Staff"},
        {"JSS1","JSS1"},
        {"JSS2","JSS2"},
        {"JSS3","JSS3"},
        {"SS1","SS1"},
        {"SS2","SS2"},
         {"SS3","SS3"},
      
       };




        public static SelectList Visible
        {
            get { return new SelectList(VisibleList, "Value", "Key"); }
        }

        private static readonly IDictionary<string, string> VisibleList = new Dictionary<string, string> 
            {{"Choose..",""} ,
         {"True","True"},
        {"False","False"},
        
                   
       };


               public static SelectList Term
        {
            get { return new SelectList(TermList, "Value", "Key"); }
        }

        private static readonly IDictionary<string, string> TermList = new Dictionary<string, string> 
            {{"Choose..",""} ,
         {"FIRST","1"},
        {"SECOND","2"},
         {"THIRD","3"},      
                   
       };
    }
}

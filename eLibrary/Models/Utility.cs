//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using Microsoft.Office.Interop.Word;

//namespace eLibrary.Models
//{
//    public  class Utility
//    {
//        // private Microsoft.Office.Interop.Word.ApplicationClass MSdoc;

//        //Use for the parameter whose type are not known or say Missing
//        object Unknown = Type.Missing;

//        public void word2PDF(string source, string target)
//        {   //Creating the instance of Word Application          
//            // Create a new Microsoft Word application object





//            //Creating the instance of Word Application
//            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();

//            // specifying the Source & Target file names
//            object Source = source;
//            object Target = target;

//            // Use for the parameter whose type are not known or  
//            // say Missing
//            object Unknown = Type.Missing;

//            // Source document open here
//            // Additional Parameters are not known so that are  
//            // set as a missing type
//            newApp.Documents.Open(ref Source, ref Unknown,
//                 ref Unknown, ref Unknown, ref Unknown,
//                 ref Unknown, ref Unknown, ref Unknown,
//                 ref Unknown, ref Unknown, ref Unknown,
//                 ref Unknown, ref Unknown, ref Unknown, ref Unknown);

//            // Specifying the format in which you want the output file 
//            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

//            //Changing the format of the document
//            newApp.ActiveDocument.SaveAs(ref Target, ref format,
//                    ref Unknown, ref Unknown, ref Unknown,
//                    ref Unknown, ref Unknown, ref Unknown,
//                    ref Unknown, ref Unknown, ref Unknown,
//                    ref Unknown, ref Unknown, ref Unknown,
//                    ref Unknown, ref Unknown);

//            // for closing the application
//            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);

//           // Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

//           // // C# doesn't have optional arguments so we'll need a dummy value
//           // object oMissing = System.Reflection.Missing.Value;

//           // // Get a Word file
//           //// FileInfo wordFile = new FileInfo("myDoc.doc");
//           // FileInfo wordFile = new FileInfo(Source);
//           // word.Visible = false;
//           // word.ScreenUpdating = false;

//           // // Cast as Object for word Open method
//           // Object filename = (Object)wordFile.FullName;

//           // // Use the dummy value as a placeholder for optional arguments
//           // Document doc = word.Documents.Open(ref filename, ref oMissing,
//           // ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
//           // ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
//           // ref oMissing, ref oMissing, ref oMissing, ref oMissing);
//           // doc.Activate();

//           //object outputFileName = wordFile.FullName.Replace(".doc", ".pdf");
//           // //object outputFileName = wordFile.FullName.Replace(Source, Target);
//           // object fileFormat = WdSaveFormat.wdFormatPDF;

//           // // Save document into PDF Format
//           // doc.SaveAs(ref outputFileName,
//           //     ref fileFormat, ref oMissing, ref oMissing,
//           //     ref oMissing, ref oMissing, ref oMissing, ref oMissing,
//           //     ref oMissing, ref oMissing, ref oMissing, ref oMissing,
//           //     ref oMissing, ref oMissing, ref oMissing, ref oMissing);

//           // // Close the Word document, but leave the Word application open.
//           // // doc has to be cast to type _Document so that it will find the
//           // // correct Close method.                
//           // object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
//           // ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
//           // doc = null;

//           // // word has to be cast to type _Application so that it will find
//           // // the correct Quit method.
//           // ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
//           // word = null;
//        }
//    }
//}
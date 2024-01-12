﻿using System.IO;
using System.Linq;
using Castle.Core.Logging;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfTools.Actions
{
    class CombineAction : IAction
    {
        public void Do(string[] args)
        {
            CombineMultiplePDF(args.Skip(2).ToArray(), args[1]);
        }

        public void GetHelp()
        {
            throw new System.NotImplementedException();
        }

        private static void CombineMultiplePDF(string[] fileNames, string outFile)
        {
            // todo TLey: _logger.Trace($"Combile multiple pdf files {string.Join(",", fileNames)} into {outFile}");

            // step 1: creation of a document-object
            Document document = new Document();
            //create newFileStream object which will be disposed at the end
            using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
            {
                // step 2: we create a writer that listens to the document
                PdfCopy writer = new PdfCopy(document, newFileStream);

                // step 3: we open the document
                document.Open();

                foreach (string fileName in fileNames)
                {
                    // we create a reader for a certain document
                    PdfReader reader = new PdfReader(fileName);
                    reader.ConsolidateNamedDestinations();

                    // step 4: we add content
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        writer.AddPage(page);
                    }

                    //PRAcroForm form = reader.AcroForm;
                    //if (form != null)
                    //{
                    //    writer.AddDocument(reader);
                    //}

                    reader.Close();
                }

                // step 5: we close the document and writer
                writer.Close();
                document.Close();
            }//disposes the newFileStream object
        }

    }
}
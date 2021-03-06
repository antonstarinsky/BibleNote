﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BibleNote.Common.Helpers;
using BibleNote.Providers.FileSystem.DocumentId;
using BibleNote.Providers.Html.Contracts;
using BibleNote.Providers.Web.DocumentId;
using BibleNote.Services.Contracts;
using HtmlAgilityPack;

namespace BibleNote.Providers.Html
{
    public class HtmlDocumentHandler : IHtmlDocumentHandler
    {
        public HtmlDocument HtmlDocument { get; private set; }

        public IDocumentId DocumentId { get; private set; }

        public HtmlDocumentHandler(IDocumentId documentId)
        {
            DocumentId = documentId;           
        }

        public async Task LoadPageContentAsync()
        {
            HtmlDocument = await ReadDocumentAsync(DocumentId);
        }

        private static async Task<HtmlDocument> ReadDocumentAsync(IDocumentId documentId)
        {
            string fileContent = null;

            if (documentId is FileDocumentId fileDocumentId)
            {
                var filePath = fileDocumentId.FilePath;
                fileContent = File.ReadAllText(filePath);

                if (!StringUtils.ContainsHtml(fileContent))
                {
                    fileContent = string.Concat(
                        fileContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => $"<p>{p}</p>"));

                    documentId.SetReadonly();
                }
            }
            else if (documentId is WebDocumentId webDocumentId)
            {
                throw new NotImplementedException();  // todo async?
            }
            else
            {
                throw new NotSupportedException(documentId.GetType().Name);
            }

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(fileContent);
            return htmlDoc;
        }

        public void SetDocumentChanged()
        {
            DocumentId.SetChanged();
        }
  
        public async ValueTask DisposeAsync()
        {
            if (!DocumentId.IsReadonly && DocumentId.Changed)
            {
                var filePath = ((FileDocumentId)DocumentId).FilePath;

                var encoding = FileUtils.GetEncoding(filePath);
                HtmlDocument.Save(filePath, encoding);
            }
        }
    }
}

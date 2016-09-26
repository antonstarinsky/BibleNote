﻿using BibleNote.Analytics.Contracts.Providers;
using BibleNote.Analytics.Contracts.VerseParsing;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using BibleNote.Analytics.Core.Extensions;
using BibleNote.Analytics.Models.Verse;
using BibleNote.Analytics.Models.VerseParsing;
using BibleNote.Analytics.Core.Constants;
using BibleNote.Analytics.Models.Contracts.ParseContext;

namespace BibleNote.Analytics.Providers.Html
{
    public class HtmlProvider : IDocumentProvider
    {
        private readonly IDocumentParserFactory _documentParserFactory;

        private readonly IHtmlDocumentConnector _htmlDocumentConnector;

        public bool IsReadonly { get { return false; } }   // todo: надо дополнительно этот параметр вынести выше - на уровень NavigationProviderInstance

        public HtmlProvider(IDocumentParserFactory documentParserFactory, IHtmlDocumentConnector htmlDocumentConnector)
        {
            _documentParserFactory = documentParserFactory;
            _htmlDocumentConnector = htmlDocumentConnector;
        }

        public string GetVersePointerLink(VersePointer versePointer)
        {
            return string.Format($"<a href='bnVerse:{versePointer}'>{versePointer.GetOriginalVerseString()}</a>");
        }        

        public DocumentParseResult ParseDocument(IDocumentId documentId)
        {
            DocumentParseResult result;
            using (var docHandler = _htmlDocumentConnector.Connect(documentId))
            {
                using (var docParser = _documentParserFactory.Create(this))
                {
                    ParseNode(docParser, docHandler.HtmlDocument.DocumentNode);
                    result = docParser.DocumentParseResult;
                }

                if (result.ParagraphParseResults.Any(pr => pr.IsValuable))
                    docHandler.SetDocumentChanged();
            }

            return result;
        }

        private void ParseNode(IDocumentParser docParser, HtmlNode node)
        {
            var state = GetParagraphType(node);
            if (state.IsHierarchical())
            {
                using (docParser.ParseHierarchyElement(state))
                {
                    foreach (var childNode in node.ChildNodes)
                    {
                        ParseNode(docParser, childNode);
                    }
                }
            }
            else
            {
                if (node.HasChildNodes || node.IsValuableTextNode())
                    docParser.ParseParagraph(node);
            }
        }

        private ElementType GetParagraphType(HtmlNode node)
        {
            if (HtmlTags.BlockElements.Contains(node.Name))
                return ElementType.HierarchicalBlock;

            if (HtmlTags.Lists.Contains(node.Name))
                return ElementType.List;

            if (HtmlTags.ListElements.Contains(node.Name))
                return ElementType.ListElement;

            if (HtmlTags.TableCells.Contains(node.Name))
                return ElementType.TableCell;

            if (HtmlTags.TableBodys.Contains(node.Name))
                return ElementType.TableBody;

            switch (node.Name)
            {
                case HtmlTags.Table:
                    return ElementType.Table;
                case HtmlTags.TableRow:
                    return ElementType.TableRow;                
                case HtmlTags.Head:
                    if (node.ParentNode?.Name == HtmlTags.Html)
                        return ElementType.Title;
                    break;                
            }            

            return ElementType.SimpleBlock;
        }
    }
}

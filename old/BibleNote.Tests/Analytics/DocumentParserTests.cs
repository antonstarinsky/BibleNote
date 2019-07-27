﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibleNote.Analytics.Services.Unity;
using BibleNote.Analytics.Contracts.VerseParsing;
using Microsoft.Practices.Unity;
using BibleNote.Tests.Analytics.Mocks;
using BibleNote.Analytics.Contracts.Providers;
using BibleNote.Analytics.Models.Contracts.ParseContext;
using BibleNote.Analytics.Models.VerseParsing.ParseContext;
using BibleNote.Tests.Analytics.TestsBase;
using BibleNote.Analytics.Core.Contracts;
using BibleNote.Analytics.Providers.Html;
using System.Linq;
using BibleNote.Analytics.Models.VerseParsing.ParseResult;

namespace BibleNote.Tests.Analytics
{
    [TestClass]
    public class DocumentParserTests : DocumentParserTestsBase
    {
        private IDocumentProviderInfo _documentProvider;
        private IDocumentParserFactory _documentParserFactory;
        private IDocumentParseContextEditor _documentParseContext;

        [TestInitialize]
        public override void Init()
        {
            base.Init();

            _documentProvider = new MockDocumentProviderInfo() { IsReadonly = true };
            _documentParserFactory = DIContainer.Resolve<IDocumentParserFactory>();

            _documentParseContext = new DocumentParseContext();
            DIContainer.Container.RegisterInstance(_documentParseContext);
        }

        [TestCleanup]
        public void Done()
        {

        }

        private void CheckParseResults(DocumentParseResult docParseResult, params string[][] expectedResults)
        {
            base.CheckParseResults(docParseResult.GetAllParagraphParseResults().ToList(), expectedResults);
        }

        private static IXmlNode GetNode(string html)
        {
            return new HtmlNodeWrapper(html);
        }

        [TestMethod]
        public void DocParser_Test1()
        {
            var node = GetNode("<div>Это <p>тестовая <font>Мк 5:</font>6-7!!</p> строка</div>");
            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node);
                docParseResult = docParser.DocumentParseResult;
            }

            CheckParseResults(docParseResult, new string[] { "Мк 5:6-7" });
        }

        [TestMethod]
        public void DocParser_Test2()
        {
            var node1 = GetNode("Мк 5:6");
            var node2 = GetNode("Ин 1:1");
            var node3 = GetNode("не с начала Лк 1:1");
            var verseNode = GetNode(":12 и :13");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node1);

                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node2);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(verseNode);
                    }
                }

                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node3);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(verseNode);
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мк 5:6" },
                new string[] { "Ин 1:1" },
                new string[] { "Ин 1:12", "Ин 1:13" },
                new string[] { "Лк 1:1" });
        }

        [TestMethod]
        public void DocParser_Test2_1()
        {
            var node1 = GetNode("Мк 5:6");
            var node2 = GetNode("не с начала Лк 1:1");
            var verseNode = GetNode(":12 и :13");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node1);

                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node2);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(verseNode);
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мк 5:6" },
                new string[] { "Лк 1:1" },
                new string[] { "Мк 5:12", "Мк 5:13" });
        }

        [TestMethod]
        public void DocParser_Test3()
        {
            var emptyNode = GetNode("Пустая строка");
            var node2 = GetNode("Мк 5:6");
            var node3 = GetNode("Ин 1:1");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Table))
                {
                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node2);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node3);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мк 5:6" },
                new string[] { "Ин 1:1" },
                new string[] { "Мк 5:12" });
        }

        [TestMethod]
        public void DocParser_Test4()
        {
            var emptyNode = GetNode("Пустая строка");
            var node1 = GetNode("Ин 1:1");
            var node2 = GetNode("Мк 5:6");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Table))
                {
                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node2);
                            docParser.ParseParagraph(verseNode);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node1);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node1);

                            using (docParser.ParseHierarchyElement(ElementType.List))
                            {
                                using (docParser.ParseHierarchyElement(ElementType.ListElement))
                                {
                                    docParser.ParseParagraph(verseNode);
                                }
                            }

                            docParser.ParseParagraph(verseNode);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.List))
                            {
                                using (docParser.ParseHierarchyElement(ElementType.ListElement))
                                {
                                    docParser.ParseParagraph(verseNode);
                                }
                            }
                        }
                    }
                }

                docParser.ParseParagraph(verseNode);

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мк 5:6" },
                new string[] { "Мк 5:12" },
                new string[] { "Ин 1:1" },
                new string[] { "Ин 1:12" },
                new string[] { "Мк 5:12" },
                new string[] { "Ин 1:1" },
                new string[] { "Ин 1:12" },
                new string[] { "Ин 1:12" },
                new string[] { "Мк 5:12" });
        }

        [TestMethod]
        public void DocParser_Test5()
        {
            var emptyNode = GetNode("Пустая строка");
            var node1 = GetNode("Ин 1:1");
            var node2 = GetNode("не сначала Мк 5:6");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node1);

                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.Table))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.TableRow))
                            {
                                using (docParser.ParseHierarchyElement(ElementType.TableCell))
                                {
                                    docParser.ParseParagraph(node2);
                                }

                                using (docParser.ParseHierarchyElement(ElementType.TableCell))
                                {
                                    docParser.ParseParagraph(verseNode);
                                }
                            }
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.Table))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.TableRow))
                            {
                                using (docParser.ParseHierarchyElement(ElementType.TableCell))
                                {
                                    docParser.ParseParagraph(emptyNode);
                                }

                                using (docParser.ParseHierarchyElement(ElementType.TableCell))
                                {
                                    docParser.ParseParagraph(verseNode);
                                    docParser.ParseParagraph(verseNode);
                                }
                            }
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Ин 1:1" },
                new string[] { "Мк 5:6" },
                new string[] { "Мк 5:12" },
                new string[] { "Ин 1:12" },
                new string[] { "Ин 1:12" });
        }

        [TestMethod]
        public void DocParser_Test6()
        {
            var emptyNode = GetNode("Пустая строка");
            var node1 = GetNode("Ин 1:1");
            var node2 = GetNode("Мк 5:6");
            var verseNode = GetNode(":12");
            var titleNode = GetNode("Лк 3");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Title))
                {
                    docParser.ParseParagraph(titleNode);
                }

                using (docParser.ParseHierarchyElement(ElementType.Table))
                {
                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node1);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(node2);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(emptyNode);
                        }

                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            docParser.ParseParagraph(verseNode);
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;           
            }

            CheckParseResults(docParseResult,
               new string[] { "Лк 3" },
               new string[] { "Ин 1:1" },
               new string[] { "Ин 1:12" },
               new string[] { "Мк 5:6" },
               new string[] { "Ин 1:12" },
               new string[] { "Лк 3:12" },
               new string[] { "Лк 3:12" });
        }

        [TestMethod]
        public void DocParser_Test7()
        {
            var node = GetNode("Ин 1:1");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Title))
                {
                    docParser.ParseParagraph(node);
                }

                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {
                    docParser.ParseParagraph(verseNode);
                }

                docParseResult = docParser.DocumentParseResult;             
            }

            CheckParseResults(docParseResult,
                new string[] { "Ин 1:1" },
                new string[] { "Ин 1:12" });
        }

        [TestMethod]
        public void DocParser_Test8()
        {
            var emptyNode = GetNode("Пустая строка");
            var node1 = GetNode("Мф 1:1 - стих в начале");
            var node2 = GetNode("Стих не в начале - Мк 2:2");
            var node3 = GetNode("Лк 3-4 - несколько глав в начале");
            var node4 = GetNode("Несколько глав не в начале - Ин 5-6");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {
                    docParser.ParseParagraph(node1);
                    docParser.ParseParagraph(node2);
                    docParser.ParseParagraph(emptyNode);
                    docParser.ParseParagraph(verseNode);
                }

                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {
                    docParser.ParseParagraph(node1);
                    docParser.ParseParagraph(node3);
                    docParser.ParseParagraph(verseNode);
                }

                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {
                    docParser.ParseParagraph(node1);
                    docParser.ParseParagraph(node4);
                    docParser.ParseParagraph(verseNode);
                }

                docParseResult = docParser.DocumentParseResult;               
            }

            CheckParseResults(docParseResult,
                new string[] { "Мф 1:1" },
                new string[] { "Мк 2:2" },
                new string[] { "Мф 1:12" },
                new string[] { "Мф 1:1" },
                new string[] { "Лк 3-4" },
                new string[] { "Мф 1:1" },
                new string[] { "Ин 5-6" },
                new string[] { "Мф 1:12" });
        }

        [TestMethod]
        public void DocParser_Test9()
        {
            var emptyNode = GetNode("Пустая строка");
            var node0 = GetNode("1Петр 3:3");
            var node1 = GetNode("Мф 1:1 - стих в начале");
            var node2 = GetNode("Стих не в начале - Мк 2:2");
            var node3 = GetNode("Лк 3-4 - несколько глав в начале");
            var node4 = GetNode("Несколько глав не в начале - Ин 5-6");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node0);
                        docParser.ParseParagraph(emptyNode);
                        docParser.ParseParagraph(verseNode);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node1);

                        using (docParser.ParseHierarchyElement(ElementType.List))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(emptyNode);
                                docParser.ParseParagraph(verseNode);
                            }

                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(node0);
                                docParser.ParseParagraph(verseNode);
                            }

                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(node2);
                                docParser.ParseParagraph(verseNode);
                            }

                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(verseNode);
                            }

                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(node3);
                                docParser.ParseParagraph(verseNode);
                            }

                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(node4);
                                docParser.ParseParagraph(verseNode);
                            }
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node2);

                        using (docParser.ParseHierarchyElement(ElementType.List))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(verseNode);
                            }
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node3);

                        using (docParser.ParseHierarchyElement(ElementType.List))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.ListElement))
                            {
                                docParser.ParseParagraph(verseNode);
                            }
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;           
            }

            CheckParseResults(docParseResult,
               new string[] { "1Петр 3:3" },
               new string[] { "1Петр 3:12" },
               new string[] { "Мф 1:1" },
               new string[] { "Мф 1:12" },
               new string[] { "1Петр 3:3" },
               new string[] { "1Петр 3:12" },
               new string[] { "Мк 2:2" },
               new string[] { "1Петр 3:12" },
               new string[] { "Лк 3-4" },
               new string[] { "Ин 5-6" },
               new string[] { "Мк 2:2" },
               //new string[] { "Мк 2:12" }, ?????
               new string[] { "Лк 3-4" });
        }

        [TestMethod]
        public void DocParser_Test10()
        {
            var node = GetNode("Мф 1:1 и Мк 2:2");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node);
                docParser.ParseParagraph(verseNode);

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мф 1:1", "Мк 2:2" });
        }

        [TestMethod]
        public void DocParser_Test11()
        {
            var node1 = GetNode("Мф 1:1 и 2:2");
            var node2 = GetNode("Мф 1:1 и :2");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {

                    docParser.ParseParagraph(node1);
                    docParser.ParseParagraph(verseNode);
                }

                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                {

                    docParser.ParseParagraph(node2);
                    docParser.ParseParagraph(verseNode);
                }

                docParseResult = docParser.DocumentParseResult;             
            }

            CheckParseResults(docParseResult,
                new string[] { "Мф 1:1", "Мф 2:2" },
                new string[] { "Мф 1:1", "Мф 1:2" },
                new string[] { "Мф 1:12" });
        }

        [TestMethod]
        public void DocParser_Test12()
        {
            var emptyNode = GetNode("Пустая строка");
            var node = GetNode("Мф 1");
            var verseNode1 = GetNode(":2 и :3-4");
            var verseNode2 = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.List))
                {
                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(node);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(verseNode1);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(emptyNode);
                    }

                    using (docParser.ParseHierarchyElement(ElementType.ListElement))
                    {
                        docParser.ParseParagraph(verseNode2);
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Мф 1" },
                new string[] { "Мф 1:2", "Мф 1:3-4" },
                new string[] { "Мф 1:12" });
        }

        [TestMethod]
        public void DocParser_Test13()
        {
            var node1 = GetNode("Ин 1");
            var node2 = GetNode("Тест Мф 1-2 и :5");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                docParser.ParseParagraph(node1);
                docParser.ParseParagraph(node2);
                docParser.ParseParagraph(verseNode);

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Ин 1" },
                new string[] { "Мф 1-2" },
                new string[] { "Ин 1:12" });
        }

        [TestMethod]
        public void DocParser_Test14()
        {
            var node1 = GetNode("Ин 1");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Table))
                {
                    using (docParser.ParseHierarchyElement(ElementType.TableBody))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableRow))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.TableCell))
                            {
                                docParser.ParseParagraph(node1);
                            }
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableBody))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableRow))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.TableCell))
                            {
                                docParser.ParseParagraph(verseNode);
                            }
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Ин 1" },
                new string[] { "Ин 1:12" });
        }

        [TestMethod]
        public void DocParser_Test15()
        {
            var node1 = GetNode("Ин 1");
            var verseNode = GetNode(":12");

            DocumentParseResult docParseResult;
            using (var docParser = _documentParserFactory.Create(_documentProvider))
            {
                using (docParser.ParseHierarchyElement(ElementType.Table))
                {
                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                            {
                                using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                                {
                                    docParser.ParseParagraph(node1);
                                }
                            }
                        }
                    }

                    using (docParser.ParseHierarchyElement(ElementType.TableRow))
                    {
                        using (docParser.ParseHierarchyElement(ElementType.TableCell))
                        {
                            using (docParser.ParseHierarchyElement(ElementType.HierarchicalBlock))
                            {
                                docParser.ParseParagraph(verseNode);
                            }
                        }
                    }
                }

                docParseResult = docParser.DocumentParseResult;                
            }

            CheckParseResults(docParseResult,
                new string[] { "Ин 1" },
                new string[] { "Ин 1:12" });
        }
    }
}
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibleNote.Analytics.Providers.Html;
using BibleNote.Tests.Analytics.TestsBase;
using BibleNote.Analytics.Services.DocumentProvider.Contracts;
using BibleNote.Analytics.Providers.Html.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BibleNote.Tests.Analytics
{
    [TestClass]
    public class HtmlDocumentProviderTests : FileDocumentParserTestsBase
    {
        private const string TempFolderName = "HtmlDocumentProviderTests";

        [TestInitialize]
        public void Init()
        {
            base.Init(TempFolderName, services => services
                .AddScoped<IHtmlDocumentConnector, HtmlDocumentConnector>()
                .AddScoped<IDocumentProvider, HtmlProvider>());            
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void CheckTitle()
        {
            TestFile(@"..\..\..\TestData\Html_CheckTitle.html", true, false,
                new string[] { "Ин 1:1" });            
        }

        [TestMethod]
        public void CheckPage()
        {
            TestFile(@"..\..\..\TestData\Html_CheckPage.html",
                new string[] { "Ин 1:1" },
                new string[] { "Исх 12:27" },
                new string[] { "Ин 1:50-2:3" },
                new string[] { "Ин 3:16", "1Кор 5:7-9" },
                new string[] { "Ис 44" },
                new string[] { "Ис 44:24" },
                new string[] { "Евр 1:2", "Евр 1:10" },
                new string[] { "Ис 44:6" },                
                new string[] { "Ис 44:5" },
                new string[] { "Ис 44:6" });           

            
            //    Почему то только при третьем прогоне обнаружилась ссылка Ин 1:17. Хотя он вообще не должна обнаруживаться, если мы вынесли ссылку из Title
            //    При повторном прогоне не находится ссылка Евр 1:10.
            
        }

        [TestMethod]
        public void CheckFullPage()
        {
            TestFile(@"..\..\..\TestData\Html_CheckFullPage.html",
                new string[] { "Ин 1:1" },
                new string[] { "Исх 12:27" },
                new string[] { "Ин 1:50-2:3" },
                new string[] { "Ин 3:16", "1Кор 5:7-9" },
                new string[] { "Ис 44" },
                new string[] { "Ис 44:24" },
                new string[] { "Евр 1:2", "Евр 1:10" },
                new string[] { "Ис 44:6" },
                new string[] { "Ин 1:17" },
                new string[] { "Ис 44:5" },
                new string[] { "Ис 44:6" });


            //    Почему то только при третьем прогоне обнаружилась ссылка Ин 1:17. Хотя он вообще не должна обнаруживаться, если мы вынесли ссылку из Title
            //    При повторном прогоне не находится ссылка Евр 1:10.

        }

        [TestMethod]
        public void CheckTable()
        {
            TestFile(@"..\..\..\TestData\Html_CheckTable.html", 
                new string[] { "Ин 1" },
                new string[] { "Ин 2" },
                new string[] { "Ин 1:3" },
                new string[] { "Ин 2:4" },
                new string[] { "Ин 1:1" },
                new string[] { "Ин 2:2" });
        }
    }
}
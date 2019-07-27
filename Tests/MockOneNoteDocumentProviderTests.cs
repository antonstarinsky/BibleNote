﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibleNote.Analytics.Providers.OneNote.Services;
using BibleNote.Analytics.Providers.OneNote.Contracts;
using BibleNote.Tests.Analytics.Mocks;
using BibleNote.Tests.Analytics.TestsBase;
using BibleNote.Analytics.Services.DocumentProvider.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BibleNote.Tests.Analytics
{
    [TestClass]
    public class MockOneNoteDocumentProviderTests : FileDocumentParserTestsBase
    {
        private const string TempFolderName = "MockOneNoteDocumentProviderTests";

        [TestInitialize]
        public void Init()
        {
            base.Init(TempFolderName, services => services
                .AddScoped<IOneNoteDocumentConnector, MockOneNoteDocumentConnector>()
                .AddScoped<IDocumentProvider, OneNoteProvider>());            
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void Test1()
        {
            TestFile(@"..\..\..\TestData\OneNote_1.html",
                new string[] { "Ин 1:1" },
                new string[] { "Исх 12:27" },
                new string[] { "1Кор 5:7" },
                new string[] { "Ис 44" },
                new string[] { "Ис 44:24" },
                new string[] { "Евр 1:2", "Евр 1:10" },
                new string[] { "Ис 44:6" },
                new string[] { "Ин 1:17" },
                new string[] { "Ис 44:5" },
                new string[] { "Ис 44:6" });
        }

        [TestMethod]
        public void Test2()
        {
            TestFile(@"..\..\..\TestData\OneNote_2.html",
               new string[] { "Ин 1" },
               new string[] { "Ин 1:5" },
               new string[] { "Мк 2:5" },
               new string[] { "Ин 1:6" },
               new string[] { "Ин 1:7" },
               new string[] { "Ин 1:8" },
               new string[] { "Ин 1:9" });
        }

        [TestMethod]
        public void Test3()
        {
            TestFile(@"..\..\..\TestData\OneNote_3.html", 
               new string[] { "1Пет 3:3" },
               new string[] { "1Пет 3:9" },
               new string[] { "Мф 1:1" },
               new string[] { "Мф 1:10" },
               new string[] { "1Пет 4:5" },
               new string[] { "1Пет 4:11" },
               new string[] { "Мк 2:2" },
               new string[] { "1Пет 4:12" },
               new string[] { "Лк 3-4" },
               new string[] { "Ин 5-6" },
               new string[] { "Мк 3:3" },
               new string[] { "Лк 3-4" });
        }
    }
}
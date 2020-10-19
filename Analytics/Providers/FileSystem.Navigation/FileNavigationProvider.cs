﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibleNote.Analytics.Domain.Contracts;
using BibleNote.Analytics.Domain.Entities;
using BibleNote.Analytics.Domain.Enums;
using BibleNote.Analytics.Providers.FileSystem.DocumentId;
using BibleNote.Analytics.Providers.Html;
using BibleNote.Analytics.Providers.Pdf;
using BibleNote.Analytics.Services.DocumentProvider.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BibleNote.Analytics.Providers.FileSystem.Navigation
{
    /// <summary>
    /// Folder with files: .txt, .html, .docx, .pdf.
    /// </summary>
    public class FileNavigationProvider : INavigationProvider<FileDocumentId>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsReadonly { get; set; }
        public string FolderPath { get; set; }

        private readonly IServiceProvider serviceProvider;
        private readonly ITrackingDbContext dbContext;

        public FileNavigationProvider(
            IServiceProvider serviceProvider,
            ITrackingDbContext dbContext)
        {
            this.serviceProvider = serviceProvider;
            this.dbContext = dbContext;
        }

        public IDocumentProvider GetProvider(FileDocumentId document)
        {
            var fileType = FileTypeHelper.GetFileType(document.FilePath);

            switch (fileType)
            {
                case FileType.Html:
                case FileType.Text:
                    return this.serviceProvider.GetService<HtmlProvider>();
                case FileType.Word:
                    return this.serviceProvider.GetService<WordProvider>();
                case FileType.Pdf:
                    return this.serviceProvider.GetService<PdfProvider>();
                default:
                    throw new NotSupportedException(fileType.ToString());
            }
        }

        public async Task<IEnumerable<FileDocumentId>> LoadDocuments(bool newOnly, bool updateDb = false, CancellationToken cancellationToken = default)
        {
            var documents = GetFolderDocuments(FolderPath, null, newOnly);
            
            if (updateDb)
                await this.dbContext.SaveChangesAsync(cancellationToken);

            return documents.Select(d => new FileDocumentId(d.Id, d.Path, IsReadonly));
        }

        public IEnumerable<Document> GetFolderDocuments(string folderPath, DocumentFolder parentFolder, bool newOnly)
        {
            var result = new List<Document>();
            var folders = GetSubFolders(folderPath, parentFolder);

            foreach (var folder in folders)
            {
                var files = Directory
                    .GetFiles(folder.Path, "*", SearchOption.TopDirectoryOnly)
                    .Where(f => FileTypeHelper.SupportedFileExtensions.Contains(Path.GetExtension(f)));

                foreach (var file in files)
                {
                    var dbFile = this.dbContext.DocumentRepository.SingleOrDefault(d => d.Path == file);
                    if (dbFile == null)
                    {
                        dbFile = new Document()
                        {
                            Folder = folder,
                            Name = Path.GetFileName(file),
                            Path = file                            
                        };

                        this.dbContext.DocumentRepository.Add(dbFile);                            
                    }

                    if (!newOnly || dbFile.Id <= 0) // По непонятным причинам EF Core для нового файла выставляет Id = -2147482647
                        result.Add(dbFile);
                }

                result.AddRange(GetFolderDocuments(folder.Path, folder, newOnly));
            }

            return result;
        }

        private List<DocumentFolder> GetSubFolders(string folderPath, DocumentFolder parentFolder)
        {
            var result = new List<DocumentFolder>();
            var folders = Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly);

            foreach (var folder in folders)
            {
                var dbFolder = this.dbContext.DocumentFolderRepository.SingleOrDefault(f => f.Path == folder);
                if (dbFolder == null)
                {
                    dbFolder = new DocumentFolder()
                    {
                        Name = Path.GetFileName(folder),
                        ParentFolder = parentFolder,
                        Path = folder,
                        NavigationProviderName = nameof(FileNavigationProvider)
                    };

                    this.dbContext.DocumentFolderRepository.Add(dbFolder);                    
                }

                result.Add(dbFolder);
            }

            return result;
        }
    }
}

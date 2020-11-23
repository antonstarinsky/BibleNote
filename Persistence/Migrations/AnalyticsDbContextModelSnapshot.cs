﻿// <auto-generated />
using System;
using BibleNote.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BibleNote.Persistence.Migrations
{
    [DbContext(typeof(AnalyticsDbContext))]
    partial class AnalyticsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("BibleNote.Domain.Entities.AnalysisSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedDocumentsCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeletedDocumentsCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FinishTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GetDocumentsInfoTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("NavigationProviderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdatedDocumentsCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NavigationProviderId");

                    b.ToTable("AnalysisSessions");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DocumentFolderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LatestAnalysisSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DocumentFolderId");

                    b.HasIndex("LatestAnalysisSessionId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.DocumentFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LatestAnalysisSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NavigationProviderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentFolderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LatestAnalysisSessionId");

                    b.HasIndex("ParentFolderId");

                    b.HasIndex("NavigationProviderId", "Path")
                        .IsUnique();

                    b.ToTable("DocumentFolders");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.DocumentParagraph", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DocumentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentParagraphs");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.NavigationProviderInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsReadonly")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ParametersRaw")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("NavigationProvidersInfo");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.VerseEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DocumentParagraphId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Suffix")
                        .HasColumnType("TEXT");

                    b.Property<long>("VerseId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DocumentParagraphId");

                    b.ToTable("VerseEntries");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.VerseRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DocumentParagraphId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("RelationWeight")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RelativeDocumentParagraphId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RelativeVerseId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("VerseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DocumentParagraphId");

                    b.HasIndex("RelativeDocumentParagraphId");

                    b.HasIndex("RelativeVerseId");

                    b.HasIndex("VerseId");

                    b.ToTable("VerseRelations");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.AnalysisSession", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.NavigationProviderInfo", "NavigationProvider")
                        .WithMany()
                        .HasForeignKey("NavigationProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.Document", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.DocumentFolder", "Folder")
                        .WithMany()
                        .HasForeignKey("DocumentFolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibleNote.Domain.Entities.AnalysisSession", "LatestAnalysisSession")
                        .WithMany()
                        .HasForeignKey("LatestAnalysisSessionId");
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.DocumentFolder", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.AnalysisSession", "LatestAnalysisSession")
                        .WithMany()
                        .HasForeignKey("LatestAnalysisSessionId");

                    b.HasOne("BibleNote.Domain.Entities.NavigationProviderInfo", "NavigationProvider")
                        .WithMany()
                        .HasForeignKey("NavigationProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibleNote.Domain.Entities.DocumentFolder", "ParentFolder")
                        .WithMany("ChildrenFolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.DocumentParagraph", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.Document", "Document")
                        .WithMany("Paragraphs")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.VerseEntry", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.DocumentParagraph", "DocumentParagraph")
                        .WithMany()
                        .HasForeignKey("DocumentParagraphId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BibleNote.Domain.Entities.VerseRelation", b =>
                {
                    b.HasOne("BibleNote.Domain.Entities.DocumentParagraph", "DocumentParagraph")
                        .WithMany()
                        .HasForeignKey("DocumentParagraphId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibleNote.Domain.Entities.DocumentParagraph", "RelativeDocumentParagraph")
                        .WithMany()
                        .HasForeignKey("RelativeDocumentParagraphId");
                });
#pragma warning restore 612, 618
        }
    }
}

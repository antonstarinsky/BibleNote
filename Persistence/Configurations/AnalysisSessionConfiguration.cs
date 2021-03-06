﻿using BibleNote.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleNote.Persistence.Configurations
{
    class AnalysisSessionConfiguration : IEntityTypeConfiguration<AnalysisSession>
    {
        public void Configure(EntityTypeBuilder<AnalysisSession> builder)
        {
            builder.ToTable(nameof(AnalyticsDbContext.AnalysisSessions));

            builder.Property(d => d.GetDocumentsInfoTime).IsRequired(true);
            builder.Property(d => d.Status).IsRequired(true);
            builder.Property(d => d.NavigationProviderId).IsRequired(true);
        }
    }
}

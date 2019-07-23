﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibleNote.Analytics.Data.Entities
{
    [Table(nameof(AnalyticsContext.VerseEntries))]
    public class VerseEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VerseEntryId { get; set; }

        [Required]
        public long VerseId { get; set; }
        
        public decimal Weight { get; set; }

        public string Suffix { get; set; }

        [Required]
        public int DocumentParagraphId { get; set; }        

        [ForeignKey(nameof(DocumentParagraphId))]
        public virtual DocumentParagraph DocumentParagraph { get; set; }
    }
}
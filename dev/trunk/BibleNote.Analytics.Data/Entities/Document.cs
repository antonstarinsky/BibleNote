﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibleNote.Analytics.Data.Entities
{    
    [Table("Documents")]
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int DocumentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DocumentFolderId { get; set; }

        [ForeignKey("DocumentFolderId")]
        public DocumentFolder Folder { get; set; }

        [Required]
        public string Path { get; set; }
    }
}

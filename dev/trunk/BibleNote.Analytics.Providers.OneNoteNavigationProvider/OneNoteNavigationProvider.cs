﻿using BibleNote.Analytics.Contracts.Providers;

namespace BibleNote.Analytics.Providers.OneNoteNavigationProvider
{
    public class OneNoteNavigationProvider : INavigationProvider
    {
        public string Name
        {
            get
            {
                return "OneNoteNavigationProvider";
            }
        }

        public string Description
        {
            get
            {
                return "OneNote pages.";
            }
        }        
    }
}
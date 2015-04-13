﻿using BibleNote.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleNote.Core.Contracts
{
    public interface IVersePointerFactory
    {
        VersePointer CreateVersePointer(string text);
    }
}
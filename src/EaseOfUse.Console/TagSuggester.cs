using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Challenge.Core;

namespace EaseOfUse
{
    public class TagSuggester : ChallengeBase
    {
        private readonly Dictionary<string, List<Note>> _items
            = new Dictionary<string, List<Note>>();

        public TagSuggester(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        public void AddNote(Note note)
        {
            //TODO: if the notes/tags operate like I think they do, this one looks a little suspicious.
            foreach (var tag in
                from t in note.Tags
                select t.ToLower().Trim())
            {
                List<Note> list;

                if (_items.ContainsKey(tag))
                {
                    list = _items[tag];
                }
                else
                {
                    list = new List<Note>();
                    _items.Add(tag, list);
                }

                list.Add(note);
            }
        }

        private void ReadNotes(int count, params string[] lines)
        {
            foreach (var line in lines)
                AddNote(new Note("", line));
        }

        private readonly List<string> _queries = new List<string>();

        private void ReadQueries(int count, params string[] queries)
        {
            _queries.AddRange(queries);
        }

        protected override void Read(TextReader reader)
        {
            var lines = ReadLines(reader);

            var noteCount = int.Parse(lines[0]);

            ReadNotes(noteCount, lines.Skip(1).Take(noteCount).ToArray());

            var queryCount = int.Parse(lines.Skip(noteCount + 1).ElementAt(0));

            ReadQueries(queryCount, lines.Skip(noteCount + 2).ToArray());
        }

        public List<string> GetSuggestions(params string[] prefixes)
        {
            var suggestions = new Dictionary<string, List<Suggestion>>();

            // Make sure we are hitting each of the prefixes here.
            foreach (var prefix in from p in prefixes select p.ToLower().Trim())
            {
                if (string.IsNullOrEmpty(prefix))
                    continue;

                if (!suggestions.ContainsKey(prefix))
                    suggestions[prefix] = new List<Suggestion>();

                foreach (var tag in _items.Keys)
                {
                    int edits;

                    if (tag.StartsWith(prefix))
                    {
                        edits = 0;
                    }
                    else if (EqualsIgnoreTypo(prefix, tag.Substring(0, Math.Min(tag.Length, prefix.Length))))
                    {
                        edits = 1;
                    }
                    else
                    {
                        edits = 2;
                    }

                    if (edits < 2)
                    {
                        suggestions[prefix].Add(new Suggestion(tag, edits, _items[tag].Count));
                    }
                }
            }

            var values = suggestions.Values.Aggregate(
                (IEnumerable<Suggestion>) new List<Suggestion>(),
                (g, x) => g.Concat(x)).ToList();

            values.Sort();

            var tags = (from s in values select s.Tag).ToList();

            return tags;
        }

        public static bool EqualsIgnoreTypo(string suspect, string word)
        {
            var typo = suspect.Equals(word);

            // letter deletion
            // try deleting letter from suspect to see if it equals word
            typo = typo || EqualIfDeleteLetter(suspect, word);

            // letter addition
            // adding to suspect is the same as deleting from word
            typo = typo || EqualIfDeleteLetter(word, suspect);

            // letter substitution
            // count how many letters are different between the two words.
            if (!typo && suspect.Length == word.Length)
            {
                var diff = 0;
                for (var i = 0; i < suspect.Length && diff < 2; i++)
                {
                    if (suspect[i] != word[i])
                    {
                        diff++;
                    }
                }
                typo = diff == 1;
            }
            return typo;
        }

        private static bool EqualIfDeleteLetter(string deletable, string word)
        {
            if (deletable.Length - word.Length != 1)
            {
                return false;
            }
            var i = 0;
            while (i < word.Length && deletable[i] == word[i])
            {
                i++;
            }
            while (i + 1 < deletable.Length && i < word.Length && deletable[i + 1] == word[i])
            {
                i++;
            }
            return i == word.Length;
        }

        private readonly StringBuilder _output = new StringBuilder();

        protected override void Run()
        {
            const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;

            foreach (var output in from q in _queries
                select GetSuggestions(q.Split(new[] {' '}, options))
                    .Aggregate(string.Empty,
                        (g, x) => string.IsNullOrEmpty(g) ? x : g + "," + x))
            {
                _output.AppendLine(output);
            }
        }

        protected override void Report(TextWriter writer)
        {
            using (var reader = new StringReader(_output.ToString()))
            {
                string line;
                while ((line = reader.ReadLineAsync().Result) != null)
                    writer.WriteLineAsync(line).Wait();
            }
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EaseOfUse
{
    public class Note
    {
        private readonly string _content;

        private readonly List<string> _tags = new List<string>();

        public Note(string content, string tags)
        {
            _content = content;
            _tags.AddRange(tags.Split(','));
        }

        public string Content
        {
            get { return _content; }
        }

        private ICollection<string> _readonlyTags;

        public ICollection<string> Tags
        {
            get
            {
                return _readonlyTags ?? (_readonlyTags
                    = new ReadOnlyCollection<string>(_tags));
            }
        }
    }
}

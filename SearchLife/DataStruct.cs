using System.Collections.Generic;

namespace SearchLife
{
    public class DataStruct
    {
        public Dictionary<char, DataStruct> children { get; set; }
        public char value;

        /*public string GetValue(string looked)
        {
            if (children == null)
            {
                return looked + value.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(looked))
                {
                    children.TryGetValue(looked[0], out DataStruct child);
                    return (child != null) ? looked + child.GetValue(looked.TrimStart(value)) : null;
                }
                else
                    return null;
            }
        }*/

    }
}
